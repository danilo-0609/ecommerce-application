using Ecommerce.BuildingBlocks.Application.Commands;
using ErrorOr;
using MediatR;

namespace Ecommerce.Catalog.Application.Ratings.DeleteRating;

public sealed record DeleteRatingCommand(
    Guid ProductId,
    Guid RatingId) : IQueryRequest<ErrorOr<Unit>>;
