using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnlineTicariOtomasyon.Models
{
    public class Departman
    {
        public int Id { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(30, ErrorMessage = "En fazla 30 karakter girebilirsiniz")]
        [Required(ErrorMessage = "Bu alanı boş geçemezsiniz")]
        public string Ad { get; set; }
        public bool Sil { get; set; } = false;
        public ICollection<Personel> Personels { get; set; }
    }
}