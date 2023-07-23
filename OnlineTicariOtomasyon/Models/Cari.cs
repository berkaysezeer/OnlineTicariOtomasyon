using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnlineTicariOtomasyon.Models
{
    public class Cari
    {
        public int Id { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(30, ErrorMessage = "En fazla 30 karakter girebilirsiniz")]
        [Required(ErrorMessage ="Bu alanı boş geçemezsiniz")]
        public string Ad { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(30, ErrorMessage = "En fazla 30 karakter girebilirsiniz")]
        [Required(ErrorMessage = "Bu alanı boş geçemezsiniz")]
        public string Soyad { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(13, ErrorMessage = "En fazla 13 karakter girebilirsiniz")]
        public string Sehir { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(50, ErrorMessage = "En fazla 50 karakter girebilirsiniz")]
        public string Eposta { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(50, ErrorMessage = "En fazla 50 karakter girebilirsiniz")]
        public string Sifre { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(50, ErrorMessage = "En fazla 50 karakter girebilirsiniz")]
        public string Meslek { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(13, ErrorMessage = "En fazla 13 karakter girebilirsiniz")]
        public string CepTelefonu { get; set; }

        public bool Sil { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(10)]
        public string Yetki { get; set; } = "Cari";

        [Column(TypeName = "Varchar")]
        [StringLength(50)]
        public string Guid { get; set; }

        public Cari()
        {
            Sil = false;
        }

        [Column(TypeName = "Varchar")]
        [StringLength(255, ErrorMessage = "En fazla 255 karakter girebilirsiniz")]
        public string Gorsel { get; set; }

        public ICollection<SatisHareket> SatisHarekets { get; set; }
        public ICollection<Paylasim> Paylasims { get; set; }
    }
}