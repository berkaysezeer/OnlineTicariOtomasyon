using OnlineTicariOtomasyon.Functions;
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

        public ActionResult Index()
        {
            var cariler = db.Caris.Where(x => x.Sil == false).OrderBy(x => x.Ad).ToList();
            return View(cariler);
        }


        [HttpGet]

        public ActionResult Ekle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Ekle(Cari cari)
        {
            if (Request.Files.Count > 0)
            {
                if (cari != null)
                {
                    if (!ModelState.IsValid)
                    {
                        return View();
                    }
                    else
                    {
                        if (db.Caris.FirstOrDefault(x => x.Sil == false && x.Eposta == cari.Eposta) is null)
                        {
                            string guid = Guid.NewGuid().ToString();
                            cari.Guid = guid;
                            cari.Gorsel = GorselKaydet.CariGorselKaydet(Request, Server, guid);
                            string encSifre = DataSecurity.Encrypt(cari.Sifre);
                            cari.Sifre = encSifre;

                            db.Caris.Add(cari);
                            db.SaveChanges();

                            TempData["CariSuccess"] = $"{cari.Ad} {cari.Soyad} başarıyla eklendi";

                            return RedirectToAction("Index");
                        }
                        else
                        {
                            TempData["DangerCari"] = "Girilen müşteri kaydı sistemde kayıtlı";
                            return View();
                        }
                    }
                }
                else
                {
                    ViewBag.DepartmanListesi = DropdownListItems.Departman();
                    return View();
                }
            }
            else
            {
                ViewBag.DepartmanListesi = DropdownListItems.Departman();
                return View();
            }
        }


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
                if (!ModelState.IsValid)
                    return View(cari);
                else
                {
                    if (db.Caris.FirstOrDefault(x => x.Sil == false && x.Eposta == c.Eposta) is null || c.Eposta == cari.Eposta)
                    {
                        if (Request.Files.Count > 0)
                        {
                            string gorsel = GorselKaydet.CariGorselKaydet(Request, Server, cari.Guid);
                            if (!string.IsNullOrEmpty(gorsel)) cari.Gorsel = gorsel;
                        }

                        cari.Ad = c.Ad;
                        cari.Soyad = c.Soyad;
                        cari.Eposta = c.Eposta;
                        cari.CepTelefonu = c.CepTelefonu;
                        cari.Meslek = c.Meslek;
                        cari.Sehir = c.Sehir;

                        db.SaveChanges();

                        TempData["CariSuccess"] = $"{c.Ad} {c.Soyad} başarıyla düzenlendi";

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["DangerCari"] = "Girilen müşteri kaydı sistemde kayıtlı";
                        return View(cari);
                    }
                }
            }
            else return RedirectToAction("Index");
        }

        [HttpGet]

        public ActionResult CariSatis(int Id)
        {
            var cari = db.Caris.Where(x => x.Id == Id).Select(x => x.Ad + " " + x.Soyad).FirstOrDefault();
            ViewBag.CariBilgisi = cari;
            var satislar = db.SatisHarekets.Where(x => x.CariId == Id).ToList();

            return View(satislar);
        }
    }
}