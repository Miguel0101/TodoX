using TodoX.Domain.TodoItems.Entities;

namespace TodoX.Domain.TodoItems.Interfaces;

public interface ITodoItemRepository
{
    #region [Queries]

    Task<TodoItem?> GetByIdAsync(Guid id);
    Task<List<TodoItem>> GetListAsync();

    #endregion

    #region [Commands]

    Task AddAsync(TodoItem todoItem);
    Task EditAsync(Guid id, TodoItem todoItem);
    Task RemoveAsync(Guid id);

    #endregion
}
