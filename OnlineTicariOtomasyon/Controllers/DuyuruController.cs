using OnlineTicariOtomasyon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineTicariOtomasyon.Controllers
{
    public class DuyuruController : Controller
    {
        Context db = new Context();
        // GET: Duyuru
        public ActionResult Index()
        {
            int personelId = Convert.ToInt32(Session["PersonelId"]);
            var duyurular = db.Duyurus.Where(x => x.PersonelId == personelId).ToList();
            return View(duyurular);
        }

        [HttpPost]
        public ActionResult Ekle(Duyuru duyuru)
        {
            int personelId = Convert.ToInt32(Session["PersonelId"]);
            duyuru.PersonelId = personelId;
            duyuru.Tarih = DateTime.Now;

            if (!ModelState.IsValid)
            {

            }
            else
            {
                db.Duyurus.Add(duyuru);
                db.SaveChanges();
                TempData["DuyuruSuccess"] = "Duyuru başarıyla eklendi";
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Sil(int Id)
        {
            var duyuru = db.Duyurus.FirstOrDefault(x => x.Id == Id);
            duyuru.Sil = true;
            db.SaveChanges();
            TempData["DuyuruSuccess"] = $"{duyuru.Konu} konulu duyuru başarıyla silindi.";

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Duzenle(Duyuru d)
        {
            int personelId = Convert.ToInt32(Session["PersonelId"]);
            var duyuru = db.Duyurus.FirstOrDefault(x => x.Id == d.Id);
            //duyuru.PersonelId = personelId;

            if (!ModelState.IsValid) { }
            else
            {
                duyuru.Konu = d.Konu;
                duyuru.Aciklama = d.Aciklama;
                db.SaveChanges();

                TempData["DuyuruSuccess"] = $"{duyuru.Konu} konulu duyuru başarıyla düzenlendi.";
            }

            return RedirectToAction("Index");
        }
    }
}