using Ecommerce.BuildingBlocks.Domain;

namespace Ecommerce.Catalog.Domain.Products.Events;

public sealed record ProductSoldDomainEvent(
    Guid DomainEventId,
    ProductId ProductId,
    Guid UserId,
    DateTime OcurredOn) : IDomainEvent;
