using TodoX.Application.Common.DTOs;
using TodoX.Application.TodoLists.DTOs;

namespace TodoX.Application.TodoLists.Services;

public interface ITodoListService
{
    Task<ResponseDto<List<TodoListDto>>> GetTodoLists();
    Task<ResponseDto<TodoListDto>> GetTodoList(Guid id);
    Task<ResponseDto<TodoListDto>> AddTodoList(CreateTodoListDto todoListDto);
    Task<ResponseDto<TodoListDto>> UpdateTodoList(Guid id, UpdateTodoListDto todoListDto);
    Task<ResponseDto> DeleteTodoList(Guid id);
}