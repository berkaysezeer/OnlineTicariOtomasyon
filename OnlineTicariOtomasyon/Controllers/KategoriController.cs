using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineTicariOtomasyon.Models;

namespace OnlineTicariOtomasyon.Controllers
{
    public class KategoriController : Controller
    {
        Context db = new Context();

        public ActionResult Index()
        {
            var kategoriler = db.Kategoris
                .Where(x => x.Sil == false)
                .OrderBy(x => x.Ad)
                .ToList();

            return View(kategoriler);
        }

        [HttpGet]
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

                return RedirectToAction("Index", "Kategori");
            }
            else return View();
        }

       
        public ActionResult Sil(int Id)
        {
            var kategori = db.Kategoris.FirstOrDefault(x => x.Id == Id);

            if (kategori != null)
            {
                kategori.Sil = true;
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Duzenle(int Id)
        {
            var kategori = db.Kategoris.FirstOrDefault(x => x.Id == Id);

            if (kategori != null) return View(kategori);
            else return RedirectToAction("Index");
        }

        public ActionResult Duzenle(Kategori k)
        {
            var kategori = db.Kategoris.FirstOrDefault(x => x.Id == k.Id);

            if(kategori != null)
            {
                kategori.Ad = k.Ad;
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}