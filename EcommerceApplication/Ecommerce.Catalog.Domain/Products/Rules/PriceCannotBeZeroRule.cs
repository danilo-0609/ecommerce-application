using Ecommerce.BuildingBlocks.Domain;
using Ecommerce.Catalog.Domain.Products.Errors;
using ErrorOr;

namespace Ecommerce.Catalog.Domain.Products.Rules;

public sealed class PriceCannotBeZeroRule : IBusinessRule
{
    private readonly decimal _price;

    public PriceCannotBeZeroRule(decimal price)
    {
        _price = price;
    }

    public static string Message => "Price cannot be equals to zero.";

    public bool IsBroken()
    {
        if (_price == 0)
        {
            return true;
        }

        return false;
    }

    public Error Error => ProductErrors.Price.PriceEqualsToZero;
}
