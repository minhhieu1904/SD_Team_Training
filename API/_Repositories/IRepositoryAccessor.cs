
using API._Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace API._Repositories
{
    public interface IRepositoryAccessor
    {
        // IERP_ISSUE_DTL_Repository ERP_ISSUE_DTL {get;}
        public IMS_Shift_Repository MS_Shift {get;}
        public IMS_Department_Repository MS_Department {get;}
        IMS_LocationRepository  MS_Location{get;}

        Task<bool> Save();
        Task<IDbContextTransaction> BeginTransactionAsync();

        
    }
}