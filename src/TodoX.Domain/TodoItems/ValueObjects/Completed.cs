namespace TodoX.Domain.TodoItems.ValueObjects;

public record Completed
{
    public bool Value { get; }

    private Completed(bool value)
    {
        Value = value;
    }

    public static Completed Create(bool value)
    {
        return new Completed(value);
    }

    public bool Done()
    {
        return Value;
    }
}