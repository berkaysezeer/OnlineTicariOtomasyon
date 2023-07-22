using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnlineTicariOtomasyon.Models
{
    public class Mesaj
    {
        public int Id { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(50)]
        public string Gonderici { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(50)]
        public string GondericiEposta { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(50)]
        public string Alici { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(50)]
        public string AliciEposta { get; set; }

        [Column(TypeName = "Varchar")]
        [Required]
        [StringLength(100, ErrorMessage = "En fazla 100 karakter girebilirsiniz")]
        public string Konu { get; set; }

        [Column(TypeName = "Varchar")]
        [Required]
        [StringLength(2000, ErrorMessage = "En fazla 2000 karakter girebilirsiniz")]
        public string Icerik { get; set; }

        public DateTime Tarih { get; set; }

        public bool Sil { get; set; } = false;

        public bool Okundu { get; set; } = false;
    }
}