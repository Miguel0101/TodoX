using Microsoft.EntityFrameworkCore;
using TodoX.Domain.TodoLists.Entities;
using TodoX.Domain.TodoLists.Interfaces;
using TodoX.Infrastructure.Data;

namespace TodoX.Infrastructure.TodoLists.Repositories;

public class TodoListRepository : ITodoListRepository
{
    private readonly AppDbContext _db;

    public TodoListRepository(AppDbContext db)
    {
        _db = db;
    }

    #region [Queries]

    public async Task<TodoList?> GetByIdAsync(Guid id)
    {
        return await _db.TodoLists.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<List<TodoList>> GetListAsync()
    {
        return await _db.TodoLists.ToListAsync();
    }

    #endregion

    #region [Commands]

    public async Task AddAsync(TodoList todoList)
    {
        await _db.TodoLists.AddAsync(todoList);
        await _db.SaveChangesAsync();
    }

    public async Task EditAsync(Guid id, TodoList todoList)
    {
        TodoList? todoListEntity = await GetByIdAsync(id) ?? throw new NullReferenceException("The todo list doesn't exist.");

        todoListEntity.Title = todoList.Title;

        await _db.SaveChangesAsync();
    }

    public async Task RemoveAsync(Guid id)
    {
        TodoList? todoListEntity = await GetByIdAsync(id) ?? throw new NullReferenceException("The todo list doesn't exist.");

        _db.Remove(todoListEntity);
        await _db.SaveChangesAsync();
    }

    #endregion
}