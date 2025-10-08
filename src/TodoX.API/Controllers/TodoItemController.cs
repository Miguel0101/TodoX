using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoX.Application.Common.DTOs;
using TodoX.Application.Common.Enums;
using TodoX.Application.TodoItems.DTOs;
using TodoX.Application.TodoItems.Services;

namespace TodoX.API.Controllers;

[ApiController]
[Route("api/todolists/{listId}/items"), Authorize]
public class TodoItemController : ControllerBase
{
    private readonly ITodoItemService _todoItemService;

    public TodoItemController(ITodoItemService todoItemService)
    {
        _todoItemService = todoItemService;
    }

    [HttpGet]
    public async Task<IActionResult> GetItemsAsync(Guid listId)
    {
        ResponseDto<List<TodoItemDto>> response = await _todoItemService.GetTodoItems(listId);

        switch (response.ErrorCode)
        {
            case ErrorCode.Success:
                return Ok(response);

            case ErrorCode.TodoListNotFound:
                return NotFound(response);

            case ErrorCode.Unauthorized:
                return Unauthorized(response);

            default:
                return Problem(response.Message, null, 500);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid listId, Guid id)
    {
        ResponseDto<TodoItemDto> response = await _todoItemService.GetTodoItem(listId, id);

        switch (response.ErrorCode)
        {
            case ErrorCode.Success:
                return Ok(response);

            case ErrorCode.TodoListNotFound:
            case ErrorCode.TodoItemNotFound:
                return NotFound(response);

            case ErrorCode.Unauthorized:
                return Unauthorized(response);

            default:
                return Problem(response.Message, null, 500);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(Guid listId, [FromBody] CreateTodoItemDto todoItemDto)
    {
        ResponseDto<TodoItemDto> response = await _todoItemService.AddTodoItem(listId, todoItemDto);

        switch (response.ErrorCode)
        {
            case ErrorCode.Success:
                return Ok(response);

            case ErrorCode.TodoListNotFound:
                return NotFound(response);

            case ErrorCode.InvalidField:
                return BadRequest(response);

            case ErrorCode.Unauthorized:
                return Unauthorized(response);

            default:
                return Problem(response.Message, null, 500);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> EditAsync(Guid listId, Guid id, [FromBody] UpdateTodoItemDto todoItemDto)
    {
        ResponseDto<TodoItemDto> response = await _todoItemService.UpdateTodoItem(listId, id, todoItemDto);

        switch (response.ErrorCode)
        {
            case ErrorCode.Success:
                return Ok(response);

            case ErrorCode.TodoListNotFound:
            case ErrorCode.TodoItemNotFound:
                return NotFound(response);

            case ErrorCode.InvalidField:
                return BadRequest(response);

            case ErrorCode.Unauthorized:
                return Unauthorized(response);

            default:
                return Problem(response.Message, null, 500);
        }
    }

    [HttpPatch("{id}/complete")]
    public async Task<IActionResult> CompleteAsync(Guid listId, Guid id)
    {
        ResponseDto<TodoItemDto> response = await _todoItemService.CompleteTodoItem(listId, id);

        switch (response.ErrorCode)
        {
            case ErrorCode.Success:
                return Ok(response);

            case ErrorCode.TodoListNotFound:
            case ErrorCode.TodoItemNotFound:
                return NotFound(response);

            case ErrorCode.Unauthorized:
                return Unauthorized(response);

            default:
                return Problem(response.Message, null, 500);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid listId, Guid id)
    {
        ResponseDto response = await _todoItemService.DeleteTodoItem(listId, id);

        switch (response.ErrorCode)
        {
            case ErrorCode.Success:
                return Ok(response);

            case ErrorCode.TodoListNotFound:
            case ErrorCode.TodoItemNotFound:
                return NotFound(response);

            case ErrorCode.Unauthorized:
                return Unauthorized(response);

            default:
                return Problem(response.Message, null, 500);
        }
    }
}