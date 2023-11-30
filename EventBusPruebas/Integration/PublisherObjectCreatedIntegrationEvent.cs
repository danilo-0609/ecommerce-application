namespace Integration;

public sealed record PublisherObjectCreatedIntegrationEvent(
    Guid IntegrationEventId,
    string Content,
    DateTime OcurredOn);