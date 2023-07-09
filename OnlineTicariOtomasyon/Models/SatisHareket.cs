using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        public int Adet { get; set; }

        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        public int UrunId { get; set; }
        public virtual Urun Urun { get; set; }

        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        public int CariId { get; set; }
        public virtual Cari Cari { get; set; }

        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        public int PersonelId { get; set; }
        public virtual Personel Personel { get; set; }
    }
}