using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnlineTicariOtomasyon.Models
{
    public class Personel
    {
        public int Id { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(30, ErrorMessage = "En fazla 30 karakter girebilirsiniz")]
        [Required(ErrorMessage = "Bu alanı boş geçemezsiniz")]
        public string Ad { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(30, ErrorMessage = "En fazla 30 karakter girebilirsiniz")]
        [Required(ErrorMessage = "Bu alanı boş geçemezsiniz")]
        public string Soyad { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(255, ErrorMessage = "En fazla 255 karakter girebilirsiniz")]
        public string Gorsel { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(255, ErrorMessage = "En fazla 255 karakter girebilirsiniz")]
        public string Adres { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(11, ErrorMessage = "En fazla 11 karakter girebilirsiniz")]
        public string CepTelefonu { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(50, ErrorMessage = "En fazla 50 karakter girebilirsiniz")]
        public string Eposta { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(50, ErrorMessage = "En fazla 50 karakter girebilirsiniz")]
        public string Sifre { get; set; }

        public ICollection<SatisHareket> SatisHarekets { get; set; }

        public int DepartmanId { get; set; }
        public virtual Departman Departman { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(10)]
        public string Yetki { get; set; } = "Cari";

        [Column(TypeName = "Varchar")]
        [StringLength(50)]
        public string Guid { get; set; }

        public bool Sil { get; set; }

        public Personel()
        {
            Sil = false;
        }

        public ICollection<Duyuru> Duyurus { get; set; }
    }
}