namespace Ecommerce.Catalog.Application.Ratings;

public sealed record RatingDto(
    Guid RatingId,
    string? Comment,
    Guid UserId,
    Guid ProductId,
    double RatingValue,
    DateTime CreatedDateTime);
