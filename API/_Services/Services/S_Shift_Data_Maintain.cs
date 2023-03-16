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
    public class S_Shift_Data_Maintain : I_Shift_Data_Maintain// anh chỉ cần (ctrl + .)
    {
        // Khai báo Repository Accessor
        private readonly IRepositoryAccessor _repository;
        // Thêm vào hàm khởi tạo
        public S_Shift_Data_Maintain(IRepositoryAccessor repository)
        {
            _repository = repository;
        }


        public async Task<OperationResult> Create(MS_Shift_DTO dto)
        {
            dto.Manuf ="N";
            if(checkExist(dto)){
                return new OperationResult{IsSuccess=false};
            }
            var entity = dto.Map().ToANew<MS_Shift>(c => c.MapEntityKeys());
            _repository.MS_Shift.Add(entity);

            await _repository.Save();
            return new OperationResult{IsSuccess=true};

        }
        private bool checkExist(MS_Shift_DTO dto){
          return  _repository.MS_Shift.Any(x=>x.Shift ==dto.Shift && x.ShiftName==dto.ShiftName);
        }

        public async Task<PaginationUtility<MS_Shift_DTO>> Search(PaginationParam pagination,MS_Shift_DTO dto)
        {
            // Kiểm tra empty
            // cái này dùng thư viện có sẵn là linqKit
            var predicate = PredicateBuilder.New<MS_Shift>(true);
            if(!string.IsNullOrEmpty(dto.Shift))
            predicate.And(x => x.Shift == dto.Shift);
            if(!string.IsNullOrEmpty(dto.ShiftName))
            predicate.And(x => x.ShiftName == dto.ShiftName);
            var data = _repository.MS_Shift.FindAll(predicate).AsNoTracking().Select(x=> new MS_Shift_DTO{ // findAll là lấy tất cả, xong truyền dkien search ở trên xuống
                Shift = x.Shift,
                ShiftName=x.ShiftName
            });
            return await PaginationUtility<MS_Shift_DTO>.CreateAsync(data, pagination.PageNumber, pagination.PageSize);

        }

        public async Task<OperationResult> Update(MS_Shift_DTO dto)
        {
            dto.Manuf="N";
            var entity = dto.Map().ToANew<MS_Shift>(c => c.MapEntityKeys());
            _repository.MS_Shift.Update(entity);

            await _repository.Save();
            return new OperationResult{IsSuccess=true};
        }

        // Thao tác xử lý logic ở đây a, vd giờ e lấy chi tiết a xem
        public async Task<MS_Shift_DTO> GetDetail(MS_Shift_DTO dto)
        {
            var data = await _repository.MS_Shift.FindAll(x=>x.Shift == dto.Shift).Select(x=> new MS_Shift_DTO{
                Manuf = x.Manuf,
                Shift = x.Shift,
            }).FirstOrDefaultAsync();

            return data;
        }
        // private async bool checkExist(){

        //     return await _repository.Save();
        // }
    }
}