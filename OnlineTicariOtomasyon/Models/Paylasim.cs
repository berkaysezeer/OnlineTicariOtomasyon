using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnlineTicariOtomasyon.Models
{
    public class Paylasim
    {
        public int Id { get; set; }

        [Column(TypeName = "Varchar")]
        [Required]
        [StringLength(250, ErrorMessage = "En fazla 250 karakter girebilirsiniz")]
        public string Aciklama { get; set; }
        public DateTime Tarih { get; set; }
        public bool Sil { get; set; } = false;
        public int CariId { get; set; }
        public virtual Cari Cari { get; set; }
    }
}