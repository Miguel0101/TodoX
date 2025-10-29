using TodoX.Domain.Users.ValueObjects;

namespace TodoX.Domain.Users.Entities;

public class UserAccessToken
{
    public Guid Id { get; set; }
    public AccessToken? Token { get; set; }
    public bool IsUsed { get; set; }
    public DateTime Expiration { get; set; } = DateTime.UtcNow.AddSeconds(60);
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}