
using API._Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace API._Repositories
{
    public interface IRepositoryAccessor
    {
        // IERP_ISSUE_DTL_Repository ERP_ISSUE_DTL {get;}
        public IMS_Department_Repository MS_Department {get;}

        Task<bool> Save();
        Task<IDbContextTransaction> BeginTransactionAsync();

        
    }
}