using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs.AuthorizationSetting
{
    public class UserParam
    {
        public string Account { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public string UpdateBy { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}