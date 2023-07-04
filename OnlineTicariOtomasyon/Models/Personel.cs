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
        [StringLength(30)]
        public string Ad { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string Soyad { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(255)]
        public string Gorsel { get; set; }
        public ICollection<SatisHareket> SatisHarekets { get; set; }
        public int DepartmanId { get; set; }
        public virtual Departman Departman { get; set; }
    }
}