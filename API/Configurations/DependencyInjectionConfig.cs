
using API._Repositories;
using API._Services.Interfaces;
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
            services.AddScoped<I_ShiftDataMaintainServices, S_ShiftDataMaintainServices>();
            services.AddScoped<I_WareHouseBasicDataServices, S_WareHouseBasicDataServices>();
            services.AddScoped<I_DepartmentDataMaintainServices, S_DepartmentDataMaintainServices>();

        }
    }
}