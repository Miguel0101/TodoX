using TodoX.Domain.Users.ValueObjects;

namespace TodoX.Domain.Users.Entities;

public class User
{
    public Guid Id { get; set; }
    public Name? Name { get; set; }
    public Email? Email { get; set; }
    public Password? Password { get; set; }
}