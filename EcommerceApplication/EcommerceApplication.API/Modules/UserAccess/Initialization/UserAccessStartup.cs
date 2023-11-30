using Ecommerce.UserAccess.Application;
using Ecommerce.UserAccess.Infrastructure;

namespace Ecommerce.API.Modules.Initialization.UsersStartup;

public static class UserAccessStartup
{
    public static IServiceCollection AddUserAccess(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApplication();

        services.AddInfrastructure(configuration);
    
        return services;
    }
}
