using Ecommerce.BuildingBlocks.Domain;

namespace Ecommerce.Catalog.Domain.Products.Events;

public sealed record ProductUpdatedDomainEvent(
    Guid DomainEventId, 
    ProductId ProductId) : IDomainEvent;
