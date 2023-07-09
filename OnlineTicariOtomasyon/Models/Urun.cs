using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnlineTicariOtomasyon.Models
{
    public class Urun
    {
        public int Id { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(100)]
        public string Ad { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(200)]
        public string Aciklama { get; set; }

        public short Stok { get; set; }
        public decimal AlisFiyati { get; set; }
        public decimal SatisFiyati { get; set; }
        public bool Durum { get; set; }
        public bool Sil { get; set; }

        public Urun()
        {
            Sil = false;
        }

        [Column(TypeName = "Varchar")]
        [StringLength(255)]
        public string UrunGorsel { get; set; }
        public int KategoriId { get; set; }
        public virtual Kategori Kategori { get; set; }
        public int MarkaId { get; set; }
        public virtual Marka Marka { get; set; }
        public ICollection<SatisHareket> SatisHarekets { get; set; }
    }
}