using Ecommerce.BuildingBlocks.Application.Commands;
using ErrorOr;

namespace Ecommerce.Catalog.Application.Products.ModifyPostProduct.ModifyName;

public sealed record ModifyNameCommand(
    Guid ProductId,
    string Name) : IQueryRequest<ErrorOr<Guid>>;
