using FluentValidation;

namespace Ecommerce.Catalog.Application.Products.SellProducts;

internal sealed class SellProductsCommandValidator : AbstractValidator<SellProductsCommand>
{
    public SellProductsCommandValidator()
    {
        RuleFor(r => r.AmountOfProducts)
            .NotNull().WithMessage("The amount of products sold cannot be null")
            .NotEmpty().WithMessage("The amount of products sold cannot be empty");

        RuleFor(r => r.ProductId)
            .NotNull().WithMessage("Product id cannot be null")
            .NotEmpty().WithMessage("Product id cannot be empty");
    }
}
