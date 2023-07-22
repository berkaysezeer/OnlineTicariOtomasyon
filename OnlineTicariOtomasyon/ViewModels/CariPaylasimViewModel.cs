using OnlineTicariOtomasyon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineTicariOtomasyon.ViewModels
{
    public class CariPaylasimViewModel
    {
        public ICollection<Paylasim> Paylasims { get; set; }
        public Cari Cari { get; set; }
        public Paylasim Paylasim { get; set; }
    }
}