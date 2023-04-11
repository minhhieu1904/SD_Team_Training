using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs.Login
{
    public class UserLogin
    {
        public string account { get; set; }
        public string password { get; set; }
    }
}