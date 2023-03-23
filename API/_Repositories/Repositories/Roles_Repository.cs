using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API._Repositories.Interfaces;
using API.Data;
using API.Models;

namespace API._Repositories.Repositories
{
    public class Roles_Repository : Repository<Roles>, IRolesRepository
    {
        public Roles_Repository(DBContext context) : base(context)
        {
        }

        public static implicit operator Roles_Repository(RoleUser_Repository v)
        {
            throw new NotImplementedException();
        }
    }
}