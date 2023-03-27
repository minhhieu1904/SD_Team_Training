using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgileObjects.AgileMapper;
using AgileObjects.AgileMapper.Extensions;
using API._Repositories;
using API._Services.Interfaces;
using API.Dtos;
using API.Dtos.Maintain.AuthorizationSetting;
using API.Models;
using Microsoft.EntityFrameworkCore;
using SDCores;

namespace API._Services.Services.maintain
{
    public class S_AuthorizationSettingService : IAuthorizationSettingService
    {
        private readonly IRepositoryAccessor _repository;

        public S_AuthorizationSettingService(IRepositoryAccessor repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> Add(UserDto userDto)
        {
            var item = await _repository.Users.FindAll(x => x.account.Trim() == userDto.account.Trim()).FirstOrDefaultAsync();
            if (item == null)
            {
                var model = userDto.Map().ToANew<Users>(c => c.MapEntityKeys());
                _repository.Users.Add(model);
                return new OperationResult(true);
            }
            return new OperationResult(false);
        }

        public async Task<OperationResult> Edit(UserDto userDto)
        {
            var model = userDto.Map().ToANew<Users>(c => c.MapEntityKeys());
            _repository.Users.Update(model);
            await _repository.Save();
            return new OperationResult(false);
        }

        public async Task<PaginationUtility<Users>> GetDataUsers(PaginationParam pagination)
        {
            var data = _repository.Users.FindAll();
            return await PaginationUtility<Users>.CreateAsync(data, pagination.PageNumber, pagination.PageSize);
        }

        public async Task<List<RolesUserStatus>> GetRoleUser(string account)
        {
            var data = await _repository.RoleUser.FindAll(x => x.user_account.Trim() == account.Trim())
            .Select(x => new RolesUserStatus()
            {
                role_unique = x.role_unique,
                status = true
            }).ToListAsync();

            var rolesUserHave = data.Select(x => x.role_unique).ToList();
            var allRoles = await _repository.Roles.FindAll().ToListAsync();
            var rolesNotInUser = allRoles.Where(x => !rolesUserHave.Contains(x.role_unique.Trim())).ToList();
            if (rolesNotInUser.Any())
            {
                foreach (var item in rolesNotInUser)
                {
                    var notRolesItem = new RolesUserStatus();
                    notRolesItem.role_unique = item.role_unique;
                    notRolesItem.status = false;
                    data.Add(notRolesItem);
                }
            }

            var result = data.Join(
                allRoles,
                x => x.role_unique,
                y => y.role_unique,
                (x, y) => new RolesUserStatus()
                {
                    user_account = account,
                    role_name = y.role_name,
                    role_unique = y.role_unique,
                    role_sequence = y.role_sequence,
                    status = x.status
                }
            ).OrderBy(x => x.role_sequence).ToList();
            return result;
        }

        public async Task<bool> UpdateAuthorUser(List<RolesUserStatus> authors, string userUpdate)
        {
            var user_account = authors[0].user_account.Trim();
            var rolesUserCurrent = await _repository.RoleUser.FindAll(x => x.user_account.Trim() == user_account).ToListAsync();

            foreach (var author in authors)
            {
                var timeNow = DateTime.Now;
                var roleFind = rolesUserCurrent.FirstOrDefault(x => x.role_unique.Trim() == author.role_unique.Trim());

                //role dc check
                if (author.status)
                {
                    if (roleFind == null)
                    {
                        //role dc check nhung trong DB chua co thi add vao
                        var roleUserAdd = Mapper.Map(author).ToANew<RoleUser>(x => x.MapEntityKeys());
                        roleUserAdd.create_by = userUpdate;
                        roleUserAdd.create_time = timeNow;
                        _repository.RoleUser.Add(roleUserAdd);
                    }
                }
                else
                //role khong dc check
                if (roleFind != null)
                {
                    //role dc check nhung trong DB chua co thi add vao
                    _repository.RoleUser.Remove(roleFind);
                }
            }
            return await _repository.Save();
        }
    }

}