
namespace API.DTOs.Login
{
    public class UserLoginDTO
    {
        public string Account { get; set; }
        public string User { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public List<RoleInfomation> Roles { get; set; }
    }
}