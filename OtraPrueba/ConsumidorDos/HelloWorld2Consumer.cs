using IntegrationEvents;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace ConsumidorDos;

public class HelloWorld2Consumer : IConsumer<HelloWorldIntegrationEvent>
{
    private readonly ILogger<HelloWorld2Consumer> _logger;

    public HelloWorld2Consumer(ILogger<HelloWorld2Consumer> logger)
    {
        _logger = logger;
    }

    public Task Consume(ConsumeContext<HelloWorldIntegrationEvent> context)
    {
        _logger.LogInformation($"Consuming 2: Hello world! {context.Message.content}, {context.Message.OcurredOn}");

        return Task.CompletedTask;
    }
}