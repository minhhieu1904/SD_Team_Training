
using API._Repositories;
using API._Services.Interfaces;
using API._Services.Services.S_WarehouseBasicDataMaintenance;
namespace API.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            // Add RepositoryAccessor
            services.AddScoped<IRepositoryAccessor, RepositoryAccessor>();

            // Add Service
            services.AddScoped<I_WarehouseBasicData, S_WarehouseBasicData>();
        }
    }
}