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

        public virtual DbSet<MS_Shift> MS_Shift { get; set; }
        public virtual DbSet<MS_Department> MS_Department { get; set; }

        public virtual DbSet<MS_Location> MS_Location { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<RoleUser> RoleUser { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<MS_Shift>(entity =>
            {
                entity.HasKey(x => new { x.Manuf, x.Shift });
            });

            modelBuilder.Entity<MS_Department>(entity =>
            {
                entity.HasKey(x => new { x.Manuf, x.ParNo });
            });

            modelBuilder.Entity<MS_Location>(entity =>
            {
                entity.HasKey(x => new { x.Manuf, x.StoreH });
            });
            modelBuilder.Entity<MS_Package>(entity =>
           {
               entity.HasKey(x => new { x.Manuf, x.PackageNo });
           });
            modelBuilder.Entity<Roles>(entity =>
             {
                 entity.HasKey(x => new { x.role_unique });
             });
            modelBuilder.Entity<RoleUser>(entity =>
            {
                entity.HasKey(x => new { x.user_account, x.role_unique });
            });
            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(x => new { x.account });
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}