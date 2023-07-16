using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnlineTicariOtomasyon.Models
{
    public class Yapilacak
    {
        public int Id { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(30, ErrorMessage = "En fazla 30 karakter girebilirsiniz")]
        public string Baslik { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(512, ErrorMessage = "En fazla 512 karakter girebilirsiniz")]
        public string Detay { get; set; }

        public bool YapildiMi { get; set; }

        public bool Sil { get; set; }

        public DateTime Tarih { get; set; }

        public Yapilacak()
        {     
            YapildiMi = false;
            Sil = false;
        }
    }
}