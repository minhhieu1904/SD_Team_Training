using AgileObjects.AgileMapper.Extensions;
using API._Repositories;
using API._Services.Interfaces;
using API.DTOs;
using API.Models;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using SD3_API.Helpers.Utilities;

namespace API._Services.Services
{
    public class S_Warehouse_Basic_Data_Maintenance : I_Warehouse_Basic_Data_Maintenance
    {
        private readonly IRepositoryAccessor _repository;
        public S_Warehouse_Basic_Data_Maintenance(IRepositoryAccessor repository)
        {
            _repository = repository;
        }


        public async Task<OperationResult> Create(MS_Location_DTO dto)
        {
            dto.Manuf = "N";
            if(checkExist(dto)){
                return new OperationResult{IsSuccess=false};
            }
            var entity = dto.Map().ToANew<MS_Location>(c => c.MapEntityKeys());
            _repository.MS_Location.Add(entity);

            await _repository.Save();
            return new OperationResult{IsSuccess=true};
        }

        private bool checkExist(MS_Location_DTO dto)
        {
            return _repository.MS_Location.Any(x=>x.StoreH ==dto.StoreH && x.LocationName==dto.LocationName);
        }

        public async Task<OperationResult> Update(MS_Location_DTO dto)
        {
            dto.Manuf = "N";
            if(checkExist(dto)){
                return new OperationResult{IsSuccess=false};
            }
            var entity = dto.Map().ToANew<MS_Location>(c => c.MapEntityKeys());
            _repository.MS_Location.Add(entity);

            await _repository.Save();
            return new OperationResult{IsSuccess=true};
        }

        public async Task<PaginationUtility<MS_Location_DTO>> Search(PaginationParam pagination, MS_Location_DTO dto)
        {
            // manuf no ko dc null, luc nay can gan lại cho nó giống thằng crate ở trên
             dto.Manuf = "N";
            var predicate = PredicateBuilder.New<MS_Location>(true);
            if(!string.IsNullOrEmpty(dto.StoreH))
            predicate.And(x => x.StoreH == dto.StoreH);
            if(!string.IsNullOrEmpty(dto.LocationName))
            predicate.And(x => x.LocationName == dto.LocationName);
            var data = _repository.MS_Location.FindAll(predicate).AsNoTracking().Select(x=> new MS_Location_DTO{
                StoreH = x.StoreH,
                LocationName = x.LocationName
            });

            return await PaginationUtility<MS_Location_DTO>.CreateAsync(data, pagination.PageNumber, pagination.PageSize);
        }

        public async Task<MS_Location_DTO> GetDetail(MS_Location_DTO dto)
        {
            var data = await _repository.MS_Location.FindAll(x => x.StoreH == dto.StoreH).Select(x => new MS_Location_DTO{
                Manuf = x.Manuf,
                StoreH = x.StoreH,
            }).FirstOrDefaultAsync();

            return data;
        }
    }
}