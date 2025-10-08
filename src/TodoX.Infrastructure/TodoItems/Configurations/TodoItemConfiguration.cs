using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoX.Domain.TodoItems.Entities;
using TodoX.Domain.TodoItems.ValueObjects;

namespace TodoX.Infrastructure.TodoItems.Configurations;

public class TodoItemConfiguration : IEntityTypeConfiguration<TodoItem>
{
    public void Configure(EntityTypeBuilder<TodoItem> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Title)
               .HasConversion(title => title!.Value, value => Title.Create(value))
               .HasColumnName("Title")
               .IsRequired()
               .HasMaxLength(Title.MaxLength);

        builder.Property(d => d.Description)
               .HasConversion(description => description!.Value, value => Description.Create(value))
               .HasColumnName("Description")
               .IsRequired()
               .HasMaxLength(Description.MaxLength);

        builder.Property(c => c.Completed)
               .HasConversion(completed => completed!.Value, value => Completed.Create(value))
               .HasColumnName("Completed")
               .IsRequired();
    }
}