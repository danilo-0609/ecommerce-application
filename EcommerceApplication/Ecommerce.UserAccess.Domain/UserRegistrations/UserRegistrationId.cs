using Ecommerce.BuildingBlocks.Domain;

namespace Ecommerce.UserAccess.Domain.UserRegistrations;

public sealed record UserRegistrationId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

    public static UserRegistrationId Create(Guid id) => new UserRegistrationId(id); 

    public static UserRegistrationId CreateUnique() => new UserRegistrationId(Guid.NewGuid());
  
    private UserRegistrationId(Guid value)
    {
        Value = value;
    }

    private UserRegistrationId() { }
}
