
using API._Repositories;
using API._Services.Interfaces;
using API._Services.Interfaces.Login;
using API._Services.Interfaces.report;
using API._Services.Services;
using API._Services.Services.Login;
using API._Services.Services.Maintain;
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
            services.AddScoped<I_ShiftDataMaintainServices, S_ShiftDataMaintainServices>();
            services.AddScoped<I_WareHouseBasicDataServices, S_WareHouseBasicDataServices>();
            services.AddScoped<I_DepartmentDataMaintainServices, S_DepartmentDataMaintainServices>();
            services.AddScoped<I_StandardPackingQuantityServices, S_StandardPackingQuantityServices>();
            services.AddScoped<I_AuthorizationServices, S_AuthorizationServices>();
            services.AddScoped<I_LoginServices, S_Loginservices>();
            services.AddScoped<I_WkshSumReport_Services, S_WkshSumReport_Services>();

        }
    }
}