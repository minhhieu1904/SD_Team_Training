using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs.userLogin;

namespace API._Services.Interfaces
{
    public interface I_LoginServices
    {
     Task<listUser> Login(userLogin user);
    }
}