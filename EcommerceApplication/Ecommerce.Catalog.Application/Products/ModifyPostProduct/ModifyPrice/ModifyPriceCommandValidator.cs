using Ecommerce.Catalog.Domain.Products.Rules;
using FluentValidation;

namespace Ecommerce.Catalog.Application.Products.ModifyPostProduct.ModifyPrice;

internal sealed class ModifyPriceCommandValidator 
    : AbstractValidator<ModifyPriceCommand>
{
    public ModifyPriceCommandValidator()
    {
        RuleFor(r => r.ProductId)
           .NotNull()
           .NotEmpty();

        RuleFor(r => r.Price)
            .NotEmpty().WithMessage("Price is required")
            .NotNull().WithMessage("Price is required")
            .Must(price =>
            {
                PriceCannotBeZeroRule priceEqualsToZeroRule = new(price);

                if (priceEqualsToZeroRule.IsBroken())
                {
                    return false;
                }

                return true;

            }).WithMessage(PriceCannotBeZeroRule.Message)
            .Must(price =>
            {
                PriceCannotBeOverExpensiveRule priceOverExpensiveRule = new(price);

                if (priceOverExpensiveRule.IsBroken())
                {
                    return false;
                }

                return true;

            }).WithMessage(PriceCannotBeOverExpensiveRule.Message);
    }
}
