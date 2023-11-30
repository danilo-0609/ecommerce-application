using Ecommerce.BuildingBlocks.Domain;
using MediatR;

namespace Ecommerce.BuildingBlocks.Application.Events;

public interface IDomainEventHandler<in TEventNotification> : INotificationHandler<TEventNotification>
    where TEventNotification : IDomainEvent
{
}
