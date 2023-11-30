using Ecommerce.BuildingBlocks.Domain;
using Ecommerce.Catalog.Application.Data;
using Ecommerce.Catalog.Domain.Comments;
using Ecommerce.Catalog.Domain.Common;
using Ecommerce.Catalog.Domain.Products;
using Ecommerce.Catalog.Domain.Ratings;
using Ecommerce.Catalog.Domain.Sales;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Ecommerce.Catalog.Infrastructure.Persistence;

public sealed class ApplicationDbContext : DbContext, IApplicationDbContext, IUnitOfWork
{
    private readonly IPublisher _publisher;
    private readonly IConfiguration _configuration;

    public ApplicationDbContext(IPublisher publisher, IConfiguration configuration)
    {
        _publisher = publisher;
        _configuration = configuration;
    }

    public DbSet<Product> Products { get; set; }

    public DbSet<Comment> Comments { get; set; }

    public DbSet<Sale> Sale { get; set; }

    public DbSet<Rating> Ratings { get; set; }

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
