using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs.Author
{
     public class UserLogin
    {
        public string account { get; set; }
        public string password { get; set; }
    }
    public class RoleInformation
    {
        public string name { get; set; }
        public string unique { get; set; }
        public double? position { get; set; }
    }
    public class UserLoginDTO
    {
        public string account { get; set; }
        public string name { get; set; }
        public string Username { get; set; }
        public string email { get; set; }
        public List<RoleInformation> roles { get; set; }
        public List<RoleInformation> roleAll { get; set; }
    }
   
}