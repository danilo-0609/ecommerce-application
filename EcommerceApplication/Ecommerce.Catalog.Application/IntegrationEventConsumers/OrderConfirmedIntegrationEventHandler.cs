using MassTransit;
using Ecommerce.Shopping.IntegrationEvents;
using Ecommerce.Catalog.Domain.Products;

namespace Ecommerce.Catalog.Application.IntegrationEventConsumers;

public sealed class OrderConfirmedIntegrationEventHandler : IConsumer<OrderConfirmedIntegrationEvent>
{
    private readonly IProductRepository _productRepository;

    public OrderConfirmedIntegrationEventHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task Consume(ConsumeContext<OrderConfirmedIntegrationEvent> context)
    {
        Product? product = await _productRepository
            .GetProductById(ProductId.Create(context.Message.ProductId));
    
        if (product!.InStock == 0)
        {
            product.OutOfStock();
        }

        product.Sell(context.Message.AmountOfProducts);
    }
}
