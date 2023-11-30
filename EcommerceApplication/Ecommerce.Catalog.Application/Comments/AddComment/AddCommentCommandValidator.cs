using FluentValidation;

namespace Ecommerce.Catalog.Application.Comments.AddComment;

internal sealed class AddCommentCommandValidator : AbstractValidator<AddCommentCommand>
{
    public AddCommentCommandValidator()
    {
        RuleFor(r => r.ProductId)
            .NotEmpty().WithMessage("Product id is required")
            .NotNull().WithMessage("Product id cannot be null");

        RuleFor(r => r.Comment)
            .NotEmpty().WithMessage("Comment is required")
            .NotNull().WithMessage("Comment is required")
            .MaximumLength(4000).WithMessage("Comment length cannot be greater than 4000 letters");
    }
}
