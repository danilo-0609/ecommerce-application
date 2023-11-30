using Ecommerce.Catalog.Domain.Sales;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Catalog.Infrastructure.Persistence.Configuration;

public sealed class SaleConfiguration : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.ToTable("Sales", "catalog");

        builder.HasKey(x => x.SaleId);

        builder.Property(x => x.SaleId)
            .IsRequired()
            .ValueGeneratedNever()
            .HasColumnName("SaleId");

        builder.Property(x => x.ProductId)
            .IsRequired()
            .ValueGeneratedNever()
            .HasColumnName("ProductId");

        builder.Property(x => x.Price)
            .IsRequired()
            .HasColumnName("Price");
            
        builder.Property(x => x.UserId)
            .IsRequired()
            .HasColumnName("UserId");

        builder.Property(x => x.CreatedDateTime)
            .IsRequired()
            .HasColumnName("CreatedDateTime");
    }
}
