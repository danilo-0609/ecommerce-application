using Ecommerce.BuildingBlocks.Domain;

namespace Ecommerce.Catalog.Domain.Sales;

public sealed record SaleId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

    public static SaleId Create(Guid value) => new SaleId(value);

    public static SaleId CreateUnique() => new SaleId(Guid.NewGuid());

    public SaleId(Guid value)
    {
        Value = value;
    }
}
