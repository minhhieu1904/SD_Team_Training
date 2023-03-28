using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API._Repositories;
using API._Services.Interfaces;
using API.DTOs;
using API.Models;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using SD3_API.Helpers.Utilities;

namespace API._Services.Services
{
    public class S_AuthorizationServices : I_AuthorizationServices
    {
        private readonly IRepositoryAccessor _repositoryAccessor;
        public S_AuthorizationServices(IRepositoryAccessor repository)
        {
            _repositoryAccessor = repository;
        }
        public async Task<OperationResult> Add(User model)
        {
            var originalItem = await _repositoryAccessor.User.FindAll(
                x => x.Account == model.Account).FirstOrDefaultAsync();

            if (originalItem != null)
                return new OperationResult(false);

            _repositoryAccessor.User.Add(model);

            if (await _repositoryAccessor.Save())
                return new OperationResult(true);
            return new OperationResult(false);

        }

        public async Task<RoleUserParam> GetAllRoleByAccount(string account)
        {
            var user = await _repositoryAccessor.User.FirstOrDefaultAsync(x=>x.Account == account.Trim());
            if(user == null)
                return new RoleUserParam();
            var allRole = await _repositoryAccessor.Role.FindAll().Select(x=> new RoleParam(){
                roleUnique = x.RoleUnique,
                roleName = x.RoleName,
                roleSequence = x.RoleSequence,
                isCheck = false
            }).OrderBy(x=>x.roleSequence).ToListAsync();

            var roleUser = _repositoryAccessor.RoleUser.FindAll(x=> x.UserAccount == user.Account)
            .Select(x => x.RoleUnique).ToList();
            foreach(var role in roleUser){
                var check = allRole.Where(x=>x.roleUnique == role).FirstOrDefault();
                if(check!=null)
                    check.isCheck = true;
            }
            return new RoleUserParam() { Account = user.Account, listRole = allRole};
        }

        public async Task<PaginationUtility<User>> GetData(PaginationParam pagination, string account)
        {
            var pred = PredicateBuilder.New<User>(true);
            if(!string.IsNullOrEmpty(account))
                pred.And(x=>x.Account == account.Trim());
            var data = await _repositoryAccessor.User.FindAll(pred).ToListAsync();
            return PaginationUtility<User>.Create(data, pagination.PageNumber, pagination.PageSize);
        }

        public async Task<User> GetDataOnly(string account)
        {
            return await _repositoryAccessor.User.FirstOrDefaultAsync(item => item.Account == account.Trim());
        }

        public async Task<OperationResult> Update(User model)
        {
            var original = await _repositoryAccessor.User.FirstOrDefaultAsync(x=>x.Account == model.Account.Trim());
            if(original==null)
                return new OperationResult(false);
            original.Email = model.Email.Trim();
            original.Name = model.Name.Trim();
            original.Password = model.Password.Trim();
            original.IsActive = model.IsActive;
            _repositoryAccessor.User.Update(original);
            if(await _repositoryAccessor.Save())
                return new OperationResult(true);
            return new OperationResult(false);
        }

        public async Task<OperationResult> UpdateAuthorization(RoleUserParam userRoleParam)
        {
            var original = await _repositoryAccessor.RoleUser.FindAll(x=>x.UserAccount.Trim() 
            == userRoleParam.Account.Trim()).AsNoTracking().ToListAsync();
            if(original.Any())
                _repositoryAccessor.RoleUser.RemoveMultiple(original);
            var authorizationNew = new List<RoleUser>();
            foreach(var item in userRoleParam.listRole){
                if(item.isCheck == true){
                    var roleUserItem = new RoleUser();
                    roleUserItem.UserAccount = userRoleParam.Account;
                    roleUserItem.RoleUnique = item.roleUnique;
                    roleUserItem.CreateBy = "Admin";
                    roleUserItem.CreateTime = DateTime.Now;

                    authorizationNew.Add(roleUserItem);
                }
            }
            _repositoryAccessor.RoleUser.AddMultiple(authorizationNew);
            await _repositoryAccessor.Save();
            return new OperationResult(true);
        }
    }
}