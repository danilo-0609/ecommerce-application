using Ecommerce.BuildingBlocks.Application.Commands;
using ErrorOr;

namespace Ecommerce.Catalog.Application.Products.ModifyPostProduct.ModifyDescription;

public sealed record ModifyDescriptionCommand(
    Guid ProductId,
    string Description) : IQueryRequest<ErrorOr<Guid>>;
