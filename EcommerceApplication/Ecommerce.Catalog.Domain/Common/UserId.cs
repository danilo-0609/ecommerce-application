using Ecommerce.BuildingBlocks.Domain;

namespace Ecommerce.Catalog.Domain.Common;

public sealed record UserId : EntityId<Guid>
{
    public override Guid Value { get; protected set; }

    public UserId(Guid id)
    {
        Value = id;
    }

    private UserId()
    {

    }
}
