using TodoX.Application.Users.DTOs;
using TodoX.Application.Common.Enums;
using TodoX.Domain.Users.Entities;
using TodoX.Domain.Users.Interfaces;
using TodoX.Domain.Users.ValueObjects;
using TodoX.Application.Common.Interfaces;
using TodoX.Application.Common.DTOs;

namespace TodoX.Application.Users.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;

    public UserService(IUserRepository userRepository, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
    }

    public async Task<ResponseDto<TokenDto>> Login(LoginUserDto userDto)
    {
        try
        {
            Email email = Email.Create(userDto.Email ?? "");

            User? user = await _userRepository.FindAsync(email);

            if (user == null)
            {
                return ResponseDto<TokenDto>.Error(ErrorCode.UserNotFound, "User not found");
            }

            if (!user.Password!.Verify(userDto.Password ?? ""))
            {
                return ResponseDto<TokenDto>.Error(ErrorCode.InvalidPassword, "Invalid password.");
            }

            string token = _tokenService.GenerateJwtToken(user);

            TokenDto tokenDto = new()
            {
                Token = token
            };

            return ResponseDto<TokenDto>.Success("Success", tokenDto);
        }
        catch (ArgumentException e)
        {
            return ResponseDto<TokenDto>.Error(ErrorCode.InvalidField, e.Message);
        }
        catch (Exception e)
        {
            return ResponseDto<TokenDto>.Error(ErrorCode.UndefinedError, e.Message);
        }
    }

    public async Task<ResponseDto<UserDto>> Register(CreateUserDto userDto)
    {
        try
        {
            User user = new()
            {
                Name = Name.Create(userDto.Name ?? ""),
                Email = Email.Create(userDto.Email ?? ""),
                Password = Password.Create(userDto.Password ?? "")
            };

            if (await _userRepository.FindAsync(user.Email) != null)
            {
                return ResponseDto<UserDto>.Error(ErrorCode.UserAlreadyExists, "User already exists.");
            }

            await _userRepository.AddAsync(user);

            UserDto userResultDto = new()
            {
                Id = user.Id,
                Name = user.Name?.Value,
                Email = user.Email?.Value
            };

            return ResponseDto<UserDto>.Success("Registered.", userResultDto);
        }
        catch (ArgumentException e)
        {
            return ResponseDto<UserDto>.Error(ErrorCode.InvalidField, e.Message);
        }
        catch (Exception e)
        {
            return ResponseDto<UserDto>.Error(ErrorCode.UndefinedError, e.Message);
        }
    }

    public async Task<ResponseDto<UserDto>> Details(Guid id)
    {
        if (id == Guid.Empty)
        {
            return ResponseDto<UserDto>.Error(ErrorCode.Unauthorized, "Unauthorized.");
        }

        User? user = await _userRepository.GetByIdAsync(id);

        if (user == null)
        {
            return ResponseDto<UserDto>.Error(ErrorCode.UserNotFound, "User not found.");
        }

        UserDto userResultDto = new()
        {
            Id = user.Id,
            Name = user.Name?.Value,
            Email = user.Email?.Value
        };

        return ResponseDto<UserDto>.Success("Success", userResultDto);
    }
}