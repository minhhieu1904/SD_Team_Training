using API._Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace API._Repositories
{
    public interface IRepositoryAccessor
    {

        // IERP_ISSUE_DTL_Repository ERP_ISSUE_DTL {get;}
        IMS_Department_Repository MS_Department { get; }
        IMS_Location_Repository MS_Location { get; }
        IMS_Package_Repository MS_Package { get; }
        IMS_QR_Cycle_Repository MS_QR_Cycle { get; }
        IMS_QR_Label_Repository MS_QR_Label { get; }
        IMS_QR_Order_Log_OnlyForST_Repository MS_QR_Order_Log_OnlyForST { get; }
        IMS_QR_Order_Repository MS_QR_Order { get; }
        IMS_QR_PickingDetail_Repository MS_QR_PickingDetail { get; }
        IMS_QR_PickingMain_Repository MS_QR_PickingMain { get; }
        IMS_QR_Sort_Repository MS_QR_Sort { get; }
        IMS_QR_Storage_Repository MS_QR_Storage { get; }
        IMS_QR_StorageOut_Repository MS_QR_StorageOut { get; }
        IMS_Shift_Repository MS_Shift { get; }
        IRoles_Repository Roles { get; }
        IRoleUser_Repository RoleUser { get; }
        IUsers_Repository Users { get; }
        Task<bool> Save();
        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}