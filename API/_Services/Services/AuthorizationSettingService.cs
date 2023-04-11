using System.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API._Repositories;
using API._Services.Interfaces;
using API.Helper.Params;
using API.Models;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using SD3_API.Helpers.Utilities;
using AgileObjects.AgileMapper;
using API.DTOs.Role;

namespace API._Services.Services
{
    public class AuthorizationSettingService : IAuthorizationSettingService
    {
        private readonly IRepositoryAccessor _repositoryAccessor;
        public AuthorizationSettingService(IRepositoryAccessor repositoryAccessor)
        {
            _repositoryAccessor = repositoryAccessor;
        }

        public async Task<OperationResult> Create(UserFormParam model)
        {
            var data = await _repositoryAccessor.User.FirstOrDefaultAsync(x => x.account == model.account);
            if (data != null)
                return new OperationResult(false);

            var item = Mapper.Map(model).ToANew<User>(x => x.MapEntityKeys());
            item.update_time = DateTime.Now;

            if (model.is_active == 0)
            {
                item.is_active = false;
            }
            else
            {
                item.is_active = true;
            }

            _repositoryAccessor.User.Add(item);
            await _repositoryAccessor.Save();

            return new OperationResult(true);
        }

        public async Task<PaginationUtility<User>> GetAll(PaginationParam pagination, AuthorizationSettingParam param)
        {
            var pre = PredicateBuilder.New<User>(true);
            if (!string.IsNullOrEmpty(param.account))
                pre.And(x => x.account.Trim() == param.account.Trim());
            if (!string.IsNullOrEmpty(param.name))
                pre.And(x => x.name.Trim().Contains(param.name.Trim()));
            if (!string.IsNullOrEmpty(param.email))
                pre.And(x => x.email.Trim() == param.email.Trim());
            if (!string.IsNullOrEmpty(param.update_by))
                pre.And(x => x.update_by.Trim() == param.update_by.Trim());

            if (param.is_active == 1)
            {
                pre.And(x => x.is_active == true);
            }
            if (param.is_active == 0)
            {
                pre.And(x => x.is_active == false);
            }

            var data = _repositoryAccessor.User.FindAll(pre).AsNoTracking();
            return await PaginationUtility<User>.CreateAsync(data, pagination.PageNumber, pagination.PageSize);
        }

        public async Task<UserRoleDTO> GetAllRoleByAccount(string account)
        {
            var user = await _repositoryAccessor.User.FirstOrDefaultAsync(x => x.account == account.Trim());
            if (user == null)
                return new UserRoleDTO();

            var allRole = await _repositoryAccessor.Role.FindAll().Select(x => new RoleDTO()
            {
                role_unique = x.role_unique,
                role_sequence = x.role_sequence,
                role_name = x.role_name,
                IsCheck = false
            }).OrderBy(x => x.role_sequence).ToListAsync();

            var roleUser = _repositoryAccessor.RoleUser.FindAll(x => x.user_account == account).ToList();
            foreach (var role in roleUser)
            {
                var check = allRole.Where(x => x.role_unique == role.role_unique).FirstOrDefault();

                if (check != null)
                {
                    check.IsCheck = true;
                }
            }
            return new UserRoleDTO() { Account = user.account, ListRoles = allRole };
        }

        public async Task<OperationResult> Update(UserFormParam model)
        {
            var item = Mapper.Map(model).ToANew<User>(x => x.MapEntityKeys());

            item.update_time = DateTime.Now;
            if (model.is_active == 0)
            {
                item.is_active = false;
            }
            else
            {
                item.is_active = true;
            }

            _repositoryAccessor.User.Update(item);

            await _repositoryAccessor.Save();

            return new OperationResult(true);
        }

        public async Task<OperationResult> UpdateRoleByAccount(UserRoleDTO model)
        {
            var items = await _repositoryAccessor.RoleUser.FindAll(x => x.user_account.Trim() == model.Account.Trim()).ToListAsync();

            if (items.Any())
            {
                _repositoryAccessor.RoleUser.RemoveMultiple(items);
            }

            var modelNew = new List<RoleUserDTO>();
            foreach (var item in model.ListRoles)
            {
                if (item.IsCheck == true)
                {
                    var roleUserItem = new RoleUserDTO();
                    roleUserItem.user_account = model.Account;
                    roleUserItem.role_unique = item.role_unique;
                    roleUserItem.create_by = "Admin";
                    roleUserItem.create_time = DateTime.Now;

                    modelNew.Add(roleUserItem);
                }
            }
            var data = Mapper.Map(modelNew).ToANew<List<RoleUser>>(x => x.MapEntityKeys());

            _repositoryAccessor.RoleUser.AddMultiple(data);
            await _repositoryAccessor.Save();

            return new OperationResult(true);
        }
    }
}