using Ecommerce.Catalog.Application;
using Ecommerce.Catalog.Infrastructure;

namespace Ecommerce.API.Modules.Catalog.Initialization
{
    public static class CatalogStartup
    {
        public static IServiceCollection AddCatalog(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddApplication();
            services.AddInfraestructure(configuration);

            return services;
        }

    }
}
