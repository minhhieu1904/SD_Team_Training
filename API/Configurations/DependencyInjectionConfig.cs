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
            // services.AddScoped<I_ShiftDataMaintainServices, S_ShiftDataMaintainServices>();
            // services.AddScoped<I_WareHouseBasicDataServices, S_WareHouseBasicDataServices>();
            // services.AddScoped<I_DepartmentDataMaintainServices, S_DepartmentDataMaintainServices>();
            // services.AddScoped<I_StandardPackingQuantityServices, S_StandardPackingQuantityServices>();
            // services.AddScoped<I_AuthorizationServices, S_AuthorizationServices>();
            // services.AddScoped<I_LoginServices, S_Loginservices>();
            // services.AddScoped<I_WkshSumReport_Services, S_WkshSumReport_Services>();
            // services.AddScoped<I_SortSumReport_Services, S_SortSumReport_Services>();
            // services.AddScoped<I_StorageSumReport_Services, S_StorageSumReport_Services>();
            // services.AddScoped<I_QRCodeWIPReportServices, S_QRCodeWIPReportServices>();

        }
    }
}