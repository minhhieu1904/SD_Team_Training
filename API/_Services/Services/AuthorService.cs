using API._Repositories;
using API._Services.Interfaces;
using API.DTOs.Author;
using Microsoft.EntityFrameworkCore;
namespace API._Services.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IRepositoryAccessor _repositoryAccessor;
        public AuthorService(IRepositoryAccessor repositoryAccessor)
        {
            _repositoryAccessor = repositoryAccessor;
        }
        public async Task<UserLoginDTO> Login(UserLogin userLogin)
        {
            var user = await _repositoryAccessor.User.FindSingle(x => x.account.Trim() == userLogin.account.Trim() && x.is_active == true);

            if (user == null)
                return null;

            if (user.password != userLogin.password)
                return null;

            var role = _repositoryAccessor.Role.FindAll();
            var roleUser = _repositoryAccessor.RoleUser.FindAll(x => x.user_account.Trim() == userLogin.account.Trim());
            var data = await roleUser.Join(role,
                                            x => x.role_unique,
                                            y => y.role_unique,
                                            (x, y) => new RoleInformation { name = y.role_name, unique = y.role_unique, position = y.role_sequence }
            ).ToListAsync();

            var result = new UserLoginDTO
            {
                account = user.account,
                email = user.email,
                Username = user.account,
                name = user.name,
                roles = data.OrderBy(x => x.position).ToList(),
                roleAll = data.ToList(),
            };
            return result;
        }
    }
}