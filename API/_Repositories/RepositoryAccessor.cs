
using API._Repositories.Interfaces;
using API._Repositories.Repositories;
using API.Data;
using API.Models;
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
            MsQrOrder = new MS_QROrder_Repository(_dbContext);
            MSQrSort = new MS_QrSort_Repository(dbContext);
            MSQrLabel = new MS_QrLabel_Repository(dbContext);
        }

        // public IERP_ISSUE_DTL_Repository ERP_ISSUE_DTL {get;set;}
        public IMS_QROrder_Repository MsQrOrder {get;set;}
        public IMS_QrSort_Repository MSQrSort { get; set; }
        public IMS_QrLabel_Repository MSQrLabel { get; set; }
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