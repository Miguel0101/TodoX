using TodoX.Domain.TodoLists.Entities;

namespace TodoX.Domain.TodoLists.Interfaces;

public interface ITodoListRepository
{
    #region [Queries]

    Task<TodoList?> GetByIdAsync(Guid id);
    Task<List<TodoList>> GetListAsync();

    #endregion

    #region [Commands]

    Task AddAsync(TodoList todoList);
    Task EditAsync(Guid id, TodoList todoList);
    Task RemoveAsync(Guid id);

    #endregion
}