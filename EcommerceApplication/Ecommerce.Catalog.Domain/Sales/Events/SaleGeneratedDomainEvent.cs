using Ecommerce.BuildingBlocks.Domain;
using Ecommerce.Catalog.Domain.Products;

namespace Ecommerce.Catalog.Domain.Sales.Events;

public sealed record SaleGeneratedDomainEvent(
    Guid DomainEventId,
    ProductId ProductId,
    Guid UserId,
    DateTime OcurredOn) : IDomainEvent;
