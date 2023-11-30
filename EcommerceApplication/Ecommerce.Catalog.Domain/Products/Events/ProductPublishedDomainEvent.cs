using Ecommerce.BuildingBlocks.Domain;

namespace Ecommerce.Catalog.Domain.Products.Events;

public sealed record ProductPublishedDomainEvent(
    Guid DomainEventId, 
    ProductId ProductId,
    Guid SellerId,
    string Name,
    string Description,
    decimal Price,
    int InStock,
    string Size,
    string ProductType) : IDomainEvent;
