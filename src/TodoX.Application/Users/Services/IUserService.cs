using TodoX.Application.Common.DTOs;
using TodoX.Application.Users.DTOs;

namespace TodoX.Application.Users.Services;

public interface IUserService
{
    Task<ResultDto> Login();
    Task<UserResponseDto> Register(CreateUserDto user);
    Task<UserResponseDto> Update(UpdateUserDto user);
}
