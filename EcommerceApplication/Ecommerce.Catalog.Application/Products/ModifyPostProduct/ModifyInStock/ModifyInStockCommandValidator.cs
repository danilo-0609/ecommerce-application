using FluentValidation;

namespace Ecommerce.Catalog.Application.Products.ModifyPostProduct.ModifyInStock;

internal sealed class ModifyInStockCommandValidator 
    : AbstractValidator<ModifyInStockCommand>
{
    public ModifyInStockCommandValidator()
    {
        RuleFor(r => r.ProductId)
           .NotNull()
           .NotEmpty();

        RuleFor(r => r.InStock)
            .NotEmpty().WithMessage("Amount in stock is required")
            .NotNull().WithMessage("Amount in stock is required");
    }
}
