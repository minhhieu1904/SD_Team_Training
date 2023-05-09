
using API._Repositories;
using API._Repositories.Interfaces;
using API._Repositories.Repositories;
using API._Services.Interfaces;
using API._Services.Interfaces.Common;
using API._Services.Services;
using API._Services.Services.Common;
using SD3_API.Helpers.Utilities;
using Scrutor;

namespace API.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.Scan(scan => scan
                .FromCallingAssembly()
                .AddClasses()
                .AsImplementedInterfaces()
                .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                .AsMatchingInterface()
                .WithScopedLifetime());

            // // Add RepositoryAccessor
            // services.AddScoped<IRepositoryAccessor, RepositoryAccessor>();

            // // Add Service
            // services.AddScoped<IShiftDataMaintainService, ShiftDataMaintainService>();
            // services.AddScoped<IWarehouseBasicDataMaintenanceService, WarehouseBasicDataMaintenanceService>();
            // services.AddScoped<IDepartmentDataMaintenanceService, DepartmentDataMaintenanceService>();
            // services.AddScoped<IStandardPackingQuantitySettingService, StandardPackingQuantitySettingService>();
            // services.AddScoped<IAuthorizationSettingService, AuthorizationSettingService>();
            // services.AddScoped<IAuthorService, AuthorService>();
            // services.AddScoped<ISearchForOrderDataService, SearchForOrderDataService>();
            // services.AddScoped<IPackingScanService, PackingScanService>();
            // services.AddScoped<ITransactionCommonService, TransactionCommonService>();
            // services.AddScoped<IFunctionUtility, FunctionUtility>();
        }
    }
}