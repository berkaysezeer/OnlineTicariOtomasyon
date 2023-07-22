using OnlineTicariOtomasyon.Models;
using OnlineTicariOtomasyon.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineTicariOtomasyon.Controllers
{
    public class GrafikController : Controller
    {
        Context db = new Context();
        // GET: Grafik
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UrunStok()
        {
            var urunStok = db.Uruns.Where(x => x.Sil == false)
                                   .Select(x => new GrafikViewModel { Stok = x.Stok, UrunAdi = x.Ad })
                                   .ToList();

            return Json(urunStok, JsonRequestBehavior.AllowGet);
        }
    }
}