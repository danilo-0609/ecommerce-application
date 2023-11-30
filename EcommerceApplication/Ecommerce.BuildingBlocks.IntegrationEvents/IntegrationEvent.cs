using MediatR;

namespace Ecommerce.BuildingBlocks.IntegrationEvents;

public record IntegrationEvent(
    Guid IntegrationEventId,
    DateTime OcurredOn) : INotification;
