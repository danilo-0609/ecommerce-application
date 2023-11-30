using Ecommerce.BuildingBlocks.Application.EventBus;

namespace Ecommerce.Catalog.IntegrationEvents;

public sealed record ProductSoldIntegrationEvent(
    Guid IntegrationEventId,
    Guid ProductId,
    DateTime OcurredOn) : IntegrationEvent(IntegrationEventId, OcurredOn);
