
using API._Repositories;
using API._Services.Interfaces;
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
            services.AddScoped<I_Shift_Data_Maintain, S_Shift_Data_Maintain>();

            // khai bao o day
            services.AddScoped<I_Warehouse_Basic_Data_Maintenance, S_Warehouse_Basic_Data_Maintenance>();

            services.AddScoped<I_Department_Data_Maintain, S_Department_Data_Maintain>();

            services.AddScoped<I_Packing_Quantity_Setting, S_Packing_Quantity_Setting>();
        }
    }
}