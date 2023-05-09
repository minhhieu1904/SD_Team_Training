using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs.Author;

namespace API._Services.Interfaces
{
    public interface IAuthorService
    {
        Task<UserLoginDTO> Login (UserLogin userLogin);
    }
}