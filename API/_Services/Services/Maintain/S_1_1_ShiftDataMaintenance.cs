using AgileObjects.AgileMapper;
using API._Repositories;
using API._Services.Interfaces.Maintain;
using API.DTOs.Maintain;
using API.Models;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using SDCores;

namespace API._Services.Services
{
    public class S_1_1_ShiftDataMaintenance : I_1_1_ShiftDataMaintenance
    {
        // Khai báo Repository Accessor
        private readonly IRepositoryAccessor _repository;
        private readonly IConfiguration _configuration;
        private readonly string _manuf;
        // Thêm vào hàm khởi tạo
        public S_1_1_ShiftDataMaintenance(
            IRepositoryAccessor repository,
            IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
            _manuf = _configuration.GetSection("Appsettings:FactoryCode").Value;
        }

        #region Search
        public async Task<PaginationUtility<ShiftDataMaintenanceDto>> GetDataPagination(PaginationParam pagination, ShiftDataMaintenanceParam param)
        {
            //Tạo biểu thức tìm kiếm(Predicate)
            var pred = PredicateBuilder.New<MS_Shift>(true); //PredicateBuilder dùng để xây dựng các biểu thức tìm kiếm động

            //Kiểm tra dữ liệu
            if (!string.IsNullOrWhiteSpace(param.Shift))
                pred.And(x => x.Shift == param.Shift);
            if (!string.IsNullOrWhiteSpace(param.ShiftName))
                pred.And(x => x.ShiftName == param.ShiftName);

            //Tạo truy vấn cơ sở dữ liệu
            var query = _repository.MS_Shift.FindAll(pred).AsNoTracking();

            //Thực hiện câu truy vấn và chuyển đổi
            var data = await query.Select(x => new ShiftDataMaintenanceDto
            {
                Manuf = x.Manuf,
                Shift = x.Shift,
                ShiftName = x.ShiftName
            }).ToListAsync();

            //Tạo và trả về đối tượng Pagination để phân trang
            return PaginationUtility<ShiftDataMaintenanceDto>.Create(data, pagination.PageNumber, pagination.PageSize);
        }
        #endregion

        #region Add New
        public async Task<OperationResult> AddNew(ShiftDataMaintenanceDto data)
        {
            data.Manuf = _manuf;
            //Kiểm tra input không được để trống
            if (string.IsNullOrWhiteSpace(data.Shift))
                return new OperationResult(false, "Shift cannot be empty or whitespace.");
            if (string.IsNullOrWhiteSpace(data.ShiftName))
                return new OperationResult(false, "Shift Name cannot be empty or whitespace.");

            //Kiểm tra dữ liệu dựa trên khóa chính
            if (await _repository.MS_Shift.AnyAsync(x => x.Manuf.Trim() == _manuf.Trim() && x.Shift.Trim() == data.Shift.Trim()))
                //Xuất thông báo
                return new OperationResult(false, "Shift already exist");

            //Tạo mới
            var item = Mapper.Map(data).ToANew<MS_Shift>(x => x.MapEntityKeys());

            //Thêm vào cơ sở dữ liệu
            _repository.MS_Shift.Add(item);

            //Lưu thay đổi
            await _repository.Save();
            //Trả kết quả thành công
            return new OperationResult(true, "Add Successfully");
        }
        #endregion

        #region Edit
        public async Task<OperationResult> Edit(ShiftDataMaintenanceDto data)
        {
            if (string.IsNullOrWhiteSpace(data.ShiftName))
                return new OperationResult(false, "Shift Name cannot be empty or whitespace.");

            //Tạo mới
            var item = Mapper.Map(data).ToANew<MS_Shift>(x => x.MapEntityKeys());

            //Cập nhật dữ liệu
            _repository.MS_Shift.Update(item);

            //Lưu thay đổi
            await _repository.Save();

            //Trả kết quả thành công
            return new OperationResult(true, "Update Succesfully");
        }
        #endregion
    }
}