using OnlineTicariOtomasyon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineTicariOtomasyon.Controllers
{
    public class YapilacaklarController : Controller
    {
        Context db = new Context();

        [Authorize]
        public ActionResult Index()
        {
            var yapilacaklar = db.Yapilacaks.Where(x => x.Sil == false).OrderByDescending(x => x.Tarih).ToList();
            return View(yapilacaklar);
        }

        [HttpPost]
        public ActionResult YapildiMiGuncelle(int Id)
        {
            var yapilacak = db.Yapilacaks.Where(x => x.Id == Id).FirstOrDefault();

            if (yapilacak.YapildiMi) yapilacak.YapildiMi = false;
            else yapilacak.YapildiMi = true;
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}