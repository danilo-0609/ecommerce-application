using Ecommerce.BuildingBlocks.Application.EventBus;

namespace Ecommerce.UserAccess.IntegrationEvents;

public sealed record NewUserRegisteredIntegrationEvent(
    Guid IntegrationEventId,
    Guid UserRegistrationId,
    string Login,
    string Email,
    string FirstName,
    string LastName,
    string Name,
    DateTime CreatedOnUtc) : IntegrationEvent(IntegrationEventId, CreatedOnUtc);