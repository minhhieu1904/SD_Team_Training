using API._Repositories;
using API._Services.Interfaces;
using API.DTOs.Login;
using Microsoft.EntityFrameworkCore;
using SD3_API.Helpers.Utilities;

namespace API._Services.Services
{
    //Service kế thừa từ Interfaces
    public class S_Login : I_Login //Ctrl + . để implement
    {
        private readonly IRepositoryAccessor _repository;


        public S_Login(IRepositoryAccessor repository)
        {
            _repository = repository;
        }

        public async Task<UserLoginDTO> Login(UserLogin login)
        {
            //var user = _repository.Users.FindSingle(x => x.account == login.account && x.password == login.password && x.is_active == true);
            //Khai báo user
            var user = await _repository.Users.FindSingle(x => x.account.Trim() == login.account.Trim() && x.is_active == true); //Trim() là bỏ tất cả khoảng trống
            if (user == null) return null;
            if (user.password != login.password) return null;
            var role = _repository.Roles.FindAll();
            var roleUser = _repository.RoleUser.FindAll(x => x.user_account == user.account);
            var data = await roleUser.Join(role,
            x => x.role_unique,
            y => y.role_unique,
            (x, y) => new RoleInfomation { name = y.role_name, unique = y.role_unique, position = y.role_sequence }).ToListAsync();
            var result = new UserLoginDTO{
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