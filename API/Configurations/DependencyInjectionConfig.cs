
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
            services.AddScoped<IMSShiftServices, SMMShiftServices>();
            services.AddScoped<IMSLocationServices, SMSLocationServices>();
           
            services.AddScoped<IRepositoryAccessor, RepositoryAccessor>();
           

            // Add Service
             services.AddScoped<IMSLocationServices, SMSLocationServices>();

            // Add Service
            services.AddScoped<IMSDepartmentServices, SMSDepartmentServices>();
        }
    }
}