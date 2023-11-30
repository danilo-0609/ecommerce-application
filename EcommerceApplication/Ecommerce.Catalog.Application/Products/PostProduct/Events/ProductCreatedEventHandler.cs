using Ecommerce.BuildingBlocks.Application.EventBus;
using Ecommerce.BuildingBlocks.Application.Events;
using Ecommerce.Catalog.Domain.Products.Events;
using Ecommerce.Catalog.IntegrationEvents;
using MassTransit;
using MediatR.Wrappers;

namespace Ecommerce.Catalog.Application.Products.PostProduct.Events;

internal sealed class ProductCreatedEventHandler : IDomainEventHandler<ProductPublishedDomainEvent>
{
    private readonly IEventBus _eventBus;

    internal ProductCreatedEventHandler(IEventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public async Task Handle(ProductPublishedDomainEvent notification, CancellationToken cancellationToken)
    {
        await _eventBus.PublishAsync(new ProductPublishedIntegrationEvent(
            notification.DomainEventId,
            notification.ProductId.Value,
            notification.SellerId,
            notification.Name,
            notification.Description,
            notification.Price,
            notification.InStock,
            notification.Size,
            notification.ProductType,
            DateTime.UtcNow));
    }
}
