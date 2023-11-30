using Ecommerce.BuildingBlocks.Domain;

namespace Ecommerce.Catalog.Domain.Products.Events;

public sealed record ProductOutOfStockDomainEvent(
    Guid DomainEventId,
    ProductId ProductId,
    DateTime OcurredOn) : IDomainEvent;
