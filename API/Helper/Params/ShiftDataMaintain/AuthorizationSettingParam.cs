using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Helper.Params
{
    public class AuthorizationSettingParam
    {
        public string account { get; set; }
        public string password { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public int is_active { get; set; }
        public string update_by { get; set; }
        public DateTime update_time { get; set; }
    }
}