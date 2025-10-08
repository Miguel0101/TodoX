using TodoX.Application.Common.DTOs;
using TodoX.Application.Users.DTOs;

namespace TodoX.Application.Users.Services;

public interface IUserService
{
    Task<ResponseDto<TokenDto>> Login(LoginUserDto userDto);
    Task<ResponseDto<UserDto>> Register(CreateUserDto userDto);
    Task<ResponseDto<UserDto>> Details(Guid id);
}
