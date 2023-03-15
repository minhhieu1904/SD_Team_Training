using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public partial class DBContext : DbContext
    {

        public DBContext()
        {
        }

        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {

            Database.SetCommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds);
        }


        public virtual DbSet<MS_Location> MS_Location { get ; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<MS_Location>(entity =>
            {
                entity.HasKey(x => new {x.Manuf, x.StoreH});
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}