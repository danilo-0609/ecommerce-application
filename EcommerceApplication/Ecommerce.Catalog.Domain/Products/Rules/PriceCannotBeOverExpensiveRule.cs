using Ecommerce.BuildingBlocks.Domain;
using Ecommerce.Catalog.Domain.Products.Errors;
using ErrorOr;

namespace Ecommerce.Catalog.Domain.Products.Rules;

public sealed class PriceCannotBeOverExpensiveRule : IBusinessRule
{
    private const decimal MaxAmountOfPrice = 100000000m;
    private readonly decimal _price;

    public PriceCannotBeOverExpensiveRule(decimal price)
    {
        _price = price;
    }

    public static string Message => "Price cannot be more expensive than 100.000.000 COP";

    public bool IsBroken()
    {
        if (_price > MaxAmountOfPrice)
        {
            return true;
        }

        return false;
    }

    public Error Error => ProductErrors.Price.PriceOverExpensive;
}
