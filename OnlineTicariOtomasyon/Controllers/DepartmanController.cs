using OnlineTicariOtomasyon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineTicariOtomasyon.Controllers
{
    public class DepartmanController : Controller
    {
        // GET: Departman
        Context db = new Context();
        public ActionResult Index()
        {
            var departmanlar = db.Departmans.Where(x => x.Sil == false).OrderBy(x => x.Ad).ToList();
            return View(departmanlar);
        }

        [HttpGet]
        public ActionResult Ekle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Ekle(Departman departman)
        {
            if (departman != null)
            {
                db.Departmans.Add(departman);
                db.SaveChanges();
                TempData["Success"] = $"{departman.Ad} departmanı başarıyla eklendi";

                return RedirectToAction("Index");
            }
            else return View();
        }

        public ActionResult Sil(int Id)
        {
            var departman = db.Departmans.FirstOrDefault(x => x.Id == Id);

            if (departman != null)
            {
                departman.Sil = true;
                db.SaveChanges();

                TempData["Success"] = $"{departman.Ad} departmanı başarıyla silindi";
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Duzenle(int Id)
        {
            var departman = db.Departmans.FirstOrDefault(x => x.Id == Id);

            if (departman != null) return View(departman);
            else return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Duzenle(Departman d)
        {
            var departman = db.Departmans.FirstOrDefault(x => x.Id == d.Id);

            if (departman != null)
            {
                departman.Ad = d.Ad;
                db.SaveChanges();

                TempData["Success"] = $"{departman.Ad} departmanı başarıyla düzenlendi";

                return RedirectToAction("Index");
            }
            else return View();

        }

        [HttpGet]
        public ActionResult Detay(int Id)
        {
            var departmanAdi = db.Departmans.Where(x => x.Id == Id).Select(x => x.Ad).FirstOrDefault();
            ViewBag.DepartmanAdi = departmanAdi;
            var personeller = db.Personels.Where(x => x.DepartmanId == Id).OrderBy(x => x.Ad).ToList();
            return View(personeller);
        }

        [HttpGet]
        public ActionResult PersonelSatis(int Id)
        {
            var personel = db.Personels.Where(x => x.Id == Id).FirstOrDefault();
            ViewBag.PersonelBilgisi = $"{personel.Ad} {personel.Soyad}";

            var personelSatislari = db.SatisHarekets.Where(x => x.PersonelId == Id).OrderByDescending(x => x.Tarih).ToList();
            return View(personelSatislari);
        }
    }
}