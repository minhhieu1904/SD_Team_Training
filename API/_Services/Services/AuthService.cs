using API._Repositories;
using API._Services.Interfaces;
using API.Dtos.Auth;
using API.Helpers.Login;
using Microsoft.EntityFrameworkCore;

namespace API._Services.Services
{
    public class AuthService : IAuthService
    {
        private readonly IRepositoryAccessor _repository;
        public AuthService(IRepositoryAccessor repository)
        {
            _repository = repository;
        }
        public async Task<UserForLoggedDTO> Login(UserLoginParam userForLogin)
        {
            var user = await _repository.Users.FirstOrDefaultAsync(x => x.account.Trim() == userForLogin.Username.Trim() && x.is_active);

            if (user == null)
            {
                return null;
            }

            if (user.password != userForLogin.Password)
            {
                return null;
            }

            var roleUsers = _repository.RoleUser.FindAll(x => x.user_account.Trim() == user.account.Trim());
            var roles = _repository.Roles.FindAll();
            var roleForUser = await roleUsers.Join(
                roles,
                x => x.role_unique,
                y => y.role_unique,
                (x, y) => new RoleInfomation()
                {
                    Name = y.role_name,
                    Unique = y.role_unique,
                    Position = y.role_sequence
                }).ToListAsync();

            var result = new UserForLoggedDTO
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