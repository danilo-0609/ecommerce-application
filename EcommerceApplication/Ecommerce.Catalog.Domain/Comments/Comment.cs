using Ecommerce.BuildingBlocks.Domain;
using Ecommerce.Catalog.Domain.Common;
using Ecommerce.Catalog.Domain.Products;

namespace Ecommerce.Catalog.Domain.Comments;

public sealed class Comment : AggregateRoot<CommentId, Guid>
{
    public CommentId CommentId { get; }

    public UserId UserId { get; private set; }

    public ProductId ProductId { get; private set; } 

    public string CommentText { get; private set; }
    
    public bool IsActive { get; private set; }

    public DateTime CreatedDateTime { get; private set; }

    public DateTime? UpdatedDateTime { get; private set; }

    public static Comment? Create(Guid userIdValue,
        ProductId productId,
        string? commentValue,
        DateTime createdOnUtcNow)
    {
        if (productId is null ||
            string.IsNullOrEmpty(commentValue)) 
        {
            return null; 
        }


        var productCommentId = CommentId.CreateUnique();
        var userId = new UserId(userIdValue);

        var comment = new Comment(productCommentId,
            userId,
            productId,
            commentValue,
            createdOnUtcNow,
            null);

        return comment;
    }

    public static Comment? Update(Guid userIdValue,
        ProductId productId,
        string? commentValue,
        DateTime createdOnUtcTime,
        DateTime updatedOnUtcTime)
    {
        if (productId is null ||
         string.IsNullOrEmpty(commentValue))
        {
            return null;
        }

        var productCommentId = CommentId.CreateUnique();
        var userId = new UserId(userIdValue);

        var comment = new Comment(productCommentId,
            userId,
            productId,
            commentValue,
            createdOnUtcTime,
            updatedOnUtcTime);

        return comment;
    }

    public void Delete(Comment comment)
    {
        comment.IsActive = false;
    }

    private Comment(CommentId id,
        UserId userId,
        ProductId productId,
        string comment,
        DateTime createdDateTime,
        DateTime? updatedDateTime) : base(id)
    {
        CommentId = id;
        UserId = userId;
        ProductId = productId;
        CommentText = comment;
        CreatedDateTime = createdDateTime;
        UpdatedDateTime = updatedDateTime;
    }
}
