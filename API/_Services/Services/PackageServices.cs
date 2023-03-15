
using AgileObjects.AgileMapper;
using API._Repositories;
using API._Services.Interfaces;
using API.Models;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using SD3_API.Helpers.Utilities;

namespace API._Services.Services
{
    public class PackageServices : IPackageServices
    {
        private readonly IRepositoryAccessor _repoAccessor;

        public PackageServices(IRepositoryAccessor repoAccessor)
        {
            _repoAccessor = repoAccessor;
        }
         public async Task<OperationResult> Add(MS_Package packageNo)
        {
            var data = await _repoAccessor.MS_Package.FindSingle(x=> x.PackageNo == packageNo.PackageNo);
            if (data != null)
            {
                return new OperationResult(false, "đã tồn tại" );      
            }
            else{
                var item = Mapper.Map(packageNo).ToANew<MS_Package>(x=> x.MapEntityKeys());
                _repoAccessor.MS_Package.Add(item);
                await _repoAccessor.Save();
            }
            return new OperationResult(true,"Add thành công");
        }
        public async Task<PaginationUtility<MS_Package>> LoadData(PaginationParam paginationParams, string packageNo, decimal packageQty)
        {
            var pred = PredicateBuilder.New<MS_Package>(true);
            if(!string.IsNullOrEmpty(packageNo))
            {
                pred.And(x => x.PackageNo == packageNo.Trim() );
            }
            if(packageQty != 0)
            {
                pred.And(x => x.PackageQty == packageQty);
            }
            var data = await _repoAccessor.MS_Package.FindAll(pred).ToListAsync();
            return PaginationUtility<MS_Package>.Create(data, paginationParams.PageNumber, paginationParams.PageSize, true);

        }

        public async Task<OperationResult> Update(MS_Package packageNo)
        {
            var item = await _repoAccessor.MS_Package.FirstOrDefaultAsync(x=> x.Manuf.Trim() == "N" && x.PackageNo== packageNo.PackageNo);
            if(item == null)
            {
                return new OperationResult(false);
            }
            else{
                item.PackageQty=packageNo.PackageQty;
                _repoAccessor.MS_Package.Update(item);
                await _repoAccessor.Save();
            }
            return new OperationResult(true, "Update thành công");
        }
    }

}