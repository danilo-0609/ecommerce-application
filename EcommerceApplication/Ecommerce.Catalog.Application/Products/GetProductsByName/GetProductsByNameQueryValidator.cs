using FluentValidation;

namespace Ecommerce.Catalog.Application.Products.GetProductsByName;

internal sealed class GetProductsByNameQueryValidator
    : AbstractValidator<GetProductsByNameQuery>
{
    public GetProductsByNameQueryValidator()
    {
        RuleFor(r => r.ProductName)
            .NotEmpty().WithMessage("Product name is required")
            .NotNull().WithMessage("Product name is required");
    }
}
