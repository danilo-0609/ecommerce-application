using Ecommerce.Catalog.Application.Comments;
using Ecommerce.Catalog.Application.Ratings;

namespace Ecommerce.Catalog.Application.Products;

public sealed record ProductDto(
    Guid ProductId,
    Guid SellerId,
    string Name,
    decimal Price,
    string Description,
    string Size,
    string Color,
    string ProductType,
    int InStock);

