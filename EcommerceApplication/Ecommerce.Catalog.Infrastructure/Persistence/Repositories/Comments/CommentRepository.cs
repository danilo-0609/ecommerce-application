using Ecommerce.Catalog.Domain.Comments;
using Ecommerce.Catalog.Domain.Products;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Catalog.Infrastructure.Persistence.Repositories.Comments;

internal sealed class CommentRepository : ICommentRepository
{
    private readonly ApplicationDbContext _dbContext;

    public CommentRepository(ApplicationDbContext context)
    {
        _dbContext = context;
    }

    public async Task AddAsync(Comment comment)
    {
        await _dbContext.Comments.AddAsync(comment);
    }

    public async Task DeleteAsync(Comment comment, CancellationToken cancellationToken = default)
    {
        await _dbContext.Comments
            .Where(x => x.CommentId == comment.CommentId)
            .ExecuteDeleteAsync();
    }

    public async Task<List<Comment>?> GetAllCommentsByProductId(ProductId productId, CancellationToken cancellationToken = default)
    {
        List<Comment> comments = await _dbContext
            .Comments
            .Where(x => x.ProductId == productId)
            .ToListAsync();

        return comments;
    }

    public async Task<Comment?> GetCommentByIdAsync(CommentId id)
    {
        Comment? comment = await _dbContext
            .Comments
            .Where(x => x.CommentId == id)
            .SingleOrDefaultAsync();

        if (comment is null)
        {
            return null;
        }

        return comment;
    }

    public async Task UpdateAsync(Comment comment, CancellationToken cancellationToken = default)
    {
        await _dbContext
            .Comments
            .Where(x => x.CommentId == comment.CommentId)
            .ExecuteUpdateAsync(setters =>
                setters.SetProperty(b => b.CommentId, comment.CommentId)
                       .SetProperty(b => b.UserId, comment.UserId)
                       .SetProperty(b => b.ProductId, comment.ProductId)
                       .SetProperty(b => b.CommentText, comment.CommentText)
                       .SetProperty(b => b.IsActive, comment.IsActive)
                       .SetProperty(b => b.CreatedDateTime, comment.CreatedDateTime)
                       .SetProperty(b => b.UpdatedDateTime, comment.UpdatedDateTime));
    }
}
