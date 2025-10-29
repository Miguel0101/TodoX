using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoX.Domain.Users.Entities;
using TodoX.Domain.Users.ValueObjects;

namespace TodoX.Infrastructure.Users.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Name)
               .HasConversion(name => name!.Value, value => Name.Create(value))
               .HasColumnName("Name")
               .IsRequired()
               .HasMaxLength(Name.MaxLength);

        builder.Property(u => u.Email)
               .HasConversion(email => email!.Value, value => Email.Create(value))
               .HasColumnName("Email")
               .IsRequired();

        builder.Property(u => u.Password)
               .HasConversion(password => password!.HashedValue, hashed => Password.FromHash(hashed))
               .HasColumnName("Password")
               .IsRequired();
    }
}