namespace IntegrationEvents;

public sealed record HelloWorldIntegrationEvent(
    Guid IntegrationDomainEventId,
    string content,
    DateTime OcurredOn);
