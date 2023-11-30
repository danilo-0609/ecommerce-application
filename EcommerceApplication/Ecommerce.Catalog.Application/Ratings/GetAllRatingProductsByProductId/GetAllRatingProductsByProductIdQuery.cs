using Ecommerce.BuildingBlocks.Application.Queries;
using ErrorOr;

namespace Ecommerce.Catalog.Application.Ratings.GetAllRatingProductsByProductId;

public sealed record GetAllRatingProductsByProductIdQuery(
    Guid ProductId) : IQueryRequest<ErrorOr<IReadOnlyList<RatingDto>>>;
