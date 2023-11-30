using Ecommerce.BuildingBlocks.Domain;

namespace Ecommerce.Catalog.Domain.Ratings;

public sealed record RatingId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

    private RatingId(Guid value)
    {
        Value = value;
    }

    public static RatingId Create(Guid value) => new RatingId(value);

    public static RatingId CreateUnique() => new RatingId(Guid.NewGuid());
}
