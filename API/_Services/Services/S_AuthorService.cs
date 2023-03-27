using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API._Repositories;
using API._Services.Interfaces;
using API.DTOs.userLogin;
using Microsoft.EntityFrameworkCore;

namespace API._Services.Services
{
    public class S_AuthorService : IAuthorService
    {
        public readonly IRepositoryAccessor _reposioryAccessor;
        public S_AuthorService(IRepositoryAccessor reposioryAccessor)
        {
            this._reposioryAccessor = reposioryAccessor;
        }

        public async Task<userLoginDTO> Login(userLogin userLogin)
        {
            var user = await _reposioryAccessor.Users.FindSingle(x => x.account.Trim() == userLogin.account.Trim() && x.is_active == true);
            if (user == null) return null;
            if (user.password != userLogin.password) return null;
            var role = _reposioryAccessor.Roles.FindAll();
            var roleUser = _reposioryAccessor.RoleUser.FindAll(x => x.user_account == user.account);
            var data = await roleUser.Join(role,
            x => x.role_unique,
            y => y.role_unique,
            (x, y) => new roleInfomation { name = y.role_name, unique = y.role_unique, position = y.role_sequence }).ToListAsync();
            var result = new userLoginDTO{
                account = user.account,
                email = user.email,
                user = user.account,
                name = user.name,
                roles = data.OrderBy(x => x.position).ToList()
            };

            return result;
        }
    }
}