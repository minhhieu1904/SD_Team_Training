
using API._Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace API._Repositories
{
    public interface IRepositoryAccessor
    {
        // IERP_ISSUE_DTL_Repository ERP_ISSUE_DTL {get;}

        public I_MS_Shift_Repository MS_Shift {get;}
        public I_MS_Location_Repository Ms_Location {get;}
        public I_MS_Department_Repository MS_Department{ get; } 
        public I_MS_Package_Repository Ms_Package{get;}
        
        Task<bool> Save();
        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}