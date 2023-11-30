using Ecommerce.BuildingBlocks.Domain;
using Ecommerce.UserAccess.Application.Data;
using Ecommerce.UserAccess.Domain.Common;
using Ecommerce.UserAccess.Domain.UserRegistrations;
using Ecommerce.UserAccess.Domain.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Ecommerce.UserAccess.Infrastructure.Persistence;

internal sealed class ApplicationDbContext : DbContext, IApplicationDbContext, IUnitOfWork
{
    private readonly IPublisher _publisher;
    private readonly IConfiguration _configuration;

    public ApplicationDbContext(IPublisher publisher, 
        IConfiguration configuration)
    {
        _publisher = publisher;
        _configuration = configuration;
    }

    public DbSet<User> Users { get; set; }
    
    public DbSet<UserRegistration> UserRegistrations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseSqlServer(_configuration.GetConnectionString("Database"));
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        List<IDomainEvent> domainEvents = ChangeTracker
            .Entries<IHasDomainEvents>()
            .Select(m => m.Entity)
            .Where(m => m.DomainEvents.Any())
            .SelectMany(m => m.DomainEvents)
            .ToList();

        int result = await base.SaveChangesAsync(cancellationToken);

        foreach (IDomainEvent domainEvent in domainEvents)
        {
            await _publisher.Publish(domainEvent, cancellationToken);
        }

        return result;
    }
}
