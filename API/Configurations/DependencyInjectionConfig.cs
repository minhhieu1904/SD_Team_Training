
using API._Repositories;
using API._Services.Interfaces;
using API._Services.Services;
using API._Services.Services.DepartmentDataMaintenance;

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
            // services.AddScoped<IShiltDataMaintainServices, ShiltDataMaintainServices>();
           services.AddScoped<I_ShiftDataMaintain, S_ShiltDataMaintain>();
           
            services.AddScoped<I_DepartmentDataMaintenance, S_DepartmentDataMaintenance>();


            services.AddScoped<I_WarehouseBasicData, S_WarehouseBasicData>();
            
            services.AddScoped<I_StandardPackingQuantity, S_StandardPackingQuantity>();
        }
    }
}