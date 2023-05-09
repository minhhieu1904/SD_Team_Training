using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API._Repositories.Interfaces;
using API.Data;
using API.Models;

namespace API._Repositories.Repositories
{
    public class MS_QR_SortRepository : Repository<MS_QR_Sort>, IMS_QR_SortRepository
    {
        public MS_QR_SortRepository(DBContext context) : base(context)
        {
        }
    }
}