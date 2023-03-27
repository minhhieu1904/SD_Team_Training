
using API._Repositories;
using API._Services.Interfaces;
using API.DTOs;
using API.DTOs.Author;
using Microsoft.EntityFrameworkCore;
using SD3_API.Helpers.Utilities;

namespace API._Services.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IRepositoryAccessor _repoAccessor;

        public AuthorService(IRepositoryAccessor repoAccessor)
        {
            _repoAccessor = repoAccessor;
        }


        public async Task<OperationResult> changePassword(UserPassword user)
        {
            var item = await _repoAccessor.Users.FindSingle(x => x.account == user.account);
            if (item == null)
                return new OperationResult(false, "Người dùng không tồn tại");
            return new OperationResult(true, "Cập nhật tài khoản thành công !");
        }



        public async Task<UserLoginDto> Login(UserLogin userLogin)
        {
            var user = await _repoAccessor.Users.FindSingle(x => x.account.Trim() == userLogin.account.Trim() && x.is_active == true);
            if (user == null) return null;
            if (user.password != userLogin.password) return null;
            var role = _repoAccessor.Roles.FindAll();
            var roleUser = _repoAccessor.RoleUser.FindAll(x => x.user_account == user.account);
            var data = await roleUser.Join(role,
                                   x => x.role_unique,
                                   y => y.role_unique,
                                   (x, y) => new RoleInformation { name = y.role_name, unique = y.role_unique, position = y.role_sequence }).ToListAsync();
            var result = new UserLoginDto
            {
                account = user.account,
                email = user.email,
                username = user.account,
                name = user.name,
                roles = data.OrderBy(x => x.position).ToList()
            };
            return result;
        }


    }
}