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

        builder.OwnsOne(t => t.Title, title =>
        {
            title.Property(t => t.Value)
                .HasColumnName("Title")
                .IsRequired()
                .HasMaxLength(Title.MaxLength);
        });

        builder.OwnsOne(d => d.Description, description =>
        {
            description.Property(d => d.Value)
                       .HasColumnName("Description")
                       .IsRequired()
                       .HasMaxLength(Description.MaxLength);
        });

        builder.OwnsOne(c => c.Completed, completed =>
        {
            completed.Property(c => c.Value)
                     .HasColumnName("Completed")
                     .IsRequired();
        });
    }
}