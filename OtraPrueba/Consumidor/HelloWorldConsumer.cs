using IntegrationEvents;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Consumidor;

public sealed class HelloWorldConsumer : IConsumer<HelloWorldIntegrationEvent>
{
    private readonly ILogger<HelloWorldConsumer> _logger;

    public HelloWorldConsumer(ILogger<HelloWorldConsumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<HelloWorldIntegrationEvent> context)
    {
        _logger.LogInformation($"Consuming: Hello world! {context.Message.content}, {context.Message.OcurredOn}");

        return Task.CompletedTask;
    }
}