using Microsoft.EntityFrameworkCore;
using TodoX.Domain.TodoItems.Entities;
using TodoX.Domain.TodoItems.Interfaces;
using TodoX.Infrastructure.Data;

namespace TodoX.Infrastructure.TodoItems.Repositories;

public class TodoItemRepository : ITodoItemRepository
{
    private readonly AppDbContext _db;

    public TodoItemRepository(AppDbContext db)
    {
        _db = db;
    }

    #region [Queries]

    public async Task<TodoItem?> GetByIdAsync(Guid id)
    {
        return await _db.TodoItems
            .Include(t => t.TodoList)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<List<TodoItem>> GetByListIdAsync(Guid todoListId)
    {
        return await _db.TodoItems
            .Include(t => t.TodoList)
            .Where(t => t.TodoListId == todoListId)
            .ToListAsync();
    }

    #endregion

    #region [Commands]

    public async Task AddAsync(TodoItem todoItem)
    {
        await _db.TodoItems.AddAsync(todoItem);
        await _db.SaveChangesAsync();
    }

    public async Task EditAsync(TodoItem todoItem)
    {
        TodoItem todoItemEntity = await GetByIdAsync(todoItem.Id) ?? throw new NullReferenceException("The todo item doesn't exist.");

        todoItemEntity.Title = todoItem.Title;
        todoItemEntity.Description = todoItem.Description;

        await _db.SaveChangesAsync();
    }

    public async Task RemoveAsync(Guid id)
    {
        TodoItem todoItemEntity = await GetByIdAsync(id) ?? throw new NullReferenceException("The todo item doesn't exist.");

        _db.Remove(todoItemEntity);
        await _db.SaveChangesAsync();
    }

    #endregion
}