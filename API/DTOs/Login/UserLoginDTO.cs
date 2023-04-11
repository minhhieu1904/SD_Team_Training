using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs.Login
{
    

    public class UserLoginDTO
    {
        public string account {get; set;}
        public string user {get;set;}
        public string name {get; set;}
        public string email {get; set;}
        public List<RoleInfomation> roles {get;set;}
    }
}