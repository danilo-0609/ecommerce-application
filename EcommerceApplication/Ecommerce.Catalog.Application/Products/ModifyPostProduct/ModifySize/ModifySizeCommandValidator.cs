using FluentValidation;

namespace Ecommerce.Catalog.Application.Products.ModifyPostProduct.ModifySize;

internal sealed class ModifySizeCommandValidator 
    : AbstractValidator<ModifySizeCommand>
{
    public ModifySizeCommandValidator()
    {
        RuleFor(r => r.ProductId)
           .NotNull()
           .NotEmpty();

        RuleFor(r => r.Size)
            .NotEmpty().WithMessage("Size is required")
            .NotNull().WithMessage("Size is required");
    }
}
