using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public partial class DBContext : DbContext
    {
        public DBContext()
        {
        }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<MS_Department> MS_Department { get; set; }
        public virtual DbSet<MS_Location> MS_Location { get; set; }
        public virtual DbSet<MS_Package> MS_Package { get; set; }
        public virtual DbSet<MS_QR_Cycle> MS_QR_Cycle { get; set; }
        public virtual DbSet<MS_QR_Label> MS_QR_Label { get; set; }
        public virtual DbSet<MS_QR_Order> MS_QR_Order { get; set; }
        public virtual DbSet<MS_QR_Order_Log_OnlyForST> MS_QR_Order_Log_OnlyForST { get; set; }
        public virtual DbSet<MS_QR_PickingDetail> MS_QR_PickingDetail { get; set; }
        public virtual DbSet<MS_QR_PickingMain> MS_QR_PickingMain { get; set; }
        public virtual DbSet<MS_QR_Sort> MS_QR_Sort { get; set; }
        public virtual DbSet<MS_QR_Storage> MS_QR_Storage { get; set; }
        public virtual DbSet<MS_QR_StorageOut> MS_QR_StorageOut { get; set; }
        public virtual DbSet<MS_Shift> MS_Shift { get; set; }
        public virtual DbSet<RoleUser> RoleUser { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Report_wksh_SumParam> Report_Wksh_SumResult {get; set;}
        public virtual DbSet<Report_Sort_SumParam> Report_Sort_SumParam {get; set;}
        public virtual DbSet<Report_Storage_SumParam> Report_Storage_SumParam {get; set;}


        public virtual DbSet<__MigrationHistory> __MigrationHistory { get; set; }


        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
            Database.SetCommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
     
            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
               
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey, e.UserId });
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });
            });

            modelBuilder.Entity<MS_Department>(entity =>
            {
                entity.HasKey(e => new { e.Manuf, e.ParNo });
            });

            modelBuilder.Entity<MS_Location>(entity =>
            {
                entity.HasKey(e => new { e.Manuf, e.StoreH });
            });

            modelBuilder.Entity<MS_Package>(entity =>
            {
                entity.HasKey(e => new { e.Manuf, e.PackageNo });
            });

            modelBuilder.Entity<MS_QR_Cycle>(entity =>
            {
                entity.HasKey(e => new { e.purno, e.manno, e.seq, e.cyno, e.size, e.manuf });
            });

            modelBuilder.Entity<MS_QR_Label>(entity =>
            {
                entity.HasKey(e => new { e.Manuf, e.QRCodeID });
            });

            modelBuilder.Entity<MS_QR_Order>(entity =>
            {
                entity.HasKey(e => new { e.manuf, e.purno, e.manno, e.size, e.wkshno, e.prtno });
            });

            modelBuilder.Entity<MS_QR_Order_Log_OnlyForST>(entity =>
            {
                entity.HasKey(e => new { e.manuf, e.purno, e.manno, e.size, e.wkshno, e.prtno });
            });

            modelBuilder.Entity<MS_QR_PickingDetail>(entity =>
            {
                entity.HasKey(e => new { e.manuf, e.pickingTrNo, e.QRCodeID });
            });

            modelBuilder.Entity<MS_QR_PickingMain>(entity =>
            {
                entity.HasKey(e => new { e.manuf, e.pickingTrNo });
            });

            modelBuilder.Entity<MS_QR_Sort>(entity =>
            {
                entity.HasKey(e => new { e.Manuf, e.TrNo, e.QRCodeID });
            });

            modelBuilder.Entity<MS_QR_Storage>(entity =>
            {
                entity.HasKey(e => new { e.manuf, e.trno, e.QRCodeID });
            });

            modelBuilder.Entity<MS_QR_StorageOut>(entity =>
            {
                entity.HasKey(e => new { e.manuf, e.iono, e.QRCodeID });
            });

            modelBuilder.Entity<MS_Shift>(entity =>
            {
                entity.HasKey(e => new { e.Manuf, e.Shift });
            });

            modelBuilder.Entity<RoleUser>(entity =>
            {
                entity.HasKey(e => new { e.user_account, e.role_unique });
            });

            modelBuilder.Entity<__MigrationHistory>(entity =>
            {
                entity.HasKey(e => new { e.MigrationId, e.ContextKey });
            });
            modelBuilder.Entity<Report_wksh_SumParam>(entity =>
            {
                entity.HasKey(e => new { e.purno, e.manno, e.size });
            });
            modelBuilder.Entity<Report_Sort_SumParam>(entity =>
            {
                entity.HasKey(e => new { e.purno, e.manno, e.size });
            });
            modelBuilder.Entity<Report_Storage_SumParam>(entity =>
            {
                entity.HasKey(e => new { e.purno, e.manno, e.size });
            });
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
