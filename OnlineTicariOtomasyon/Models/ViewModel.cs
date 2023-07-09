using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineTicariOtomasyon.Models
{
    public class ViewModel
    {
        public ICollection<Urun> Uruns { get; set; }
        public ICollection<Marka> Markas { get; set; }
    }
}