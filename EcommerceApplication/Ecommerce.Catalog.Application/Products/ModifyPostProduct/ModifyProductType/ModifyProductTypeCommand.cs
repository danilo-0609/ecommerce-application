using Ecommerce.BuildingBlocks.Application.Commands;
using ErrorOr;

namespace Ecommerce.Catalog.Application.Products.ModifyPostProduct.ModifyProductType;

public sealed record ModifyProductTypeCommand(
    Guid ProductId,
    string ProductType) : IQueryRequest<ErrorOr<Guid>>;
