namespace TodoX.Application.TodoItems.DTOs;

public class TodoItemDto
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public bool? Completed { get; set; }
}