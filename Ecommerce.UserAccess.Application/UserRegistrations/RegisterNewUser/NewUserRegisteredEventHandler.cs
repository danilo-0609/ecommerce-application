using Ecommerce.BuildingBlocks.Application.EventBus;
using Ecommerce.BuildingBlocks.Application.Events;
using Ecommerce.UserAccess.Domain.UserRegistrations.Events;
using Ecommerce.UserAccess.IntegrationEvents;
using MassTransit;

namespace Ecommerce.UserAccess.Application.UserRegistrations.RegisterNewUser;

internal sealed class NewUserRegisteredEventHandler : IDomainEventHandler<NewUserRegisteredDomainEvent>
{
    private readonly IEventBus _eventBus;

    public NewUserRegisteredEventHandler(IEventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public async Task Handle(NewUserRegisteredDomainEvent notification, CancellationToken cancellationToken)
    {
        NewUserRegisteredIntegrationEvent newUserRegisteredIntegrationEvent = new(
            notification.DomainEventId,
            notification.UserRegistrationId.Value,
            notification.Name,
            notification.Email,
            notification.FirstName,
            notification.LastName,
            notification.Name,
            notification.RegisterDate);

        await _eventBus.PublishAsync(newUserRegisteredIntegrationEvent);
    }
}
