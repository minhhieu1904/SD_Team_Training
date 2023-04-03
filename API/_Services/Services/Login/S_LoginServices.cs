using API._Repositories;
using API._Services.Interfaces;
using API.DTOs.Login;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API._Services.Services.Maintain
{
    public class S_LoginServices : I_LoginServices
    {
        private readonly IRepositoryAccessor _repositoryAccessor;

        public S_LoginServices(IRepositoryAccessor repositoryAccessor)
        {
            _repositoryAccessor = repositoryAccessor;
        }

        public async Task<UserLoginDTO> Login(UserLogin userLogin)
        {
            // Tìm tài khoản dựa vào tài khoản
            var user = await _repositoryAccessor.User.FindSingle(x => x.Account == userLogin.Account.Trim() && x.IsActive == true);

            // Không tìm thấy hoặc MK khác với MK trong dữ liệu tài khoản trả về Null
            if (user == null || user.Password != userLogin.Password.Trim()) return null;

            var role = _repositoryAccessor.Role.FindAll();
            var roleUser = _repositoryAccessor.RoleUser.FindAll(x => x.UserAccount == userLogin.Account.Trim());
            var data = await roleUser.Join(role, x => x.RoleUnique, y => y.RoleUnique, (x, y) =>
            new RoleInfomation { Name = y.RoleName, Unique = y.RoleUnique, Position = y.RoleSequence }).ToListAsync();

            var result = new UserLoginDTO
            {
                Account = user.Account,
                Email = user.Email,
                Name = user.Name,
                User = user.Account,
                Roles = data.OrderBy(x => x.Position).ToList()
            };

            return result;
        }
    }
}