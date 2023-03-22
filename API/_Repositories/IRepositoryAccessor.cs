using API._Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace API._Repositories
{
    public interface IRepositoryAccessor
    {
        // IERP_ISSUE_DTL_Repository ERP_ISSUE_DTL {get;}

        public I_MS_Shift_Repository MS_Shift {get;}
        
        Task<bool> Save();
        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}