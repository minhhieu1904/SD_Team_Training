using Microsoft.EntityFrameworkCore;
using API.Models;

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

        public virtual DbSet<MS_QR_Order> MS_QR_Order { get; set; }
        public virtual DbSet<MS_QR_Sort> MS_QR_Sort { get; set; }
        public virtual DbSet<MS_QR_Storage> MS_QR_Storage { get; set; }
        public virtual DbSet<Report_wksh_SumResult> Report_Wksh_SumResult { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MS_QR_Order>(etity =>
            {
                etity.HasKey(e => new { e.manuf, e.purno, e.manno, e.size, e.wkshno, e.prtno });
            });
            modelBuilder.Entity<MS_QR_Sort>(entity =>
            {
                entity.HasKey(e => new { e.Manuf, e.TrNo, e.QRCodeID });
            });
            modelBuilder.Entity<MS_QR_Storage>(entity =>
            {
                entity.HasKey(e => new { e.manuf, e.trno, e.QRCodeID });
            });
            modelBuilder.Entity<Report_wksh_SumResult>(entity =>
            {
                entity.HasNoKey().ToView(null);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}