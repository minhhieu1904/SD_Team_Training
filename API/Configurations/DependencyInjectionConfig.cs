
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
            services.AddScoped<IWarehouseBasicdataService, WarehouseBasicdataService>();
            services.AddScoped<IShiftDataMaintenanceService, ShiftDataMaintenanceService>();
            services.AddScoped<IDepartmentDataServices, DepartmentDataServices>();
            services.AddScoped<IPackageServices, PackageServices>();
            services.AddScoped<IUserRoleServices, UserRoleServices>();
            services.AddScoped<IAuthorService, AuthorService>();
        }
    }
}