
using API._Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace API._Repositories
{
    public interface IRepositoryAccessor
    {
        // IERP_ISSUE_DTL_Repository ERP_ISSUE_DTL {get;}
        public IMS_QROrder_Repository MsQrOrder {get;}
        
        public IMS_QrSort_Repository MSQrSort { get; }
        
        public IMS_QrLabel_Repository MSQrLabel { get; }

        Task<bool> Save();
        Task<IDbContextTransaction> BeginTransactionAsync();
    }
}