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
        public Urun Urun { get; set; }
        public Cari Cari { get; set; }
        public Personel Personel { get; set; }
    }
}