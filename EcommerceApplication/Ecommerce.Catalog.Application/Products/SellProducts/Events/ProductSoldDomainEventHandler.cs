using Ecommerce.BuildingBlocks.Application.EventBus;
using Ecommerce.BuildingBlocks.Application.Events;
using Ecommerce.Catalog.Domain.Products;
using Ecommerce.Catalog.Domain.Products.Events;
using Ecommerce.Catalog.IntegrationEvents;
using MediatR;

namespace Ecommerce.Catalog.Application.Products.SellProducts.Events;

internal class ProductSoldDomainEventHandler : IDomainEventHandler<ProductSoldDomainEvent>
{
    private readonly IProductRepository _productRepository;
    private readonly IEventBus _bus;

    public ProductSoldDomainEventHandler(IProductRepository productRepository, IEventBus bus)
    {
        _productRepository = productRepository;
        _bus = bus;
    }

    public async Task Handle(ProductSoldDomainEvent notification, CancellationToken cancellationToken)
    {
        Product? product = await _productRepository.GetProductById(notification.ProductId);

        if (product!.InStock == 0)
        {
            product.OutOfStock();
        }

        await _bus.PublishAsync(new ProductSoldIntegrationEvent(
            notification.DomainEventId,
            notification.ProductId.Value,
            notification.OcurredOn));
    }
}
