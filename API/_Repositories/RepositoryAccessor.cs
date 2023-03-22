
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
            Users = new UserRepository(_dbContext);
            Roles = new RolesRepository(_dbContext);
            RoleUser = new RoleUserRepository(_dbContext);
        }

        // public IERP_ISSUE_DTL_Repository ERP_ISSUE_DTL {get;set;}
        public IUserRepository Users { get; private set; }
        public IRoleUserRepository RoleUser { get; private set; }
        public IRolesRepository Roles { get; private set; }

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