using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoX.Domain.Users.Entities;
using TodoX.Domain.Users.ValueObjects;

namespace TodoX.Infrastructure.Users.Configurations;

public class UserAccessTokenConfiguration : IEntityTypeConfiguration<UserAccessToken>
{
    public void Configure(EntityTypeBuilder<UserAccessToken> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Token)
               .HasConversion(token => token!.Value, value => AccessToken.FromToken(value))
               .HasColumnName("Token")
               .IsRequired()
               .HasMaxLength(AccessToken.Digits);
    }
}