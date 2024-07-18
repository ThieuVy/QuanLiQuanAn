using Microsoft.EntityFrameworkCore;
using QuanLiQuanAn.Models;

namespace QuanLiQuanAn.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<CTaiKhoan> TaiKhoans { get; set; }
        public DbSet<CDatBan> DatBans { get; set; }
        public DbSet<CDatMonAn> DatMonAns { get; set; }
        public DbSet<CHoaDon> HoaDons { get; set; }
        public DbSet<CCTHD> CCTHDs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // CTaiKhoan configuration
            modelBuilder.Entity<CTaiKhoan>()
                .HasKey(t => t.TenTK);

            // CDatBan configuration
            modelBuilder.Entity<CDatBan>()
                .HasKey(d => new { d.TenTK });
            modelBuilder.Entity<CDatBan>()
                .HasOne(d => d.TaiKhoan)
                .WithMany(t => t.DatBans)
                .HasForeignKey(d => d.TenTK);

            // CDatMonAn configuration
            modelBuilder.Entity<CDatMonAn>()
                .HasKey(d => d.MaMA);

            // CHoaDon configuration
            modelBuilder.Entity<CHoaDon>()
                .HasKey(h => h.SoHD);
            modelBuilder.Entity<CHoaDon>()
                .HasOne(h => h.TaiKhoan)
                .WithMany(t => t.HoaDons)
                .HasForeignKey(h => h.TenTK);
            modelBuilder.Entity<CHoaDon>()
                .HasOne(h => h.MonAn)
                .WithMany(m => m.HoaDons)
                .HasForeignKey(h => h.MaMA);

            // CCTHD configuration
            modelBuilder.Entity<CCTHD>()
                .HasKey(c => new { c.SoHD, c.MaMA });
            modelBuilder.Entity<CCTHD>()
                .HasOne(c => c.HoaDon)
                .WithMany(h => h.CCTHDs)
                .HasForeignKey(c => c.SoHD);
            modelBuilder.Entity<CCTHD>()
                .HasOne(c => c.MonAn)
                .WithMany(m => m.CCTHDs)
                .HasForeignKey(c => c.MaMA);
        }
    }
}
