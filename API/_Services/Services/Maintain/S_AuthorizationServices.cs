using API._Repositories;
using API._Services.Interfaces;
using API.DTOs.AuthorizationSetting;
using API.Models;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using SD3_API.Helpers.Utilities;

namespace API._Services.Services.Maintain
{
    public class S_AuthorizationServices : I_AuthorizationServices
    {
        private readonly IRepositoryAccessor _repositoryAccessor;

        public S_AuthorizationServices(IRepositoryAccessor repository)
        {
            _repositoryAccessor = repository;
        }

        #region Add
        public async Task<OperationResult> Add(User model)
        {
            var originalItem = await _repositoryAccessor.User.FindAll(x => x.Account == model.Account).FirstOrDefaultAsync();

            if (originalItem != null)
                return new OperationResult { IsSuccess = false, Error = "Data is exists" };

            _repositoryAccessor.User.Add(model);

            try
            {
                await _repositoryAccessor.Save();
                return new OperationResult { IsSuccess = true };
            }
            catch
            {
                return new OperationResult { IsSuccess = false, Error = "" };
            }
        }
        #endregion

        #region Update
        public async Task<OperationResult> Update(User model)
        {
            var originalItem = await _repositoryAccessor.User.FirstOrDefaultAsync(x => x.Account == model.Account.Trim());

            if (originalItem == null)
                return new OperationResult { IsSuccess = false, Error = "Data Not Found" };

            originalItem.Email = model.Email.Trim();
            originalItem.Name = model.Name.Trim();
            originalItem.Password = model.Password.Trim();
            originalItem.IsActive = model.IsActive;

            _repositoryAccessor.User.Update(originalItem);

            try
            {
                await _repositoryAccessor.Save();
                return new OperationResult { IsSuccess = true };
            }
            catch
            {
                return new OperationResult { IsSuccess = false, Error = "" };
            }
        }
        #endregion

        #region Get Data
        public async Task<PaginationUtility<User>> GetData(PaginationParam pagination, string account)
        {
            var pred_User = PredicateBuilder.New<User>(true);
            if (!string.IsNullOrEmpty(account))
                pred_User.And(x => x.Account.Trim().ToLower().Contains(account.Trim().ToLower()));

            var data = await _repositoryAccessor.User.FindAll(pred_User).ToListAsync();

            return PaginationUtility<User>.Create(data, pagination.PageNumber, pagination.PageSize);
        }

        public async Task<User> GetDataOnly(string account)
        {
            return await _repositoryAccessor.User.FirstOrDefaultAsync(item => item.Account.Trim() == account.Trim());
        }

        public async Task<List_RoleUserParam> GetAllRoleByAccount(string account)
        {
            // Kiểm tra User có tồn tại hay không
            var user = await _repositoryAccessor.User.FirstOrDefaultAsync(x => x.Account.Trim() == account.Trim());

            if (user == null)
                return new List_RoleUserParam();
            // Lấy tất cả các quyền có trong bảng Role
            var allRole = await _repositoryAccessor.Role.FindAll().Select(x => new RoleParam()
            {
                RoleUnique = x.RoleUnique,
                RoleName = x.RoleName,
                RoleSequence = x.RoleSequence,
                IsCheck = false
            }).OrderBy(x => x.RoleSequence).ToListAsync();

            // Sau khi kiểm tra User thì lấy các quyền của User đó ra
            var roleOfUser = _repositoryAccessor.RoleUser.FindAll(x => x.UserAccount == user.Account)
            .Select(x => x.RoleUnique).ToList();

            foreach (var role in roleOfUser)
            {
                var check = allRole.Where(x => x.RoleUnique == role).FirstOrDefault();
                if (check != null)
                    check.IsCheck = true;
            }

            return new List_RoleUserParam() { Account = user.Account, ListRoles = allRole };
        }
        #endregion

        #region Update Authorization
        public async Task<OperationResult> UpdateAuthorization(List_RoleUserParam list_RoleUserParam)
        {
            var originalItem = await _repositoryAccessor.RoleUser.FindAll(x => x.UserAccount.Trim() == list_RoleUserParam.Account.Trim()).AsNoTracking().ToListAsync();

            if (originalItem.Any())
                _repositoryAccessor.RoleUser.RemoveMultiple(originalItem);

            var authorizationNew = new List<RoleUser>();

            foreach (var item in list_RoleUserParam.ListRoles)
            {
                if (item.IsCheck == true)
                {
                    var roleUserItem = new RoleUser();
                    roleUserItem.UserAccount = list_RoleUserParam.Account;
                    roleUserItem.RoleUnique = item.RoleUnique;
                    roleUserItem.CreateBy = "Admin";
                    roleUserItem.CreateTime = DateTime.Now;

                    authorizationNew.Add(roleUserItem);
                }
            }
            _repositoryAccessor.RoleUser.AddMultiple(authorizationNew);
            try
            {
                await _repositoryAccessor.Save();
                return new OperationResult { IsSuccess = true };
            }
            catch
            {
                return new OperationResult { IsSuccess = false, Error = "" };
            }
        }
        #endregion
    }
}