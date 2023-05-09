using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs.Role
{
    public class RoleDTO
    {
        public string user_account { get; set; }
        public string role_unique { get; set; }
        public string role_name { get; set; }
        public string role_type { get; set; }
        public string role_note { get; set; }
        public double role_sequence { get; set; }
        public bool IsCheck { get; set; }
    }
    public class UserRoleDTO
    {
        public string Account { get; set; }
        public List<RoleDTO> ListRoles { get; set; }
    }
    // hứng giá trị khi join 2 bảng UserRoleDTO và RoleUser
    public partial class RoleUserDTO
    {
        public string user_account { get; set; }
        public string role_unique { get; set; }
        public string create_by { get; set; }
        public DateTime create_time { get; set; }
    }

}