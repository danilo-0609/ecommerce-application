using Ecommerce.BuildingBlocks.Domain;
using Ecommerce.Catalog.Domain.Products.Errors;
using Ecommerce.Catalog.Domain.Products.Rules;
using ErrorOr;
using MediatR;

namespace Ecommerce.Catalog.Domain.Products.ValueObjects;

public sealed record Price : ValueObject
{
    public decimal Value { get; private set; } = decimal.Zero;

    public static ErrorOr<Price> Create(decimal price)
    {
        var checkRules = CheckRules(price);

        if (checkRules.IsError)
        {
            return checkRules.FirstError;
        }

        return new Price(price);
    }

    private static ErrorOr<Unit> CheckRules(decimal price)
    {
        PriceCannotBeZeroRule priceEqualsToZeroRule = new(price);

        if (priceEqualsToZeroRule.IsBroken())
        {
            return ProductErrors.Price.PriceEqualsToZero; ;
        }

        PriceCannotBeOverExpensiveRule priceOverExpensiveRule = new(price);

        if (priceOverExpensiveRule.IsBroken())
        {
            return ProductErrors.Price.PriceOverExpensive;
        }

        return Unit.Value;
    }

    private Price(decimal value)
    {
        Value = value;
    }

}
