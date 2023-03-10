
using AgileObjects.AgileMapper;
using API._Repositories;
using API._Services.Interfaces;
using API.Models;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using SD3_API.Helpers.Utilities;

namespace API._Services.Services
{
    public class WarehouseBasicdataService : IWarehouseBasicdataService
    {
         private readonly IRepositoryAccessor _repoAccessor;

        public WarehouseBasicdataService(IRepositoryAccessor repoAccessor)
        {
            _repoAccessor = repoAccessor;
        }
        public async Task<OperationResult> Add(MS_Location location)
        {
            var data = await _repoAccessor.MS_Location.FindSingle(x => x.StoreH==location.StoreH);
            if(data != null)
            {
                return new OperationResult(false, "đã tồn tại");
            }
            else{
                //truyền dữ liệu location vào models
                var item = Mapper.Map(location).ToANew<MS_Location>(x => x.MapEntityKeys());
                _repoAccessor.MS_Location.Add(item);
            }
            await _repoAccessor.Save();
            return new OperationResult(true, "add successfully");
        }

        public async Task<PaginationUtility<MS_Location>> LoadData(PaginationParam paginationParams, string location, string locationName)
        {
            var pred = PredicateBuilder.New<MS_Location>(true);

            if(!string.IsNullOrEmpty(location))
            {
                pred.And(x=> x.StoreH == location.Trim());
            }
            
            if(!string.IsNullOrEmpty(locationName))
            {
                pred.And(x=> x.LocationName == locationName.Trim());
            }
            var data = await _repoAccessor.MS_Location.FindAll(pred).ToListAsync();
            return PaginationUtility<MS_Location>.Create(data, paginationParams.PageNumber, paginationParams.PageSize, true);

        }

        public async Task<OperationResult> Update(MS_Location location)
        {
            var item = await _repoAccessor.MS_Location.FirstOrDefaultAsync(x=>x.Manuf.Trim() == "N"  && x.StoreH == location.StoreH);
            if (item == null)
            {
                return new OperationResult(false);
            }
            else{
                item.LocationName= location.LocationName.Trim();
                _repoAccessor.MS_Location.Update(item);
                await _repoAccessor.Save();
            }
            return new OperationResult(true,"Update successfully");
        }
    }
}