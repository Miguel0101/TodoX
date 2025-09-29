using Microsoft.AspNetCore.Identity;

namespace TodoX.Domain.Users.ValueObjects;

public record Password
{
    public static int MinLength { get; } = 8;

    public string HashedValue { get; } = string.Empty;

    private static readonly PasswordHasher<string?> Hasher = new();

    private Password(string hashedValue)
    {
        HashedValue = hashedValue;
    }

    public static Password Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("The password cannot be null or empty.", nameof(value));

        if (value.Length < MinLength)
            throw new ArgumentOutOfRangeException(nameof(value), $"The password must be at least {MinLength} characters");

        string hashed = Hasher.HashPassword(null, value);

        return new Password(hashed);
    }

    public bool Verify(string password)
    {
        return Hasher.VerifyHashedPassword(null, HashedValue, password) != PasswordVerificationResult.Failed;
    }
}