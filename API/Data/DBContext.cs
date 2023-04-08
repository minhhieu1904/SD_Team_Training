using API.Models;
using API.Models.report;
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
        public virtual DbSet<MS_QR_Order> MS_QR_Order { get; set; }

        public virtual DbSet<Report_wksh_SumResult> Report_wksh_SumResult { get; set; }
        public virtual DbSet<Report_Sort_SumResult> Report_Sort_SumResult { get; set; }
        public virtual DbSet<MS_QR_Sort> MS_QR_Sort { get; set; }
        public virtual DbSet<MS_QR_Label> MS_QR_Label { get; set; }

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

            modelBuilder.Entity<MS_QR_Order>(entity =>
            {
                entity.HasKey(e => new { e.manuf, e.purno, e.manno, e.size, e.wkshno, e.prtno });
            });
            modelBuilder.Entity<Report_wksh_SumResult>(entity =>
            {
                entity.HasKey(e => new { e.purno, e.manno, e.size });
            });
            modelBuilder.Entity<Report_Sort_SumResult>().HasNoKey().ToView(null);
            modelBuilder.Entity<MS_QR_Label>(entity =>
            {
                entity.HasKey(e => new { e.Manuf, e.QRCodeID });
            
            });

            modelBuilder.Entity<MS_QR_Sort>(entity =>
            {
                entity.HasKey(e => new { e.Manuf, e.TrNo, e.QRCodeID });
            });

            OnModelCreatingPartial(modelBuilder);
        }


        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}