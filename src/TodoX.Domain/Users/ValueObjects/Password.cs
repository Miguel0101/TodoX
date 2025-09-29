using Microsoft.AspNetCore.Identity;

namespace TodoX.Domain.Users.ValueObjects;

public record Password
{
    public static int MinLength { get; } = 8;

    public string Value { get; set; } = string.Empty;

    private Password(string value)
    {
        Value = value;
    }

    public static Password Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("The password cannot be null or empty.", nameof(value));

        if (value.Length < MinLength)
            throw new ArgumentOutOfRangeException(nameof(value), $"The password must be at least {MinLength} characters");

        var hasher = new PasswordHasher<Password>();
        var hashed = hasher.HashPassword()

        return new Password(value);
    }
}