using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public partial class MyDBContext : DbContext
    {
        public MyDBContext()
        {

        }
        public MyDBContext(DbContextOptions<MyDBContext> options) : base(options)
        {
            Database.SetCommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds);
        }
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
        public virtual DbSet<Report_wksh_SumResult> Report_Wksh_SumResult { get; set; }
        public virtual DbSet<Report_Sort_SumResult> Report_Sort_SumResult { get; set; }
        public virtual DbSet<Report_Storage_SumResult> Report_Storage_SumResult { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<MS_Department>(entity =>
            {
                entity.HasKey(e => new { e.Manuf, e.ParNo })
                    .HasName("pk_MS_Department");
            });

            modelBuilder.Entity<MS_Location>(entity =>
            {
                entity.HasKey(e => new { e.Manuf, e.StoreH })
                    .HasName("pk_MS_Location");
            });
            modelBuilder.Entity<MS_Package>(entity =>
            {
                entity.HasKey(e => new { e.Manuf, e.PackageNo })
                    .HasName("pk_MS_Package");
            });

            modelBuilder.Entity<MS_QR_Cycle>(entity =>
            {
                entity.HasKey(e => new { e.purno, e.manno, e.seq, e.cyno, e.size, e.manuf })
                    .HasName("pk_MS_QR_Cycle");
            });

            modelBuilder.Entity<MS_QR_Label>(entity =>
            {
                entity.HasKey(e => new { e.Manuf, e.QRCodeID })
                    .HasName("PK_MS_QR_Label_1");

                entity.Property(e => e.Flag).HasDefaultValueSql("('Y')");

                entity.Property(e => e.Grade)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<MS_QR_Order>(entity =>
            {
                entity.HasKey(e => new { e.manuf, e.purno, e.manno, e.size, e.wkshno, e.prtno })
                    .HasName("PK_MS_QR_Order_1");
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

                entity.Property(e => e.status).HasDefaultValueSql("('N')");
            });

            modelBuilder.Entity<MS_QR_Sort>(entity =>
            {
                entity.HasKey(e => new { e.Manuf, e.TrNo, e.QRCodeID });

                entity.Property(e => e.EndCod).HasDefaultValueSql("('N')");

                entity.Property(e => e.Grade)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<MS_QR_Storage>(entity =>
            {
                entity.HasKey(e => new { e.manuf, e.trno, e.QRCodeID });

                entity.Property(e => e.Grade)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<MS_QR_StorageOut>(entity =>
            {
                entity.HasKey(e => new { e.manuf, e.iono, e.QRCodeID });

                entity.Property(e => e.Grade)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<MS_Shift>(entity =>
            {
                entity.HasKey(e => new { e.Manuf, e.Shift });
            });

            modelBuilder.Entity<RoleUser>(entity =>
            {
                entity.HasKey(e => new { e.user_account, e.role_unique })
                    .HasName("PK_RoleUser_1");
            });

            modelBuilder.Entity<Report_wksh_SumResult>(entity =>
            {
                entity.HasNoKey().ToView(null);
            });

            modelBuilder.Entity<Report_Sort_SumResult>(entity =>
            {
                entity.HasNoKey();
            });
            modelBuilder.Entity<Report_Storage_SumResult>(entity =>
            {
                entity.HasNoKey();
            });
            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}