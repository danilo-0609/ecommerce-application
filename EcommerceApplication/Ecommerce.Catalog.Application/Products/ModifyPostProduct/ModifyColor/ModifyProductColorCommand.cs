using Ecommerce.BuildingBlocks.Application.Commands;
using ErrorOr;

namespace Ecommerce.Catalog.Application.Products.ModifyPostProduct.ModifyColor;

public sealed record ModifyProductColorCommand(
    Guid ProductId,
    string Color) : IQueryRequest<ErrorOr<Guid>>;
