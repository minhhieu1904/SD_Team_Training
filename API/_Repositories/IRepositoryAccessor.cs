
using API._Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace API._Repositories
{
    public interface IRepositoryAccessor
    {
        // IERP_ISSUE_DTL_Repository ERP_ISSUE_DTL {get;}
        public I_Role_Repository Role {get;}
        public I_RoleUser_Repository RoleUser {get;}
        public I_User_Repository User {get;}
        Task<bool> Save();
        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}