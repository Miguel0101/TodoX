using TodoX.Domain.TodoLists.Entities;
using TodoX.Domain.Users.ValueObjects;

namespace TodoX.Domain.Users.Entities;

public class User
{
    public Guid Id { get; set; }
    public Name? Name { get; set; }
    public Email? Email { get; set; }
    public Password? Password { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation
    public List<TodoList> TodoLists { get; set; } = [];
}