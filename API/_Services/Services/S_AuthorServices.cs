using API._Repositories;
using API._Services.Interfaces;
using API.DTOs.Author;
using Microsoft.EntityFrameworkCore;

namespace API._Services.Services
{
    public class SAuthorServices : IAuthorServices
    {
        private readonly IRepositoryAccessor _repoAccessor;

        public SAuthorServices(IRepositoryAccessor repoAccessor)
        {
            _repoAccessor = repoAccessor;
        }
        public async Task<UserLoginDto> Login(UserLogin userLogin)
        {
            var user = await _repoAccessor.Users.FindSingle(x => x.Account.Trim() == userLogin.account.Trim() && x.IsActive == true);
            if (user == null) return null;
            if (user.Password != userLogin.password) return null;
            var role = _repoAccessor.Roles.FindAll();
            var roleUser = _repoAccessor.RoleUser.FindAll(x => x.UserAccount == user.Account);
            var data = await roleUser.Join(role,
            x => x.RoleUnique,
            y => y.RoleUnique,
            (x, y) => new RoleInformation { name = y.RoleName, unique = y.RoleUnique, position = y.RoleSequence }).ToListAsync();
            var result = new UserLoginDto
            {
                account = user.Account,
                email = user.Email,
                username = user.Account,
                name = user.Name,
                roles = data.OrderBy(x => x.position).ToList()
            };
            return result;
        }
    }
}