
using API._Repositories;
using API._Services.Interfaces;
using API._Services.Interfaces.report;
using API._Services.Services;
using API._Services.Services.DepartmentDataMaintenance;
using API._Services.Services.login;
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
            // services.AddScoped<IShiltDataMaintainServices, ShiltDataMaintainServices>();
           services.AddScoped<I_ShiftDataMaintain, S_ShiltDataMaintain>();
           
            services.AddScoped<I_DepartmentDataMaintenance, S_DepartmentDataMaintenance>();


            services.AddScoped<I_WarehouseBasicData, S_WarehouseBasicData>();
            
            services.AddScoped<I_StandardPackingQuantity, S_StandardPackingQuantity>();
            services.AddScoped<I_AuthorizationSetting, S_AuthorizationSetting>();
            services.AddScoped<ILoginUser, S_LoginUser>();
            services.AddScoped<I_WkshSumReport, S_WkshSumReport>();
            services.AddScoped<I_SortSumReport, S_SortSumReport>();
            services.AddScoped<I_StorageSumReport, S_StorageSumReport>();

        }
    }
}