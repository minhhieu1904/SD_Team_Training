
using API._Repositories;
using API._Services.Interfaces.report;
using API._Services.Services.report;

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
            services.AddScoped<I_WkshSumReport_Services, S_WkshSumReport_Services>();
        }
    }
}