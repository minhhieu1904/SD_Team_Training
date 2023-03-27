using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore.Storage;
using SDCores;

namespace API._Repositories
{
    public class RepositoryAccessor : IRepositoryAccessor
    {
        private DBContext _dbContext;
        public RepositoryAccessor(DBContext dbContext)
        {
            _dbContext = dbContext;
            Users = new Repository<Users, DBContext>(_dbContext);
            Roles = new Repository<Roles, DBContext>(_dbContext);
            RoleUser = new Repository<RoleUser, DBContext>(_dbContext);
            MS_Shift = new Repository<MS_Shift, DBContext>(_dbContext);
            MS_QR_Storage = new Repository<MS_QR_Storage, DBContext>(_dbContext);
            MS_QR_Sort = new Repository<MS_QR_Sort, DBContext>(_dbContext);
            MS_QR_Order = new Repository<MS_QR_Order, DBContext>(_dbContext);
            MS_QR_Label = new Repository<MS_QR_Label, DBContext>(_dbContext);
            MS_QR_Cycle = new Repository<MS_QR_Cycle, DBContext>(_dbContext);
            MS_Package = new Repository<MS_Package, DBContext>(_dbContext);
            MS_Location = new Repository<MS_Location, DBContext>(_dbContext);
            MS_Department = new Repository<MS_Department, DBContext>(_dbContext);
            MS_QR_PickingDetail = new Repository<MS_QR_PickingDetail, DBContext>(_dbContext);
            MS_QR_PickingMain = new Repository<MS_QR_PickingMain, DBContext>(_dbContext);
        }

        public IRepository<Users> Users { get; private set; }

        public IRepository<Roles> Roles { get; private set; }

        public IRepository<RoleUser> RoleUser { get; private set; }

        public IRepository<MS_Shift> MS_Shift { get; private set; }

        public IRepository<MS_QR_Storage> MS_QR_Storage { get; private set; }

        public IRepository<MS_QR_Sort> MS_QR_Sort { get; private set; }

        public IRepository<MS_QR_Order> MS_QR_Order { get; private set; }

        public IRepository<MS_QR_Label> MS_QR_Label { get; private set; }

        public IRepository<MS_QR_Cycle> MS_QR_Cycle { get; private set; }

        public IRepository<MS_Package> MS_Package { get; private set; }

        public IRepository<MS_Location> MS_Location { get; private set; }

        public IRepository<MS_Department> MS_Department { get; private set; }

        public IRepository<MS_QR_PickingDetail> MS_QR_PickingDetail { get; private set; }

        public IRepository<MS_QR_PickingMain> MS_QR_PickingMain { get; private set; }

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