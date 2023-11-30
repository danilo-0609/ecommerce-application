using Ecommerce.Catalog.Domain.Products;

namespace Ecommerce.Catalog.Domain.Comments;

public interface ICommentRepository
{
    Task AddAsync(Comment comment);

    Task<Comment?> GetCommentByIdAsync(CommentId id);

    Task DeleteAsync(Comment comment, CancellationToken cancellationToken = default);

    Task<List<Comment>?> GetAllCommentsByProductId(ProductId productId, CancellationToken cancellationToken = default);

    Task UpdateAsync(Comment comment, CancellationToken cancellationToken = default);
}
