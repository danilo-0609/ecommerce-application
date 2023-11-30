using Ecommerce.BuildingBlocks.Application.EventBus;

namespace Ecommerce.Catalog.IntegrationEvents;

public sealed record ProductUpdatedIntegrationEvent(
    Guid IntegrationEventId,
    Guid ProductId,
    DateTime OcurredOn) : IntegrationEvent(IntegrationEventId, OcurredOn);
