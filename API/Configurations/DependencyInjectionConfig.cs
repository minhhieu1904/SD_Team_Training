
using API._Repositories;
using API._Services.Interfaces;
using API._Services.Services.Maintain;
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
            services.AddScoped<I_ShiftDataMaintainServices, S_ShiftDataMaintainServices>();
            services.AddScoped<I_WarehouseBasicDataServices, S_WarehouseBasicDataServices>();
            services.AddScoped<I_DepartmentDataServices, S_DepartmentDataServices>();
            services.AddScoped<I_StandardPackingQuantityServices, S_StandardPackingQuantityServices>();
        }
    }
}