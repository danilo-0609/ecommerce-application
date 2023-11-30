using Ecommerce.BuildingBlocks.Application.Commands;
using ErrorOr;

namespace Ecommerce.Catalog.Application.Products.PostProduct;

public sealed record PostProductCommand(
    string Name,
    decimal Price,
    string Description,
    int inStock,
    string Size,
    string Color,
    string ProductType) : IQueryRequest<ErrorOr<Guid>>;
