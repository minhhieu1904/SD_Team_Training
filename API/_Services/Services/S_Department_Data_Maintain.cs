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
    public class S_Department_Data_Maintain : I_Department_Data_Maintain
    {
        private readonly IRepositoryAccessor _repository;
        public S_Department_Data_Maintain(IRepositoryAccessor repository){
            _repository = repository;
        }

        private bool checkExist(MS_Department_DTO dto){
            return _repository.MS_Department.Any(x => x.ParNo == dto.ParNo && x.ParName == dto.ParName);
        }

        public async Task<OperationResult> Create(MS_Department_DTO dto)
        {
            dto.Manuf = "N";
            if(checkExist(dto)){
                return new OperationResult{IsSuccess = false};
            }
            var entity = dto.Map().ToANew<MS_Department>(c => c.MapEntityKeys());
            _repository.MS_Department.Add(entity);

            await _repository.Save();
            return new OperationResult{IsSuccess = true};
        }

        public async Task<PaginationUtility<MS_Department_DTO>> Search(PaginationParam pagination, MS_Department_DTO dto)
        {
            var predicate = PredicateBuilder.New<MS_Department>(true);
            if(!string.IsNullOrEmpty(dto.ParNo))
            predicate.And(x => x.ParNo == dto.ParNo);
            if(!string.IsNullOrEmpty(dto.ParName))
            predicate.And(x => x.ParName == dto.ParName);
            var data = _repository.MS_Department.FindAll(predicate).AsNoTracking().Select(x => new MS_Department_DTO{
                ParNo = x.ParNo,
                ParName = x.ParName
            });
            return await PaginationUtility<MS_Department_DTO>.CreateAsync(data, pagination.PageNumber, pagination.PageSize);
        }

        public async Task<OperationResult> Update(MS_Department_DTO dto)
        {
            dto.Manuf = "N";
            var entity = dto.Map().ToANew<MS_Department>(c => c.MapEntityKeys());
            _repository.MS_Department.Add(entity);

            await _repository.Save();
            return new OperationResult{IsSuccess = true};
        }

        public async Task<MS_Department_DTO> GetDetail(MS_Department_DTO dto)
        {
            var data = await _repository.MS_Department.FindAll(x => x.ParNo == dto.ParNo).Select(x => new MS_Department_DTO{
                Manuf = x.Manuf,
                ParNo = x.ParNo,
            }).FirstOrDefaultAsync();
            return data;
        }
    }
}