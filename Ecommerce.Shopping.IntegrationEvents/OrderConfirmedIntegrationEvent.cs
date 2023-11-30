using Ecommerce.BuildingBlocks.Application.EventBus;

namespace Ecommerce.Shopping.IntegrationEvents;

public sealed record OrderConfirmedIntegrationEvent(
    Guid IntegrationEventId,
    Guid ProductId,
    int AmountOfProducts,
    DateTime OcurredOn) : IntegrationEvent(IntegrationEventId, OcurredOn);
