using Ecommerce.Catalog.Domain.Ratings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Catalog.Infrastructure.Persistence.Configuration;

internal sealed class RatingConfiguration : IEntityTypeConfiguration<Rating>
{
    public void Configure(EntityTypeBuilder<Rating> builder)
    {
        builder.ToTable("Ratings", "catalog");

        builder.HasKey(builder => builder.RatingId);

        builder.Property(builder => builder.RatingId)
            .IsRequired()
            .ValueGeneratedNever()
            .HasColumnName(nameof(RatingId));

        builder.Property(builder => builder.RatingComment)
            .HasColumnName("RatingComment")
            .HasMaxLength(500);

        builder.Property(p => p.UserId)
            .IsRequired()
            .HasColumnName("UserId");

        builder.Property(p => p.ProductId)
            .IsRequired()
            .HasColumnName("ProductId");

        builder.Property(p => p.RatingValue)
            .IsRequired()
            .HasColumnName("RatingValue");

        builder.Property(p => p.CreatedDateTime)
            .IsRequired()
            .HasColumnName("CreatedDateTime");

        builder.Property(p => p.UpdatedDateTime)
            .IsRequired()
            .HasColumnName("UpdatedDateTime");
    }
}
