using Ecommerce.Catalog.Domain.Comments;
using Ecommerce.Catalog.Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Catalog.Infrastructure.Persistence.Configuration;

internal sealed class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.ToTable("Comments", "catalog");

        builder.HasKey(k => k.CommentId);

        builder.Property<CommentId>(p => p.CommentId)
            .IsRequired()
            .ValueGeneratedNever()
            .HasColumnName("CommentId");

        builder.Property<Guid>(p => p.UserId.Value)
            .IsRequired()
            .HasColumnName("UserId");

        builder.Property<ProductId>(p => p.ProductId)
            .IsRequired()
            .HasColumnName("ProductId");

        builder.Property<string>(p => p.CommentText)
            .IsRequired()
            .HasColumnName("CommentText")
            .HasMaxLength(4000);

        builder.Property<bool>(p => p.IsActive)
            .IsRequired()
            .HasColumnName("IsActive");

        builder.Property<DateTime>(p => p.CreatedDateTime)
            .IsRequired()
            .HasColumnName("CreatedDateTime");

        builder.Property<DateTime?>(p => p.UpdatedDateTime)
            .HasColumnName("UpdatedDateTime");
    }
}
