using Ecommerce.BuildingBlocks.Application.EventBus;
using Ecommerce.BuildingBlocks.Application.Events;
using Ecommerce.Catalog.Domain.Products.Events;
using Ecommerce.Catalog.IntegrationEvents;

namespace Ecommerce.Catalog.Application.Products.ModifyPostProduct.Events;

internal sealed class ProductUpdatedDomainEventHandler : IDomainEventHandler<ProductUpdatedDomainEvent>
{
    private readonly IEventBus _bus;

    internal ProductUpdatedDomainEventHandler(IEventBus bus)
    {
        _bus = bus;
    }

    public async Task Handle(ProductUpdatedDomainEvent notification, CancellationToken cancellationToken)
    {
        await _bus.PublishAsync(new ProductUpdatedIntegrationEvent(
            notification.DomainEventId, 
            notification.ProductId.Value, 
            DateTime.UtcNow));
    }
}
