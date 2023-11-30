using Ecommerce.Catalog.Application.Products.GetProductsByName;
using FluentValidation;

namespace Ecommerce.Catalog.Application.Products.GetProductsByProductType;

internal sealed class GetProductsByProductTypeQueryValidator 
    : AbstractValidator<GetProductByProductTypeQuery>
{
    public GetProductsByProductTypeQueryValidator()
    {
        RuleFor(r => r.ProductType)
            .NotNull().WithMessage("Product type is required")
            .NotEmpty().WithMessage("Product type is required");
    }
}
