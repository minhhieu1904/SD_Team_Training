using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API._Repositories;
using API._Services.Interfaces;
using API.DTOs;
using API.Models;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using SD3_API.Helpers.Utilities;

namespace API._Services.Services
{
    public class S_AuthorizationServices : I_AuthorizationServices
    {
        private readonly IRepositoryAccessor _repositoryAccessor;
        public S_AuthorizationServices(IRepositoryAccessor repository)
        {
            _repositoryAccessor = repository;
        }
        public async Task<OperationResult> Add(User model)
        {
            var originalItem = await _repositoryAccessor.User.FindAll(
                x => x.Account == model.Account).FirstOrDefaultAsync();

            if (originalItem != null)
                return new OperationResult(false);

            _repositoryAccessor.User.Add(model);

            if (await _repositoryAccessor.Save())
                return new OperationResult(true);
            return new OperationResult(false);

        }
        // phương thức của lớp, nhận đầu vào là một chuỗi "account" và trả về một đối tượng "RoleUserParam".
        public async Task<RoleUserParam> GetAllRoleByAccount(string account)
        {
            // Tìm kiếm người dùng trong cơ sở dữ liệu với điều kiện tài khoản (Account) bằng giá trị được chuyền vào ("account"), sau đó gán kết quả cho biến user
            var user = await _repositoryAccessor.User.FirstOrDefaultAsync(x=>x.Account == account.Trim());
            // Nếu không tìm thấy người dùng (user == null), phương thức sẽ trực tiếp trả về một RoleUserParam rỗng để thông báo cho người gọi biết.
            if(user == null)
                return new RoleUserParam();
            // Lấy tất cả các vai trò có trong cơ sở dữ liệu và chuyển chúng thành RoleParams thông qua Select(), 
            // sau đó sắp xếp theo thuộc tính roleSequence. Tất cả các VaiTrò này ban đầu có isCheck = false (chưa được chọn).
            var allRole = await _repositoryAccessor.Role.FindAll().Select(x=> new RoleParam(){
                roleUnique = x.RoleUnique,
                roleName = x.RoleName,
                roleSequence = x.RoleSequence,
                isCheck = false
            }).OrderBy(x=>x.roleSequence).ToListAsync();
            //  Lấy danh sách mã vai trò đã được liên kết với user và lưu nó vào List<string> roleUser.
            var roleUser = _repositoryAccessor.RoleUser.FindAll(x=> x.UserAccount == user.Account)
            .Select(x => x.RoleUnique).ToList();
            // Sử dụng foreach để duyệt qua từng mã vai trong danh sách roleUser, sau đó xem xét nếu mã này khớp với mã của bất kỳ 
            // hồ sơ vai nào trong allRoles, thiết lập isCheck thành true. Mục tiêu láy ra các loại Role của User.
            foreach(var role in roleUser){
                var check = allRole.Where(x=>x.roleUnique == role).FirstOrDefault();
                if(check!=null)
                    check.isCheck = true;
            }
            // Đối tượng RoleUserParam được trả về, trong đó Account chứa giá trị account ban đầu và listRole chứa danh sách vai trò đã có 
            // isCheck (được liên kết với người dùng) được thiết lập thành true.
            return new RoleUserParam() { Account = user.Account, listRole = allRole};
        }
        // pagination: kiểu PaginationParam, đại diện cho thông tin về trang và kích cỡ của dữ liệu được yêu cầu
        // account: kiểu string, tên tài khoản người dùng.
        public async Task<PaginationUtility<User>> GetData(PaginationParam pagination, string account)
        {
            // tạo một biến "pred" kiểu PredicateBuilder<User>(true) để xây dựng điều kiện where cho câu lệnh truy vấn.
            var pred = PredicateBuilder.New<User>(true);
            // Kiểm tra nếu "account" không null hoặc rỗng(thông tin tài khoản được yêu cầu), 
            // ta add tiếp điều kiện là Account == account.Trim() vào biến pred đã khởi tạo ở trên(bằng hàm Extension And)
            if(!string.IsNullOrEmpty(account))
                pred.And(x=>x.Account == account.Trim());
            // Sau đó, chúng ta gọi phương thức FindAll() của _repositoryAccessor.User để lấy danh sách các User thoả mãn điều kiện trong predicate được xác định trước.
            var data = await _repositoryAccessor.User.FindAll(pred).ToListAsync();
            // phân trang kết quả data dựa trên pageNumber và pageSize thông qua phương thức tĩnh Create () của class PaginationUtility<T> ​​và return ra kết quả này.
            return PaginationUtility<User>.Create(data, pagination.PageNumber, pagination.PageSize);
        }

        public async Task<User> GetDataOnly(string account)
        {
            return await _repositoryAccessor.User.FirstOrDefaultAsync(item => item.Account == account.Trim());
        }

        public async Task<OperationResult> Update(User model)
        {
            var original = await _repositoryAccessor.User.FirstOrDefaultAsync(x=>x.Account == model.Account.Trim());
            if(original==null)
                return new OperationResult(false);
            original.Email = model.Email.Trim();
            original.Name = model.Name.Trim();
            original.Password = model.Password.Trim();
            original.IsActive = model.IsActive;
            _repositoryAccessor.User.Update(original);
            if(await _repositoryAccessor.Save())
                return new OperationResult(true);
            return new OperationResult(false);
        }
        // Khai báo phương thức UpdateAuthorization, trả về Task<OperationResult>
        public async Task<OperationResult> UpdateAuthorization(RoleUserParam userRoleParam)
        {
            // Lấy tất cả các bản ghi có thuộc tính UserAccount giống với userRoleParam.Account thông qua RoleUser RepositoryAccessor và lưu vào original.
            var original = await _repositoryAccessor.RoleUser.FindAll(x=>x.UserAccount.Trim() == userRoleParam.Account.Trim()).AsNoTracking().ToListAsync();
            // Kiểm tra nếu có ít nhất một bản ghi, xóa chúng khỏi danh sách.
            if(original.Any()) _repositoryAccessor.RoleUser.RemoveMultiple(original);
            // Khởi tạo một List<RoleUser> mới, được đặt tên author.
            var author = new List<RoleUser>();
            // Sử dụng vòng lặp foreach để duyệt hết các Phần tử(listRole) trong userRoleParam.listRole . Nếu isCheck = true (được chọn), thêm một RoleUser mới vào danh sách author.
            foreach(var item in userRoleParam.listRole){
                if(item.isCheck == true){
                    var roleUserItem = new RoleUser();
                    roleUserItem.UserAccount = userRoleParam.Account;
                    roleUserItem.RoleUnique = item.roleUnique;
                    roleUserItem.CreateBy = "Admin";
                    roleUserItem.CreateTime = DateTime.Now;
                    author.Add(roleUserItem);
                }
            }
            // Thêm toàn bộ danh sách author đã được cập nhật vào RepositoryAccessor.RoleUser
            _repositoryAccessor.RoleUser.AddMultiple(author);
            // Lưu lại sự thay đổi vào database thông qua _repositoryAccessor.Save().
            await _repositoryAccessor.Save();
            // Trả về kết quả OperationResult(true) nếu thành công trong việc update quyền cho người dùng này.
            return new OperationResult(true);
        }
    }
}