using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineTicariOtomasyon.Models;
using PagedList;
using PagedList.Mvc;

namespace OnlineTicariOtomasyon.Controllers
{
    public class KategoriController : Controller
    {
        Context db = new Context();

        [Authorize]
        public ActionResult Index()
        {
            var kategoriler = db.Kategoris
                .Where(x => x.Sil == false)
                .OrderBy(x => x.Ad)
                .ToList();

            return View(kategoriler);
        }

        [Authorize]
        public ActionResult PageIndex(int? s, string p)
        {
            var kategoriler = db.Kategoris
                .Where(x => x.Sil == false && (x.Ad.Contains(p) || p == null))
                .OrderBy(x => x.Ad)
                .ToList().ToPagedList(s ?? 1, 5); //int null ise sayfa 1, sayfa başına düşen data

            return View(kategoriler);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Ekle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Ekle(Kategori kategori)
        {
            if (kategori != null)
            {
                db.Kategoris.Add(kategori);
                db.SaveChanges();

                TempData["KategoriSuccess"] = $"{kategori.Ad} başarıyla eklendi";

                return RedirectToAction("Index", "Kategori");
            }
            else return View();
        }

        [Authorize]
        public ActionResult Sil(int Id)
        {
            var kategori = db.Kategoris.FirstOrDefault(x => x.Id == Id);

            if (kategori != null)
            {
                kategori.Sil = true;
                db.SaveChanges();

                TempData["KategoriSuccess"] = $"{kategori.Ad} başarıyla silindi";
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize]
        public ActionResult Duzenle(int Id)
        {
            var kategori = db.Kategoris.FirstOrDefault(x => x.Id == Id);

            if (kategori != null) return View(kategori);
            else return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Duzenle(Kategori k)
        {
            var kategori = db.Kategoris.FirstOrDefault(x => x.Id == k.Id);

            if (kategori != null)
            {
                kategori.Ad = k.Ad;
                db.SaveChanges();

                TempData["KategoriSuccess"] = $"{kategori.Ad} başarıyla düzenlendi";
            }

            return RedirectToAction("Index");
        }
    }
}