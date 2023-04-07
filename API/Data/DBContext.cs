using System;
using System.Collections.Generic;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public partial class DBContext : DbContext
{
    public DBContext()
    {
    }

    public DBContext(DbContextOptions<DBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<MS_Department> MS_Departments { get; set; }

    public virtual DbSet<MS_Location> MS_Locations { get; set; }

    public virtual DbSet<MS_Package> MS_Packages { get; set; }

    public virtual DbSet<MS_QR_Cycle> MS_QR_Cycles { get; set; }

    public virtual DbSet<MS_QR_Label> MS_QR_Labels { get; set; }

    public virtual DbSet<MS_QR_Order> MS_QR_Orders { get; set; }

    public virtual DbSet<MS_QR_Order_Log_OnlyForST> MS_QR_Order_Log_OnlyForSTs { get; set; }

    public virtual DbSet<MS_QR_PickingDetail> MS_QR_PickingDetails { get; set; }

    public virtual DbSet<MS_QR_PickingMain> MS_QR_PickingMains { get; set; }

    public virtual DbSet<MS_QR_Sort> MS_QR_Sorts { get; set; }

    public virtual DbSet<MS_QR_Storage> MS_QR_Storages { get; set; }

    public virtual DbSet<MS_QR_StorageOut> MS_QR_StorageOuts { get; set; }

    public virtual DbSet<MS_Shift> MS_Shifts { get; set; }

    public virtual DbSet<MS_Warehouse> MS_Warehouses { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RoleUser> RoleUsers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<__MigrationHistory> __MigrationHistories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=10.4.4.229;Database=BottomQRCode_T;MultipleActiveResultSets=true;User Id=sd_local;Password=shc@ssb1234;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.AspNetRoles");
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.AspNetUsers");

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRole",
                    r => r.HasOne<AspNetRole>().WithMany()
                        .HasForeignKey("RoleId")
                        .HasConstraintName("FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId"),
                    l => l.HasOne<AspNetUser>().WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId").HasName("PK_dbo.AspNetUserRoles");
                    });
        });

        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.AspNetUserClaims");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasConstraintName("FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId");
        });

        modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey, e.UserId }).HasName("PK_dbo.AspNetUserLogins");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasConstraintName("FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId");
        });

        modelBuilder.Entity<MS_Department>(entity =>
        {
            entity.HasKey(e => new { e.Manuf, e.ParNo }).HasName("pk_MS_Department");
        });

        modelBuilder.Entity<MS_Location>(entity =>
        {
            entity.HasKey(e => new { e.Manuf, e.StoreH }).HasName("pk_MS_Location");
        });

        modelBuilder.Entity<MS_Package>(entity =>
        {
            entity.HasKey(e => new { e.Manuf, e.PackageNo }).HasName("pk_MS_Package");
        });

        modelBuilder.Entity<MS_QR_Cycle>(entity =>
        {
            entity.HasKey(e => new { e.purno, e.manno, e.seq, e.cyno, e.size, e.manuf }).HasName("pk_MS_QR_Cycle");

            entity.ToTable("MS_QR_Cycle", tb => tb.HasComment("回轉包裝明細檔"));

            entity.Property(e => e.purno).HasComment("訂購單號");
            entity.Property(e => e.manno).HasComment("企劃單號");
            entity.Property(e => e.seq).HasComment("批次");
            entity.Property(e => e.cyno).HasComment("迴轉");
            entity.Property(e => e.size).HasComment("尺寸");
            entity.Property(e => e.manuf)
                .IsFixedLength()
                .HasComment("廠別");
            entity.Property(e => e.Biz_Key).HasComputedColumnSql("((((([manuf]+[purno])+[manno])+CONVERT([char](3),[seq]))+CONVERT([char](21),[cyno]))+[size])", false);
            entity.Property(e => e.article).HasComment("鞋型");
            entity.Property(e => e.cqty).HasComment("取消列印雙數");
            entity.Property(e => e.endcod)
                .HasDefaultValueSql("('N')")
                .IsFixedLength()
                .HasComment("完成列印否");
            entity.Property(e => e.pqty).HasComment("已列印雙數");
            entity.Property(e => e.qty).HasComment("雙數");
            entity.Property(e => e.update_time).HasComment("異動時間");
            entity.Property(e => e.updated_by).HasComment("異動者");
        });

        modelBuilder.Entity<MS_QR_Label>(entity =>
        {
            entity.HasKey(e => new { e.Manuf, e.QRCodeID }).HasName("PK_MS_QR_Label_1");

            entity.Property(e => e.Flag).HasDefaultValueSql("('Y')");
            entity.Property(e => e.Grade).IsFixedLength();
            entity.Property(e => e.ManNo).HasDefaultValueSql("(N'ManNo')");
            entity.Property(e => e.Prt_Cnt).HasDefaultValueSql("((1))");
            entity.Property(e => e.QRCodeValue).HasComputedColumnSql("(((((((((((((((([ManNo]+',')+[PurNo])+',')+[Size])+',')+CONVERT([nvarchar](5),[Qty]))+',')+CONVERT([nvarchar](5),[Serial]))+',')+[wkshno])+',')+[prtno])+',')+[empno])+',')+[Grade])", false);
            entity.Property(e => e.Type).HasDefaultValueSql("('A')");
            entity.Property(e => e.prtno).HasComment("列印單號");
            entity.Property(e => e.wkshno).HasComment("派工單號");
        });

        modelBuilder.Entity<MS_QR_Order>(entity =>
        {
            entity.HasKey(e => new { e.manuf, e.purno, e.manno, e.size, e.wkshno, e.prtno }).HasName("PK_MS_QR_Order_1");

            entity.ToTable("MS_QR_Order", tb => tb.HasComment("企劃單明細檔"));

            entity.Property(e => e.manuf).HasComment("廠別");
            entity.Property(e => e.purno).HasComment("訂購單號");
            entity.Property(e => e.manno).HasComment("企劃單號");
            entity.Property(e => e.size).HasComment("接單尺寸");
            entity.Property(e => e.wkshno).HasComment("派工單號");
            entity.Property(e => e.prtno).HasComment("列印單號");
            entity.Property(e => e.Biz_Key).HasComputedColumnSql("((([manuf]+[purno])+[manno])+[size])", false);
            entity.Property(e => e.addqty).HasComment("企劃單增加");
            entity.Property(e => e.article).HasComment("Article");
            entity.Property(e => e.bitnbr).HasComment("品號");
            entity.Property(e => e.brandname).HasComment("品牌名稱");
            entity.Property(e => e.cqty).HasComment("取消列印雙數");
            entity.Property(e => e.cusid).HasComment("客戶編號");
            entity.Property(e => e.cusna).HasComment("客戶名稱");
            entity.Property(e => e.cycle_flag)
                .HasDefaultValueSql("('N')")
                .HasComment("");
            entity.Property(e => e.endcod)
                .HasDefaultValueSql("('N')")
                .HasComment("完成列印否");
            entity.Property(e => e.eta).HasComment("確認交期");
            entity.Property(e => e.kind).HasComment("訂單類別");
            entity.Property(e => e.lessqty).HasComment("企劃單減少");
            entity.Property(e => e.mdat).HasComment("生產日期");
            entity.Property(e => e.pqty).HasComment("訂購數");
            entity.Property(e => e.qty).HasComment("訂購數");
            entity.Property(e => e.ritnbr).HasComment("鞋廠料號");
            entity.Property(e => e.rmodel).HasComment("模具編號");
            entity.Property(e => e.style).HasComment("鞋型");
            entity.Property(e => e.tolcls).HasComment("工具類別");
            entity.Property(e => e.tsize).HasComment("工具尺寸");
            entity.Property(e => e.update_time).HasComment("異動時間");
            entity.Property(e => e.updated_by).HasComment("異動者");
            entity.Property(e => e.uscod).IsFixedLength();
            entity.Property(e => e.wkshqty).HasComment("派工雙數");
        });

        modelBuilder.Entity<MS_QR_Order_Log_OnlyForST>(entity =>
        {
            entity.Property(e => e.manuf).HasComment("廠別");
            entity.Property(e => e.purno).HasComment("訂購單號");
            entity.Property(e => e.manno).HasComment("企劃單號");
            entity.Property(e => e.size).HasComment("接單尺寸");
            entity.Property(e => e.wkshno).HasComment("派工單號");
            entity.Property(e => e.prtno).HasComment("列印單號");
            entity.Property(e => e.addqty).HasComment("企劃單增加");
            entity.Property(e => e.article).HasComment("Article");
            entity.Property(e => e.bitnbr).HasComment("品號");
            entity.Property(e => e.brandname).HasComment("品牌名稱");
            entity.Property(e => e.cqty).HasComment("取消列印雙數");
            entity.Property(e => e.cusid).HasComment("客戶編號");
            entity.Property(e => e.cusna).HasComment("客戶名稱");
            entity.Property(e => e.cycle_flag)
                .HasDefaultValueSql("('N')")
                .HasComment("");
            entity.Property(e => e.endcod)
                .HasDefaultValueSql("('N')")
                .HasComment("完成列印否");
            entity.Property(e => e.eta).HasComment("確認交期");
            entity.Property(e => e.kind).HasComment("訂單類別");
            entity.Property(e => e.lessqty).HasComment("企劃單減少");
            entity.Property(e => e.mdat).HasComment("生產日期");
            entity.Property(e => e.pqty).HasComment("訂購數");
            entity.Property(e => e.qty).HasComment("訂購數");
            entity.Property(e => e.ritnbr).HasComment("鞋廠料號");
            entity.Property(e => e.rmodel).HasComment("模具編號");
            entity.Property(e => e.style).HasComment("鞋型");
            entity.Property(e => e.tolcls).HasComment("工具類別");
            entity.Property(e => e.tsize).HasComment("工具尺寸");
            entity.Property(e => e.update_time).HasComment("異動時間");
            entity.Property(e => e.updated_by).HasComment("異動者");
            entity.Property(e => e.uscod).IsFixedLength();
            entity.Property(e => e.wkshqty).HasComment("派工雙數");
        });

        modelBuilder.Entity<MS_QR_PickingDetail>(entity =>
        {
            entity.Property(e => e.Grade).IsFixedLength();
            entity.Property(e => e.bitnbr).HasComment("品號");
            entity.Property(e => e.prtno).HasComment("列印單號");
            entity.Property(e => e.ritnbr).HasComment("鞋廠料號");
            entity.Property(e => e.wkshno).HasComment("派工單號");
        });

        modelBuilder.Entity<MS_QR_PickingMain>(entity =>
        {
            entity.Property(e => e.status).HasDefaultValueSql("('N')");
        });

        modelBuilder.Entity<MS_QR_Sort>(entity =>
        {
            entity.Property(e => e.EndCod).HasDefaultValueSql("('N')");
            entity.Property(e => e.Grade).IsFixedLength();
        });

        modelBuilder.Entity<MS_QR_Storage>(entity =>
        {
            entity.Property(e => e.Grade).IsFixedLength();
            entity.Property(e => e.IsPicking).HasDefaultValueSql("('N')");
            entity.Property(e => e.IsStorageOut).HasDefaultValueSql("('N')");
            entity.Property(e => e.bitnbr).HasComment("品號");
            entity.Property(e => e.biz_flag).HasDefaultValueSql("('N')");
            entity.Property(e => e.prtno).HasComment("列印單號");
            entity.Property(e => e.ritnbr).HasComment("鞋廠料號");
            entity.Property(e => e.wkshno).HasComment("派工單號");
        });

        modelBuilder.Entity<MS_QR_StorageOut>(entity =>
        {
            entity.Property(e => e.Grade).IsFixedLength();
            entity.Property(e => e.bitnbr).HasComment("品號");
            entity.Property(e => e.biz_flag).HasDefaultValueSql("('N')");
            entity.Property(e => e.prtno).HasComment("列印單號");
            entity.Property(e => e.ritnbr).HasComment("鞋廠料號");
            entity.Property(e => e.wkshno).HasComment("派工單號");
        });

        modelBuilder.Entity<MS_Shift>(entity =>
        {
            entity.HasKey(e => new { e.Manuf, e.Shift }).HasName("pk_MS_Shift");
        });

        modelBuilder.Entity<MS_Warehouse>(entity =>
        {
            entity.Property(e => e.Warehouse).IsFixedLength();
            entity.Property(e => e.WarehouseName).IsFixedLength();
        });

        modelBuilder.Entity<RoleUser>(entity =>
        {
            entity.HasKey(e => new { e.user_account, e.role_unique }).HasName("PK_RoleUser_1");
        });

        modelBuilder.Entity<__MigrationHistory>(entity =>
        {
            entity.HasKey(e => new { e.MigrationId, e.ContextKey }).HasName("PK_dbo.__MigrationHistory");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
