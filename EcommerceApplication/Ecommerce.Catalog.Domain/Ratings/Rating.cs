using Ecommerce.BuildingBlocks.Domain;
using Ecommerce.Catalog.Domain.Common;
using Ecommerce.Catalog.Domain.Products;
using Ecommerce.Catalog.Domain.Ratings.Events;

namespace Ecommerce.Catalog.Domain.Ratings;

public sealed class Rating : AggregateRoot<RatingId, Guid>
{
    public RatingId RatingId { get; private set; }

    public string? RatingComment { get; private set; }

    public UserId UserId { get; private set; }

    public ProductId ProductId { get; private set; }

    public double RatingValue { get; private set; }

    public DateTime CreatedDateTime { get; private set; }

    public DateTime? UpdatedDateTime { get; private set; }

    public static Rating Create(double ratingValue, 
        UserId userId,
        ProductId productId,
        DateTime createdOnUtcTime,
        string? ratingComment = null)
    {
        var ratingId = RatingId.CreateUnique();

        var rating = new Rating(ratingId, 
            ratingComment, 
            userId, 
            ratingValue,
            productId,
            createdOnUtcTime);

        return rating;
    }

    public static Rating Update(Guid id,
        double ratingValue,
        UserId userId,
        ProductId productId,
        DateTime createdOnUtcTime,
        DateTime updatedOnUtcTime,
        string? ratingComment = null)
    {
        var ratingId = RatingId.Create(id);

        var rating = new Rating(ratingId, 
            ratingComment, 
            userId,
            ratingValue,
            productId,
            createdOnUtcTime, 
            updatedOnUtcTime);

        return rating;
    }

    private Rating(RatingId id, 
        string? ratingComment, 
        UserId userId, 
        double ratingValue,
        ProductId productId,
        DateTime createdDateTime,
        DateTime? updatedDateTime = null) 
        : base(id)
    {
        RatingId = id;
        RatingComment = ratingComment;

        UserId = userId;
        ProductId = productId;
        RatingValue = ratingValue;

        CreatedDateTime = createdDateTime;
        UpdatedDateTime = updatedDateTime;
    }

    private Rating() { }
}
