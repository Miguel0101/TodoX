using TodoX.Application.Common.DTOs;
using TodoX.Application.TodoItems.DTOs;

namespace TodoX.Application.TodoItems.Services;

public interface ITodoItemService
{
    Task<ResponseDto<List<TodoItemDto>>> GetTodoItems(Guid listId);
    Task<ResponseDto<TodoItemDto>> GetTodoItem(Guid listId, Guid id);
    Task<ResponseDto<TodoItemDto>> AddTodoItem(Guid listId, CreateTodoItemDto todoItemDto);
    Task<ResponseDto<TodoItemDto>> UpdateTodoItem(Guid listId, Guid id, UpdateTodoItemDto todoItemDto);
    Task<ResponseDto<TodoItemDto>> CompleteTodoItem(Guid listId, Guid id);
    Task<ResponseDto> DeleteTodoItem(Guid listId, Guid id);
}