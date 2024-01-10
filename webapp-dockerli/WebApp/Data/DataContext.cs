using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions <DataContext> options) : base (options) { 
        
        }
        public DbSet<Bolum> Bolums { get; set; }
        public DbSet<Dersler> Derslers { get; set; }
        public DbSet<Donemler> Donemlers { get; set; }
        public DbSet<Fakulte> Fakultes { get; set; }
        public DbSet<Notlar> Notlars { get; set; }
        public DbSet<Ogrenci> Ogrencis { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // İlişkileri manuel olarak yapılandırma
            modelBuilder.Entity<Bolum>()
                .HasMany(b => b.Ogrencis)
                .WithOne(o => o.Bolum)
                .HasForeignKey(o => o.Bolumu);

            modelBuilder.Entity<Dersler>()
                .HasOne(d => d.Donemler)
                .WithMany(dn => dn.Derslers)
                .HasForeignKey(d => d.Donemi);

            modelBuilder.Entity<Donemler>()
                .HasMany(dn => dn.Derslers)
                .WithOne(d => d.Donemler)
                .HasForeignKey(d => d.Donemi);

            modelBuilder.Entity<Notlar>()
                .HasOne(n => n.Dersler)
                .WithMany(d => d.Notlars)
                .HasForeignKey(n => n.derskod);

            modelBuilder.Entity<Notlar>()
                .HasOne(n => n.Ogrenci)
                .WithMany(o => o.Notlars)
                .HasForeignKey(n => n.ogrno);

            modelBuilder.Entity<Bolum>()
                .HasOne(b => b.Fakulte)
                .WithMany(f => f.Bolums)
                .HasForeignKey(b => b.Fakultesi);

            modelBuilder.Entity<Bolum>().HasKey(b => b.BolumKod);
            modelBuilder.Entity<Dersler>().HasKey(d => d.DersKodu);
            modelBuilder.Entity<Donemler>().HasKey(d => d.donemKod);
            modelBuilder.Entity<Fakulte>().HasKey(f => f.FKKod);
            modelBuilder.Entity<Ogrenci>().HasKey(o => o.OgrNo);
        }


    }
}
