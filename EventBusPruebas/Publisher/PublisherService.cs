using Integration;
using MassTransit;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace Publisher;

public sealed class PublisherService
{
    private readonly IBus _bus;
    private readonly ILogger<PublisherService> _logger;

    public PublisherService(IBus bus, ILogger<PublisherService> logger)
    {
        _bus = bus;
        _logger = logger;
    }

    public async Task<Publisher> Create(string name, string description)
    {
        var pato = new Publisher(name, description);

        _logger.LogInformation("Publishing a new fucking pato");

        await _bus.Publish(new PublisherObjectCreatedIntegrationEvent(
            Guid.NewGuid(),
            JsonSerializer.Serialize<Publisher>(pato), 
            DateTime.UtcNow));

        return pato;
    }
}
