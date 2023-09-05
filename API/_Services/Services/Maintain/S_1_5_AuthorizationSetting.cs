using AgileObjects.AgileMapper;
using API._Repositories;
using API._Services.Interfaces.Maintain;
using API.Dtos.Maintain.AuthorizationSetting;
using API.DTOs.Maintain;
using API.Models;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using SDCores;

namespace API._Services.Services.Maintain
{
    public class S_1_5_AuthorizationSetting : I_1_5_AuthorizationSetting
    {
        // Khai báo Repository Accessor
        private readonly IRepositoryAccessor _repository;
        // Thêm vào hàm khởi tạo
        public S_1_5_AuthorizationSetting(
            IRepositoryAccessor repository)
        {
            _repository = repository;
        }

        #region Search
        public async Task<PaginationUtility<AuthorizationDto>> GetDataPagination(PaginationParam pagination, AuthorizationParam param)
        {
            //Tạo biểu thức tìm kiếm(Predicate)
            var pred = PredicateBuilder.New<Users>(x => true); //Biểu thức tìm kiếm ban đầu, với điều kiện luôn đúng

            //Kiểm tra dữ liệu
            if (!string.IsNullOrWhiteSpace(param.account))
                pred = pred.And(x => x.account == param.account); //Thêm điều kiện tìm kiếm
            if (!string.IsNullOrWhiteSpace(param.name))
                pred = pred.And(x => x.name == param.name); //Thêm điều kiện tìm kiếm

            //Tạo truy vấn cơ sở dữ liệu
            var data = _repository.Users
                        .FindAll(pred)  //Truy vấn các bản ghi thỏa mãn điều kiện pred
                        .Select(user => new AuthorizationDto
                        {
                            account = user.account,
                            password = user.password,
                            name = user.name,
                            email = user.email,
                            is_active = user.is_active,
                            update_by = user.update_by,
                            update_time = user.update_time
                        }).AsNoTracking(); //Ánh xạ các trường dữ liệu cần lấy vào đối tượng Dto

            //Tạo và trả về đối tượng phân trang dựa trên dữ liệu và thông tin phân trang đã cho
            return await PaginationUtility<AuthorizationDto>.CreateAsync(data, pagination.PageNumber, pagination.PageSize);
        }
        #endregion

        #region Add New
        public async Task<OperationResult> AddNew(AuthorizationDto data)
        {
            //Kiểm tra input không được để trống
            if (string.IsNullOrWhiteSpace(data.account))
                return new OperationResult(false, "User Account is required.");
            if (string.IsNullOrWhiteSpace(data.name))
                return new OperationResult(false, "User Name is required.");
            if (string.IsNullOrWhiteSpace(data.password))
                return new OperationResult(false, "Password is required.");

            //Kiểm tra dữ liệu dựa trên khóa chính
            bool dataExists = await _repository.Users.AnyAsync(x => x.account.Trim() == data.account.Trim());
            if (dataExists)
                //Xuất thông báo
                return new OperationResult(false, "Data already exist");

            //Tạo mới
            var item = Mapper.Map(data).ToANew<Users>(x => x.MapEntityKeys());

            //Thêm vào cơ sở dữ liệu
            _repository.Users.Add(item);

            //Lưu thay đổi
            await _repository.Save();
            //Trả kết quả thành công
            return new OperationResult(true, "Add Successfully");
        }
        #endregion

        #region Edit
        public async Task<OperationResult> Edit(AuthorizationDto data)
        {
            //Dữ liệu bắt buộc nhập
            if (string.IsNullOrWhiteSpace(data.password))
                return new OperationResult(false, "Password is required.");

            //Tạo mới
            var item = Mapper.Map(data).ToANew<Users>(x => x.MapEntityKeys());

            //Cập nhật dữ liệu
            _repository.Users.Update(item);

            //Lưu thay đổi
            await _repository.Save();

            //Trả kết quả thành công
            return new OperationResult(true, "Update Succesfully");
        }
        #endregion

        #region Get Role User
        public async Task<List<RolesUserStatus>> GetRoleUser(string account)
        {
            //Lấy tất cả các Roles
            var roles = _repository.Roles.FindAll();
            //Lấy tất cả các RoleUser dựa theo account
            var userRoles = _repository.RoleUser.FindAll(x => x.user_account.Trim() == account.Trim());
            //Kết hợp roles vs userRoles dựa trên trường role_unique
            var result = await roles.GroupJoin(
                userRoles,
                x => x.role_unique, //role_unique trong Roles
                y => y.role_unique, //role_unique trong RoleUser
                (x, y) => new { Roles = x, UserRoles = y } //Tạo bảng ghi kết hợp giữa roles và userRoles
            )
            //Mở rộng tập hợp con và tạo danh sách bản ghi mới
            .SelectMany(
                x => x.UserRoles.DefaultIfEmpty(), //Lấy tất cả Roles của UserRoles kể cả rỗng
                (x, y) => new RolesUserStatus()
                {
                    user_account = account,
                    role_name = x.Roles.role_unique,
                    role_unique = x.Roles.role_unique,
                    role_sequence = x.Roles.role_sequence,
                    status = y != null //Kiểm tra userRoles có hay không
                })
            //Sắp xếp kết quả theo trường role_sequence
            .OrderBy(x => x.role_sequence).ToListAsync(); //ToListAsync() để chuyển về danh sách

            return result;
        }
        #endregion

        #region EditRoleUser
        public async Task<bool> EditRoleUser(List<RolesUserStatus> roles, string updateBy)
        {
            //Lấy user_account và thời gian hiện tại
            var user_account = roles.First().user_account.Trim();
            var now = DateTime.Now;

            //Lấy danh sách RoleUser hiện tại dựa trên user_account
            var roleUserCurrent = (await _repository.RoleUser
                .FindAll(x => x.user_account.Trim() == user_account)
                .ToListAsync());

            //Duyệt qua danh sách roles được truyền vào
            foreach (var author in roles)
            {
                //Lấy role_unique từ phần tử hiện tại trong danh sách roles
                var role_unique = author.role_unique.Trim();
                //Tìm roleUser trong danh sách hiện tại dựa trên role_unique
                var roleUser = roleUserCurrent.FirstOrDefault(x => x.role_unique.Trim() == role_unique);

                //Nếu status là true và roleUser == null thì thực hiện thêm mới
                if (author.status && roleUser == null)
                {
                    //Tạo một đối tượng roleUserAdd mới bằng cách ánh xạ từ author
                    var roleUserAdd = Mapper.Map(author).ToANew<RoleUser>(x => x.MapEntityKeys());

                    //Thêm create_by và create_time
                    roleUserAdd.create_by = updateBy;
                    roleUserAdd.create_time = now;

                    //Thêm đối tượng RoleUser mới vào repository
                    _repository.RoleUser.Add(roleUserAdd);
                }
                //Nếu status là false và RoleUser tồn tại, Remove
                else if (!author.status && roleUser != null)
                {
                    _repository.RoleUser.Remove(roleUser);
                }
            }

            return await _repository.Save();
        }
        #endregion
    }
}