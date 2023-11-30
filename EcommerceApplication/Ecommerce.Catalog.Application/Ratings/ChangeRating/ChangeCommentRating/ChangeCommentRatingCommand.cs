using Ecommerce.BuildingBlocks.Application.Commands;
using ErrorOr;

namespace Ecommerce.Catalog.Application.Ratings.ChangeRating.ChangeCommentRating;

public sealed record ChangeCommentRatingCommand(
    Guid ProductId,
    Guid RatingId,
    string Comment) : IQueryRequest<ErrorOr<Guid>>;
