using Ecommerce.BuildingBlocks.Application.EventBus;

namespace Ecommerce.Catalog.IntegrationEvents;

public sealed record ProductPublishedIntegrationEvent(
    Guid IntegrationEventId,
    Guid ProductId,
    Guid SellerId,
    string Name,
    string Description,
    decimal Price,
    int InStock,
    string Size,
    string ProductType,
    DateTime OcurredOn) : IntegrationEvent(IntegrationEventId, OcurredOn);
