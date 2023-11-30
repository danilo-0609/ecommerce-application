using Ecommerce.BuildingBlocks.Application.Commands;
using ErrorOr;

namespace Ecommerce.Catalog.Application.Ratings.RatingProduct;

public sealed record AddRatingProductCommand(
    double RatingValue,
    Guid ProductId,
    string? RatingComment) : IQueryRequest<ErrorOr<Guid>>;
