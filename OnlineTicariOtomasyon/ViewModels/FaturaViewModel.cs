using OnlineTicariOtomasyon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineTicariOtomasyon.ViewModels
{
    public class FaturaViewModel
    {
        public ICollection<Fatura>  Faturas { get; set; }
        public Fatura Fatura { get; set; }
    }
}