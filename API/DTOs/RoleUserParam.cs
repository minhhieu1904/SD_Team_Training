using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class RoleUserParam
    {
        public string Account {get; set;}
        public List<RoleParam> listRole {get; set;}
    }
}