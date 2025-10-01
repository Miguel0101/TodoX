using System.Security.Claims;
using TodoX.Application.Common.Interfaces;
using System.IdentityModel.Tokens.Jwt;

namespace TodoX.Application.Common.Services;

public class TokenService : ITokenService
{
    public string GenerateToken(Guid id, string email)
    {
        JwtSecurityTokenHandler handler = new();

        List<Claim> claims =
        [
            new Claim(JwtRegisteredClaimNames.Sub, id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, email)
        ];
    }
}