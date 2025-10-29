using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using TodoX.Domain.Users.Entities;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using System.Text;
using TodoX.Application.Common.Interfaces;
using System.Security.Cryptography;

namespace TodoX.Infrastructure.Identity;

public class TokenService : ITokenService
{
    private readonly JwtConfiguration _jwtConfiguration;

    public TokenService(IOptions<JwtConfiguration> jwtConfiguration)
    {
        _jwtConfiguration = jwtConfiguration.Value;
    }

    /// <summary>
    /// Generates a JWT token with user informations.
    /// </summary>
    /// <param name="user"></param>
    /// <returns>A JWT token string.</returns>
    public string GenerateJwtToken(User user)
    {
        JwtSecurityTokenHandler handler = new();

        SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_jwtConfiguration.PrivateKey));

        SigningCredentials credentials = new(key, SecurityAlgorithms.HmacSha512);

        SecurityTokenDescriptor tokenDescriptor = new()
        {
            SigningCredentials = credentials,
            Issuer = _jwtConfiguration.Issuer,
            Audience = _jwtConfiguration.Audience,
            Subject = GenerateClaims(user),
            Expires = DateTime.UtcNow.AddMinutes(_jwtConfiguration.Lifetime)
        };

        SecurityToken token = handler.CreateToken(tokenDescriptor);
        return handler.WriteToken(token);
    }

    /// <summary>
    /// Generates the user claims.
    /// </summary>
    /// <param name="user"></param>
    /// <returns>The claims identity.</returns>
    private static ClaimsIdentity GenerateClaims(User user)
    {
        ClaimsIdentity claimsIdentity = new();

        List<Claim> claims =
        [
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email!.Value),
            new Claim(JwtRegisteredClaimNames.Name, user.Name!.Value)
        ];

        claimsIdentity.AddClaims(claims);

        return claimsIdentity;
    }
}