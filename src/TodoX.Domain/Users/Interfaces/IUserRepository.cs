using TodoX.Domain.Users.Entities;

namespace TodoX.Domain.Users.Interfaces;

public interface IUserRepository
{
    #region [Queries]

    Task<User?> GetByIdAsync(Guid id);
    Task<List<User>> GetListAsync();

    #endregion

    #region [Commands]

    Task AddAsync(User user);
    Task EditAsync(Guid id, User user);
    Task RemoveAsync(Guid id);

    #endregion
}
