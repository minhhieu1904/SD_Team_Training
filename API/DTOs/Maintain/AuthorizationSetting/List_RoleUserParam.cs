using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs.AuthorizationSetting
{
    public class List_RoleUserParam
    {
        public string Account { get; set; }
        public List<RoleParam> ListRoles { get; set; }
    }
}