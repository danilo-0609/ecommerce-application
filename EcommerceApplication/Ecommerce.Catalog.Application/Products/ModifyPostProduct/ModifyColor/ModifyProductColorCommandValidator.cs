using FluentValidation;

namespace Ecommerce.Catalog.Application.Products.ModifyPostProduct.ModifyColor;

internal sealed class ModifyProductColorCommandValidator
    : AbstractValidator<ModifyProductColorCommand>
{
    public ModifyProductColorCommandValidator()
    {
        RuleFor(r => r.ProductId)
            .NotNull()
            .NotEmpty();

        RuleFor(r => r.Color)
            .NotEmpty().WithMessage("Color is required")
            .NotNull().WithMessage("Color is required");
    }
}
