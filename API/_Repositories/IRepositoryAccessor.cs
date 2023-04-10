using API._Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace API._Repositories
{
    public interface IRepositoryAccessor
    {
        // IERP_ISSUE_DTL_Repository ERP_ISSUE_DTL {get;}
        IMS_QR_Order_Repository MS_QR_Order { get; }
        IMS_QR_Sort_Repository MS_QR_Sort { get; }
        IMS_QR_Label_Repository MS_QR_Label { get; }
        Task<bool> Save();
        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}