namespace TodoX.Infrastructure.Identity;

public class JwtConfiguration
{
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public string PrivateKey { get; set; } = string.Empty;
    public int Lifetime { get; set; }
}