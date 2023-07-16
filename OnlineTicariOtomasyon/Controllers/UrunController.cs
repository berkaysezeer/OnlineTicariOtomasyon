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

        [Authorize]
        public ActionResult Index()
        {
            ViewBag.CariListesi = Functions.DropdownListItems.Cari();
            ViewBag.PersonelId = Session["PersonelId"];
            var urunler = db.Uruns.Where(x => x.Sil == false).OrderBy(x => x.Ad).ToList();
            return View(urunler);
        }

        [HttpGet]
        [Authorize]
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

        [Authorize]
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
        [Authorize]
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

        [HttpPost]
        public ActionResult SatisEkle(SatisHareket satisHareket)
        {
            if (satisHareket != null)
            {
                satisHareket.Tarih = DateTime.Now;

                if (!ModelState.IsValid)
                {
                    TempData["UrunDanger"] = "Bir ya da birden fazla alanı boş bıraktınız";
                }
                else
                {
                    var urun = db.Uruns.Where(x => x.Id == satisHareket.UrunId).FirstOrDefault();
                    int urunAdet = satisHareket.Adet;
                    string urunAdi = urun.Ad;

                    if (urunAdet > urun.Stok)
                        TempData["UrunDanger"] = $"{urunAdi} için yeterli sayıda stok bulunamamaktardır. Mevcut stok: {urun.Stok}";
                    else
                    {
                        string takipKodu = Functions.KargoIslemleri.TakipKoduUret();
                        DateTime tarih = DateTime.Now;

                        var kargoDurum = db.KargoDurums.FirstOrDefault(x => x.Id == 1);

                        Kargo kargo = new Kargo()
                        {
                            KargoDurum = kargoDurum,
                            Tarih = tarih,
                            TahminiTeslimat = tarih.AddDays(2),
                            Aciklama = $"{urun.Ad} ait kargo işleme alınmıştır",
                            TakipKodu = takipKodu,
                        };

                        satisHareket.Kargo = kargo;
                        satisHareket.Tutar = urun.SatisFiyati;
                        satisHareket.ToplamTutar = urun.SatisFiyati * urunAdet;

                        db.SatisHarekets.Add(satisHareket);
                        db.SaveChanges();

                        TempData["UrunSuccess"] = $"{urunAdi} satışı başarıyla gerçekleşti";
                    }

                }
            }

            return RedirectToAction("Index");
        }
    }
}