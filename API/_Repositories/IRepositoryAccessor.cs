
using API._Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace API._Repositories
{
    public interface IRepositoryAccessor
    {
        IMS_ShiftRepository MS_Shift {get;}
        IMS_LocationRepository  MS_Location{get;}
        IMS_DepartmentRepository MS_Department{get;}
        IMS_PackageRepository MS_Package{get;}
        Task<bool> Save();
        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}