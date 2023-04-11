
using AgileObjects.AgileMapper;
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
    public class S_UserRoleServices : I_UserRoleServices
    {
        // Khai báo Repository Accessor
        private readonly IRepositoryAccessor _repository;
        // Thêm vào hàm khởi tạo
        public S_UserRoleServices(IRepositoryAccessor repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Create(UserDTO user)
        {
            var data = await _repository.Users.FindSingle(x => x.account == user.account);
            if (data != null)
            {
                return new OperationResult(false, "đã tồn tại");
            }
            else
            {
                var item = Mapper.Map(user).ToANew<Users>(x => x.MapEntityKeys());
                _repository.Users.Add(item);
                await _repository.Save();
            }
            return new OperationResult(true, "Add thành công");
        }

        public async Task<UserRoleDTO> GetAllRoleByAccount(string account)
        {
            if(account == null)
           return new UserRoleDTO();
            var user = await _repository.Users.FirstOrDefaultAsync(x => x.account == account.Trim());
             if (user == null)
                return new UserRoleDTO();

            var allRole = await _repository.Roles.FindAll().Select(x => new RoleDTO()
            {
                user_account = user.name,
                role_unique = x.role_unique,
                role_sequence = x.role_sequence,
                role_name = x.role_name,
                IsCheck = false
            })
            .OrderBy(x => x.role_sequence)
            .ToListAsync();

            var roleOfUser = _repository.RoleUser.FindAll(x => x.user_account == account).ToList();

            foreach (var role in roleOfUser)
            {
                var check = allRole.Where(x => x.role_unique == role.role_unique).FirstOrDefault();
                if (check != null)
                    check.IsCheck = true;
            }
            return new UserRoleDTO() { Account = user.account, ListRoles = allRole };
        }

        public async Task<PaginationUtility<Users>>  Search(PaginationParam paginationParams, string account, string name)
        {
            var pred = PredicateBuilder.New<Users>(true);

            if (!string.IsNullOrEmpty(account))
                pred.And(x => x.account == account.Trim());

            if (!string.IsNullOrEmpty(name))
                pred.And(x => x.name.Contains(name.Trim()));
            var data = await _repository.Users.FindAll(pred).ToListAsync();
            return PaginationUtility<Users>.Create(data, paginationParams.PageNumber, paginationParams.PageSize);
        }

        public async Task<OperationResult> Update(UserDTO user)
        {
            var item = await _repository.Users.FirstOrDefaultAsync(x => x.account == user.account && x.name == user.name);
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
                _repository.Users.Update(item);
                await _repository.Save();
            }
            return new OperationResult(true, "Update successfully");
        }

        public async Task<OperationResult> UpdateAuthorization(UserRoleDTO authors)
        {
            var rolesUserFind = await _repository.RoleUser.FindAll(x => x.user_account.Trim() == authors.Account.Trim()).ToListAsync();
            if (rolesUserFind.Any())
            {
                _repository.RoleUser.RemoveMultiple(rolesUserFind);
            }
            var authorsNew = new List<RoleUserDTO>();

            foreach (var item in authors.ListRoles)
            {
                if (item.IsCheck == true)
                {
                    var roleUserItem = new RoleUserDTO();
                    roleUserItem.user_account = authors.Account;
                    roleUserItem.role_unique = item.role_unique;
                    roleUserItem.create_by = "Admin";
                    roleUserItem.create_time = DateTime.Now;
                    authorsNew.Add(roleUserItem);
                }
            }
            var data = Mapper.Map(authorsNew).ToANew<List<RoleUser>>(x => x.MapEntityKeys());
            _repository.RoleUser.AddMultiple(data);
            var save = await _repository.Save();
            return new OperationResult(true, "Update successfully");
        }
    }
}