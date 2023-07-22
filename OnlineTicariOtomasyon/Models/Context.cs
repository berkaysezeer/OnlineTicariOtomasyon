using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OnlineTicariOtomasyon.Models
{
    public class Context : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SatisHareket>()
                        .HasOptional(s => s.Kargo)
                        .WithRequired(ad => ad.SatisHareket);
        }


        public DbSet<Admin> Admins { get; set; }
        public DbSet<Cari> Caris { get; set; }
        public DbSet<Departman> Departmans { get; set; }
        public DbSet<Fatura> Faturas { get; set; }
        public DbSet<FaturaKalem> FaturaKalems { get; set; }
        public DbSet<Gider> Giders { get; set; }
        public DbSet<Kategori> Kategoris { get; set; }
        public DbSet<Personel> Personels { get; set; }
        public DbSet<SatisHareket> SatisHarekets { get; set; }
        public DbSet<Urun> Uruns { get; set; }
        public DbSet<Marka> Markas { get; set; }
        public DbSet<Yapilacak> Yapilacaks { get; set; }
        public DbSet<Kargo> Kargos { get; set; }
        public DbSet<KargoDurum> KargoDurums { get; set; }
        public DbSet<Mesaj> Mesajs { get; set; }
        public DbSet<Paylasim> Paylasims { get; set; }
        public DbSet<Duyuru> Duyurus { get; set; }
    }
}