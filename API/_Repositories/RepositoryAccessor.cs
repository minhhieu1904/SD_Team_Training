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
            MS_QR_Order = new MS_QR_Order_Repository(_dbContext);
            MS_QR_Sort = new MS_QR_Sort_Repository(_dbContext);
            MS_QR_Label = new MS_QR_Label_Repository(_dbContext);
        }

        // public IERP_ISSUE_DTL_Repository ERP_ISSUE_DTL {get;set;}
        public IMS_QR_Order_Repository MS_QR_Order { get; set; }

        public IMS_QR_Sort_Repository MS_QR_Sort { get; set; }

        public IMS_QR_Label_Repository MS_QR_Label { get; set; }

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