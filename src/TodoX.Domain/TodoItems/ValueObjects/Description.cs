namespace TodoX.Domain.TodoItems.ValueObjects;

public record Description
{
    public static int MaxLength { get; } = 512;
    public static int MinLength { get; set; } = 8;

    public string Value { get; }

    private Description(string value)
    {
        Value = value;
    }

    public static Description Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("The todo item description cannot be null or empty.", nameof(value));

        if (value.Length < MinLength || value.Length > MaxLength)
            throw new ArgumentOutOfRangeException(nameof(value), $"The todo item description must be between {MinLength} and {MaxLength}");

        return new Description(value);
    }
}