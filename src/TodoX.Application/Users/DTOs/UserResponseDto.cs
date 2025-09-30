using TodoX.Application.Common.DTOs;

namespace TodoX.Application.Users.DTOs;

public class UserResponseDto : ResultDto
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
}