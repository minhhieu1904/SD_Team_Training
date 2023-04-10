using API._Repositories.Repositories;
using API.Data;
using API._Repositories.Interfaces;
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
            MS_Location = new MS_Location_Repository(_dbContext);
            MS_Department = new MS_Department_Repository(dbContext);
            MS_Package = new MS_Package_Repository(dbContext);
            User = new User_Repository(_dbContext);
            Role = new Role_Repository(dbContext);
            RoleUser = new RoleUser_Repository(dbContext);
            MS_QrOrder = new MS_QrOrder_Repository(dbContext);
            MS_QrSort = new MS_QrSort_Repository(dbContext);
            MS_QrLabel = new MS_QrLabel_Repository(dbContext);
        }

        // public IERP_ISSUE_DTL_Repository ERP_ISSUE_DTL {get;set;}
        public I_MS_Shift_Repository MS_Shift { get; set; }
        public I_MS_Location_Repository MS_Location { get; set; }
        public I_MS_Department_Repository MS_Department { get; set; }
        public I_MS_Package_Repository MS_Package { get; set; }
        public I_User_Repository User { get; set; }
        public I_Role_Repository Role { get; set; }
        public I_RoleUser_Repository RoleUser { get; set; }
        public I_MS_QrOrder_Repository MS_QrOrder { get; set; }
        public I_MS_QrSort_Repository MS_QrSort { get; set; }
        public I_MS_QrLabel_Repository MS_QrLabel { get; set; }
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