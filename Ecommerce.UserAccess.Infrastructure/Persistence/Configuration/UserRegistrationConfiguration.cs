using Ecommerce.UserAccess.Domain.Common;
using Ecommerce.UserAccess.Domain.UserRegistrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.UserAccess.Infrastructure.Persistence.Configuration;

internal sealed class UserRegistrationConfiguration : IEntityTypeConfiguration<UserRegistration>
{
    public void Configure(EntityTypeBuilder<UserRegistration> builder)
    {
        builder.ToTable("UserRegistrations", "users");

        builder.HasKey(x => x.Id);

        builder.Property<string>(p => p.Login).HasColumnName("Login");
        builder.Property<string>(p => p.Email).HasColumnName("Email");

        builder.Property(p => p.Password)
            .HasConversion(
                password => password.Value,
                value => Password.Create(value)!)
            .HasColumnName("Password");

        builder.Property<string>(p => p.FirstName).HasColumnName("FirstName");
        
        builder.Property<string>(p => p.LastName)
            .IsRequired(false)
            .HasDefaultValue("")
            .HasColumnName("LastName");

        builder.Property<string>(p => p.Name).HasColumnName("Name");
        builder.Property<DateTime>(p => p.RegisteredDate).HasColumnName("RegisterDate");
        builder.Property<DateTime?>(p => p.ConfirmedDate).HasColumnName("ConfirmedDate");

        builder.OwnsOne<UserRegistrationStatus>("Status", b =>
        {
            b.Property(x => x.Value).HasColumnName("StatusCode");
        });
    }
}
