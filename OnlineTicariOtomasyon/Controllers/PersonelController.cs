using OnlineTicariOtomasyon.Functions;
using OnlineTicariOtomasyon.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineTicariOtomasyon.Controllers
{
    public class PersonelController : Controller
    {
        Context db = new Context();

        [Authorize]
        public ActionResult Index()
        {
            var personeller = db.Personels.Where(x => x.Sil == false).ToList();
            return View(personeller);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Ekle()
        {
            ViewBag.DepartmanListesi = Functions.DropdownListItems.Departman();
            return View();
        }

        [HttpPost]
        public ActionResult Ekle(Personel personel)
        {
            if (Request.Files.Count > 0)
            {
                if (personel != null)
                {
                    if (!ModelState.IsValid)
                    {
                        ViewBag.DepartmanListesi = Functions.DropdownListItems.Departman();
                        return View();
                    }
                    else
                    {
                        personel.Gorsel = GorselKaydet.PersonelGorselKaydet(Request, Server, personel);
                        string encSifre = Functions.DataSecurity.Encrypt(personel.Sifre);
                        personel.Sifre = encSifre;

                        db.Personels.Add(personel);
                        db.SaveChanges();

                        TempData["PersonelSuccess"] = $"{personel.Ad} {personel.Soyad} başarıyla eklendi";

                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    ViewBag.DepartmanListesi = Functions.DropdownListItems.Departman();
                    return View();
                }
            }
            else
            {
                ViewBag.DepartmanListesi = Functions.DropdownListItems.Departman();
                return View();
            }
        }

        [Authorize]
        public ActionResult Sil(int Id)
        {
            var personel = db.Personels.FirstOrDefault(x => x.Id == Id);

            if (personel != null)
            {
                personel.Sil = true;
                db.SaveChanges();
                TempData["PersonelSuccess"] = $"{personel.Ad} {personel.Soyad} başarıyla silindi";
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize]
        public ActionResult Duzenle(int Id)
        {
            ViewBag.DepartmanListesi = Functions.DropdownListItems.Departman();
            ViewBag.PersonelBilgisi = db.Personels.Where(x => x.Id == Id).Select(x => x.Ad + " " + x.Soyad).FirstOrDefault();
            var personel = db.Personels.FirstOrDefault(x => x.Id == Id);
            return View(personel);
        }

        [HttpPost]
        public ActionResult Duzenle(Personel p)
        {
            var personel = db.Personels.Where(x => x.Id == p.Id).FirstOrDefault();

            if (personel != null)
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.DepartmanListesi = Functions.DropdownListItems.Departman();
                    ViewBag.PersonelBilgisi = db.Personels.Where(x => x.Id == p.Id).Select(x => x.Ad + " " + x.Soyad).FirstOrDefault();
                    return View(personel);
                }
                else
                {
                    if (Request.Files.Count > 0) personel.Gorsel = GorselKaydet.PersonelGorselKaydet(Request, Server, personel);

                    personel.Ad = p.Ad;
                    personel.Soyad = p.Soyad;
                    personel.DepartmanId = p.DepartmanId;
                    db.SaveChanges();

                    TempData["PersonelSuccess"] = $"{personel.Ad} {personel.Soyad} başarıyla düzenlendi";

                    return RedirectToAction("Index");
                }
            }
            else return RedirectToAction("Index");


        }
    }
}