using Ecommerce.Catalog.Domain.Products;
using Ecommerce.Catalog.Domain.Products.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Catalog.Infrastructure.Persistence.Configuration;

internal sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products", "catalog");

        builder.HasKey(p => p.ProductId);

        builder.Property<ProductId>(p => p.ProductId)
            .IsRequired()
            .ValueGeneratedNever()
            .HasColumnName("ProductId");

        builder.Property<Guid>(p => p.SellerId)
            .IsRequired()
            .ValueGeneratedNever()
            .HasColumnName("SellerId");

        builder.Property<string>(p => p.Name)
            .IsRequired()
            .HasColumnName("Name")
            .HasMaxLength(300);

        builder.OwnsOne<Price>(p => p.Price, b =>
        {
            b.Property(i => i.Value).HasColumnName("PriceValue");
        });

        builder.Property<string>(p => p.Description)
            .IsRequired()
            .HasColumnName("Description")
            .HasMaxLength(2500);

        builder.Property<string>(p => p.Size)
            .IsRequired()
            .HasColumnName("Size")
            .HasMaxLength(10);

        builder.Property<string>(p => p.Color)
            .IsRequired()
            .HasColumnName("Color")
            .HasMaxLength(50);

        builder.OwnsOne(p => p.ProductType, b =>
        {
            b.Property(p => p.Value).HasColumnName("ProductTypeValue");
        });

        builder.Property<int>(p => p.InStock)
            .IsRequired()
            .HasColumnName("InStock");

        builder.Property<bool>(p => p.IsActive)
            .IsRequired()
            .ValueGeneratedNever()
            .HasColumnName("IsActive");

        builder.Property<DateTime>(p => p.CreatedDateTime)
            .IsRequired()
            .ValueGeneratedNever()
            .HasColumnName("CreatedDateTime");

        builder.Property<DateTime?>(p => p.UpdatedDateTime)
            .HasColumnName("UpdatedDateTime");

        builder.Property<DateTime?>(p => p.ExpireDateTime)
            .HasColumnName("ExpiredDateTime");
    }
}
