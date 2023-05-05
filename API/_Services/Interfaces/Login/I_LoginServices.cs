using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs.userLogin;

namespace API._Services.Interfaces.Login
{
    public interface I_LoginServices
    {
     Task<listUser> Login(userLogin user);
    }
}