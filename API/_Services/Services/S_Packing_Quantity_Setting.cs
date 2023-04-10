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
    public class S_Packing_Quantity_Setting : I_Packing_Quantity_Setting
    {
        private readonly IRepositoryAccessor _repository;

        public S_Packing_Quantity_Setting(IRepositoryAccessor repository){
            _repository = repository;
        }

        public async Task<OperationResult> Create(MS_Package_DTO dto)
        {
            dto.Manuf = "N";
            if(checkExist(dto)){
                return new OperationResult{IsSuccess = false};
            }
            var entity = dto.Map().ToANew<MS_Package>(c => c.MapEntityKeys());
            _repository.MS_Package.Add(entity);

            await _repository.Save();
            return new OperationResult{IsSuccess = true};
        }

        private bool checkExist(MS_Package_DTO dto){
            return _repository.MS_Package.Any(x => x.PackageNo == dto.PackageNo && x.PackageQty == dto.PackageQty);
        }

        public async Task<PaginationUtility<MS_Package_DTO>> Search(PaginationParam pagination, MS_Package_DTO dto)
        {
            var predicate = PredicateBuilder.New<MS_Package>(true);
            if(!string.IsNullOrEmpty(dto.PackageNo))
            predicate.And(x => x.PackageNo == dto.PackageNo);
            if(dto.PackageQty != 0)
            predicate.And(x => x.PackageQty == dto.PackageQty);
            var data = _repository.MS_Package.FindAll(predicate).AsNoTracking().Select(x => new MS_Package_DTO{
                PackageNo = x.PackageNo,
                PackageQty = x.PackageQty,
            });
            return await PaginationUtility<MS_Package_DTO>.CreateAsync(data, pagination.PageNumber, pagination.PageSize);
        }

        public async Task<OperationResult> Update(MS_Package_DTO dto)
        {
            dto.Manuf="N";
            var entity = dto.Map().ToANew<MS_Package>(c => c.MapEntityKeys());
            _repository.MS_Package.Update(entity);

            await _repository.Save();
            return new OperationResult{IsSuccess = true};
        }

        public async Task<MS_Package_DTO> GetDetail(MS_Package_DTO dto)
        {
            var data = await _repository.MS_Package.FindAll(x => x.PackageNo == dto.PackageNo).Select(x => new MS_Package_DTO{
                Manuf = x.Manuf,
                PackageNo = x.PackageNo,
            }).FirstOrDefaultAsync();

            return data;
        }
    }
}