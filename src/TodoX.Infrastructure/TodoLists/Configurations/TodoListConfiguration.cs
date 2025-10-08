using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoX.Domain.TodoLists.Entities;
using TodoX.Domain.TodoLists.ValueObjects;

namespace TodoX.Infrastructure.TodoLists.Configurations;

public class TodoListConfiguration : IEntityTypeConfiguration<TodoList>
{
    public void Configure(EntityTypeBuilder<TodoList> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Title)
               .HasConversion(title => title!.Value, value => Title.Create(value))
               .HasColumnName("Title")
               .IsRequired()
               .HasMaxLength(Title.MaxLength);
    }
}