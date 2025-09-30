using System.Security.Claims;
using TodoX.Application.Common.Interfaces;

namespace TodoX.Application.Common.Services;

public class TokenService : ITokenService
{
    public string GenerateToken(Guid id, string email)
    {
        List<Claim> claims =
        [
            new Claim(ClaimTypes.NameIdentifier, email),
        ];
    }
}