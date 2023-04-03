using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API._Repositories.Interfaces;
using API.Data;
using API.Models;

namespace API._Repositories.Repositories
{
    public class MS_QR_OrderRepository : Repository<MS_QR_Order>, IMS_QR_OrderRepository
    {
        private DBContext _context;
        public MS_QR_OrderRepository(DBContext context) : base(context)
        {
            _context = context;
        }


    }
}