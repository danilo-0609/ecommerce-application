using Ecommerce.Catalog.Domain.Products;
using Ecommerce.Catalog.Domain.Products.Events;
using Ecommerce.Catalog.Domain.Products.ValueObjects;
using ErrorOr;
using Xunit.Abstractions;

namespace Ecommerce.Catalog.Tests.UnitTests.Domain.Products;

public sealed class ProductTests
{
    private readonly decimal _priceZero = 0m;
    private readonly decimal _expensivePrice = 110000000m;
    private readonly ITestOutputHelper _testOutputHelper;

    public ProductTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void Publish_Should_ReturnAnError_WhenPriceIsZero()
    {

        ErrorOr<Product> product = Product.Publish(
            Guid.NewGuid(),
            "Product name",
            _priceZero,
            ProductType.Shoes,
            1,
            DateTime.UtcNow);

        Assert.True(product.IsError);
    }

    [Fact]
    public void Publish_Should_ReturnAnError_WhenPriceIsTooExpensive()
    {
        ErrorOr<Product> product = Product.Publish(
            Guid.NewGuid(),
            "Product name",
            _expensivePrice,
            ProductType.Shoes,
            10,
            DateTime.UtcNow);

        Assert.True(product.IsError);
    }

    [Fact]
    public void Publish_Should_ReturnAnError_WhenInStockIsEmpty()
    {
        ErrorOr<Product> product = Product.Publish(
            Guid.NewGuid(),
            "Product name",
            10000m,
            ProductType.Shoes,
            0,
            DateTime.UtcNow);

        Assert.True(product.IsError);
    }

    [Fact]
    public void Publish_Should_RaiseProductCreatedEvent_WhenSucced()
    {
        ErrorOr<Product> product = Product.Publish(
            Guid.NewGuid(),
            "Product name",
            100m,
            ProductType.Shoes,
            15,
            DateTime.UtcNow);

        bool raiseDomainEvents = product.Value.DomainEvents
            .Any(d => d.GetType() == typeof(ProductPublishedDomainEvent));

        Assert.True(raiseDomainEvents);
    }

    [Fact]
    public void Update_Should_ReturnAnError_WhenPriceIsZero()
    {
        ErrorOr<Product> product = Product.Update(
            ProductId.CreateUnique(),
            Guid.NewGuid(),
            "Product name",
            _priceZero,
            ProductType.Shoes,
            100,
            DateTime.UtcNow,
            DateTime.Now);

        Assert.True(product.IsError);
    }

    [Fact]
    public void Update_Should_ReturnAnError_WhenPriceIsTooExpensive()
    {
        ErrorOr<Product> product = Product.Update(
            ProductId.CreateUnique(),
            Guid.NewGuid(),
            "Product name",
            _expensivePrice,
            ProductType.Shoes,
            100,
            DateTime.UtcNow,
            DateTime.Now);

        Assert.True(product.IsError);
    }

    [Fact]
    public void Update_Should_RaiseProductUpdateEvent()
    {
        ErrorOr<Product> product = Product.Update(
            ProductId.CreateUnique(),
            Guid.NewGuid(),
            "Product name",
            100m,
            ProductType.Shoes,
            230,
            DateTime.UtcNow,
            DateTime.Now);

        bool raiseDomainEvents = product.Value.DomainEvents
            .Any(d => d.GetType() == typeof(ProductUpdatedDomainEvent));

        Assert.True(raiseDomainEvents);
    }

    [Fact]
    public void Remove_Should_RaiseProductRemovedEvent()
    {
        ErrorOr<Product> product = Product.Publish(
            Guid.NewGuid(),
            "Product name",
            100m,
            ProductType.Shoes,
            10,
            DateTime.UtcNow);

        product.Value.Remove();

        bool raiseDomainEvents = product.Value.DomainEvents
            .Any(j => j.GetType() == typeof(ProductRemovedDomainEvent));

        Assert.True(raiseDomainEvents);
    }

    [Fact]
    public void OutOfStock_Should_ReturnAnError_WhenProductHasStock()
    {
        ErrorOr<Product> product = Product.Publish(
            Guid.NewGuid(),
            "Product name",
            10000,
            ProductType.Shoes,
            10,
            DateTime.UtcNow);

        var outOfStock = product.Value.OutOfStock();

        Assert.True(outOfStock.IsError);
    }
}
