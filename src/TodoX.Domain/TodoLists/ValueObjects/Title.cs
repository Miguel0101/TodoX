namespace TodoX.Domain.TodoLists.ValueObjects;

public record Title
{
    public static int MaxLength { get; } = 100;
    public static int MinLength { get; } = 5;

    public string Value { get; } = string.Empty;

    private Title(string value)
    {
        Value = value;
    }

    public static Title Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("The todo list title cannot be null or empty.", nameof(value));

        if (value.Length < MinLength || value.Length > MaxLength)
            throw new ArgumentOutOfRangeException(nameof(value), $"The todo list title must be between {MinLength} and {MaxLength} characters.");

        return new Title(value);
    }
}