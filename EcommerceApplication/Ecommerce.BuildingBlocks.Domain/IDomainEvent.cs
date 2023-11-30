using MediatR;

namespace Ecommerce.BuildingBlocks.Domain;

public interface IDomainEvent : INotification
{
    Guid DomainEventId { get; }
}
