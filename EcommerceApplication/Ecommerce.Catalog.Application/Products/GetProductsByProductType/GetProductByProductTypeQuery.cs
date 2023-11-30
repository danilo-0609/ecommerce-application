using Ecommerce.BuildingBlocks.Application.Queries;
using ErrorOr;

namespace Ecommerce.Catalog.Application.Products.GetProductsByProductType;

public sealed record GetProductByProductTypeQuery(string ProductType) : IQueryRequest<ErrorOr<IReadOnlyList<ProductDto>>>;
