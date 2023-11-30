using Ecommerce.BuildingBlocks.Domain;

namespace Ecommerce.UserAccess.Domain.UserRegistrations.Events;

public sealed record NewUserRegisteredDomainEvent(Guid DomainEventId,
    UserRegistrationId UserRegistrationId,
    string Login,
    string Email,
    string FirstName,
    string LastName,
    string Name,
    DateTime RegisterDate) : IDomainEvent;
