﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnlineTicariOtomasyon.Models
{
    public class Gider
    {
        public int Id { get; set; }

        [Column(TypeName = "Varchar")]
        [Required]
        [StringLength(100, ErrorMessage = "En fazla 100 karakter girebilirsiniz")]
        public string Aciklama { get; set; }
        public DateTime Tarih { get; set; }
        public decimal Tutar { get; set; }
    }
}