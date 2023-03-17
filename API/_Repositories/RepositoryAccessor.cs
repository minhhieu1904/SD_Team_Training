
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

        public IMSDepartment_Repository MSDepartmet {get; set;}

        public RepositoryAccessor(DBContext dbContext)
        {
            _dbContext = dbContext;
            MSDepartmet = new MSDepartment_Repository(_dbContext);
            // ERP_ISSUE_DTL = new ERP_ISSUE_DTL_Repository(_dbContext);
        }

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