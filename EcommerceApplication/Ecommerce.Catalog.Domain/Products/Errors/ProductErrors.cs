using Ecommerce.Catalog.Domain.Products.Events;
using Ecommerce.Catalog.Domain.Products.Rules;
using ErrorOr;

namespace Ecommerce.Catalog.Domain.Products.Errors;

public static class ProductErrors
{
    public static Error ProductOutOfStock =>
        Error.Validation("Product.OutOfStock", ProductCannotBeSoldWhenProductIsOutOfStock.Message);

    public static Error ProductPublishWithStockEmpty =>
        Error.Validation("Product.OutOfStock", ProductStockCannotBeZeroWhenPublishRule.Message);

    public static class Price
    {
        public static Error PriceOverExpensive =>
            Error.Validation("Price.OverExpensive", PriceCannotBeOverExpensiveRule.Message);

        public static Error PriceEqualsToZero =>
            Error.Validation("Price.EqualsToZero", PriceCannotBeZeroRule.Message);
    }

    public static class Images
    {
        public static Error InvalidFormat(string description) =>
            Error.Validation("Image.InvalidFormat", description);

        public static Error ExcessiveSize(string description) =>
            Error.Validation("Image.ExcessiveSize", description);
    }
}
