
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
            Users = new UsersRepository(_dbContext);
            Roles = new Roles_Repository(_dbContext);
            RoleUser = new RoleUser_Repository(_dbContext);
            // ERP_ISSUE_DTL = new ERP_ISSUE_DTL_Repository(_dbContext);
        }

        public UsersRepository Users {get; set;}
        public Roles_Repository Roles {get; set;}
        public RoleUser_Repository RoleUser { get; }
       
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