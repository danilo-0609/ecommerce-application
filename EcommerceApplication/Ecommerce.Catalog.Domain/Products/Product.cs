using Ecommerce.BuildingBlocks.Domain;
using Ecommerce.Catalog.Domain.Products.Events;
using Ecommerce.Catalog.Domain.Products.Rules;
using Ecommerce.Catalog.Domain.Products.ValueObjects;
using ErrorOr;
using MediatR;

namespace Ecommerce.Catalog.Domain.Products;

public sealed class Product : AggregateRoot<ProductId, Guid>
{
    public ProductId ProductId { get; private set; }

    public Guid SellerId { get; private set; }

    public string Name { get; private set; }

    public Price Price { get; private set; }

    public string Description { get; private set; }

    public string Size { get; private set; } = string.Empty;

    public string Color { get; private set; } = string.Empty;

    public ProductType ProductType { get; private set; }

    public int InStock { get; private set; }

    public bool IsActive { get; private set; }

    public DateTime CreatedDateTime { get; private set; }

    public DateTime? UpdatedDateTime { get; private set; }

    public DateTime? ExpireDateTime { get; private set; }

    public static ErrorOr<Product> Publish(Guid sellerId,
            string name,
            decimal priceValue,
            ProductType productType,
            int inStock,
            DateTime createdOnUtcNow,
            string description = "",
            string size = "",
            string color = "")
    {

        var price = Price.Create(priceValue);
        
        if (price.IsError)
        {
            return price.FirstError;
        }

        var productId = ProductId.CreateUnique();
        
        var product = new Product(productId,
            sellerId,
            name,
            price.Value,
            description,
            size,
            color,
            productType,
            inStock,
            createdOnUtcNow);

        var checkRule = product.CheckRule(new ProductStockCannotBeZeroWhenPublishRule(inStock));

        if (checkRule.IsError)
        {
            return checkRule.FirstError;
        }

        product.AddDomainEvent(new ProductPublishedDomainEvent(
            Guid.NewGuid(), 
            product.ProductId,
            product.SellerId,
            product.Name,
            product.Description,
            product.Price.Value,
            product.InStock,
            product.Size,
            product.ProductType.Value));

        return product;
    }

    public static ErrorOr<Product> Update(ProductId productId,
            Guid sellerId,
            string name,
            decimal priceValue,
            ProductType productType,
            int inStock,
            DateTime createdOnUtcTime,
            DateTime updatedOnUtcTime,
            string description = "",
            string size = "",
            string color = "")
    {
        var price = Price.Create(priceValue);
       
        if (price.IsError)
        {
            return price.FirstError;
        }
        
        var product = new Product(productId,
                sellerId,
                name,
                price.Value,
                description,
                size,
                color,
                productType,
                inStock,
                createdOnUtcTime,
                updatedOnUtcTime);

        var checkRule = product.CheckRule(new ProductStockCannotBeZeroWhenPublishRule(inStock));

        if (checkRule.IsError)
        {
            return checkRule.FirstError;
        }


        product.AddDomainEvent(new ProductUpdatedDomainEvent(Guid.NewGuid(), productId));

        return product;
    }

    public void Remove()
    {
        ProductRemovedDomainEvent productRemovedEvent = new(Guid.NewGuid(), ProductId);

        ExpireDateTime = DateTime.UtcNow;
        IsActive = false;

        AddDomainEvent(productRemovedEvent);
    }

    public ErrorOr<Unit> OutOfStock()
    {
        if (InStock == 0)
        {
            AddDomainEvent(new ProductOutOfStockDomainEvent(Guid.NewGuid(),
            ProductId,
            DateTime.UtcNow));

            return Unit.Value;
        }

        return Error.Validation("Product.HasStock", "Product is not out of stock");
    }

    public ErrorOr<Unit> Sell(int amountOfProducts) 
    {
        var checkRule = CheckRule(new ProductCannotBeSoldWhenProductIsOutOfStock(InStock));

        if (checkRule.IsError)
        {
            return checkRule.FirstError;
        }

        InStock = InStock - amountOfProducts;

        AddDomainEvent(new ProductSoldDomainEvent(Guid.NewGuid(), ProductId, SellerId, DateTime.UtcNow));

        return Unit.Value;
    }

    private Product(ProductId id,
        Guid sellerId,
        string name,
        Price price,
        string description,
        string size,
        string color,
        ProductType productType,
        int inStock,
        DateTime createdDateTime,
        DateTime? updatedDateTime = null,
        DateTime? expiredDateTime = null)
            : base(id)
    {
        ProductId = id;
        SellerId = sellerId;
        Name = name;
        Price = price;
        Description = description;
        Size = size;
        Color = color;
        ProductType = productType;
        InStock = inStock;

        IsActive = true;

        CreatedDateTime = createdDateTime;
        UpdatedDateTime = updatedDateTime;
        ExpireDateTime = expiredDateTime;
    }

    private Product() { }
}