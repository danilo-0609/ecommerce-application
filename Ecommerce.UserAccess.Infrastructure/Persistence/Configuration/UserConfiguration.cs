using Ecommerce.UserAccess.Domain.Common;
using Ecommerce.UserAccess.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.UserAccess.Infrastructure.Persistence.Configuration;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users", "user_access");

        builder.HasKey(k => k.UserId);

        builder.Property(p => p.UserId)
            .IsRequired()
            .ValueGeneratedNever()
            .HasColumnName("UserId");

        builder.Property(p => p.Login)
            .IsRequired()
            .HasColumnName("Login");

        builder.Property(p => p.Password)
            .IsRequired()
            .HasConversion(
            password => password.Value,
            value => Password.Create(value)!)
            .HasColumnName("Password");

        builder.Property(p => p.Email)
            .IsRequired()
            .HasColumnName("Email");

        builder.Property(p => p.IsActive)
            .IsRequired()
            .HasColumnName("IsActive");

        builder.Property(p => p.FirstName)
            .IsRequired()
            .HasColumnName("FirstName");

        builder.Property(p => p.LastName)
            .IsRequired(false)
            .HasDefaultValue("")
            .HasColumnName("LastName");

        builder.Property(p => p.Name)
            .IsRequired()
            .HasColumnName("Name");

        builder.OwnsMany<UserRole>("UserRole" , b =>
        {
            b.WithOwner().HasForeignKey("UserId");
            b.ToTable("UserRoles", "user_access");
            b.Property<UserId>("UserId");
            b.Property<string>("Value").HasColumnName("RoleCode");
            b.HasKey("UserId", "Value");
        });
    }
}
