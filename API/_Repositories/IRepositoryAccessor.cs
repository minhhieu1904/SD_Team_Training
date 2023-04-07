using API._Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace API._Repositories
{
    public interface IRepositoryAccessor
    {
        // IERP_ISSUE_DTL_Repository ERP_ISSUE_DTL {get;}
        public IMS_QR_Order_Repository MS_QR_Order { get; }
        public IMS_QR_Sort_Repository MS_QR_Sort { get; }
        public IMS_QR_Storage_Repository MS_QR_Storage { get; }

        Task<bool> Save();
        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}