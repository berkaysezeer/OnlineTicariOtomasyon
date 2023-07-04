using OnlineTicariOtomasyon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineTicariOtomasyon.Functions
{
    public static class DropdownListItems
    {
        static Context db = new Context();

        public static IEnumerable<SelectListItem> Kategori()
        {
            IEnumerable<SelectListItem> kategoriler = new SelectList(db.Kategoris, "Id", "Ad");
            return kategoriler;
        }

        public static IEnumerable<SelectListItem> Marka()
        {
            IEnumerable<SelectListItem> markalar = new SelectList(db.Markas, "Id", "Ad");
            return markalar;
        }
    }
}