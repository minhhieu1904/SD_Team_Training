using API.Models;
using Microsoft.EntityFrameworkCore.Storage;
using SDCores;

namespace API._Repositories
{
    [DependencyInjection(ServiceLifetime.Scoped)]
    public interface IRepositoryAccessor
    {
        IRepository<Users> Users { get; }
        IRepository<Roles> Roles { get; }
        IRepository<RoleUser> RoleUser { get; }
        IRepository<MS_Shift> MS_Shift { get; }
        IRepository<MS_QR_Storage> MS_QR_Storage { get; }
        IRepository<MS_QR_Sort> MS_QR_Sort { get; }
        IRepository<MS_QR_Order> MS_QR_Order { get; }
        IRepository<MS_QR_Label> MS_QR_Label { get; }
        IRepository<MS_QR_Cycle> MS_QR_Cycle { get; }
        IRepository<MS_Package> MS_Package { get; }
        IRepository<MS_Location> MS_Location { get; }
        IRepository<MS_Department> MS_Department { get; }
        IRepository<MS_QR_PickingDetail> MS_QR_PickingDetail { get; }
        IRepository<MS_QR_PickingMain> MS_QR_PickingMain { get; }
        Task<bool> Save();
        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}