using Ecommerce.BuildingBlocks.Domain;
using Ecommerce.Catalog.Domain.Products.Errors;
using ErrorOr;

namespace Ecommerce.Catalog.Domain.Products.Events;

internal sealed class ProductStockCannotBeZeroWhenPublishRule : IBusinessRule
{
    private readonly int _inStock;

    public ProductStockCannotBeZeroWhenPublishRule(int inStock)
    {
        _inStock = inStock;
    }

    public static string Message => "Product stock cannot be zero when publishing it";

    public Error Error => ProductErrors.ProductPublishWithStockEmpty;

    public bool IsBroken() => _inStock == 0;
}
