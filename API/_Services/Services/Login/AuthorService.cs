
using AgileObjects.AgileMapper;
using API._Repositories;
using API._Services.Interfaces;
using API.DTOs.Author;
using API.Helper.Mappers;
using Microsoft.EntityFrameworkCore;

namespace API._Services.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IRepositoryAccessor _repoAccessor;

        public AuthorService(IRepositoryAccessor repoAccessor)
        {
            _repoAccessor = repoAccessor;
        }
        public async Task<UserLoginDto> Login(UserLogin userLogin)
        {

             var user = await _repoAccessor.Users.FirstOrDefaultAsync(x => x.account.Trim() == userLogin.account.Trim() && x.is_active);

            // Kiểm tra sự tồn tại của user
            if(user == null) {
                return null;
            }

            // Nếu tồn tại user => Check password
            if(user.password != userLogin.password) {
                return null;
            }

            var roleUsers = _repoAccessor.RoleUser.FindAll(x => x.user_account.Trim() == user.account.Trim());
            var roles = _repoAccessor.Roles.FindAll();

            var mapper = Mapper.CreateNew();
            mapper.WhenMapping.UseConfigurations.From<MapperConfig>();

            var roleForUser = await roleUsers.Join(
                roles,
                x => x.role_unique,
                y => y.role_unique,
                (x,y) => y).Select(x=> new RoleInformation(){
                     name = x.role_name,
                     unique = x.role_unique,
                     position = x.role_sequence
                }).ToListAsync();

            var result = new UserLoginDto
            {
                account = user.account,
                email = user.email,
                username = user.account,
                name = user.name,
                roles = roleForUser.OrderBy(x => x.position).ToList(),
                roleAll = roleForUser.ToList()
            };

            return result;

        }

       
    }
}