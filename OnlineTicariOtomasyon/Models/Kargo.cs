using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnlineTicariOtomasyon.Models
{
    public class Kargo
    {
        [Key, ForeignKey("SatisHareket")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SatisHareketId { get; set; }
        public virtual SatisHareket SatisHareket { get; set; }

        [Column(TypeName = "Varchar")]
        [Required]
        [StringLength(250, ErrorMessage = "En fazla 250 karakter girebilirsiniz")]

        public string Aciklama { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(6)]
        public string TakipKodu { get; set; }
        public DateTime Tarih { get; set; }
        public DateTime TahminiTeslimat { get; set; }
        public int KargoDurumId { get; set; }
        public string QrKodDosyaYolu { get; set; }
        public virtual KargoDurum KargoDurum { get; set; }
    }
}