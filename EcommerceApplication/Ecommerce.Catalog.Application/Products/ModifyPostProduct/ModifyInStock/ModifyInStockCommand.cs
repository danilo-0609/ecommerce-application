using Ecommerce.BuildingBlocks.Application.Commands;
using ErrorOr;

namespace Ecommerce.Catalog.Application.Products.ModifyPostProduct.ModifyInStock;

public sealed record ModifyInStockCommand(
    Guid ProductId,
    int InStock) : IQueryRequest<ErrorOr<Guid>>;
