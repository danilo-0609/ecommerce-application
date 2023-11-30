using Ecommerce.BuildingBlocks.Application.Commands;
using ErrorOr;

namespace Ecommerce.Catalog.Application.Products.ModifyPostProduct.ModifySize;

public sealed record ModifySizeCommand(
    Guid ProductId,
    string Size) : IQueryRequest<ErrorOr<Guid>>;
