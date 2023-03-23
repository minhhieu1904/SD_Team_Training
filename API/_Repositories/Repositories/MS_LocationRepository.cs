using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API._Repositories.Interfaces;
using API.Data;
using API.Models;

namespace API._Repositories.Repositories
{
    public class MS_LocationRepository : Repository<MS_Location>, IMS_LocationRepository
    {
         private DBContext _context;

        public MS_LocationRepository(DBContext context) : base(context)
        {
            _context = context;
        }
    }
}