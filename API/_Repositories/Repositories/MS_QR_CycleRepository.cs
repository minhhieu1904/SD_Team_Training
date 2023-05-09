using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API._Repositories.Interfaces;
using API.Data;
using API.Models;

namespace API._Repositories.Repositories
{
    public class MS_QR_CycleRepository :Repository<MS_QR_Cycle>, IMS_QR_CycleRepository
    {
        public MS_QR_CycleRepository(DBContext context) : base(context)
        {
        }
    }
}