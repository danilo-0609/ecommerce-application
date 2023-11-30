using Ecommerce.BuildingBlocks.Application.EventBus;
using Ecommerce.Catalog.Application.Data;
using Ecommerce.Catalog.Domain.Comments;
using Ecommerce.Catalog.Domain.Common;
using Ecommerce.Catalog.Domain.Products;
using Ecommerce.Catalog.Domain.Ratings;
using Ecommerce.Catalog.Domain.Sales;
using Ecommerce.Catalog.Infrastructure.BackgroundJobs;
using Ecommerce.Catalog.Infrastructure.EventsBus;
using Ecommerce.Catalog.Infrastructure.Persistence;
using Ecommerce.Catalog.Infrastructure.Persistence.Interceptors;
using Ecommerce.Catalog.Infrastructure.Persistence.Repositories;
using Ecommerce.Catalog.Infrastructure.Persistence.Repositories.Comments;
using Ecommerce.Catalog.Infrastructure.Persistence.Repositories.Products;
using Ecommerce.Catalog.Infrastructure.Persistence.Repositories.Ratings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace Ecommerce.Catalog.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfraestructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<ConvertDomainEventsToOutboxMessagesInterceptor>();

        services.AddDbContext<ApplicationDbContext>((sp, optionsBuilder) =>
        {
            var interceptor = sp.GetService<ConvertDomainEventsToOutboxMessagesInterceptor>();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("Database"))
                .AddInterceptors(interceptor);
        });

        services.AddQuartz(configure =>
        {
            var jobKey = new JobKey(nameof(ProcessOutboxMessageJob));

            configure.AddJob<ProcessOutboxMessageJob>(jobKey)
                .AddTrigger(
                    trigger =>
                        trigger.ForJob(jobKey)
                            .WithSimpleSchedule(
                                schedule =>
                                    schedule.WithIntervalInSeconds(10)
                                        .RepeatForever()));
        });

        services.AddQuartzHostedService();

        services.AddScoped<IApplicationDbContext>(sp =>
            sp.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<IUnitOfWork>(sp =>
            sp.GetRequiredService<ApplicationDbContext>());


        services.AddScoped<ICommentRepository, CommentRepository>();

        services.AddScoped<ICommentRepository, CommentRepository>();

        services.AddScoped<ISaleRepository, SaleRepository>();

        services.AddScoped<IProductRepository, ProductRepository>();

        services.AddScoped<IRatingRepository, RatingRepository>();

        services.AddTransient<IEventBus, EventBus>();

        return services;
    }
}
