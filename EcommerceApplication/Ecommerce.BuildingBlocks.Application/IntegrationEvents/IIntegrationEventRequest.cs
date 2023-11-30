using MediatR;

namespace Ecommerce.BuildingBlocks.Application.IntegrationEvents;

public interface IIntegrationEventRequest<out TResponse> : IRequest<TResponse>
{
}
