using Ecommerce.BuildingBlocks.Application.EventBus;
using Ecommerce.BuildingBlocks.Application.Events;
using Ecommerce.Catalog.Domain.Products.Events;
using Ecommerce.Catalog.IntegrationEvents;

namespace Ecommerce.Catalog.Application.Products.RemovePostProduct.Events;

internal sealed class ProductRemovedDomainEventHandler : IDomainEventHandler<ProductRemovedDomainEvent>
{
    private readonly IEventBus _bus;

    internal ProductRemovedDomainEventHandler(IEventBus bus)
    {
        _bus = bus;
    }

    public async Task Handle(ProductRemovedDomainEvent notification, CancellationToken cancellationToken)
    {
        await _bus.PublishAsync(new ProductRemovedIntegrationEvent
                    (notification.DomainEventId,
                    notification.ProductId.Value, 
                    DateTime.UtcNow));
    }
}
