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
            
        }
        public virtual DbSet<MS_Package> MS_Package { get; set; }
        
         protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");
            modelBuilder.Entity<MS_Package>(entity =>
            {
                entity.HasKey(e => new { e.Manuf, e.PackageNo })
                    .HasName("pk_MS_Package");
            });
            OnModelCreatingPartial(modelBuilder);
        }
         partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}