using Ecommerce.BuildingBlocks.Domain;

namespace Ecommerce.Catalog.Domain.Products.Events;

public sealed record ProductRemovedDomainEvent(Guid DomainEventId, 
    ProductId ProductId) : IDomainEvent;
