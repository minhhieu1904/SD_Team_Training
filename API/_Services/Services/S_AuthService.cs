using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API._Repositories;
using API._Services.Interfaces;
using API.Dtos.Auth;
using API.Helpers.Params.Login;
using Microsoft.EntityFrameworkCore;

namespace API._Services.Services
{
    public class S_AuthService : IAuthService
    {
        private readonly IRepositoryAccessor _repoAccessor;

        public S_AuthService(IRepositoryAccessor repoAccessor)
        {
            _repoAccessor = repoAccessor;
        }

        public async Task<UserForLoggedDTO> Login(UserLoginParam userParamLogin)
        {
            var user = await _repoAccessor.Users.FirstOrDefaultAsync(x => x.account.Trim() == userParamLogin.Username.Trim() && x.is_active);

            //Kiem tra  su ton tai cua User
            if (user == null)
            {
                return null;
            }

            var roleUser = _repoAccessor.RoleUser.FindAll(x => x.user_account.Trim() == user.account.Trim());
            var roles = _repoAccessor.Roles.FindAll();
            var roleForUser = await roleUser.Join(
                roles,
                x => x.role_unique,
                y => y.role_unique,
                (x, y) => new RoleInfomation()
                {
                    Name = y.role_name,
                    Unique = y.role_unique,
                    Position = y.role_sequence
                }).ToListAsync();
            var result = new UserForLoggedDTO()
            {
                Id = user.account,
                Email = user.email,
                Username = user.account,
                Name = user.name,
                Roles = roleForUser.OrderBy(x => x.Position).ToList()
            };

            return result;
        }
    }
}