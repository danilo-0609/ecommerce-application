using IntegrationEvents;
using MassTransit;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Publicador;

public sealed class HelloWorldService
{
    private readonly IBus _bus;
    private readonly ILogger<HelloWorldService> _logger;

    public HelloWorldService(IBus bus, ILogger<HelloWorldService> logger)
    {
        _bus = bus;
        _logger = logger;
    }

    public async Task<HelloWorld> Create()
    {
        var helloWorld = new HelloWorld("Danilo");

        _logger.LogInformation("Publishing...");

        /*await _bus.Publish(new HelloWorldIntegrationEvent(
            Guid.NewGuid(),
            helloWorldS,
            DateTime.Now));
        */


        return helloWorld;
    }

}
