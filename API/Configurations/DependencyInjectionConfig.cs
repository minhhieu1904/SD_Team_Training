using API._Repositories;
using API._Services.Interfaces;
using API._Services.Interfaces.Maintain;
using API._Services.Services;
using API._Services.Services.Maintain;

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
            services.AddScoped<I_1_2_WarehouseBasicDataMaintenance, S_1_2_WarehouseBasicDataMaintenance>();
            services.AddScoped<I_1_5_AuthorizationSetting, S_1_5_AuthorizationSetting>();
            services.AddScoped<IAuthService, AuthService>();
        }
    }
}