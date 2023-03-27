
using API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

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

        public class BlankTriggerAddingConvention : IModelFinalizingConvention
        {
            public virtual void ProcessModelFinalizing(
                IConventionModelBuilder modelBuilder,
                IConventionContext<IConventionModelBuilder> context)
            {
                foreach (var entityType in modelBuilder.Metadata.GetEntityTypes())
                {
                    var table = StoreObjectIdentifier.Create(entityType, StoreObjectType.Table);
                    if (table != null
                        && entityType.GetDeclaredTriggers().All(t => t.GetDatabaseName(table.Value) == null))
                    {
                        entityType.Builder.HasTrigger(table.Value.Name + "_Trigger");
                    }

                    foreach (var fragment in entityType.GetMappingFragments(StoreObjectType.Table))
                    {
                        if (entityType.GetDeclaredTriggers().All(t => t.GetDatabaseName(fragment.StoreObject) == null))
                        {
                            entityType.Builder.HasTrigger(fragment.StoreObject.Name + "_Trigger");
                        }
                    }
                }
            }
        }

        public virtual DbSet<MS_Department> MS_Department { get; set; }
        public virtual DbSet<MS_Location> MS_Location { get; set; }
        public virtual DbSet<MS_Package> MS_Package { get; set; }
        public virtual DbSet<MS_QR_Cycle> MS_QR_Cycle { get; set; }
        public virtual DbSet<MS_QR_Label> MS_QR_Label { get; set; }
        public virtual DbSet<MS_QR_Order> MS_QR_Order { get; set; }
        public virtual DbSet<MS_QR_Sort> MS_QR_Sort { get; set; }
        public virtual DbSet<MS_QR_Storage> MS_QR_Storage { get; set; }
        public virtual DbSet<MS_Shift> MS_Shift { get; set; }
        public virtual DbSet<RoleUser> RoleUser { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Report_Sort_SumResult> Report_Sort_SumResult { get; set; }
        public virtual DbSet<Report_Storage_SumResult> Report_Storage_SumResult { get; set; }
        public virtual DbSet<Report_Wksh_SumResult> Report_Wksh_SumResult { get; set; }
        public virtual DbSet<MS_QR_PickingDetail> MS_QR_PickingDetail { get; set; }
        public virtual DbSet<MS_QR_PickingMain> MS_QR_PickingMain { get; set; }
        public virtual DbSet<Report_New4_5_ByMidasResult> Report_New4_5_ByMidasResult { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Report_Storage_SumResult>(etity =>
            {
                etity.HasNoKey();
            });
            modelBuilder.HasAnnotation("Relational:Collation", "Chinese_Taiwan_Stroke_CI_AS");

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
                entity.Property(e => e.QRCodeValue).HasComputedColumnSql("(((((((((((((((([ManNo]+',')+[PurNo])+',')+[Size])+',')+CONVERT([nvarchar](5),[Qty]))+',')+CONVERT([nvarchar](5),[Serial]))+',')+[wkshno])+',')+[prtno])+',')+[empno])+',')+[Grade])");
            });

            modelBuilder.Entity<MS_QR_Order>(entity =>
            {
                entity.HasKey(e => new { e.manuf, e.purno, e.manno, e.size, e.wkshno, e.prtno });
            });

            modelBuilder.Entity<MS_QR_Sort>(entity =>
            {
                entity.HasKey(e => new { e.Manuf, e.TrNo, e.QRCodeID });
            });

            modelBuilder.Entity<MS_QR_Storage>(entity =>
            {
                entity.HasKey(e => new { e.manuf, e.trno, e.QRCodeID });
            });

            modelBuilder.Entity<MS_Shift>(entity =>
            {
                entity.HasKey(e => new { e.Manuf, e.Shift });
            });
            modelBuilder.Entity<RoleUser>(entity =>
            {
                entity.HasKey(e => new { e.user_account, e.role_unique });
            });
            modelBuilder.Entity<Report_Sort_SumResult>(entity =>
            {
                entity.HasNoKey();
            });
            modelBuilder.Entity<Report_Wksh_SumResult>(entity =>
            {
                entity.HasNoKey().ToView(null);
            });
            OnModelCreatingPartial(modelBuilder);
            modelBuilder.Entity<MS_QR_PickingDetail>(entity =>
            {
                entity.HasKey(e => new { e.manuf, e.iono, e.QRCodeID });

                entity.Property(e => e.bitnbr).HasComment("品號");

                entity.Property(e => e.Grade)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.prtno).HasComment("列印單號");

                entity.Property(e => e.ritnbr).HasComment("鞋廠料號");

                entity.Property(e => e.wkshno).HasComment("派工單號");
            });

            modelBuilder.Entity<MS_QR_PickingMain>(entity =>
            {
                entity.HasKey(e => new { e.iono, e.manuf, e.manno, e.purno, e.size });

                entity.Property(e => e.iono).HasComment("出庫單號");

                entity.Property(e => e.manuf).HasComment("廠區");

                entity.Property(e => e.manno).HasComment("企劃單號");

                entity.Property(e => e.purno).HasComment("訂購單號");

                entity.Property(e => e.size).HasComment("尺寸");

                entity.Property(e => e.ActualQTY)
                    .HasDefaultValueSql("((0))")
                    .HasComment("實際出貨數");

                entity.Property(e => e.declaration_no).HasComment("報關單號");

                entity.Property(e => e.ExpectQTY).HasComment("預計出貨數");

                entity.Property(e => e.ExpectStorageOutDate).HasComment("預計出貨日");

                entity.Property(e => e.invno).HasComment("發票");

                entity.Property(e => e.ReleaseDate).HasComment("放行日期");

                entity.Property(e => e.status)
                    .HasDefaultValueSql("('N')")
                    .HasComment("狀態");

                entity.Property(e => e.StorageOutDate).HasComment("出庫日期");

                entity.Property(e => e.update_time).HasComment("更新日期");

                entity.Property(e => e.updated_by).HasComment("更新人員");
            });

            modelBuilder.Entity<Report_New4_5_ByMidasResult>().HasNoKey().ToView(null);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Conventions.Add(_ => new BlankTriggerAddingConvention());
        }

    }
}