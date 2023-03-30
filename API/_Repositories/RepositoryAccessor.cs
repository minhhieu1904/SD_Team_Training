
using API._Repositories.Interfaces;
using API._Repositories.Repositories;
using API.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace API._Repositories
{
    public class RepositoryAccessor : IRepositoryAccessor
    {
        private DBContext _dbContext;

        public RepositoryAccessor(DBContext dbContext)
        {
            _dbContext = dbContext;
            // ERP_ISSUE_DTL = new ERP_ISSUE_DTL_Repository(_dbContext);

            MS_Shift = new MS_Shift_Repository(_dbContext);
            MS_Department = new MS_Department_Repository(_dbContext);
            MS_Location = new MS_LocationRepository(_dbContext);
            MS_Package = new MS_Package_Repository(_dbContext);
            Users = new UserRepository(_dbContext);
            Roles = new RolesRepository(_dbContext);
            RoleUser = new RoleUserRepository(_dbContext);
            MS_QR_Order = new MS_QR_OrderRepository(_dbContext);
        }

        // public IERP_ISSUE_DTL_Repository ERP_ISSUE_DTL {get;set;}
        public IMS_Shift_Repository MS_Shift { get; private set; }
        public IMS_Department_Repository MS_Department { get; private set; }
        public IMS_LocationRepository MS_Location { get; set; }
        public IMS_Package_Repository MS_Package { get; private set; }
        public IUserRepository Users { get; private set; }
        public IRoleUserRepository RoleUser { get; private set; }
        public IRolesRepository Roles { get; private set; }

        public IMS_QR_OrderRepository MS_QR_Order { get; private set; }


        public async Task<bool> Save()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }
        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _dbContext.Database.BeginTransactionAsync();
        }
    }
}