using System.Security.Cryptography.X509Certificates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API._Repositories;
using API._Services.Interfaces;
using API.Helper.Params.ShiftDataMaintain;
using API.Models;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using SD3_API.Helpers.Utilities;

namespace API._Services.Services
{
    public class StandardPackingQuantitySettingService : IStandardPackingQuantitySettingService
    {
        private readonly IRepositoryAccessor _repositoryAccessor;
        public StandardPackingQuantitySettingService(IRepositoryAccessor repositoryAccessor)
        {
            _repositoryAccessor = repositoryAccessor;
        }

        public async Task<OperationResult> Create(MS_Package model)
        {
            var item = await _repositoryAccessor.MS_Package.FirstOrDefaultAsync(x => x.Manuf == "N" && x.PackageNo == model.PackageNo);
            if(item != null)
            {
                return new OperationResult(false, "Đã có dữ liệu");
            }

            var modelNew = new MS_Package();
            modelNew.Manuf = "N";
            modelNew.PackageNo = model.PackageNo;
            modelNew.PackageQty = model.PackageQty;

            _repositoryAccessor.MS_Package.Add(modelNew);

            await _repositoryAccessor.Save();

            return new OperationResult(true, "Thêm dữ liệu thành công");
        }

        public async Task<PaginationUtility<MS_Package>> GetAll(PaginationParam pagination, StandardPackingQuantitySettingParam param)
        {
           var pre = PredicateBuilder.New<MS_Package>(true);
           if(!string.IsNullOrEmpty(param.PackageNo))
           {
            pre.And(x => x.PackageNo == param.PackageNo);
           }
           if(param.PackageQty > 0)
           {
            pre.And(x => x.PackageQty == param.PackageQty);
           }

           var data = _repositoryAccessor.MS_Package.FindAll(pre).AsNoTracking();
           return await PaginationUtility<MS_Package>.CreateAsync(data, pagination.PageNumber, pagination.PageSize);
        }

        public async Task<PaginationUtility<MS_Package>> Search(PaginationParam pagination, string text)
        {
            var pre = PredicateBuilder.New<MS_Package>(true);
            if(!string.IsNullOrEmpty(text)) 
            {
                text = text.ToLower();
                pre = pre.And(x => x.PackageNo.Contains(text));
            }

            var data = _repositoryAccessor.MS_Package.FindAll(pre).AsNoTracking();

            return await PaginationUtility<MS_Package>.CreateAsync(data, pagination.PageNumber, pagination.PageSize);
        }

        public async Task<OperationResult> Update(MS_Package model)
        {
           var item = await _repositoryAccessor.MS_Package.FirstOrDefaultAsync(x => x.Manuf == "N" && x.PackageNo == model.PackageNo);

           if(item == null)
           {
            return new OperationResult(false, "Chưa có dữ liệu");
           }

           item.PackageQty = model.PackageQty;
           _repositoryAccessor.MS_Package.Update(item);

           await _repositoryAccessor.Save();

           return new OperationResult(true, "Cập nhật dữ liệu thành công");
        }
    }
}