
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
    public class S_AuthorizationSetting : I_AuthorizationSetting
    {
        public readonly IRepositoryAccessor _reposioryAccessor;
        public S_AuthorizationSetting(IRepositoryAccessor reposioryAccessor)
        {
            this._reposioryAccessor = reposioryAccessor;
        }
        public async Task<PaginationUtility<Users>> GetData(PaginationParam pagination, string account, string name)
        {
            var pred_Users = PredicateBuilder.New<Users>(true);
            if (!string.IsNullOrEmpty(account))
                pred_Users.And(x => x.account == account.Trim());
            if (!string.IsNullOrEmpty(name))
                pred_Users.And(x => x.name == name.Trim());

            var data = _reposioryAccessor.Users.FindAll(pred_Users);


            return await PaginationUtility<Users>.CreateAsync(data, pagination.PageNumber, pagination.PageSize);
        }
        public async Task<OperationResult> Addnew(Users model)
        {
            var originalItem = await _reposioryAccessor.Users.FindAll(x => x.account == model.account).FirstOrDefaultAsync();
            if (originalItem != null)
            {
                return new OperationResult(false);
            }
            else
            {
                _reposioryAccessor.Users.Add(model);
                if (await _reposioryAccessor.Save())
                {
                    return new OperationResult(true);
                }
                return new OperationResult(false);
            }
        }



        public async Task<Users> GetDataOnly(string account)
        {
            return await _reposioryAccessor.Users.FirstOrDefaultAsync(item => item.account.ToUpper() == account.ToUpper());

        }

        public async Task<OperationResult> Update(Users model)
        {
            var originalItem = await _reposioryAccessor.Users.FirstOrDefaultAsync(x => x.account.Trim() == model.account.Trim() && x.name.Trim() == model.name.Trim());
            if (originalItem == null)
                return new OperationResult(false);

            originalItem.password = model.password;
            originalItem.email = model.email;
            originalItem.is_active = model.is_active;
            _reposioryAccessor.Users.Update(originalItem);

            if (await _reposioryAccessor.Save())
            {
                return new OperationResult(true);
            }
            return new OperationResult(false);
        }

        public async Task<UserRoleDTO> GetAllRoleByAccount(string account)
        {
            var user = await _reposioryAccessor.Users.FirstOrDefaultAsync(x => x.account == account.Trim());
            // // Nếu user không tồn tại trả về lỗi
            if (user == null)
                return new UserRoleDTO();

            // lấy tất cả các quyền trong role
            var allRole = await _reposioryAccessor.Roles.FindAll().Select(x => new RoleDto()
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
            var roleOfUser = _reposioryAccessor.RoleUser.FindAll(x => x.user_account == user.account)
                .Select(x => x.role_unique)
                .ToList();

            // join lại lấy các quyeenf có trong user và ko có trong role 
            foreach (var role in roleOfUser)
            {
                var check = allRole.Where(x => x.role_unique == role).FirstOrDefault();
                // Nếu roleOfUser có thằng role thì , user đó có quyền 
                if (check != null)
                    check.IsCheck = true;
            }
            return new UserRoleDTO() { Account = user.account, ListRoles = allRole };
        }

        public async Task<OperationResult> UpdateAuthorization(UserRoleDTO authors)
        {
            //tìm điều kiện account của 2 bảng rán vào rolesuserfind
            var rolesUserFind = await _reposioryAccessor.RoleUser.FindAll(x => x.user_account.Trim() == authors.Account.Trim()).AsNoTracking().ToListAsync();
            // nếu rolesuserfind trả về true or false nếu có  thì xóa giá trị trong rolesuserfind
            if (rolesUserFind.Any())
            {
                _reposioryAccessor.RoleUser.RemoveMultiple(rolesUserFind);
            }
            //khai báo biến mới 
            var authorsNew = new List<RoleUser>();

            foreach (var item in authors.ListRoles)
            {
                if (item.IsCheck == true)
                {
                    var roleUserItem = new RoleUser();
                    //tìm account của thằng RoleUserDTO = giá trị của thằng UserRoleDTO
                    roleUserItem.user_account = authors.Account;
                    //tìm role_unique của thằng RoleUserDTO = giá trị  ListRoles
                    roleUserItem.role_unique = item.role_unique;
                    //gán giá trị mặc định chi create_by là admin
                    roleUserItem.create_by = "Admin";
                    //gán giá trị mặc định chi create_time là ngày hiện tại chỉnh sửa
                    roleUserItem.create_time = DateTime.Now;
                    //thêm roleUserItem vào biến authorsNew
                    authorsNew.Add(roleUserItem);
                }
            }
            _reposioryAccessor.RoleUser.AddMultiple(authorsNew);
            await _reposioryAccessor.Save();
            return new OperationResult(true, "Update successfully");


        }
    }





}