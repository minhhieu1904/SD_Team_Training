
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
            MS_Shift = new MS_ShiftRepository(_dbContext);
            MS_Warehouse = new MS_WarehouseRepository(_dbContext);
            MS_Department = new MS_DepartmentRepository(_dbContext);
        }
        public IMS_ShiftRepository MS_Shift { get; private set; }

        public IMS_WarehouseRepository MS_Warehouse { get; private set; }

        public IMS_DepartmentRepository MS_Department { get; private set; }

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