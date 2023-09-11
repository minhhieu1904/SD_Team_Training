
using API.Models;
using Microsoft.EntityFrameworkCore.Storage;
using SDCores;
namespace API._Repositories
{  
    //Bước 3
    [DependencyInjectionAttribute(ServiceLifetime.Scoped)]
    public interface IRepositoryAccessor
    {
         Task<bool> Save();
        Task<IDbContextTransaction> BeginTransactionAsync();
        IRepository<MS_Shift> MS_Shift { get; }
        IRepository<MS_Warehouse> MS_Warehouse { get; }
        IRepository<MS_Department> MS_Department { get; }
        IRepository<MS_Package> MS_Package { get; }
        IRepository<User> User { get; }
        IRepository<Role> Role { get; }
        IRepository<RoleUser> RoleUser { get; }
        IRepository<MS_QR_Order> MS_QR_Order { get; }
        IRepository<MS_QR_Label> MS_QR_Label { get; }
        IRepository<MS_QR_Cycle> MS_QR_Cycle { get; }
        IRepository<MS_QR_Sort> MS_QR_Sort { get; }
    }
}