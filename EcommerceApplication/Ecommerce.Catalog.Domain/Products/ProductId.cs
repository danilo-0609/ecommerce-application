using Ecommerce.BuildingBlocks.Domain;

namespace Ecommerce.Catalog.Domain.Products;

public sealed record ProductId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

    private ProductId(Guid value)
        : base(value)
    {
        Value = value;
    }

    public static ProductId CreateUnique() => new ProductId(Guid.NewGuid());

    public static ProductId Create(Guid id)
    {
        return new ProductId(id);
    }
}
