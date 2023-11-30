using Ecommerce.BuildingBlocks.Application.Queries;
using ErrorOr;

namespace Ecommerce.Catalog.Application.Products.GetProductsByName;

public sealed record GetProductsByNameQuery(string ProductName) : IQueryRequest<ErrorOr<IReadOnlyList<ProductDto>>>;    
