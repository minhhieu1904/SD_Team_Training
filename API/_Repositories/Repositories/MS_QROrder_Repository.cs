using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API._Repositories.Interfaces;
using API.Data;
using API.Models;

namespace API._Repositories.Repositories
{
    public class MS_QROrder_Repository : Repository<MsQrOrder>, IMS_QROrder_Repository
    {
        public MS_QROrder_Repository(DBContext context) : base(context)
        {
        }
    }
}