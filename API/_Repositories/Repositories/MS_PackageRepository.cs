using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API._Repositories.Interfaces;
using API.Data;
using API.Models;

namespace API._Repositories.Repositories
{
    public class MS_PackageRepository : Repository<MS_Package>, IMS_PackageRepository
    { 
         private DBContext _context;

        public MS_PackageRepository(DBContext context) : base(context)
        {
            _context = context;
        }
        
    }
}