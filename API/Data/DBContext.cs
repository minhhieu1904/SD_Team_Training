using API.Models;
using Microsoft.EntityFrameworkCore;


namespace API.Data
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {

        }

        public DBContext()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }
        public virtual DbSet<MS_Shift> MS_Shift { get; set; }
        public virtual DbSet<MS_Location> MS_Location { get; set; }
        public virtual DbSet<MS_Department> MSDepartment { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MS_Shift>()
            .HasKey(m => new { m.Manuf, m.Shift });
            modelBuilder.Entity<MS_Location>()
            .HasKey(m => new { m.Manuf, m.StoreH });
            modelBuilder.Entity<MS_Department>()
           .HasKey(m => new { m.Manuf, m.ParNo });
            modelBuilder.Entity<MS_Package>()
           .HasKey(m => new { m.Manuf, m.PackageNo });
            modelBuilder.Entity<Users>().
           HasKey(m => new { m.Account });
            modelBuilder.Entity<RoleUser>()
            .HasKey(m => new { m.UserAccount, m.RoleUnique });
            modelBuilder.Entity<Roles>().
            HasKey(m => new { m.RoleUnique });
        }

        //entities
        public DbSet<MS_Package> MS_Packages { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<RoleUser> RoleUser { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }

    }
}