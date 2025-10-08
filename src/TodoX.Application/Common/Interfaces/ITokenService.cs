using TodoX.Domain.Users.Entities;

namespace TodoX.Application.Common.Interfaces;

public interface ITokenService
{
    string GenerateToken(User user);
}