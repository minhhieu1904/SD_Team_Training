using API._Repositories;
using API._Services.Interfaces;
using API._Services.Interfaces.Maintain;
using API._Services.Services;

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
            services.AddScoped<I_1_1_ShiftDataMaintenance, S_1_1_ShiftDataMaintenance>();
            services.AddScoped<IAuthService, AuthService>();
        }
    }
}