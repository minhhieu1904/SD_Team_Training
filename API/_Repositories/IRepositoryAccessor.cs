
using API._Repositories.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace API._Repositories
{
    public interface IRepositoryAccessor
    {
        // IERP_ISSUE_DTL_Repository ERP_ISSUE_DTL {get;}
        UsersRepository Users {get;}
        Roles_Repository Roles {get;}
        RoleUser_Repository RoleUser {get;}
        Task<bool> Save();
        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}