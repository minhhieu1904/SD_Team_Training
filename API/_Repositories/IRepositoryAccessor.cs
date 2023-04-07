
using API._Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace API._Repositories
{
    public interface IRepositoryAccessor
    {
        // IERP_ISSUE_DTL_Repository ERP_ISSUE_DTL {get;}
        IMS_ShiftRepository MS_Shift { get; }
        IMS_WarehouseRepository MS_Warehouse { get; }
        IMS_DepartmentRepository MS_Department { get; }
        IMS_PackageRepository MS_Package { get; }
        Task<bool> Save();
        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}