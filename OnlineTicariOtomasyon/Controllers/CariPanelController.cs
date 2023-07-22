using OnlineTicariOtomasyon.Models;
using OnlineTicariOtomasyon.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace OnlineTicariOtomasyon.Controllers
{
    public class CariPanelController : Controller
    {
        Context db = new Context();

        
        public ActionResult Index()
        {
            int cariId = Convert.ToInt32(Session["CariId"]);
            var cari = db.Caris.FirstOrDefault(x => x.Id == cariId);
            ViewBag.CariBilgisi = $"{cari.Ad} {cari.Soyad}";

            var paylasimlar = db.Paylasims.Where(x => x.CariId == cariId && x.Sil == false).OrderByDescending(x=>x.Tarih).ToList();

            CariPaylasimViewModel viewModel = new CariPaylasimViewModel()
            {
                Cari = cari,
                Paylasims = paylasimlar
            };

            return View(viewModel);
        }

        [HttpGet]
        
        public ActionResult Profil()
        {
            int cariId = Convert.ToInt32(Session["CariId"]);
            var cari = db.Caris.FirstOrDefault(x => x.Id == cariId);
            return View(cari);
        }

        [HttpPost]
        public ActionResult BilgiGuncelle(CariPaylasimViewModel c)
        {
            var cari = db.Caris.FirstOrDefault(x => x.Id == c.Cari.Id);

            if (cari != null)
            {
                if (!ModelState.IsValid)
                    return RedirectToAction("Index", "CariPanel");
                else
                {
                    cari.Ad = c.Cari.Ad;
                    cari.Soyad = c.Cari.Soyad;
                    cari.Eposta = c.Cari.Eposta;
                    cari.Sehir = c.Cari.Sehir;

                    db.SaveChanges();

                    TempData["BilgiGuncelleSuccess"] = "Bilgileriniz başarıyla güncellendi.";

                    return RedirectToAction("Index", "CariPanel");
                }
            }
            else return RedirectToAction("Index", "CariPanel");
        }

        [HttpGet]
        
        public ActionResult Siparisler()
        {
            int cariId = Convert.ToInt32(Session["CariId"]);
            var siparisler = db.SatisHarekets.Where(x => x.CariId == cariId).OrderByDescending(x => x.Tarih).ToList();

            return View(siparisler);
        }

        [HttpGet]
        
        public ActionResult Mesajlar(string f)
        {
            int cariId = Convert.ToInt32(Session["CariId"]);
            var cari = db.Caris.FirstOrDefault(x => x.Id == cariId);
            List<Mesaj> mesajlar = new List<Mesaj>();

            if (string.IsNullOrEmpty(f) || f == "gelenkutusu") mesajlar = db.Mesajs.Where(x => x.AliciEposta == cari.Eposta && x.Sil == false).OrderByDescending(x => x.Tarih).ToList();
            else if (f == "gonderilenler") mesajlar = db.Mesajs.Where(x => x.GondericiEposta == cari.Eposta && x.Sil == false).OrderByDescending(x => x.Tarih).ToList();
            else if (f == "copkutusu") mesajlar = db.Mesajs.Where(x => (x.GondericiEposta == cari.Eposta || x.AliciEposta == cari.Eposta) && x.Sil == true).OrderByDescending(x => x.Tarih).ToList();

            return View(mesajlar);
        }

        
        public PartialViewResult PartialMesajlarMenu()
        {
            int cariId = Convert.ToInt32(Session["CariId"]);
            var cari = db.Caris.FirstOrDefault(x => x.Id == cariId);

            ViewBag.GelenMesajSayisi = db.Mesajs.Where(x => x.AliciEposta == cari.Eposta && x.Sil == false && x.Okundu == false).Count();
            ViewBag.GonderilenMesajSayisi = db.Mesajs.Where(x => x.GondericiEposta == cari.Eposta && x.Sil == false).Count();
            ViewBag.SilinenMesajSayisi = db.Mesajs.Where(x => (x.GondericiEposta == cari.Eposta || x.AliciEposta == cari.Eposta) && x.Sil == true).Count();

            return PartialView();
        }

        public ActionResult MesajDetay(int? Id)
        {
            int cariId = Convert.ToInt32(Session["CariId"]);
            var cari = db.Caris.FirstOrDefault(x => x.Id == cariId);

            if (Id != null)
            {
                var mesaj = db.Mesajs.FirstOrDefault(x => x.Id == Id);

                if (mesaj != null)
                {
                    if (!mesaj.Okundu && cari.Eposta == mesaj.AliciEposta)
                    {
                        mesaj.Okundu = true;
                        db.SaveChanges();
                    }

                    return View(mesaj);
                }
                else return RedirectToAction("Index");

            }
            else return RedirectToAction("Index");

        }

        
        [HttpPost]
        public ActionResult MesajSil(int Id)
        {
            var mesaj = db.Mesajs.FirstOrDefault(x => x.Id == Id);

            if (mesaj == null) return RedirectToAction("MesajDetay", Id);
            else
            {
                mesaj.Sil = true;
                db.SaveChanges();
            }

            TempData["SilSuccess"] = $"{mesaj.Konu} konulu mesaj silindi";
            return RedirectToAction("Mesajlar");
        }

        
        [HttpGet]
        public ActionResult YeniMesaj()
        {
            ViewBag.CariListesi = Functions.DropdownListItems.Cari();
            return View();
        }

        
        [HttpPost]
        public ActionResult YeniMesaj(Mesaj mesaj, int CariId)
        {
            int id = Convert.ToInt32(Session["CariId"]);
            var cari = db.Caris.FirstOrDefault(x => x.Id == id);

            mesaj.Tarih = DateTime.Now;
            mesaj.GondericiEposta = cari.Eposta;
            mesaj.Gonderici = $"{cari.Ad} {cari.Soyad}";

            var alici = db.Caris.FirstOrDefault(x => x.Id == CariId);

            mesaj.AliciEposta = alici.Eposta;
            mesaj.Alici = $"{alici.Ad} {alici.Soyad}";
            db.Mesajs.Add(mesaj);
            db.SaveChanges();

            return RedirectToAction("Mesajlar", "CariPanel");
        }

        public ActionResult CikisYap()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }

        
        [HttpGet]
        public ActionResult Kargolar()
        {
            int id = Convert.ToInt32(Session["CariId"]);
            var kargolar = db.Kargos.Where(x => x.SatisHareket.Cari.Id == id).OrderByDescending(x => x.Tarih).ToList();


            return View(kargolar);
        }

        
        [HttpGet]
        public ActionResult KargoDetay(string Id)
        {
            var kargo = db.Kargos.FirstOrDefault(x => x.TakipKodu == Id);
            return View(kargo);
        }

        
        [HttpPost]
        public ActionResult PaylasimYap(CariPaylasimViewModel viewModel)
        {
            viewModel.Paylasim.Tarih = DateTime.Now;
            int id = Convert.ToInt32(Session["CariId"]);
            viewModel.Paylasim.CariId = id;

            db.Paylasims.Add(viewModel.Paylasim);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
 
        public PartialViewResult PartialProfilBilgileri()
        {
            int cariId = Convert.ToInt32(Session["CariId"]);
            var cari = db.Caris.FirstOrDefault(x => x.Id == cariId);

            var satis = db.SatisHarekets.Where(x => x.CariId == cariId).ToList();
            ViewBag.SatisSayisi = satis.Count();
            ViewBag.ToplamOdeme = string.Format("{0:c}", satis.Sum(x => x.ToplamTutar));
            ViewBag.ToplamUrunSayisi = satis.Sum(x => x.Adet);

            return PartialView(cari);
        }

        public ActionResult Duyurular()
        {
            var duyurular = db.Duyurus.Where(x => x.Sil == false).OrderByDescending(x => x.Tarih).ToList();
            return View(duyurular);
        }

    }
}