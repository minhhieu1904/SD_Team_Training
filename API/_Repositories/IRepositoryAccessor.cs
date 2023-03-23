
using API._Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace API._Repositories
{
    public interface IRepositoryAccessor
    {
        IMS_Package_Repository MS_Package {get;}

        // IERP_ISSUE_DTL_Repository ERP_ISSUE_DTL {get;}
        Task<bool> Save();
        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}