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
    public class S_Loginservices : I_LoginServices
    {
        public readonly IRepositoryAccessor _repositoryAccessor;
        public S_Loginservices(IRepositoryAccessor repositoryAccessor){
            _repositoryAccessor = repositoryAccessor;
        }
        // Khai báo một hàm bất đồng bộ (async) có kiểu trả về là Task và tên của hàm là Login, nhận tham số users từ DTO userLogin.
        public async Task<listUser> Login(userLogin users)
        {   
            // Tìm kiếm người dùng trong cơ sở dữ liệu theo tài khoản và trạng thái hoạt động đã được xác nhận. Sử dụng phương thức FindSingle để chỉ lấy một kết quả duy nhất.
            var user = await _repositoryAccessor.User.FindSingle(x=>x.Account.Trim() == users.account.Trim() && x.IsActive == true );
            // Kiểm tra nếu không tìm thấy người dùng, thì trả về giá trị null.
            if(user == null) return null;
            // Nếu tồn tại người dùng ứng với thông tin đã nhập vào, method tiếp tục kiểm tra tính hợp lệ của password theo cách so sánh giá trị hashed password
            // trong csdl với password được cung cấp. Nếu không đúng, phương thức sẽ return null.
            if(user.Password != users.password) return null;
            // Lấy danh sách vai trò (role), gán cho biến role; 
            var role = _repositoryAccessor.Role.FindAll();
            // Lấy danh sách vai trò hiện có của người dùng, gán cho biến roleUser
            var roleUser = _repositoryAccessor.RoleUser.FindAll(x=>x.UserAccount == user.Account);
            // Kết hợp hai bảng RoleUser và Role thông qua trường RoleUnique. Trong đó lấy ra tên vai trò, mã unique và vị trí của từng vai trò để thành object mới có 
            // kiểu là “RoleLogin”, được lưu trong danh sách data. Phương thức Join sử dụng cơ chế nhóm (group join).
            var data = await roleUser.Join(role, x=>x.RoleUnique, y=>y.RoleUnique,
            (x,y)=> new RoleLogin{name = y.RoleName, unique = y.RoleUnique, position = y.RoleSequence}).ToListAsync();
            // Tạo một đối tượng listUser để chứa thông tin người dùng đã đăng nhập thành công. Gồm các thuộc tính account (tài khoản), email, name (tên), user(tài khoản)
            // và roles(danh sách các vai trò). Vai trò sẽ được sắp xếp theo thứ tự số liệu Field Sequence khi được khởi tạo.
            var result = new listUser{
                account = user.Account,
                email = user.Email,
                user = user.Account,
                name = user.Name,
                roles = data.OrderBy(x => x.position).ToList()
            };
            // Trả về giá trị kết quả của quá trình đăng nhập vào hệ thống.
            return result;
        }
    }
}