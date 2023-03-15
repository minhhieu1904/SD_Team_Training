
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
            MS_Shift = new MS_ShiftRepository(_dbContext);
            MS_Location = new MS_LocationRepository(_dbContext);
            MS_Department = new MS_DepartmentRepository(_dbContext);
            MS_Package = new MS_PackageRepository(_dbContext);
        }
        public IMS_PackageRepository MS_Package {get;set;}
        public IMS_DepartmentRepository MS_Department {get;set;}
        public IMS_LocationRepository MS_Location {get;set;}
        public IMS_ShiftRepository MS_Shift {get;set;}
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