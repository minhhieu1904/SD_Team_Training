using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API._Repositories.Interfaces;
using API.Data;
using API.Models;

namespace API._Repositories.Repositories
{
    public class RolesRepository :  Repository<Roles>, IRolesRepository
    {
         private DBContext _context;

        public RolesRepository(DBContext context) : base(context)
        {
            _context = context;
        }
        
    }
}