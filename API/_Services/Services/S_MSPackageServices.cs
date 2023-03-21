using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API._Repositories;
using API._Services.Interfaces;
using API.Models;
using Microsoft.EntityFrameworkCore;
using SD3_API.Helpers.Utilities;

namespace API._Services.Services
{
    public class SMSPackageServices : IMSPackageServices
    {
        private readonly IRepositoryAccessor _repositoryAccessor;
        public SMSPackageServices(IRepositoryAccessor repositoryAccessor)
        {
            _repositoryAccessor = repositoryAccessor;
        }
        public async Task<OperationResult> Add(MS_Package model)
        {
            var item = await _repositoryAccessor.MSPackage.FirstOrDefaultAsync(x => x.Manuf.Trim() == "N" && x.PackageNo == model.PackageNo);
            if(item != null)
            return new OperationResult(false);
            model.Manuf = "N";
            _repositoryAccessor.MSPackage.Add(model);
            if(await _repositoryAccessor.Save())
            return new OperationResult(true);
            return new OperationResult(false);

        }

        public async Task<PaginationUtility<MS_Package>> GetDataPaing(PaginationParam param, string packageNo, decimal packageQty)
        {
            var data = await _repositoryAccessor.MSPackage.FindAll().ToListAsync();
            if(!string.IsNullOrEmpty(packageNo))
            data = data.Where(x => x.PackageNo == packageNo).ToList();
            if(packageQty!=0)
            data = data.Where(x => x.PackageQty == packageQty).ToList();
            return PaginationUtility<MS_Package>.Create(data, param.PageNumber, param.PageSize);
        }

        public async Task<MS_Package> GetItem(string manuf, string packageNo)
        {
           return await _repositoryAccessor.MSPackage.FirstOrDefaultAsync(x => x.Manuf == manuf && x.PackageNo == packageNo);
        }

        public async Task<OperationResult> Update(MS_Package model)
        {
            var item = await _repositoryAccessor.MSPackage.FirstOrDefaultAsync(x => x.Manuf == model.Manuf && x.PackageNo == model.PackageNo);
            if(item == null)
            return new OperationResult(false);
            item.PackageQty = model.PackageQty;
            if(await _repositoryAccessor.Save())
            return new OperationResult(true);
            return new OperationResult(false);
        }
    }
}