using Microsoft.EntityFrameworkCore;
using TodoX.Domain.TodoItems.Entities;
using TodoX.Domain.TodoLists.Entities;
using TodoX.Domain.Users.Entities;
using TodoX.Infrastructure.TodoLists.Configurations;
using TodoX.Infrastructure.Users.Configurations;

namespace TodoX.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<TodoList> TodoLists { get; set; }
    public DbSet<TodoItem> TodoItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new TodoListConfiguration());
        modelBuilder.ApplyConfiguration(new TodoItemConfiguration());
    }
}