using Ecommerce.BuildingBlocks.Domain;
using Ecommerce.Catalog.Domain.Products.Errors;
using ErrorOr;

namespace Ecommerce.Catalog.Domain.Products.Rules;

internal sealed class ProductCannotBeSoldWhenProductIsOutOfStock : IBusinessRule
{
    private readonly int _inStock;

    public static string Message => "Product cannot be sold when product is out of stock";

    public ProductCannotBeSoldWhenProductIsOutOfStock(int inStock)
    {
        _inStock = inStock;
    }

    public Error Error => ProductErrors.ProductOutOfStock;

    public bool IsBroken() => _inStock == 0;
}
