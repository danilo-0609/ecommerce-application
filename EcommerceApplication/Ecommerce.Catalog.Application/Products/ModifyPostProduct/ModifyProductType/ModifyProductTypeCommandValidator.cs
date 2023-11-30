using FluentValidation;

namespace Ecommerce.Catalog.Application.Products.ModifyPostProduct.ModifyProductType;

internal sealed class ModifyProductTypeCommandValidator 
    : AbstractValidator<ModifyProductTypeCommand>
{
    public ModifyProductTypeCommandValidator()
    {
        RuleFor(r => r.ProductId)
           .NotNull()
           .NotEmpty();

        RuleFor(r => r.ProductType)
            .NotEmpty().WithMessage("Product type is required")
            .NotNull().WithMessage("Product type is required");
    }
}
