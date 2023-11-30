using Integration;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Consumidor;

public class PatoConsumer : IConsumer<PublisherObjectCreatedIntegrationEvent>
{
    private readonly ILogger<PatoConsumer> _logger;

    public PatoConsumer(ILogger<PatoConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<PublisherObjectCreatedIntegrationEvent> context)
    {
        _logger.LogInformation($"Consuming: {context.Message.Content}, " +
            $"OnUtc: {context.Message.OcurredOn}");

        return Task.CompletedTask;
    }
}