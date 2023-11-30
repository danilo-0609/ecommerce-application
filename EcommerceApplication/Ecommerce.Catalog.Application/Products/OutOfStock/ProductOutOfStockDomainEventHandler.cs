using Ecommerce.BuildingBlocks.Application.EventBus;
using Ecommerce.BuildingBlocks.Application.Events;
using Ecommerce.Catalog.Domain.Products.Events;
using Ecommerce.Catalog.IntegrationEvents;
using MassTransit;

namespace Ecommerce.Catalog.Application.Products.OutOfStock;

internal sealed class ProductOutOfStockDomainEventHandler : IDomainEventHandler<ProductOutOfStockDomainEvent>
{
    private readonly IEventBus _bus;

    public ProductOutOfStockDomainEventHandler(IEventBus bus)
    {
        _bus = bus;
    }

    public async Task Handle(ProductOutOfStockDomainEvent notification, CancellationToken cancellationToken)
    {
        await _bus.PublishAsync(new ProductOutOfStockIntegrationEvent(
            notification.DomainEventId,
            notification.ProductId.Value,
            notification.OcurredOn));
    }
}
