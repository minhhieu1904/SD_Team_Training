using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs.userLogin;

namespace API._Services.Interfaces
{
    public interface IAuthorService
    {
        Task<userLoginDTO> Login(userLogin userLogin);

    }
}