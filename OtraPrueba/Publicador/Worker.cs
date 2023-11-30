using MassTransit;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Publicador;

public sealed class Worker : BackgroundService
{
    private readonly ILogger<HelloWorldService> _logger;
    private readonly IBus _bus;

    public Worker(ILogger<HelloWorldService> logger, IBus bus)
    {
        _logger = logger;
        _bus = bus;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while(!stoppingToken.IsCancellationRequested) 
        {
            _logger.LogInformation("Starting...");

            var service = new HelloWorldService(_bus, _logger);

            await service.Create();

            await Task.Delay(10000, stoppingToken);
        }
    }
}
