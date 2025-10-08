using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoX.Application.Common.DTOs;
using TodoX.Application.Common.Enums;
using TodoX.Application.TodoLists.DTOs;
using TodoX.Application.TodoLists.Services;

namespace TodoX.API.Controllers;

[ApiController]
[Route("api/todolists"), Authorize]
public class TodoListController : ControllerBase
{
    private readonly ITodoListService _todoListService;

    public TodoListController(ITodoListService todoListService)
    {
        _todoListService = todoListService;
    }

    [HttpGet]
    public async Task<IActionResult> GetList()
    {
        ResponseDto<List<TodoListDto>> response = await _todoListService.GetTodoLists();

        switch (response.ErrorCode)
        {
            case ErrorCode.Success:
                return Ok(response);

            case ErrorCode.Unauthorized:
                return Unauthorized(response);

            default:
                return Problem(response.Message, null, 500);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(Guid id)
    {
        ResponseDto<TodoListDto> response = await _todoListService.GetTodoList(id);

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

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTodoListDto todoListDto)
    {
        ResponseDto<TodoListDto> response = await _todoListService.AddTodoList(todoListDto);

        switch (response.ErrorCode)
        {
            case ErrorCode.Success:
                return Created($"/api/todolists/{response.Result?.Id}", response);

            case ErrorCode.InvalidField:
                return BadRequest(response);

            case ErrorCode.Unauthorized:
                return Unauthorized(response);

            default:
                return Problem(response.Message, null, 500);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Edit(Guid id, [FromBody] UpdateTodoListDto todoListDto)
    {
        ResponseDto<TodoListDto> response = await _todoListService.UpdateTodoList(id, todoListDto);

        switch (response.ErrorCode)
        {
            case ErrorCode.Success:
                return Ok(response);

            case ErrorCode.InvalidField:
                return BadRequest(response);

            case ErrorCode.TodoListNotFound:
                return NotFound(response);

            case ErrorCode.Unauthorized:
                return Unauthorized(response);

            default:
                return Problem(response.Message, null, 500);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        ResponseDto response = await _todoListService.DeleteTodoList(id);

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
}