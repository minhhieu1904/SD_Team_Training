using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API._Repositories.Interfaces;
using API.Data;
using API.Models;

namespace API._Repositories.Repositories
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(DBContext context) : base(context)
        {
        }
    }
}