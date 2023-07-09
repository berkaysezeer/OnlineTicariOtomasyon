using OnlineTicariOtomasyon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineTicariOtomasyon.Controllers
{
    public class UrunController : Controller
    {
        Context db = new Context();
        // GET: Urun
        public ActionResult Index()
        {
            var urunler = db.Uruns.Where(x => x.Sil == false).OrderBy(x => x.Ad).ToList();
            return View(urunler);
        }

        [HttpGet]
        public ActionResult Ekle()
        {
            ViewBag.KategoriListesi = Functions.DropdownListItems.Kategori();
            ViewBag.MarkaListesi = Functions.DropdownListItems.Marka();
            return View();
        }

        [HttpPost]
        public ActionResult Ekle(Urun urun)
        {
            db.Uruns.Add(urun);
            db.SaveChanges();
            TempData["UrunSuccess"] = $"{urun.Ad} başarıyla eklendi";
            return RedirectToAction("Index");
        }

        public ActionResult Sil(int Id)
        {
            var urun = db.Uruns.FirstOrDefault(x => x.Id == Id);

            if (urun != null)
            {
                urun.Sil = true;
                db.SaveChanges();

                TempData["UrunSuccess"] = $"{urun.Ad} başarıyla silindi";
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Duzenle(int Id)
        {
            var urun = db.Uruns.FirstOrDefault(x => x.Id == Id);

            if (urun != null)
            {
                ViewBag.KategoriListesi = Functions.DropdownListItems.Kategori();
                ViewBag.MarkaListesi = Functions.DropdownListItems.Marka();
                return View(urun);
            }
            else return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Duzenle(Urun u)
        {
            var urun = db.Uruns.FirstOrDefault(x => x.Id == u.Id);

            if (urun != null)
            {
                urun.Ad = u.Ad;
                urun.AlisFiyati = u.AlisFiyati;
                urun.SatisFiyati = u.SatisFiyati;
                urun.UrunGorsel = u.UrunGorsel;
                urun.KategoriId = u.KategoriId;
                urun.MarkaId = u.MarkaId;
                urun.Stok = u.Stok;

                db.SaveChanges();

                TempData["UrunSuccess"] = $"{urun.Ad} başarıyla düzenlendi";

                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.KategoriListesi = Functions.DropdownListItems.Kategori();
                ViewBag.MarkaListesi = Functions.DropdownListItems.Marka();
                return View();
            }
        }
    }
}