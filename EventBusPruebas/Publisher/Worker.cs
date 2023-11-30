using Integration;
using MassTransit;
using Microsoft.Extensions.Hosting;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Xml.Linq;

namespace Publisher;

public sealed class Worker : BackgroundService
{
    private readonly IBus _bus;

    public Worker(IBus bus)
    {
        _bus = bus;
    }

    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while(!stoppingToken.IsCancellationRequested)
        {
            var pato = new Publisher("pato", "pato pato pato");

            await _bus.Publish(new PublisherObjectCreatedIntegrationEvent(
                Guid.NewGuid(),
                JsonSerializer.Serialize<Publisher>(pato),
                DateTime.UtcNow));

            await Task.Delay(20000, stoppingToken);
        }
    }
}
