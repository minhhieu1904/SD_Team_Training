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

            MS_Shift = new  MS_Shift_Repository(_dbContext);
            Ms_Location = new MS_Location_Repository(_dbContext);
            MS_Department = new MS_Department_Repository(_dbContext);
            Ms_Package = new MS_Package_Repository(_dbContext);
            Role = new Role_Repository(dbContext);
            RoleUser = new RoleUser_Repository(dbContext);
            User = new User_Repository(dbContext);
        }

        // public IERP_ISSUE_DTL_Repository ERP_ISSUE_DTL {get;set;}
        
        public I_MS_Shift_Repository MS_Shift {get; private set;}
        public I_MS_Location_Repository Ms_Location {get; set;}
        public I_MS_Department_Repository MS_Department {get; set;}
        public I_MS_Package_Repository Ms_Package{get;set;}
        public I_Role_Repository Role {get; set;}
        public I_RoleUser_Repository RoleUser {get; set;}
        public I_User_Repository User {get; set;}

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