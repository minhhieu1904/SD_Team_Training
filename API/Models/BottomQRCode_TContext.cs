using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace API.Models
{
    public partial class BottomQRCode_TContext : DbContext
    {
        public BottomQRCode_TContext()
        {
        }

        public BottomQRCode_TContext(DbContextOptions<BottomQRCode_TContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; } = null!;
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; } = null!;
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; } = null!;
        public virtual DbSet<MigrationHistory> MigrationHistories { get; set; } = null!;
        public virtual DbSet<MsDepartment> MsDepartments { get; set; } = null!;
        public virtual DbSet<MsLocation> MsLocations { get; set; } = null!;
        public virtual DbSet<MsPackage> MsPackages { get; set; } = null!;
        public virtual DbSet<MsQrCycle> MsQrCycles { get; set; } = null!;
        public virtual DbSet<MsQrLabel> MsQrLabels { get; set; } = null!;
        public virtual DbSet<MsQrOrder> MsQrOrders { get; set; } = null!;
        public virtual DbSet<MsQrOrderLogOnlyForSt> MsQrOrderLogOnlyForSts { get; set; } = null!;
        public virtual DbSet<MsQrPickingDetail> MsQrPickingDetails { get; set; } = null!;
        public virtual DbSet<MsQrPickingMain> MsQrPickingMains { get; set; } = null!;
        public virtual DbSet<MsQrSort> MsQrSorts { get; set; } = null!;
        public virtual DbSet<MsQrStorage> MsQrStorages { get; set; } = null!;
        public virtual DbSet<MsQrStorageOut> MsQrStorageOuts { get; set; } = null!;
        public virtual DbSet<MsShift> MsShifts { get; set; } = null!;
        public virtual DbSet<MsWarehouse> MsWarehouses { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<RoleUser> RoleUsers { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=10.4.4.229;Database=BottomQRCode_T;MultipleActiveResultSets=true;User Id=sd_local;Password=shc@ssb1234;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasMany(d => d.Roles)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "AspNetUserRole",
                        l => l.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId").HasConstraintName("FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId"),
                        r => r.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId").HasConstraintName("FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId"),
                        j =>
                        {
                            j.HasKey("UserId", "RoleId").HasName("PK_dbo.AspNetUserRoles");

                            j.ToTable("AspNetUserRoles");

                            j.IndexerProperty<string>("UserId").HasMaxLength(128);

                            j.IndexerProperty<string>("RoleId").HasMaxLength(128);
                        });
            });

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId");
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey, e.UserId })
                    .HasName("PK_dbo.AspNetUserLogins");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId");
            });

            modelBuilder.Entity<MigrationHistory>(entity =>
            {
                entity.HasKey(e => new { e.MigrationId, e.ContextKey })
                    .HasName("PK_dbo.__MigrationHistory");
            });

            modelBuilder.Entity<MsDepartment>(entity =>
            {
                entity.HasKey(e => new { e.Manuf, e.ParNo })
                    .HasName("pk_MS_Department");
            });

            modelBuilder.Entity<MsLocation>(entity =>
            {
                entity.HasKey(e => new { e.Manuf, e.StoreH })
                    .HasName("pk_MS_Location");
            });

            modelBuilder.Entity<MsPackage>(entity =>
            {
                entity.HasKey(e => new { e.Manuf, e.PackageNo })
                    .HasName("pk_MS_Package");
            });

            modelBuilder.Entity<MsQrCycle>(entity =>
            {
                entity.HasKey(e => new { e.Purno, e.Manno, e.Seq, e.Cyno, e.Size, e.Manuf })
                    .HasName("pk_MS_QR_Cycle");

                entity.HasComment("回轉包裝明細檔");

                entity.Property(e => e.Purno).HasComment("訂購單號");

                entity.Property(e => e.Manno).HasComment("企劃單號");

                entity.Property(e => e.Seq).HasComment("批次");

                entity.Property(e => e.Cyno).HasComment("迴轉");

                entity.Property(e => e.Size).HasComment("尺寸");

                entity.Property(e => e.Manuf)
                    .IsFixedLength()
                    .HasComment("廠別");

                entity.Property(e => e.Article).HasComment("鞋型");

                entity.Property(e => e.BizKey).HasComputedColumnSql("((((([manuf]+[purno])+[manno])+CONVERT([char](3),[seq]))+CONVERT([char](21),[cyno]))+[size])", false);

                entity.Property(e => e.Cqty).HasComment("取消列印雙數");

                entity.Property(e => e.Endcod)
                    .HasDefaultValueSql("('N')")
                    .IsFixedLength()
                    .HasComment("完成列印否");

                entity.Property(e => e.Pqty).HasComment("已列印雙數");

                entity.Property(e => e.Qty).HasComment("雙數");

                entity.Property(e => e.UpdateTime).HasComment("異動時間");

                entity.Property(e => e.UpdatedBy).HasComment("異動者");
            });

            modelBuilder.Entity<MsQrLabel>(entity =>
            {
                entity.HasKey(e => new { e.Manuf, e.QrcodeId })
                    .HasName("PK_MS_QR_Label_1");

                entity.Property(e => e.Flag).HasDefaultValueSql("('Y')");

                entity.Property(e => e.Grade).IsFixedLength();

                entity.Property(e => e.ManNo).HasDefaultValueSql("(N'ManNo')");

                entity.Property(e => e.PrtCnt).HasDefaultValueSql("((1))");

                entity.Property(e => e.Prtno).HasComment("列印單號");

                entity.Property(e => e.QrcodeValue).HasComputedColumnSql("(((((((((((((((([ManNo]+',')+[PurNo])+',')+[Size])+',')+CONVERT([nvarchar](5),[Qty]))+',')+CONVERT([nvarchar](5),[Serial]))+',')+[wkshno])+',')+[prtno])+',')+[empno])+',')+[Grade])", false);

                entity.Property(e => e.Type).HasDefaultValueSql("('A')");

                entity.Property(e => e.Wkshno).HasComment("派工單號");
            });

            modelBuilder.Entity<MsQrOrder>(entity =>
            {
                entity.HasKey(e => new { e.Manuf, e.Purno, e.Manno, e.Size, e.Wkshno, e.Prtno })
                    .HasName("PK_MS_QR_Order_1");

                entity.HasComment("企劃單明細檔");

                entity.Property(e => e.Manuf).HasComment("廠別");

                entity.Property(e => e.Purno).HasComment("訂購單號");

                entity.Property(e => e.Manno).HasComment("企劃單號");

                entity.Property(e => e.Size).HasComment("接單尺寸");

                entity.Property(e => e.Wkshno).HasComment("派工單號");

                entity.Property(e => e.Prtno).HasComment("列印單號");

                entity.Property(e => e.Addqty).HasComment("企劃單增加");

                entity.Property(e => e.Article).HasComment("Article");

                entity.Property(e => e.Bitnbr).HasComment("品號");

                entity.Property(e => e.BizKey).HasComputedColumnSql("((([manuf]+[purno])+[manno])+[size])", false);

                entity.Property(e => e.Brandname).HasComment("品牌名稱");

                entity.Property(e => e.Cqty).HasComment("取消列印雙數");

                entity.Property(e => e.Cusid).HasComment("客戶編號");

                entity.Property(e => e.Cusna).HasComment("客戶名稱");

                entity.Property(e => e.CycleFlag)
                    .HasDefaultValueSql("('N')")
                    .HasComment("");

                entity.Property(e => e.Endcod)
                    .HasDefaultValueSql("('N')")
                    .HasComment("完成列印否");

                entity.Property(e => e.Eta).HasComment("確認交期");

                entity.Property(e => e.Kind).HasComment("訂單類別");

                entity.Property(e => e.Lessqty).HasComment("企劃單減少");

                entity.Property(e => e.Mdat).HasComment("生產日期");

                entity.Property(e => e.Pqty).HasComment("訂購數");

                entity.Property(e => e.Qty).HasComment("訂購數");

                entity.Property(e => e.Ritnbr).HasComment("鞋廠料號");

                entity.Property(e => e.Rmodel).HasComment("模具編號");

                entity.Property(e => e.Style).HasComment("鞋型");

                entity.Property(e => e.Tolcls).HasComment("工具類別");

                entity.Property(e => e.Tsize).HasComment("工具尺寸");

                entity.Property(e => e.UpdateTime).HasComment("異動時間");

                entity.Property(e => e.UpdatedBy).HasComment("異動者");

                entity.Property(e => e.Uscod).IsFixedLength();

                entity.Property(e => e.Wkshqty).HasComment("派工雙數");
            });

            modelBuilder.Entity<MsQrOrderLogOnlyForSt>(entity =>
            {
                entity.HasKey(e => new { e.Manuf, e.Purno, e.Manno, e.Size, e.Wkshno, e.Prtno });

                entity.Property(e => e.Manuf).HasComment("廠別");

                entity.Property(e => e.Purno).HasComment("訂購單號");

                entity.Property(e => e.Manno).HasComment("企劃單號");

                entity.Property(e => e.Size).HasComment("接單尺寸");

                entity.Property(e => e.Wkshno).HasComment("派工單號");

                entity.Property(e => e.Prtno).HasComment("列印單號");

                entity.Property(e => e.Addqty).HasComment("企劃單增加");

                entity.Property(e => e.Article).HasComment("Article");

                entity.Property(e => e.Bitnbr).HasComment("品號");

                entity.Property(e => e.Brandname).HasComment("品牌名稱");

                entity.Property(e => e.Cqty).HasComment("取消列印雙數");

                entity.Property(e => e.Cusid).HasComment("客戶編號");

                entity.Property(e => e.Cusna).HasComment("客戶名稱");

                entity.Property(e => e.CycleFlag)
                    .HasDefaultValueSql("('N')")
                    .HasComment("");

                entity.Property(e => e.Endcod)
                    .HasDefaultValueSql("('N')")
                    .HasComment("完成列印否");

                entity.Property(e => e.Eta).HasComment("確認交期");

                entity.Property(e => e.Kind).HasComment("訂單類別");

                entity.Property(e => e.Lessqty).HasComment("企劃單減少");

                entity.Property(e => e.Mdat).HasComment("生產日期");

                entity.Property(e => e.Pqty).HasComment("訂購數");

                entity.Property(e => e.Qty).HasComment("訂購數");

                entity.Property(e => e.Ritnbr).HasComment("鞋廠料號");

                entity.Property(e => e.Rmodel).HasComment("模具編號");

                entity.Property(e => e.Style).HasComment("鞋型");

                entity.Property(e => e.Tolcls).HasComment("工具類別");

                entity.Property(e => e.Tsize).HasComment("工具尺寸");

                entity.Property(e => e.UpdateTime).HasComment("異動時間");

                entity.Property(e => e.UpdatedBy).HasComment("異動者");

                entity.Property(e => e.Uscod).IsFixedLength();

                entity.Property(e => e.Wkshqty).HasComment("派工雙數");
            });

            modelBuilder.Entity<MsQrPickingDetail>(entity =>
            {
                entity.HasKey(e => new { e.Manuf, e.PickingTrNo, e.QrcodeId });

                entity.Property(e => e.Bitnbr).HasComment("品號");

                entity.Property(e => e.Grade).IsFixedLength();

                entity.Property(e => e.Prtno).HasComment("列印單號");

                entity.Property(e => e.Ritnbr).HasComment("鞋廠料號");

                entity.Property(e => e.Wkshno).HasComment("派工單號");
            });

            modelBuilder.Entity<MsQrPickingMain>(entity =>
            {
                entity.HasKey(e => new { e.Manuf, e.PickingTrNo });

                entity.Property(e => e.Status).HasDefaultValueSql("('N')");
            });

            modelBuilder.Entity<MsQrSort>(entity =>
            {
                entity.HasKey(e => new { e.Manuf, e.TrNo, e.QrcodeId });

                entity.Property(e => e.EndCod).HasDefaultValueSql("('N')");

                entity.Property(e => e.Grade).IsFixedLength();
            });

            modelBuilder.Entity<MsQrStorage>(entity =>
            {
                entity.HasKey(e => new { e.Manuf, e.Trno, e.QrcodeId });

                entity.Property(e => e.Bitnbr).HasComment("品號");

                entity.Property(e => e.BizFlag).HasDefaultValueSql("('N')");

                entity.Property(e => e.Grade).IsFixedLength();

                entity.Property(e => e.IsPicking).HasDefaultValueSql("('N')");

                entity.Property(e => e.IsStorageOut).HasDefaultValueSql("('N')");

                entity.Property(e => e.Prtno).HasComment("列印單號");

                entity.Property(e => e.Ritnbr).HasComment("鞋廠料號");

                entity.Property(e => e.Wkshno).HasComment("派工單號");
            });

            modelBuilder.Entity<MsQrStorageOut>(entity =>
            {
                entity.HasKey(e => new { e.Manuf, e.Iono, e.QrcodeId });

                entity.Property(e => e.Bitnbr).HasComment("品號");

                entity.Property(e => e.BizFlag).HasDefaultValueSql("('N')");

                entity.Property(e => e.Grade).IsFixedLength();

                entity.Property(e => e.Prtno).HasComment("列印單號");

                entity.Property(e => e.Ritnbr).HasComment("鞋廠料號");

                entity.Property(e => e.Wkshno).HasComment("派工單號");
            });

            modelBuilder.Entity<MsShift>(entity =>
            {
                entity.HasKey(e => new { e.Manuf, e.Shift })
                    .HasName("pk_MS_Shift");
            });

            modelBuilder.Entity<MsWarehouse>(entity =>
            {
                entity.Property(e => e.Warehouse).IsFixedLength();

                entity.Property(e => e.WarehouseName).IsFixedLength();
            });

            modelBuilder.Entity<RoleUser>(entity =>
            {
                entity.HasKey(e => new { e.UserAccount, e.RoleUnique })
                    .HasName("PK_RoleUser_1");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
