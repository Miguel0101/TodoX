using TodoX.Domain.TodoLists.ValueObjects;
using TodoX.Domain.Users.Entities;

namespace TodoX.Domain.TodoLists.Entities;

public class TodoList
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Title? Title { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation
    public User User { get; set; } = null!;
}