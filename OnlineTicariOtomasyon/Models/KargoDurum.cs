
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineTicariOtomasyon.Models
{
    public class KargoDurum
    {
        public int Id { get; set; }
        public string Durum { get; set; }
        public ICollection<Kargo> Kargos { get; set; }
    }
}