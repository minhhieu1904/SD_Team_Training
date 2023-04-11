using API._Repositories.Interfaces;
using API._Repositories.Repositories;
using API.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace API._Repositories
{
    public class RepositoryAccessor : IRepositoryAccessor
    {
        private MyDBContext _dbContext;
        public RepositoryAccessor(MyDBContext dbContext)
        {
            _dbContext = dbContext;
            // ERP_ISSUE_DTL = new ERP_ISSUE_DTL_Repository(_dbContext);
            MS_Department = new MS_Department_Repository(_dbContext);
            MS_Location = new MS_Location_Repository(_dbContext);
            MS_Package = new MS_Package_Repository(_dbContext);
            MS_QR_Cycle = new MS_QR_Cycle_Repository(_dbContext);
            MS_QR_Label = new MS_QR_Label_Repository(_dbContext);
            MS_QR_Order_Log_OnlyForST = new MS_QR_Order_Log_OnlyForST_Repository(_dbContext);
            MS_QR_Order = new MS_QR_Order_Repository(_dbContext);
            MS_QR_PickingDetail = new MS_QR_PickingDetail_Repository(_dbContext);
            MS_QR_PickingMain = new MS_QR_PickingMain_Repository(_dbContext);
            MS_QR_Sort = new MS_QR_Sort_Repository(_dbContext);
            MS_QR_Storage = new MS_QR_Storage_Repository(_dbContext);
            MS_QR_StorageOut = new MS_QR_StorageOut_Repository(_dbContext);
            MS_Shift = new MS_Shift_Repository(_dbContext);
            Roles = new Roles_Repository(_dbContext);
            RoleUser = new RoleUser_Repository(_dbContext);
            Users = new Users_Repository(_dbContext);
        }

        public IMS_Department_Repository MS_Department { get; set; }

        public IMS_Location_Repository MS_Location { get; set; }

        public IMS_Package_Repository MS_Package { get; set; }

        public IMS_QR_Cycle_Repository MS_QR_Cycle { get; set; }

        public IMS_QR_Label_Repository MS_QR_Label { get; set; }

        public IMS_QR_Order_Log_OnlyForST_Repository MS_QR_Order_Log_OnlyForST { get; set; }

        public IMS_QR_Order_Repository MS_QR_Order { get; set; }

        public IMS_QR_PickingDetail_Repository MS_QR_PickingDetail { get; set; }

        public IMS_QR_PickingMain_Repository MS_QR_PickingMain { get; set; }

        public IMS_QR_Sort_Repository MS_QR_Sort { get; set; }

        public IMS_QR_Storage_Repository MS_QR_Storage { get; set; }

        public IMS_QR_StorageOut_Repository MS_QR_StorageOut { get; set; }

        public IMS_Shift_Repository MS_Shift { get; set; }

        public IRoles_Repository Roles { get; set; }

        public IRoleUser_Repository RoleUser { get; set; }

        public IUsers_Repository Users { get; set; }

        // public IERP_ISSUE_DTL_Repository ERP_ISSUE_DTL {get;set;}
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