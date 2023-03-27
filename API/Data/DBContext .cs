
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options): base(options)
        { 

        }

        public DBContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
      
        public virtual DbSet<MS_Location> MS_Location { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {           
            modelBuilder.Entity<MS_Location>()
            .HasKey(m => new { m.Manuf, m.StoreH});
        }

        //entities
        
    }

}