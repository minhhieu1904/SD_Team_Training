using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API._Repositories.Interfaces;
using API.Data;
using API.Models;

namespace API._Repositories.Repositories
{
    public class MS_QR_Storage_Repository : Repository<MS_QR_Storage>, IMS_QR_Storage_Repository
    {
        private MyDBContext _context;
        public MS_QR_Storage_Repository(MyDBContext context) : base(context)
        {
            _context = context;
        }
    }
}