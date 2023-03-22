
using API._Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace API._Repositories
{
    public interface IRepositoryAccessor
    {
        // IERP_ISSUE_DTL_Repository ERP_ISSUE_DTL {get;}
        public IMS_Shift_Repository MS_Shift { get; }
        public IMS_Department_Repository MS_Department { get; }
        public IMS_LocationRepository MS_Location { get; }

        public IMS_Package_Repository MS_Package { get; }
        public IUserRepository Users { get; }
        public IRolesRepository Roles { get; }
        public IRoleUserRepository RoleUser { get; }
        Task<bool> Save();
        Task<IDbContextTransaction> BeginTransactionAsync();


    }
}