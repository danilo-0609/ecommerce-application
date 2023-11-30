using Ecommerce.BuildingBlocks.Domain;
using System.Runtime.CompilerServices;

namespace Ecommerce.UserAccess.Domain.Users;

public sealed record UserId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

    public static UserId Create(Guid id) => new UserId(id);

    public static UserId CreateUnique() => new UserId(Guid.NewGuid());

    private UserId(Guid value)
    {
        Value = value;
    }

    private UserId() { }
}
