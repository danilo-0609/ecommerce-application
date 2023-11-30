using Ecommerce.BuildingBlocks.Application.Commands;
using ErrorOr;
using MediatR;

namespace Ecommerce.Catalog.Application.Products.RemovePostProduct;

public sealed record RemovePostProductCommand(Guid ProductId) : IQueryRequest<ErrorOr<Unit>>;
