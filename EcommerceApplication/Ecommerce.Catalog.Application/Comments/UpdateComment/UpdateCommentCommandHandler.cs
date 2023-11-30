using Ecommerce.BuildingBlocks.Application;
using Ecommerce.BuildingBlocks.Application.Commands;
using Ecommerce.Catalog.Domain.Comments;
using Ecommerce.Catalog.Domain.Products;
using ErrorOr;

namespace Ecommerce.Catalog.Application.Comments.UpdateComment;

internal sealed class UpdateCommentCommandHandler
    : ICommandRequestHandler<UpdateCommentCommand, ErrorOr<Guid>>
{
    private readonly ICommentRepository _commentRepository;
    private readonly IExecutionContextAccessor _contextAccessor;

    public UpdateCommentCommandHandler(ICommentRepository commentRepository, IExecutionContextAccessor contextAccessor)
    {
        _commentRepository = commentRepository ?? throw new ArgumentNullException(nameof(commentRepository));
        _contextAccessor = contextAccessor;
    }

    public async Task<ErrorOr<Guid>> Handle(UpdateCommentCommand command, 
        CancellationToken cancellationToken)
    {
        var productId = ProductId.Create(command.ProductId);
        var commentId = CommentId.Create(command.CommentId);

        var comment = await _commentRepository.GetCommentByIdAsync(commentId);

        if (comment is null)
        {
            return Error.NotFound("Comment.NotFound", "Product was not found");
        }

        var commentUpdate = Comment.Update(
            _contextAccessor.UserId, 
            productId, 
            command.Comment, 
            comment.CommentResponses.ToList(),
            comment.CreatedDateTime,
            DateTime.UtcNow);

        await _commentRepository.UpdateAsync(commentUpdate);

        return commentUpdate.CommentId.Value;
    }
}
