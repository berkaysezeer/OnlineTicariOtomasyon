using OnlineTicariOtomasyon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineTicariOtomasyon.Controllers
{
    public class CariController : Controller
    {
        Context db = new Context();
        // GET: Vari
        [Authorize]
        public ActionResult Index()
        {
            var cariler = db.Caris.Where(x => x.Sil == false).OrderBy(x => x.Ad).ToList();
            return View(cariler);
        }


        [HttpGet]
        [Authorize]
        public ActionResult Ekle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Ekle(Cari cari)
        {
            if (cari != null)
            {
                db.Caris.Add(cari);
                db.SaveChanges();

                TempData["CariSuccess"] = $"{cari.Ad} {cari.Soyad} başarıyla eklendi";

                return RedirectToAction("Index");
            }
            else return View();
        }

        [Authorize]
        public ActionResult Sil(int Id)
        {
            var cari = db.Caris.Where(x => x.Id == Id).FirstOrDefault();

            if (cari != null)
            {
                cari.Sil = true;
                db.SaveChanges();
                TempData["CariSuccess"] = $"{cari.Ad} {cari.Soyad} başarıyla silindi";
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize]
        public ActionResult Duzenle(int Id)
        {
            var cari = db.Caris.FirstOrDefault(x => x.Id == Id);

            if (cari != null) return View(cari);
            else return RedirectToAction("Index");

        }

        [HttpPost]
        public ActionResult Duzenle(Cari c)
        {
            var cari = db.Caris.FirstOrDefault(x => x.Id == c.Id);

            if (cari != null)
            {
                if (!ModelState.IsValid) return View(cari);
                else
                {
                    cari.Ad = c.Ad;
                    cari.Soyad = c.Soyad;
                    cari.Eposta = c.Eposta;
                    cari.Sehir = c.Sehir;

                    db.SaveChanges();

                    TempData["CariSuccess"] = $"{c.Ad} {c.Soyad} başarıyla düzenlendi";

                    return RedirectToAction("Index");
                }

            }
            else return View("Index");
        }

        [HttpGet]
        [Authorize]
        public ActionResult CariSatis(int Id)
        {
            var cari = db.Caris.Where(x => x.Id == Id).Select(x => x.Ad + " " + x.Soyad).FirstOrDefault();
            ViewBag.CariBilgisi = cari;
            var satislar = db.SatisHarekets.Where(x => x.CariId == Id).ToList();

            return View(satislar);
        }
    }
}