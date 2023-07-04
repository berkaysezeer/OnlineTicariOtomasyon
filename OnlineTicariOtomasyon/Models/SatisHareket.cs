using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineTicariOtomasyon.Models
{
    public class SatisHareket
    {
        public int Id { get; set; }
        public DateTime Tarih { get; set; }
        public decimal Tutar { get; set; }
        public decimal ToplamTutar { get; set; }

        public int Adet { get; set; }
        public int UrunId { get; set; }
        public virtual Urun Urun { get; set; }

        public int CariId { get; set; }
        public virtual Cari Cari { get; set; }

        public int PersonelId { get; set; }
        public virtual Personel Personel { get; set; }
    }
}