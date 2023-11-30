using Ecommerce.BuildingBlocks.Domain;
using Ecommerce.Catalog.Domain.Products;
using Ecommerce.Catalog.Domain.Sales.Events;

namespace Ecommerce.Catalog.Domain.Sales;

public sealed class Sale : AggregateRoot<SaleId, Guid>
{
    public SaleId SaleId { get; private set; }
    
    public ProductId ProductId { get; private set; }

    public decimal Price { get; private set; }

    public Guid UserId { get; private set; }

    public DateTime CreatedDateTime { get; private set; }


    public static Sale Generate(
        ProductId productId,
        decimal price,
        Guid userId,
        DateTime ocurredOn)
    {
        Sale sale = new Sale(
            SaleId.CreateUnique(),
            productId,
            price,
            userId,
            ocurredOn);

        sale.AddDomainEvent(new SaleGeneratedDomainEvent(Guid.NewGuid(),
            sale.ProductId,
            sale.UserId,
            sale.CreatedDateTime));

        return sale;
    }

    private Sale(
        SaleId saleId, 
        ProductId product, 
        decimal price, 
        Guid userId, 
        DateTime createdDateTime)
    {
        SaleId = saleId;
        ProductId = product;
        Price = price;
        UserId = userId;
        CreatedDateTime = createdDateTime;
    }
}
