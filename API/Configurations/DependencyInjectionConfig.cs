using API._Repositories;
using API._Services.Interfaces.Common;
using API._Services.Interfaces.Report;
using API._Services.Services.Common;
using API._Services.Services.Report;

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
            services.AddScoped<I_CommonService, S_Common>();
            services.AddScoped<I_WksSuReportService, S_WksSuReport>();
        }
    }
}