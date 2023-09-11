


using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore.Storage;

namespace API._Repositories
{
    public class RepositoryAccessor : IRepositoryAccessor
    {
        //Bước 4
        private DBContext _dbContext;
        public RepositoryAccessor(DBContext dbContext)
        {
            _dbContext = dbContext;
            MS_Shift = new Repository<MS_Shift>(_dbContext);
            MS_Warehouse = new Repository<MS_Warehouse>(_dbContext);
            MS_Department = new Repository<MS_Department>(_dbContext);
            MS_Package = new Repository<MS_Package>(_dbContext);
            User = new Repository<User>(_dbContext);
            RoleUser = new Repository<RoleUser>(_dbContext);
            Role = new Repository<Role>(_dbContext);
            MS_QR_Label = new Repository<MS_QR_Label>(_dbContext);
            MS_QR_Order = new Repository<MS_QR_Order>(_dbContext);
            MS_QR_Cycle = new Repository<MS_QR_Cycle>(_dbContext);
            MS_QR_Sort = new Repository<MS_QR_Sort>(_dbContext);
        }

         public IRepository<MS_Shift> MS_Shift { get; set; }
         public IRepository<MS_Warehouse> MS_Warehouse { get; set; }
         public IRepository<MS_Department> MS_Department { get; set; }
         public IRepository<MS_Package> MS_Package { get; set; }
         public IRepository<User> User { get; set; }
         public IRepository<RoleUser> RoleUser { get; set; }
         public IRepository<Role> Role { get; set; }
         public IRepository<MS_QR_Order> MS_QR_Order { get; set; }
         public IRepository<MS_QR_Label> MS_QR_Label { get; set; }
         public IRepository<MS_QR_Cycle> MS_QR_Cycle { get; set; }
         public IRepository<MS_QR_Sort> MS_QR_Sort { get; set; }
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