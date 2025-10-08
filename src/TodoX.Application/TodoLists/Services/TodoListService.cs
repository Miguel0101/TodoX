using TodoX.Application.Common.DTOs;
using TodoX.Application.Common.Enums;
using TodoX.Application.TodoLists.DTOs;
using TodoX.Domain.TodoLists.ValueObjects;
using TodoX.Domain.TodoLists.Entities;
using TodoX.Domain.TodoLists.Interfaces;
using TodoX.Application.Common.Interfaces;

namespace TodoX.Application.TodoLists.Services;

public class TodoListService : ITodoListService
{
    private readonly ITodoListRepository _todoListRepository;
    private readonly IUserContext _userContext;

    public TodoListService(ITodoListRepository todoListRepository, IUserContext userContext)
    {
        _todoListRepository = todoListRepository;
        _userContext = userContext;
    }

    public async Task<ResponseDto<List<TodoListDto>>> GetTodoLists()
    {
        Guid userId = _userContext.GetUserId();

        if (userId == Guid.Empty)
        {
            return ResponseDto<List<TodoListDto>>.Error(ErrorCode.Unauthorized, "Unauthorized.");
        }

        try
        {
            List<TodoListDto> todoListDtos = (await _todoListRepository.GetListAsync())
            .Where(t => t.UserId == userId)
            .Select(t => new TodoListDto
            {
                Id = t.Id,
                Title = t.Title?.Value
            })
            .ToList();

            return ResponseDto<List<TodoListDto>>.Success("Success.", todoListDtos);
        }
        catch (Exception e)
        {
            return ResponseDto<List<TodoListDto>>.Error(ErrorCode.UndefinedError, e.Message);
        }
    }

    public async Task<ResponseDto<TodoListDto>> GetTodoList(Guid id)
    {
        Guid userId = _userContext.GetUserId();

        if (userId == Guid.Empty)
        {
            return ResponseDto<TodoListDto>.Error(ErrorCode.Unauthorized, "Unauthorized.");
        }

        try
        {
            TodoList? todoList = await _todoListRepository.GetByIdAsync(id);

            if (todoList == null || todoList.UserId != userId)
            {
                return ResponseDto<TodoListDto>.Error(ErrorCode.TodoListNotFound, "Todo list not found");
            }

            TodoListDto todoListDto = new()
            {
                Id = todoList.Id,
                Title = todoList.Title?.Value
            };

            return ResponseDto<TodoListDto>.Success("Success.", todoListDto);
        }
        catch (Exception e)
        {
            return ResponseDto<TodoListDto>.Error(ErrorCode.UndefinedError, e.Message);
        }
    }

    public async Task<ResponseDto<TodoListDto>> AddTodoList(CreateTodoListDto todoListDto)
    {
        Guid userId = _userContext.GetUserId();

        if (userId == Guid.Empty)
        {
            return ResponseDto<TodoListDto>.Error(ErrorCode.Unauthorized, "Unauthorized.");
        }

        try
        {
            TodoList todoList = new()
            {
                UserId = userId,
                Title = Title.Create(todoListDto.Title ?? "")
            };

            await _todoListRepository.AddAsync(todoList);

            TodoListDto todoListResultDto = new()
            {
                Id = todoList.Id,
                Title = todoList.Title.Value
            };

            return ResponseDto<TodoListDto>.Success("Created.", todoListResultDto);
        }
        catch (ArgumentException e)
        {
            return ResponseDto<TodoListDto>.Error(ErrorCode.InvalidField, e.Message);
        }
        catch (Exception e)
        {
            return ResponseDto<TodoListDto>.Error(ErrorCode.UndefinedError, e.Message);
        }
    }

    public async Task<ResponseDto<TodoListDto>> UpdateTodoList(Guid id, UpdateTodoListDto todoListDto)
    {
        Guid userId = _userContext.GetUserId();

        if (userId == Guid.Empty)
        {
            return ResponseDto<TodoListDto>.Error(ErrorCode.Unauthorized, "Unauthorized.");
        }

        try
        {
            TodoList? todoList = await _todoListRepository.GetByIdAsync(id);

            if (todoList == null || todoList.UserId != userId)
            {
                return ResponseDto<TodoListDto>.Error(ErrorCode.TodoListNotFound, "Todo list not found");
            }

            todoList.Title = Title.Create(todoListDto.Title ?? "");

            await _todoListRepository.EditAsync(todoList);

            TodoListDto todoListResultDto = new()
            {
                Id = todoList.Id,
                Title = todoList.Title.Value
            };

            return ResponseDto<TodoListDto>.Success("Updated.", todoListResultDto);
        }
        catch (ArgumentException e)
        {
            return ResponseDto<TodoListDto>.Error(ErrorCode.InvalidField, e.Message);

        }
        catch (Exception e)
        {
            return ResponseDto<TodoListDto>.Error(ErrorCode.UndefinedError, e.Message);
        }
    }

    public async Task<ResponseDto> DeleteTodoList(Guid id)
    {
        Guid userId = _userContext.GetUserId();

        if (userId == Guid.Empty)
        {
            return ResponseDto<TodoListDto>.Error(ErrorCode.Unauthorized, "Unauthorized.");
        }

        try
        {
            TodoList? todoList = await _todoListRepository.GetByIdAsync(id);

            if (todoList == null || todoList.UserId != userId)
            {
                return ResponseDto<TodoListDto>.Error(ErrorCode.TodoListNotFound, "Todo list not found");
            }

            await _todoListRepository.RemoveAsync(id);

            return ResponseDto<TodoListDto>.Success("Deleted.");
        }
        catch (Exception e)
        {
            return ResponseDto<TodoListDto>.Error(ErrorCode.UndefinedError, e.Message);
        }
    }
}