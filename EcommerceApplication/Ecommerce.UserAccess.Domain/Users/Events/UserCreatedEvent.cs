using Ecommerce.BuildingBlocks.Domain;

namespace Ecommerce.UserAccess.Domain.Users.Events;

public sealed record UserCreatedEvent(
    Guid DomainEventId,
    UserId UserId) : IDomainEvent;
