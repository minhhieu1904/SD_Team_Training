using API.Models;
using Microsoft.EntityFrameworkCore;
using API.DTOs;
using API.DTOs.Report;

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

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<MigrationHistory> MigrationHistories { get; set; }
        public virtual DbSet<MsDepartment> MsDepartments { get; set; }
        public virtual DbSet<MsLocation> MsLocations { get; set; }
        public virtual DbSet<MsPackage> MsPackages { get; set; }
        public virtual DbSet<MsQrCycle> MsQrCycles { get; set; }
        public virtual DbSet<MsQrLabel> MsQrLabels { get; set; }
        public virtual DbSet<MsQrOrder> MsQrOrders { get; set; }
        public virtual DbSet<MsQrOrderLogOnlyForSt> MsQrOrderLogOnlyForSts { get; set; }
        public virtual DbSet<MsQrPickingDetail> MsQrPickingDetails { get; set; }
        public virtual DbSet<MsQrPickingMain> MsQrPickingMains { get; set; }
        public virtual DbSet<MsQrSort> MsQrSorts { get; set; }
        public virtual DbSet<MsQrStorage> MsQrStorages { get; set; }
        public virtual DbSet<MsQrStorageOut> MsQrStorageOuts { get; set; }
        public virtual DbSet<MsShift> MsShifts { get; set; }
        public virtual DbSet<MsWarehouse> MsWarehouses { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<RoleUser> RoleUsers { get; set; }
        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<WkshSumReportDTO> SearchForPackingScans { get; set; }
        public virtual DbSet<SortSumReportDTO> SortSumReports { get; set; }

        public virtual DbSet<StorageSumReportDTO> StorageSumReports { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

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
                entity.HasKey(e => new { e.Manuf, e.QRCodeID })
                    .HasName("PK_MS_QR_Label_1");

                entity.Property(e => e.Flag).HasDefaultValueSql("('Y')");

                entity.Property(e => e.Grade).IsFixedLength();

                entity.Property(e => e.ManNo).HasDefaultValueSql("(N'ManNo')");

                entity.Property(e => e.Prt_Cnt).HasDefaultValueSql("((1))");

                entity.Property(e => e.prtno).HasComment("列印單號");

                entity.Property(e => e.QRCodeValue).HasComputedColumnSql("(((((((((((((((([ManNo]+',')+[PurNo])+',')+[Size])+',')+CONVERT([nvarchar](5),[Qty]))+',')+CONVERT([nvarchar](5),[Serial]))+',')+[wkshno])+',')+[prtno])+',')+[empno])+',')+[Grade])", false);

                entity.Property(e => e.Type).HasDefaultValueSql("('A')");

                entity.Property(e => e.wkshno).HasComment("派工單號");
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

            modelBuilder.Entity<WkshSumReportDTO>(entity =>
           {
               entity.HasKey(e => new { e.purno, e.manno, e.size });
           });

            modelBuilder.Entity<SortSumReportDTO>().HasNoKey();

            modelBuilder.Entity<StorageSumReportDTO>().HasNoKey();

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}