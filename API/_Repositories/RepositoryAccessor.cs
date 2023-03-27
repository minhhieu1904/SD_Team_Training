
<<<<<<< HEAD
using API._Repositories.Interfaces;
=======
>>>>>>> feature-hai-1.5-authorization_setting
using API._Repositories.Repositories;
using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore.Storage;

namespace API._Repositories
{
    public class RepositoryAccessor : IRepositoryAccessor
    {
        private DBContext _dbContext;

        public IMSShift_Repository MSShift { get; set; }
        public IMSLocation_Repository MSLocation { get; set; }
        public IMSDepartment_Repository MSDepartmet { get; set; }

        public IMSPackage_Repository MSPackage { get; private set; }
        public UsersRepository Users { get; set; }
        public Roles_Repository Roles { get; set; }
        public RoleUser_Repository RoleUser { get; }

        public RepositoryAccessor(DBContext dbContext)
        {
            _dbContext = dbContext;
            // ERP_ISSUE_DTL = new ERP_ISSUE_DTL_Repository(_dbContext);
            MSShift = new MSShift_Repository(_dbContext);
            MSLocation = new MSLocation_Repository(_dbContext);
            MSDepartmet = new MSDepartment_Repository(_dbContext);
            MSPackage = new MSPackage_Repository(_dbContext);
            Users = new UsersRepository(_dbContext);
            Roles = new Roles_Repository(_dbContext);
            RoleUser = new RoleUser_Repository(_dbContext);
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