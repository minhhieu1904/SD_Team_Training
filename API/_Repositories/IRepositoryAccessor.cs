
<<<<<<< HEAD
using API._Repositories.Interfaces;
=======
using API._Repositories.Repositories;
>>>>>>> feature-hai-1.5-authorization_setting
using Microsoft.EntityFrameworkCore.Storage;

namespace API._Repositories
{
    public interface IRepositoryAccessor
    {
        // IERP_ISSUE_DTL_Repository ERP_ISSUE_DTL {get;}
        IMSShift_Repository MSShift {get;}
        IMSLocation_Repository MSLocation {get;}
        IMSDepartment_Repository MSDepartmet {get;}
        IMSPackage_Repository MSPackage { get; }
        IUsersRepository Users {get;}
        IRolesRepository Roles {get;}
        IRoleUser_Repository RoleUser {get;}
        Task<bool> Save();
        Task<IDbContextTransaction> BeginTransactionAsync();

    }
}