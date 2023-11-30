using Ecommerce.BuildingBlocks.Domain;
using Ecommerce.Catalog.Infrastructure.Persistence;
using Ecommerce.Catalog.Infrastructure.Persistence.Outbox;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Quartz;

namespace Ecommerce.Catalog.Infrastructure.BackgroundJobs;

[DisallowConcurrentExecution]
internal sealed class ProcessOutboxMessageJob : IJob
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IBus _bus;
    private readonly ILogger<ProcessOutboxMessageJob> _logger;

    public ProcessOutboxMessageJob(ApplicationDbContext context,
        IBus bus, 
        ILogger<ProcessOutboxMessageJob> logger)
    {
        _dbContext = context;
        _bus = bus;
        _logger = logger;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        List<OutboxMessage> messages = await _dbContext
            .Set<OutboxMessage>()
            .Where(m => m.ProcessedOnUtc == null)
            .Take(20)
            .ToListAsync();

        foreach (OutboxMessage message in messages)
        {
            IDomainEvent domainEvent = JsonConvert
                .DeserializeObject<IDomainEvent>(message.Content);

            try
            {
                await _bus.Publish(domainEvent);
            }

            catch (Exception ex)
            {
                _logger.LogError($"Publishing error: {ex} ");    
            }

            message.ProcessedOnUtc = DateTime.UtcNow;
        }

        await _dbContext.SaveChangesAsync();
    }
}
