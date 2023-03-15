using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API._Repositories.Interfaces;
using API.Data;
using API.Models;

namespace API._Repositories.Repositories
{
    public class MSLocation_Repository : Repository<MS_Location>, IMSLocation_Repository
    {
        public MSLocation_Repository(DBContext context) : base(context)
        {
        }
    }
}