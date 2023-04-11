
using API._Repositories;
using API._Services.Interfaces;
using API._Services.Interfaces.Report;
using API._Services.Services;
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
            services.AddScoped<IWarehouseBasicdataService, WarehouseBasicdataService>();
            services.AddScoped<IShiftDataMaintenanceService, ShiftDataMaintenanceService>();
            services.AddScoped<IDepartmentDataServices, DepartmentDataServices>();
            services.AddScoped<IPackageServices, PackageServices>();
            services.AddScoped<IUserRoleServices, UserRoleServices>();
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IReport_wksh_SumService, Report_wksh_SumService>();
            services.AddScoped<IReport_Sort_Sumservice, Report_Sort_Sumservice>();
            services.AddScoped<IReport_Storage_Sumservice, Report_Storage_Sumservice>();

        }
    }
}