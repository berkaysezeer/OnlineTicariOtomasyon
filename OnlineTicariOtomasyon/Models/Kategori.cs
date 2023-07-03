using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnlineTicariOtomasyon.Models
{
    public class Kategori
    {
        public int Id { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string Ad { get; set; }
        public bool Sil { get; set; }
        public ICollection<Urun> Uruns { get; set; }

        public Kategori()
        {
            Sil = false;
        }
    }
}