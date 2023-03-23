using API._Repositories.Interfaces;
using API.Data;
using API.Models;

namespace API._Repositories.Repositories
{
    public class AspNetUserClaimsRepository : Repository<AspNetUserClaims>, IAspNetUserClaimsRepository
    {
        private DBContext _context;

        public AspNetUserClaimsRepository(DBContext context) : base(context)
        {
            _context = context;
        }
        
    }
}