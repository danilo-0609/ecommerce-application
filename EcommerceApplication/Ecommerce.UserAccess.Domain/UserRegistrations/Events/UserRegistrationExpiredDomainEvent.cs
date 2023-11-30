using Ecommerce.BuildingBlocks.Domain;

namespace Ecommerce.UserAccess.Domain.UserRegistrations.Events;

public sealed record UserRegistrationExpiredDomainEvent(
    Guid DomainEventId,
    UserRegistrationId UserRegistrationId) : IDomainEvent;
