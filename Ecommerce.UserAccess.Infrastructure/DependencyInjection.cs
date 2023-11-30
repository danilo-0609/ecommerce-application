

using Ecommerce.BuildingBlocks.Application.EventBus;
using Ecommerce.UserAccess.Application.Data;
using Ecommerce.UserAccess.Domain.Common;
using Ecommerce.UserAccess.Domain.UserRegistrations;
using Ecommerce.UserAccess.Domain.Users;
using Ecommerce.UserAccess.Infrastructure.Persistence;
using Ecommerce.UserAccess.Infrastructure.Persistence.Repositories;
using Ecommerce.UserAccess.Infrastructure.EventsBus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace Ecommerce.UserAccess.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>((sp, optionsbuilder) =>
        {
            optionsbuilder.UseSqlServer(configuration.GetConnectionString("Database"));
        });

        services.AddScoped<IApplicationDbContext>(sp =>
           sp.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<IUnitOfWork>(sp =>
            sp.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<IUserRegistrationRepository, UserRegistrationRepository>();

        services.AddScoped<IUserRepository, UserRepository>();

        services.AddTransient<IEventBus, EventBus>();

        return services;
    }
}
