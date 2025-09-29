using System.Net.Mail;

namespace TodoX.Domain.Users.ValueObjects;

public record Email
{
    public string Value { get; } = string.Empty;

    private Email(string value)
    {
        Value = value;
    }

    public static Email Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("The mail address cannot be null or empty.", nameof(value));

        try
        {
            var mail = new MailAddress(value);
        }
        catch
        {
            throw new ArgumentException("Invalid mail address.");
        }

        return new Email(value);
    }
}