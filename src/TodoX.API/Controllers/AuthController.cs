using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoX.Application.Common.DTOs;
using TodoX.Application.Common.Enums;
using TodoX.Application.Users.DTOs;
using TodoX.Application.Users.Services;

namespace TodoX.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] CreateUserDto userDto)
    {
        ResponseDto<UserDto> response = await _userService.Register(userDto);

        switch (response.ErrorCode)
        {
            case ErrorCode.Success:
                return Created($"/api/auth/{response.Result?.Id}", response);

            case ErrorCode.UserAlreadyExists:
                return Conflict(response);

            case ErrorCode.InvalidField:
                return BadRequest(response);

            default:
                return Problem(response.Message, null, 500);
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginUserDto userDto)
    {
        ResponseDto<TokenDto> response = await _userService.Login(userDto);

        switch (response.ErrorCode)
        {
            case ErrorCode.Success:
                return Ok(response);

            case ErrorCode.UserNotFound:
                return NotFound(response);

            case ErrorCode.InvalidField:
                return BadRequest(response);

            case ErrorCode.InvalidPassword:
                return Unauthorized(response);

            default:
                return Problem(response.Message, null, 500);
        }
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Details()
    {
        _ = Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid id);

        ResponseDto<UserDto> response = await _userService.Details(id);

        switch (response.ErrorCode)
        {
            case ErrorCode.Success:
                return Ok(response);

            case ErrorCode.UserNotFound:
                return NotFound(response);

            case ErrorCode.Unauthorized:
                return Unauthorized(response);

            default:
                return Problem(response.Message, null, 500);
        }
    }
}