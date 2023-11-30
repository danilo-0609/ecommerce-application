using Ecommerce.BuildingBlocks.Application.Commands;
using ErrorOr;
using MediatR;

namespace Ecommerce.Catalog.Application.Products.SellProducts;

internal sealed record SellProductsCommand(Guid ProductId, int AmountOfProducts) : IQueryRequest<ErrorOr<Unit>>;
