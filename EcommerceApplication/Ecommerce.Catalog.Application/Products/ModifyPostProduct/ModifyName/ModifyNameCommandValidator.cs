using FluentValidation;

namespace Ecommerce.Catalog.Application.Products.ModifyPostProduct.ModifyName;

internal sealed class ModifyNameCommandValidator 
    : AbstractValidator<ModifyNameCommand> 
{
    public ModifyNameCommandValidator()
    {
        RuleFor(r => r.ProductId)
           .NotNull()
           .NotEmpty();

        RuleFor(r => r.Name)
            .NotEmpty().WithMessage("Name is required")
            .NotNull().WithMessage("Name is required");
    }
}
