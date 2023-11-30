using FluentValidation;

namespace Ecommerce.Catalog.Application.Comments.GetAllComments;

internal sealed class GetAllCommentsQueryValidator : AbstractValidator<GetAllCommentsQuery>
{
    public GetAllCommentsQueryValidator()
    {
        RuleFor(r => r.ProductId)
            .NotNull().WithMessage("Product id is required")
            .NotEmpty().WithMessage("Product id is required");
    }
}
