using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PruebaBackAPI.Data;
using PruebaBackAPI.Dtos;
using PruebaBackAPI.Models;
using PruebaBackAPI.Services;

namespace PruebaBackAPI.Controllers;

[ApiController]
[Route("api/users")]
[Produces("application/json")]
public class UserController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IRepository<User> _repository;

    public UserController(IRepository<User> repository, IMapper mapper, IConfiguration configuration)
    {
        _repository = repository;
        _mapper = mapper;
    }

    /// <summary>
    ///     Get user list paginated.
    /// </summary>
    /// <param name="page"></param>
    /// <returns>A user</returns>
    /// <response code="200">Returns the list of users.</response>
    /// <response code="500">Error retrieving Users.</response>
    [HttpGet(Name = "Get All users")]
    [ProducesResponseType(typeof(PaginationDto<UserDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> GetAllUsers([FromQuery] int page)
    {
        page = page == 0 ? 1 : page;
        var userList = await _repository.GetAll(page, 5);

        return Ok(_mapper.Map<PaginationDto<UserDto>>(userList));
    }

    /// <summary>
    ///     Get user by id.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="clientSecret"></param>
    /// <param name="clientId"></param>
    /// <returns>A user</returns>
    /// <response code="200">Returns the user given the id.</response>
    /// <response code="404">User not found.</response>
    /// <response code="400">Missing client_secret or client_id.</response>
    /// <response code="401">Wrong client_id or client_secret.</response>
    [Authorizer]
    [HttpGet("{id:int}", Name = "GetUserById")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> GetUserById(int id, [FromHeader(Name = "client_secret")] string clientSecret,
        [FromHeader(Name = "client_id")] string clientId)
    {
        var userItem = await _repository.GetById(id);

        if (Equals(userItem, null))
            return NotFound("User not found.");

        if (string.IsNullOrWhiteSpace(clientId) || string.IsNullOrWhiteSpace(clientSecret))
            return BadRequest("Missing client_secret ,client_id.");

        return Ok(_mapper.Map<UserDto>(userItem));
    }

    /// <summary>
    ///     Create a user.
    /// </summary>
    /// <param name="userToCreate"></param>
    /// <returns>User created</returns>
    /// <response code="201">Returns the id of the created user.</response>
    /// <response code="400">Payload empty or empty email, empty first_name.</response>
    [HttpPost]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UserCreateDto>> CreateUser(UserCreateDto userToCreate)
    {
        var userModel = _mapper.Map<User>(userToCreate);
        await _repository.Create(userModel);
        await _repository.SaveChanges();

        var userCreated = _mapper.Map<UserDto>(userModel);
        return CreatedAtRoute(nameof(GetUserById), new {userCreated.Id}, userCreated);
    }

    /// <summary>
    ///     Update an existing user.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="user"></param>
    /// <returns>User created</returns>
    /// <response code="200">Success message.</response>
    /// <response code="404">User not found.</response>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> UpdateUser(int id, UserUpdateDto user)
    {
        var userFromRepo = await _repository.GetById(id);

        if (Equals(userFromRepo, null))
            return NotFound("User not found");

        userFromRepo.Email = !string.IsNullOrWhiteSpace(user.Email) ? user.Email : userFromRepo.Email;
        userFromRepo.FirstName =
            !string.IsNullOrWhiteSpace(user.FirstName) ? user.FirstName : userFromRepo.FirstName;
        userFromRepo.LastName = !string.IsNullOrWhiteSpace(user.LastName) ? user.LastName : userFromRepo.LastName;
        userFromRepo.Avatar = !string.IsNullOrWhiteSpace(user.Avatar) ? user.Avatar : userFromRepo.Avatar;
        await _repository.SaveChanges();

        return Ok("User updated");
    }
}