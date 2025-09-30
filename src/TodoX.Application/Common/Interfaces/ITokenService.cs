namespace TodoX.Application.Common.Interfaces;

public interface ITokenService
{
    string GenerateToken(Guid id, string email);
}