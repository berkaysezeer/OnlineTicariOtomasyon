using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineTicariOtomasyon.Models
{
    public class Duyuru
    {
        public int Id { get; set; }
        public string Konu { get; set; }
        public string Aciklama { get; set; }
        public DateTime Tarih { get; set; }
        public bool Sil { get; set; } = false;

        public int PersonelId { get; set; }
        public virtual Personel Personel { get; set; }
    }
}