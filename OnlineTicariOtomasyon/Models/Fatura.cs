using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnlineTicariOtomasyon.Models
{
    public class Fatura
    {
        public int Id { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(6, ErrorMessage = "En fazla 6 karakter girebilirsiniz")]
        public string SiraNo { get; set; }

        [Column(TypeName = "Char")]
        [StringLength(1, ErrorMessage = "En fazla 1 karakter girebilirsiniz")]
        public string SeriNo { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(30, ErrorMessage = "En fazla 30 karakter girebilirsiniz")]
        public string VergiDairesi { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(20, ErrorMessage = "En fazla 20 karakter girebilirsiniz")]
        public string VergiNumarasi { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(30, ErrorMessage = "En fazla 30 karakter girebilirsiniz")]
        public string TeslimEden { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(30, ErrorMessage = "En fazla 30 karakter girebilirsiniz")]
        public string TeslimAlan { get; set; }

        public decimal ToplamTutar { get; set; }

        public DateTime Tarih { get; set; }

        [Column(TypeName = "Char")]
        [StringLength(5, ErrorMessage = "En fazla 5 karakter girebilirsiniz")]
        public string Saat { get; set; }

        public ICollection<FaturaKalem> FaturaKalems { get; set; }
    }
}