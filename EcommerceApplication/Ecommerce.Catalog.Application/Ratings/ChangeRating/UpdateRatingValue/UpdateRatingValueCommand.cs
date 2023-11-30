using Ecommerce.BuildingBlocks.Application.Commands;
using ErrorOr;

namespace Ecommerce.Catalog.Application.Ratings.ChangeRating.UpdateRatingValue;

public sealed record UpdateRatingValueCommand(
    double RatingValue,
    Guid ProductId,
    Guid RatingId) : IQueryRequest<ErrorOr<Guid>>;
