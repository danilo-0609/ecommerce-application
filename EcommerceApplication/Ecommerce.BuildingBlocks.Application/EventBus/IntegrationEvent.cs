using Ecommerce.BuildingBlocks.Domain;

namespace Ecommerce.BuildingBlocks.Application.EventBus;

public abstract record IntegrationEvent(
    Guid DomainEventId, 
    DateTime OcurredOn) : IDomainEvent;
