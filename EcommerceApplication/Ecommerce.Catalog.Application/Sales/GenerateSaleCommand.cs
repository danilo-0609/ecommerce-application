using Ecommerce.BuildingBlocks.Application.Commands;
using Ecommerce.Catalog.Domain.Products;
using MediatR;

namespace Ecommerce.Catalog.Application.Sales;

internal sealed record GenerateSaleCommand(
    ProductId ProductId,
    decimal Price,
    Guid UserId) : IQueryRequest<Unit>;
