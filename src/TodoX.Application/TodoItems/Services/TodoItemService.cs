using TodoX.Application.Common.DTOs;
using TodoX.Application.Common.Enums;
using TodoX.Application.Common.Interfaces;
using TodoX.Application.TodoItems.DTOs;
using TodoX.Domain.TodoItems.Entities;
using TodoX.Domain.TodoItems.Interfaces;
using TodoX.Domain.TodoItems.ValueObjects;
using TodoX.Domain.TodoLists.Entities;
using TodoX.Domain.TodoLists.Interfaces;

namespace TodoX.Application.TodoItems.Services;

public class TodoItemService : ITodoItemService
{
    private readonly IUserContext _userContext;
    private readonly ITodoItemRepository _todoItemRepository;
    private readonly ITodoListRepository _todoListRepository;

    public TodoItemService(IUserContext userContext, ITodoItemRepository todoItemRepository, ITodoListRepository todoListRepository)
    {
        _userContext = userContext;
        _todoItemRepository = todoItemRepository;
        _todoListRepository = todoListRepository;
    }

    public async Task<ResponseDto<List<TodoItemDto>>> GetTodoItems(Guid listId)
    {
        Guid userId = _userContext.GetUserId();

        if (userId == Guid.Empty)
            return ResponseDto<List<TodoItemDto>>.Error(ErrorCode.Unauthorized, "Unauthorized.");

        try
        {
            TodoList? todoList = await _todoListRepository.GetByIdAsync(listId);

            if (todoList == null || todoList.UserId != userId)
                return ResponseDto<List<TodoItemDto>>.Error(ErrorCode.TodoListNotFound, "Todo list not found.");

            List<TodoItemDto> todoItemDtos = (await _todoItemRepository.GetByListIdAsync(listId))
                .Where(t => t.TodoList.UserId == userId)
                .Select(t => new TodoItemDto
                {
                    Id = t.Id,
                    Title = t.Title?.Value,
                    Description = t.Description?.Value,
                    Completed = t.Completed?.Value
                })
                .ToList();

            return ResponseDto<List<TodoItemDto>>.Success("Success", todoItemDtos);
        }
        catch (Exception e)
        {
            return ResponseDto<List<TodoItemDto>>.Error(ErrorCode.UndefinedError, e.Message);
        }
    }

    public async Task<ResponseDto<TodoItemDto>> GetTodoItem(Guid listId, Guid id)
    {
        Guid userId = _userContext.GetUserId();

        if (userId == Guid.Empty)
            return ResponseDto<TodoItemDto>.Error(ErrorCode.Unauthorized, "Unauthorized.");

        try
        {
            TodoItem? todoItem = await _todoItemRepository.GetByIdAsync(id);

            if (todoItem == null || todoItem.TodoListId != listId)
                return ResponseDto<TodoItemDto>.Error(ErrorCode.TodoItemNotFound, "Todo item not found.");

            if (todoItem.TodoList.UserId != userId)
                return ResponseDto<TodoItemDto>.Error(ErrorCode.TodoListNotFound, "Todo list not found.");

            TodoItemDto todoItemDto = new()
            {
                Id = todoItem.Id,
                Title = todoItem.Title?.Value,
                Description = todoItem.Description?.Value,
                Completed = todoItem.Completed?.Value
            };

            return ResponseDto<TodoItemDto>.Success("Success", todoItemDto);
        }
        catch (Exception e)
        {
            return ResponseDto<TodoItemDto>.Error(ErrorCode.UndefinedError, e.Message);
        }
    }

    public async Task<ResponseDto<TodoItemDto>> AddTodoItem(Guid listId, CreateTodoItemDto todoItemDto)
    {
        Guid userId = _userContext.GetUserId();

        if (userId == Guid.Empty)
            return ResponseDto<TodoItemDto>.Error(ErrorCode.Unauthorized, "Unauthorized");

        try
        {
            TodoList? todoList = await _todoListRepository.GetByIdAsync(listId);

            if (todoList == null || todoList.UserId != userId)
                return ResponseDto<TodoItemDto>.Error(ErrorCode.TodoListNotFound, "Todo list not found.");

            TodoItem todoItem = new()
            {
                TodoListId = listId,
                Title = Title.Create(todoItemDto.Title ?? ""),
                Description = Description.Create(todoItemDto.Description ?? ""),
                Completed = Completed.Create(false)
            };

            await _todoItemRepository.AddAsync(todoItem);

            TodoItemDto todoItemResult = new()
            {
                Id = todoItem.Id,
                Title = todoItem.Title.Value,
                Description = todoItem.Description.Value,
                Completed = todoItem.Completed.Value
            };

            return ResponseDto<TodoItemDto>.Success("Created.", todoItemResult);
        }
        catch (ArgumentException e)
        {
            return ResponseDto<TodoItemDto>.Error(ErrorCode.InvalidField, e.Message);
        }
        catch (Exception e)
        {
            return ResponseDto<TodoItemDto>.Error(ErrorCode.UndefinedError, e.Message);
        }
    }

