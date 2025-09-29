using Microsoft.EntityFrameworkCore;
using TodoX.Domain.Users.Entities;
using TodoX.Domain.Users.Interfaces;
using TodoX.Infrastructure.Data;

namespace TodoX.Infrastructure.Users.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _db;

    public UserRepository(AppDbContext db)
    {
        _db = db;
    }

    #region [Queries]

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _db.Users.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<List<User>> GetListAsync()
    {
        return await _db.Users.ToListAsync();
    }

    #endregion

    #region [Commands]

    public async Task AddAsync(User user)
    {
        await _db.Users.AddAsync(user);
        await _db.SaveChangesAsync();
    }

    public async Task EditAsync(Guid id, User user)
    {
        User? userEntity = await GetByIdAsync(id) ?? throw new NullReferenceException("The user doesn't exist.");

        userEntity.Name = user.Name;
        userEntity.Email = user.Email;

        await _db.SaveChangesAsync();
    }

    public async Task RemoveAsync(Guid id)
    {
        User? userEntity = await GetByIdAsync(id) ?? throw new NullReferenceException("The user doesn't exist.");

        _db.Remove(userEntity);
        await _db.SaveChangesAsync();
    }

    #endregion
}