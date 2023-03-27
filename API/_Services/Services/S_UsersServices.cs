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
    public class SUsersServices : IUsersServices
    {
        private readonly IRepositoryAccessor _repositoryAccessor;
        public SUsersServices(IRepositoryAccessor repositoryAccessor)
        {
            _repositoryAccessor = repositoryAccessor;
        }

        public async Task<OperationResult> AddNew(Users model)
        {
            var item = await _repositoryAccessor.Users.FirstOrDefaultAsync(x => x.Account.Trim() == model.Account.Trim() && x.Name == model.Name);
            if (item != null)
                return new OperationResult(false);
            _repositoryAccessor.Users.Add(model);
            if (await _repositoryAccessor.Save())
                return new OperationResult(true);
            return new OperationResult(false);
        }

        public async Task<PaginationUtility<Users>> GetDataPaing(PaginationParam param, string account, string name)
        {
            var data = await _repositoryAccessor.Users.FindAll().ToListAsync();
            if (!string.IsNullOrEmpty(account))
                data = data.Where(x => x.Account == account).ToList();
            if (!string.IsNullOrEmpty(name))
                data = data.Where(x => x.Name.Contains(name)).ToList();
            return PaginationUtility<Users>.Create(data, param.PageNumber, param.PageSize);
        }

        public async Task<Users> GetItem(string account, string name)
        {
            return await _repositoryAccessor.Users.FirstOrDefaultAsync(x => x.Account == account && x.Name == name);
        }

        public async Task<OperationResult> Update(Users model)
        {
            var item = await _repositoryAccessor.Users.FirstOrDefaultAsync(x => x.Account == model.Account && x.Name == model.Name);
            if (item == null)
                return new OperationResult(false);
            item.Password = model.Password;
            item.Email = model.Email;
            item.IsActive = model.IsActive;
            _repositoryAccessor.Users.Update(item);
            if (await _repositoryAccessor.Save())
                return new OperationResult(true);
            return new OperationResult(false);
        }

        public async Task<OperationResult> UpdateAuthorization(UserRoleDTO authors)
        {
            //Tìm điều kiện account của 2 bảng gán vào roleuserfind
            var roleUserFind = await _repositoryAccessor.RoleUser.FindAll(x => x.UserAccount.Trim() == authors.Account.Trim()).ToListAsync();
            if (roleUserFind.Any())
            {
                _repositoryAccessor.RoleUser.RemoveMultiple(roleUserFind);
            }
            //Khai báo biến mới
            var authorsNew = new List<RoleUser>();
            foreach (var item in authors.ListRoles)
            {
                if (item.IsCheck == true)
                {
                    var roleUserItem = new RoleUser()
                    {
                        UserAccount = authors.Account,
                        RoleUnique = item.role_unique,
                        CreateBy = "Admin",
                        CreateTime = DateTime.Now
                    };
                    authorsNew.Add(roleUserItem);
                }
            }
            _repositoryAccessor.RoleUser.AddMultiple(authorsNew);
            await _repositoryAccessor.Save();
            return new OperationResult(true, "Update successfully");
        }


        // Cách sử dụng GroupJoin
        public async Task<UserRoleDTO> GetAllRoleByAccount(string account)
        {
            var user = await _repositoryAccessor.Users.FirstOrDefaultAsync(x => x.Account == account.Trim());
            // Neu user khong ton tai tra ve loi
            if (user == null)
                return new UserRoleDTO();
            // lay tat ca cac quyen trong role
            var allRole = await _repositoryAccessor.Roles.FindAll().Select(x => new RoleDto()
            {
                role_unique = x.RoleUnique,
                role_sequence = x.RoleSequence,
                role_name = x.RoleName,
                IsCheck = false
            })
            //sap xep theo role_sequence
            .OrderBy(x => x.role_sequence).ToListAsync();
            // lay tat ca cac quyen cua user
            var roleOfUser = _repositoryAccessor.RoleUser.FindAll(x => x.UserAccount == account).ToList();
            // join lai lay cac quyen trong user va khong co trong role
            foreach (var role in roleOfUser)
            {
                var check = allRole.Where(x => x.role_unique == role.RoleUnique).FirstOrDefault();
                // Neu roleOfUser co thang role thi, user do co quyen
                if (check != null)
                    check.IsCheck = true;
            }
            return new UserRoleDTO() { Account = user.Account, ListRoles = allRole };
        }
    }
}