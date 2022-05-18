using Microsoft.AspNetCore.Mvc;
using PruebaBackAPI.Data;
using PruebaBackAPI.Models;
using System;
using PruebaBackAPI.Dtos;
using AutoMapper;
namespace PruebaBackAPI.Controllers;

[ApiController]
[Route("api/users")]
[Produces("application/json")]
public class UserController : ControllerBase
{
    private readonly IUserRepo<User> _repository;
    private readonly IMapper _mapper;
    public UserController(IUserRepo<User> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult> GetAllUsers([FromQuery] int qpage)
    {
        var page = qpage == 0 ? 1 : qpage;
        var userList = await _repository.GetAllUsers(page, 5);

        if (userList == null)
        {
            return StatusCode(500, "Error retrieving Users.");
        }

        return Ok(_mapper.Map<PaginationDto<UserDto>>(userList));
    }

/// <summary>
/// Searches for a user given an ID and returns it as a response.
/// </summary>
/// <param name="id"></param>
/// <returns>A user</returns>
/// <response code="200">Returns the user given the id</response>
/// <response code="404">User not found</response>
    [HttpGet("{id}", Name="GetUserById")]
    [ProducesResponseType(typeof(UserDto),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string),StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetUserById(int id)
    {
        var userItem = await _repository.GetUserById(id);

        if(userItem == null)
        {
            return StatusCode(404, "User not found");
        }
        
        return Ok(_mapper.Map<UserDto>(userItem));
    }

    [HttpPost]
    public async Task<ActionResult<UserCreateDto>> CreateUser(UserCreateDto userToCreate)
    {
        var userModel = _mapper.Map<User>(userToCreate);
        await _repository.CreateUser(userModel);
        await _repository.SaveChanges();

        var userCreated = _mapper.Map<UserDto>(userModel);
        return CreatedAtRoute(nameof(GetUserById), new {Id = userCreated.Id}, userCreated);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateUser(int id, UserUpdateDto user)
    {
        if (user == null || id == null)
            return NoContent();

        var userFromRepo = await _repository.GetUserById(id);
        if (userFromRepo == null)
            return NotFound("User not found");

        userFromRepo.email = !string.IsNullOrWhiteSpace(user.Email) ? user.Email : userFromRepo.email;
        userFromRepo.first_name = !string.IsNullOrWhiteSpace(user.First_name) ? user.First_name : userFromRepo.first_name;
        userFromRepo.last_name = !string.IsNullOrWhiteSpace(user.Last_name) ? user.Last_name : userFromRepo.last_name;
        userFromRepo.avatar = !string.IsNullOrWhiteSpace(user.Avatar) ? user.Avatar : userFromRepo.avatar;
        await _repository.SaveChanges();

        return Ok("User updated");

    }
}
