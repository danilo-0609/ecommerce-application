using Ecommerce.BuildingBlocks.Application.Queries;
using ErrorOr;

namespace Ecommerce.Catalog.Application.Products.GetAllProductsBySellerPosted;

public sealed record GetAllProductsPostedBySellerQuery(Guid SellerId) : IQueryRequest<ErrorOr<IReadOnlyList<ProductDto>>>;
