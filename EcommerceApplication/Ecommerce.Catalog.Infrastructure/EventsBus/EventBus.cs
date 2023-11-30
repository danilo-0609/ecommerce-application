using Ecommerce.BuildingBlocks.Application.EventBus;
using MassTransit;

namespace Ecommerce.Catalog.Infrastructure.EventsBus;

internal sealed class EventBus : IEventBus
{
    private readonly IPublishEndpoint _publishEndpoint;

    public EventBus(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public async Task PublishAsync<T>(T @event) 
        where T : IntegrationEvent
    {
        await _publishEndpoint.Publish(@event);
    }
}