    public async Task<ResponseDto<TodoItemDto>> UpdateTodoItem(Guid listId, Guid id, UpdateTodoItemDto todoItemDto)
    {
        Guid userId = _userContext.GetUserId();

        if (userId == Guid.Empty)
            return ResponseDto<TodoItemDto>.Error(ErrorCode.Unauthorized, "Unauthorized.");

        try
        {
            TodoItem? todoItem = await _todoItemRepository.GetByIdAsync(id);

            if (todoItem == null || todoItem.TodoListId != listId)
                return ResponseDto<TodoItemDto>.Error(ErrorCode.TodoItemNotFound, "Todo item not found.");

            if (todoItem.TodoList.UserId != userId)
                return ResponseDto<TodoItemDto>.Error(ErrorCode.TodoListNotFound, "Todo list not found.");

            todoItem.Title = Title.Create(todoItemDto.Title ?? "");
            todoItem.Description = Description.Create(todoItemDto.Description ?? "");

            await _todoItemRepository.EditAsync(todoItem);

            TodoItemDto todoItemResultDto = new()
            {
                Id = todoItem.Id,
                Title = todoItem.Title.Value,
                Description = todoItem.Description.Value,
                Completed = todoItem.Completed?.Value
            };

            return ResponseDto<TodoItemDto>.Success("Edited.", todoItemResultDto);
        }
        catch (ArgumentException e)
        {
            return ResponseDto<TodoItemDto>.Error(ErrorCode.InvalidField, e.Message);
        }
        catch (Exception e)
        {
            return ResponseDto<TodoItemDto>.Error(ErrorCode.UndefinedError, e.Message);
        }
    }

    public async Task<ResponseDto<TodoItemDto>> CompleteTodoItem(Guid listId, Guid id)
    {
        Guid userId = _userContext.GetUserId();

        if (userId == Guid.Empty)
            return ResponseDto<TodoItemDto>.Error(ErrorCode.Unauthorized, "Unauthorized.");

        try
        {
            TodoItem? todoItem = await _todoItemRepository.GetByIdAsync(id);

            if (todoItem == null || todoItem.TodoListId != listId)
                return ResponseDto<TodoItemDto>.Error(ErrorCode.TodoItemNotFound, "Todo item not found.");

            if (todoItem.TodoList.UserId != userId)
                return ResponseDto<TodoItemDto>.Error(ErrorCode.TodoListNotFound, "Todo list not found.");

            todoItem.Completed = Completed.Create(true);

            await _todoItemRepository.EditAsync(todoItem);

            TodoItemDto todoItemResultDto = new()
            {
                Id = todoItem.Id,
                Title = todoItem.Title?.Value,
                Description = todoItem.Description?.Value,
                Completed = todoItem.Completed?.Value
            };

            return ResponseDto<TodoItemDto>.Success("Completed.", todoItemResultDto);
        }
        catch (ArgumentException e)
        {
            return ResponseDto<TodoItemDto>.Error(ErrorCode.InvalidField, e.Message);
        }
        catch (Exception e)
        {
            return ResponseDto<TodoItemDto>.Error(ErrorCode.UndefinedError, e.Message);
        }
    }

    public async Task<ResponseDto> DeleteTodoItem(Guid listId, Guid id)
    {
        Guid userId = _userContext.GetUserId();
        if (userId == Guid.Empty)
            return ResponseDto.Error(ErrorCode.Unauthorized, "Unauthorized.");

        try
        {
            TodoItem? todoItem = await _todoItemRepository.GetByIdAsync(id);

            if (todoItem == null || todoItem.TodoListId != listId)
                return ResponseDto.Error(ErrorCode.TodoItemNotFound, "Todo item not found.");

            if (todoItem.TodoList.UserId != userId)
                return ResponseDto.Error(ErrorCode.TodoListNotFound, "Todo list not found.");

            await _todoItemRepository.RemoveAsync(todoItem.Id);

            return ResponseDto.Success("Deleted.");
        }
        catch (Exception e)
        {
            return ResponseDto.Error(ErrorCode.UndefinedError, e.Message);
        }
    }
}
