using API._Repositories;
using API._Services.Interfaces;
using API._Services.Services;
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
            services.AddScoped<I_Shift_Data_Maintain, S_Shift_Data_Maintain>();

            // khai bao o day
            services.AddScoped<I_Warehouse_Basic_Data_Maintenance, S_Warehouse_Basic_Data_Maintenance>();

            services.AddScoped<I_Department_Data_Maintain, S_Department_Data_Maintain>();

            services.AddScoped<I_Packing_Quantity_Setting, S_Packing_Quantity_Setting>();

            services.AddScoped<I_UserRoleServices, S_UserRoleServices>();

            services.AddScoped<I_Login, S_Login>();

            services.AddScoped<I_CommonService, S_Common>();

            services.AddScoped<I_WksSuReportService, S_WksSuReport>();

            services.AddScoped<I_SortSumReport, S_SortSumReport>();
        }
    }
}