using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoX.Domain.TodoLists.Entities;
using TodoX.Domain.TodoLists.ValueObjects;

namespace TodoX.Infrastructure.Users.Configurations;

public class TodoListConfiguration : IEntityTypeConfiguration<TodoList>
{
    public void Configure(EntityTypeBuilder<TodoList> builder)
    {
        builder.HasKey(t => t.Id);

        builder.OwnsOne(t => t.Title, title =>
        {
            title.Property(t => t.Value)
                .HasColumnName("Title")
                .IsRequired()
                .HasMaxLength(Title.MaxLength);
        });
    }
}