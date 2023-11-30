using Ecommerce.BuildingBlocks.Domain;

namespace Ecommerce.UserAccess.Domain.UserRegistrations.Events;

public sealed record UserRegistrationConfirmedDomainEvent(
    Guid DomainEventId,
    UserRegistrationId UserRegistrationId) : IDomainEvent;
