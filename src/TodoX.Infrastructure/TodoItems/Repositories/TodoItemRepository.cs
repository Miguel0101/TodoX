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
        return await _db.TodoItems.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<List<TodoItem>> GetListAsync()
    {
        return await _db.TodoItems.ToListAsync();
    }

    #endregion

    #region [Commands]

    public async Task AddAsync(TodoItem todoItem)
    {
        await _db.TodoItems.AddAsync(todoItem);
        await _db.SaveChangesAsync();
    }

    public async Task EditAsync(Guid id, TodoItem todoItem)
    {
        TodoItem? todoItemEntity = await GetByIdAsync(id) ?? throw new NullReferenceException("The todo item doesn't exist.");

        todoItemEntity.Title = todoItem.Title;
        todoItemEntity.Description = todoItem.Description;

        await _db.SaveChangesAsync();
    }

    public async Task RemoveAsync(Guid id)
    {
        TodoItem? todoItemEntity = await GetByIdAsync(id) ?? throw new NullReferenceException("The todo item doesn't exist.");

        _db.Remove(todoItemEntity);
        await _db.SaveChangesAsync();
    }

    #endregion
}