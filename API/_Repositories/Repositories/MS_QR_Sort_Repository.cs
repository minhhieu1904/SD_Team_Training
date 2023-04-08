using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API._Repositories.Interfaces;
using API.Data;
using API.Models.report;

namespace API._Repositories.Repositories
{
    public class MS_QR_Sort_Repository : Repository<MS_QR_Sort>, IMS_QR_Sort_Repository
    {
        public MS_QR_Sort_Repository(DBContext context) : base(context)
        {
        }
    }
}