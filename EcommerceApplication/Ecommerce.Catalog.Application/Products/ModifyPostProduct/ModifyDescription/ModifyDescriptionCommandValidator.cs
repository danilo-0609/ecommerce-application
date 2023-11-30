using FluentValidation;

namespace Ecommerce.Catalog.Application.Products.ModifyPostProduct.ModifyDescription;

internal sealed class ModifyDescriptionCommandValidator
    : AbstractValidator<ModifyDescriptionCommand>
{
    public ModifyDescriptionCommandValidator()
    {
        RuleFor(r => r.ProductId)
            .NotNull()
            .NotEmpty();

        RuleFor(r => r.Description)
            .NotEmpty().WithMessage("Description is required")
            .NotNull().WithMessage("Description is required");
    }
}
