using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs.Maintain.Authorization
{
    public class RoleParam
    {
        public string userAccount {get; set;}
        public string roleUnique {get; set;}
        public string roleName {get; set;}
        public string roleTupe {get; set;}
        public string roleNote {get; set;}
        public double roleSequence {get; set;}
        public bool isCheck {get; set;}
    }
}