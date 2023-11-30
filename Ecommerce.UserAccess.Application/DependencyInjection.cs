using Ecommerce.BuildingBlocks.Application.Abstractions;
using Ecommerce.BuildingBlocks.Application.Behaviors;
using Ecommerce.BuildingBlocks.Application.Logging;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Ecommerce.UserAccess.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblyContaining<AssemblyReference>();

            config.AddOpenBehavior(typeof(UnitOfWorkBehavior<,>));
        });

        services.AddScoped(
            typeof(IPipelineBehavior<,>),
            typeof(ValidationBehavior<,>));

        services.AddScoped(
            typeof(IPipelineBehavior<,>),
            typeof(LoggingPipelineBehavior<,>));

        services.AddValidatorsFromAssemblyContaining<AssemblyReference>();

        return services;
    }
}
