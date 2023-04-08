
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
    public class UserRoleServices : IUserRoleServices
    {
        private readonly IRepositoryAccessor _repoAccessor;

        public UserRoleServices(IRepositoryAccessor repoAccessor)
        {
            _repoAccessor = repoAccessor;
        }

        public async Task<OperationResult> Add(UserDTO user)
        {
            var data = await _repoAccessor.Users.FindSingle(x => x.account == user.account);
            if (data != null)
            {
                return new OperationResult(false, "đã tồn tại");
            }
            else
            {
                var item = Mapper.Map(user).ToANew<Users>(x => x.MapEntityKeys());
                _repoAccessor.Users.Add(item);
                await _repoAccessor.Save();
            }
            return new OperationResult(true, "Add thành công");
        }

        // cách sử dụng GroupJoin đó
        public async Task<UserRoleDTO> GetAllRoleByAccount(string account)
        {

               var user = await _repoAccessor.Users.FirstOrDefaultAsync(x => x.account == account.Trim());
            // // Nếu user không tồn tại trả về lỗi
             if (user == null)
                return new UserRoleDTO();

            // lấy tất cả các quyền trong role
            var allRole = await _repoAccessor.Roles.FindAll().Select(x => new RoleDto()
            {
                role_unique = x.role_unique,
                role_sequence = x.role_sequence,
                role_name = x.role_name,
                IsCheck = false
            })
            //sắp xếp theo role_sequence
            .OrderBy(x => x.role_sequence)
            .ToListAsync();

            // lấy tất cả các quyền của user
            var roleOfUser = _repoAccessor.RoleUser.FindAll(x => x.user_account == account).ToList();

            // join lại lấy các quyeenf có trong user và ko có trong role 
            foreach (var role in roleOfUser)
            {
                var check = allRole.Where(x => x.role_unique == role.role_unique).FirstOrDefault();
                // Nếu roleOfUser có thằng role thì , user đó có quyền 
                if (check != null)
                    check.IsCheck = true;
            }
            return new UserRoleDTO() { Account = user.account, ListRoles = allRole };

            
            
          
        }
        public async Task<PaginationUtility<Users>> LoadData(PaginationParam paginationParams, string account, string name)
        {
            var pred = PredicateBuilder.New<Users>(true);

            if (!string.IsNullOrEmpty(account))
                pred.And(x => x.account == account.Trim());

            if (!string.IsNullOrEmpty(name))
                pred.And(x => x.name.Contains(name.Trim()));
            var data = await _repoAccessor.Users.FindAll(pred).ToListAsync();
            return PaginationUtility<Users>.Create(data, paginationParams.PageNumber, paginationParams.PageSize);
        }
        public async Task<OperationResult> Update(UserDTO user)
        {
            var item = await _repoAccessor.Users.FirstOrDefaultAsync(x => x.account == user.account && x.name == user.name);
            if (item == null)
            {
                return new OperationResult(false);
            }
            else
            {
                item.account = user.account.Trim();
                item.name = user.name.Trim();
                item.password = user.password.Trim();
                item.email = user.email.Trim();
                item.is_active = user.is_active;
                _repoAccessor.Users.Update(item);
                await _repoAccessor.Save();
            }
            return new OperationResult(true, "Update successfully");
        }
        public async Task<OperationResult> UpdateAuthorization(UserRoleDTO authors)
        {

            //tìm điều kiện account của 2 bảng rán vào rolesuserfind
            var rolesUserFind = await _repoAccessor.RoleUser.FindAll(x => x.user_account.Trim() == authors.Account.Trim()).ToListAsync();
            // nếu rolesuserfind trả về true or false nếu có  thì xóa giá trị trong rolesuserfind
            if (rolesUserFind.Any())
            {
                _repoAccessor.RoleUser.RemoveMultiple(rolesUserFind);
            }
            //khai báo biến mới 
            var authorsNew = new List<RoleUserDTO>();

            foreach (var item in authors.ListRoles)
            {
                if(item.IsCheck == true)
                {
                    var roleUserItem = new RoleUserDTO();
                    //tìm account của thằng RoleUserDTO = giá trị của thằng UserRoleDTO
                    roleUserItem.user_account = authors.Account;
                    //tìm role_unique của thằng RoleUserDTO = giá trị  ListRoles
                    roleUserItem.role_unique = item.role_name;
                    roleUserItem.create_by = item.user_account;
                    //gán giá trị mặc định chi create_time là ngày hiện tại chỉnh sửa
                    roleUserItem.create_time = DateTime.Now;
                    //thêm roleUserItem vào biến authorsNew
                    authorsNew.Add(roleUserItem);
                }
            }
            var data = Mapper.Map(authorsNew).ToANew<List<RoleUser>>(x => x.MapEntityKeys());
            _repoAccessor.RoleUser.AddMultiple(data);
            var save = await _repoAccessor.Save();
            return new OperationResult(true, "Update successfully");

        }
    }
}
