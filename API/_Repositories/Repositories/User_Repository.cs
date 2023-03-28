using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API._Repositories.Interfaces;
using API.Data;
using API.Models;

namespace API._Repositories.Repositories
{
    public class User_Repository : Repository<User>, I_User_Repository
    {
        public User_Repository(DBContext context) : base(context)
        {
        }
    }
}