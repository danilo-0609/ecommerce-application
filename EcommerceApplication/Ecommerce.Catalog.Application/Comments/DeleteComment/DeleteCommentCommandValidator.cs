using FluentValidation;

namespace Ecommerce.Catalog.Application.Comments.DeleteComment;

public sealed class DeleteCommentCommandValidator : AbstractValidator<DeleteCommentCommand>
{
    public DeleteCommentCommandValidator()
    {
        RuleFor(r => r.CommentId)
            .NotEmpty().WithMessage("Comment id is required")
            .NotNull().WithMessage("Comment id is required");
    }
}
