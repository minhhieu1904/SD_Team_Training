using AgileObjects.AgileMapper;
using API._Repositories;
using API._Services.Interfaces.Maintain;
using API.DTOs.Maintain;
using API.Models;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using SDCores;

namespace API._Services.Services.Maintain
{
    public class S_1_2_WarehouseBasicDataMaintenance : I_1_2_WarehouseBasicDataMaintenance
    {
        // Khai báo Repository Accessor
        private readonly IRepositoryAccessor _repository;
        private readonly IConfiguration _configuration;
        private readonly string _manuf;
        // Thêm vào hàm khởi tạo
        public S_1_2_WarehouseBasicDataMaintenance(
            IRepositoryAccessor repository,
            IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
            _manuf = _configuration.GetSection("Appsettings:FactoryCode").Value;
        }

        #region Search
        public async Task<PaginationUtility<WarehouseDto>> GetDataPagination(PaginationParam pagination, WarehouseParam param)
        {
            //Tạo biểu thức tìm kiếm(Predicate)
            var pred = PredicateBuilder.New<MS_Location>(true); //PredicateBuilder dùng để xây dựng các biểu thức tìm kiếm động

            //Kiểm tra dữ liệu
            if (!string.IsNullOrWhiteSpace(param.StoreH))
                pred.And(x => x.StoreH == param.StoreH);
            if (!string.IsNullOrWhiteSpace(param.LocationName))
                pred.And(x => x.LocationName == param.LocationName);

            //Tạo truy vấn cơ sở dữ liệu
            var query = _repository.MS_Location.FindAll(pred).AsNoTracking();

            //Thực hiện câu truy vấn và chuyển đổi
            var data = await query.Select(x => new WarehouseDto
            {
                Manuf = x.Manuf,
                StoreH = x.StoreH,
                LocationName = x.LocationName
            }).ToListAsync();

            //Tạo và trả về đối tượng Pagination để phân trang
            return PaginationUtility<WarehouseDto>.Create(data, pagination.PageNumber, pagination.PageSize);

        }
        #endregion

        #region Add New
        public async Task<OperationResult> AddNew(WarehouseDto data)
        {
            data.Manuf = _manuf;
            //Kiểm tra input không được để trống
            if (string.IsNullOrWhiteSpace(data.StoreH))
                return new OperationResult(false, "Kode Of Warehouse cannot be empty or whitespace.");
            if (string.IsNullOrWhiteSpace(data.LocationName))
                return new OperationResult(false, "Name Of Warehouse cannot be empty or whitespace.");

            //Kiểm tra dữ liệu dựa trên khóa chính
            if (await _repository.MS_Location.AnyAsync(x => x.Manuf.Trim() == _manuf.Trim() && x.StoreH.Trim() == data.StoreH.Trim()))
                //Xuất thông báo
                return new OperationResult(false, "Shift already exist");

            //Tạo mới
            var item = Mapper.Map(data).ToANew<MS_Location>(x => x.MapEntityKeys());

            //Thêm vào cơ sở dữ liệu
            _repository.MS_Location.Add(item);

            //Lưu thay đổi
            await _repository.Save();
            //Trả kết quả thành công
            return new OperationResult(true, "Add Successfully");
        }
        #endregion

        #region Edit
        public async Task<OperationResult> Edit(WarehouseDto data)
        {
            //Kiểm tra input không được để trống
            if (string.IsNullOrWhiteSpace(data.LocationName))
                return new OperationResult(false, "Name Of Warehouse cannot be empty or whitespace.");

            //Tạo mới
            var item = Mapper.Map(data).ToANew<MS_Location>(x => x.MapEntityKeys());

            //Cập nhật dữ liệu
            _repository.MS_Location.Update(item);

            //Lưu thay đổi
            await _repository.Save();

            //Trả kết quả thành công
            return new OperationResult(true, "Update Succesfully");
        }
        #endregion

    }
}