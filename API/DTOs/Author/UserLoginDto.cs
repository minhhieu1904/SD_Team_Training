using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs.Author
{
    public class UserLoginDto
    {
        public string account { get; set; }
        public string username { get; set; }
        public string name { get; set; }
        public string email { get; set; }      
        public List<RoleInformation> roles { get; set; }
        public List<RoleInformation> roleAll { get; set; }
    }
}