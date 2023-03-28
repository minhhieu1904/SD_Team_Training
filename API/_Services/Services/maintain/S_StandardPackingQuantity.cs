
using AgileObjects.AgileMapper;
using API._Repositories;
using API._Services.Interfaces;
using API.DTOs;
using API.Models;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using SD3_API.Helpers.Utilities;

namespace API._Services.Services
{
    public class S_StandardPackingQuantity : I_StandardPackingQuantity
    {
        private readonly IRepositoryAccessor _reposioryAccessor;
        // Thêm vào hàm khởi tạo
        public S_StandardPackingQuantity(IRepositoryAccessor reposioryAccessor)
        {
            _reposioryAccessor = reposioryAccessor;
        }
        public async Task<PaginationUtility<MS_Package>> GetData(PaginationParam pagination, StandardPackingQuantityParam param)
        {
            var pred_MS_Package = PredicateBuilder.New<MS_Package>(true); 

            
            if (!string.IsNullOrEmpty(param.PackageNo))
                pred_MS_Package.And(x => x.PackageNo == param.PackageNo);
            if (param.PackageQty != 0)
                pred_MS_Package.And(x => x.PackageQty == param.PackageQty);

            var data = _reposioryAccessor.MS_Package.FindAll(pred_MS_Package);// 
            return await PaginationUtility<MS_Package>.CreateAsync(data, pagination.PageNumber, pagination.PageSize);
        }
        public async Task<OperationResult> Addnew(StandardPackingQuantityAddParam model)
        {
            var originalItem = await _reposioryAccessor.MS_Package.FindAll(x => x.Manuf == model.Manuf
            && x.PackageNo == model.PackageNo.Trim()).FirstOrDefaultAsync();
            if (originalItem != null)
            {
                return new OperationResult(false);
            }
            else
            {
                model.Manuf = "N";
                var item = Mapper.Map(model).ToANew<MS_Package>(x => x.MapEntityKeys());
                _reposioryAccessor.MS_Package.Add(item);
                if (await _reposioryAccessor.Save())
                {
                    return new OperationResult(true);
                }
                return new OperationResult(false);
            }

        }
        public async Task<OperationResult> Update(StandardPackingQuantityAddParam model)
        {
            var originalItem = await _reposioryAccessor.MS_Package.FirstOrDefaultAsync(x => x.Manuf.Trim() == "N" && x.PackageNo.Trim() == model.PackageNo.Trim());
            if (originalItem == null)
                return new OperationResult(false);

            originalItem.PackageQty = model.PackageQty;

            _reposioryAccessor.MS_Package.Update(originalItem);

            if (await _reposioryAccessor.Save())
            {
                return new OperationResult(true);
            }
            return new OperationResult(false);
        }
        public async Task<MS_Package> GetDataOnly(string manuf, string PackageNo)
        {
            return await _reposioryAccessor.MS_Package.FirstOrDefaultAsync(item => item.Manuf.ToUpper() == manuf.ToUpper() && item.PackageNo == PackageNo);

        }
    }
}