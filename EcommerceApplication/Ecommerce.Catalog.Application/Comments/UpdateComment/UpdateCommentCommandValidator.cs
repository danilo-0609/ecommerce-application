using FluentValidation;

namespace Ecommerce.Catalog.Application.Comments.UpdateComment;

internal sealed class UpdateCommentCommandValidator 
    : AbstractValidator<UpdateCommentCommand>
{
    public UpdateCommentCommandValidator()
    {
        RuleFor(r => r.ProductId)
            .NotNull().WithMessage("Product id is required")
            .NotEmpty().WithMessage("Product id is required");

        RuleFor(r => r.CommentId)
            .NotEmpty().WithMessage("Comment id is required")
            .NotNull().WithMessage("Comment id is required");

        RuleFor(r => r.Comment)
            .NotEmpty().WithMessage("New comment is required")
            .NotNull().WithMessage("New comment is required");
    }
}
