using Microsoft.AspNetCore.Mvc;

namespace TodoX.API.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    [HttpPost("register")]
    public IActionResult Register()
    {
        return Created();
    }

    [HttpPost("login")]
    public IActionResult Login()
    {
        return Ok();
    }

    [HttpGet("me")]
    public IActionResult Details()
    {
        return Ok();
    }
}