using Ecommerce.BuildingBlocks.Application.Commands;
using Ecommerce.Catalog.Domain.Comments;
using ErrorOr;
using MediatR;

namespace Ecommerce.Catalog.Application.Comments.DeleteComment;

internal sealed class DeleteCommentCommandHandler 
    : ICommandRequestHandler<DeleteCommentCommand, ErrorOr<Unit>>
{
    private readonly ICommentRepository _commentRepository;

    public DeleteCommentCommandHandler(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository ?? throw new ArgumentNullException(nameof(commentRepository));
    }

    public async Task<ErrorOr<Unit>> Handle(DeleteCommentCommand command, 
        CancellationToken cancellationToken)
    {
        var commentId = CommentId.Create(command.CommentId);

        var comment = await _commentRepository.GetCommentByIdAsync(commentId);

        if (comment is null)
        {
            return Error.NotFound("Comment.NotFound", "Comment was not found");
        }

        comment.Delete(comment);

        await _commentRepository.DeleteAsync(comment, cancellationToken);

        return Unit.Value;
    }
}
