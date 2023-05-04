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
        public virtual DbSet<Report_Storage_SumResult> Report_Storage_SumResult { get; set; }
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
            modelBuilder.Entity<Report_Storage_SumResult>().HasNoKey().ToView(null);
            OnModelCreatingPartial(modelBuilder);
        }


        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}