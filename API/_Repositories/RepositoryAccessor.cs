
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
            Users = new UserRepository(_dbContext);
            Roles = new RolesRepository(_dbContext);
            RoleUser = new RoleUserRepository(_dbContext);
            MS_QR_Order = new MS_QR_OrderRepository(_dbContext);
            MS_QR_Storage = new MS_QR_StorageRepository(_dbContext);
            Report_Wksh_SumResult = new Report_wksh_SumParamRepository(_dbContext);
            MS_QR_Sort = new MS_QR_SortRepository(_dbContext);
        }  
         public IMS_QR_SortRepository MS_QR_Sort{get;set;}
        public IReport_wksh_SumParamRepository Report_Wksh_SumResult{get; set;}
        public IMS_QR_OrderRepository MS_QR_Order{get;set;}
        public IMS_QR_StorageRepository MS_QR_Storage{get;set;}
        public IRoleUserRepository RoleUser {get;set;}
        public IRolesRepository Roles {get;set;}
        public IUserRepository Users {get;set;}
        public IMS_PackageRepository MS_Package {get;set;}
        public IMS_DepartmentRepository MS_Department {get;set;}
        public IMS_LocationRepository MS_Location {get;set;}
        public IMS_ShiftRepository MS_Shift {get;set;}

        public IReport_wksh_SumParamRepository ReportWkshSum => throw new NotImplementedException();

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