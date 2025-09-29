using TodoX.Domain.TodoItems.ValueObjects;
using TodoX.Domain.TodoLists.Entities;

namespace TodoX.Domain.TodoItems.Entities;

public class TodoItem
{
    public Guid Id { get; set; }
    public Guid TodoListId { get; set; }
    public Title? Title { get; set; }
    public Description? Description { get; set; }
    public Completed? Completed { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation
    public TodoList TodoList { get; set; } = null!;
}