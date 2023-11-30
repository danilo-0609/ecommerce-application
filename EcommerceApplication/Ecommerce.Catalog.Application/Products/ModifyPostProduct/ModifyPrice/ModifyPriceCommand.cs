using Ecommerce.BuildingBlocks.Application.Commands;
using ErrorOr;

namespace Ecommerce.Catalog.Application.Products.ModifyPostProduct.ModifyPrice;

public sealed record ModifyPriceCommand(
    Guid ProductId,
    decimal Price) : IQueryRequest<ErrorOr<Guid>>;
