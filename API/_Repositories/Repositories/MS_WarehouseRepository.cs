using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API._Repositories.Interfaces;
using API.Data;
using API.Models;

namespace API._Repositories.Repositories
{
    public class MS_WarehouseRepository : Repository<MS_Warehouse>, IMS_WarehouseRepository
    {
        public MS_WarehouseRepository(DBContext context) : base(context)
        {
        }
    }
}