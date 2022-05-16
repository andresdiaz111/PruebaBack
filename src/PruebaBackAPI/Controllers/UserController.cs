using Microsoft.AspNetCore.Mvc;

namespace PruebaBackAPI.Controllers;

[Route("api/users")]
[ApiController]
public class UserController : ControllerBase
{
    [HttpGet]
    public ActionResult holaMundo()
    {
        return Ok("Hola");
    }
}
