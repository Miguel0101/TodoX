using System.Security.Cryptography;

namespace TodoX.Domain.Users.ValueObjects;

public record AccessToken
{
    public static int Digits { get; } = 8;

    public string Value { get; } = string.Empty;

    private AccessToken(string value)
    {
        Value = value;
    }

    public static AccessToken Create()
    {
        string token = RandomNumberGenerator.GetInt32(0, (int)Math.Pow(10, Digits)).ToString($"D{Digits}");
        return new AccessToken(token);
    }

    public static AccessToken FromToken(string token)
    {
        return new AccessToken(token);
    }
}