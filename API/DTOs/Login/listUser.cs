using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs.userLogin
{
    public class listUser
    {
        public string account {get; set;}
        public string user {get;set;}
        public string name {get; set;}
        public string email {get; set;}
        public List<RoleLogin> roles {get;set;}
    }
}