namespace Ecommerce.BuildingBlocks.Application.EventBus;

public interface IEventBus
{
    Task PublishAsync<T>(T @event)
        where T : IntegrationEvent;
}
