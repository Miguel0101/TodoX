namespace TodoX.Domain.Users.ValueObjects;

public record Name
{
    public static int MaxLength { get; } = 100;
    public static int MinLength { get; } = 3;

    public string Value { get; } = string.Empty;

    private Name(string value)
    {
        Value = value;
    }

    public static Name Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("The name cannot be null or empty.", nameof(value));

        if (value.Length < MinLength || value.Length > MaxLength)
            throw new ArgumentOutOfRangeException(nameof(value), $"The name must be between {MinLength} and {MaxLength} characters.");

        return new Name(value);
    }
}