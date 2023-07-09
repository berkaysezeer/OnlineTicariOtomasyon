using OnlineTicariOtomasyon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineTicariOtomasyon.Controllers
{
    public class SatisController : Controller
    {
        Context db = new Context();
        // GET: Satis
        public ActionResult Index()
        {
            var satislar = db.SatisHarekets.ToList();
            return View(satislar);
        }

        [HttpGet]
        public ActionResult Ekle()
        {
            ViewBag.PersonelListesi = Functions.DropdownListItems.Personel();
            ViewBag.CariListesi = Functions.DropdownListItems.Cari();
            ViewBag.UrunListesi = Functions.DropdownListItems.Urun();
            ViewBag.KategoriListesi = Functions.DropdownListItems.Kategori();
            return View();
        }

        [HttpPost]
        public ActionResult Ekle(SatisHareket satisHareket)
        {
            if (satisHareket != null)
            {
                satisHareket.Tarih = DateTime.Now;

                if (!ModelState.IsValid)
                {
                    ViewBag.PersonelListesi = Functions.DropdownListItems.Personel();
                    ViewBag.CariListesi = Functions.DropdownListItems.Cari();
                    ViewBag.UrunListesi = Functions.DropdownListItems.Urun();
                    ViewBag.KategoriListesi = Functions.DropdownListItems.Kategori();
                    return View();
                }
                else
                {
                    var urun = db.Uruns.Where(x => x.Id == satisHareket.UrunId).FirstOrDefault();
                    int urunAdet = satisHareket.Adet;

                    if (urunAdet > urun.Stok)
                    {
                        TempData["SatisDanger"] = $"{urun.Ad} için yeterli sayıda stok bulunamamaktardır. Mevcut stok: {urun.Stok}";
                        ViewBag.PersonelListesi = Functions.DropdownListItems.Personel();
                        ViewBag.CariListesi = Functions.DropdownListItems.Cari();
                        ViewBag.UrunListesi = Functions.DropdownListItems.Urun();
                        ViewBag.KategoriListesi = Functions.DropdownListItems.Kategori();
                        return View();
                    }
                    else
                    {
                        satisHareket.Tutar = urun.SatisFiyati;
                        satisHareket.ToplamTutar = urun.SatisFiyati * urunAdet;
                        db.SatisHarekets.Add(satisHareket);
                        db.SaveChanges();
                        TempData["SatisSuccess"] = $"{urun.Ad} satışı başarıyla gerçekleşti";
                        return RedirectToAction("Index");
                    }
                }
            }
            {
                ViewBag.PersonelListesi = Functions.DropdownListItems.Personel();
                ViewBag.CariListesi = Functions.DropdownListItems.Cari();
                ViewBag.UrunListesi = Functions.DropdownListItems.Urun();
                ViewBag.KategoriListesi = Functions.DropdownListItems.Kategori();
                return View();
            }
        }

        [HttpGet]
        public ActionResult Duzenle(int Id)
        {
            var satis = db.SatisHarekets.Where(x => x.Id == Id).FirstOrDefault();
            ViewBag.PersonelListesi = Functions.DropdownListItems.Personel();
            ViewBag.CariListesi = Functions.DropdownListItems.Cari();
            ViewBag.UrunListesi = Functions.DropdownListItems.Urun();
            ViewBag.KategoriListesi = Functions.DropdownListItems.Kategori();
            ViewBag.UrunAdi = satis.Urun.Ad;
            return View(satis);
        }

        public ActionResult Duzenle(SatisHareket s)
        {
            var satis = db.SatisHarekets.Where(x => x.Id == s.Id).FirstOrDefault();

            if (satis != null)
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.PersonelListesi = Functions.DropdownListItems.Personel();
                    ViewBag.CariListesi = Functions.DropdownListItems.Cari();
                    ViewBag.UrunListesi = Functions.DropdownListItems.Urun();
                    ViewBag.KategoriListesi = Functions.DropdownListItems.Kategori();
                    ViewBag.UrunAdi = satis.Urun.Ad;

                    return View(satis);
                }
                else
                {
                    var urun = db.Uruns.Where(x => x.Id == s.UrunId).FirstOrDefault();
                    int urunAdet = s.Adet;

                    if (urunAdet > urun.Stok)
                    {
                        TempData["SatisDuzenleDanger"] = $"{urun.Ad} için yeterli sayıda stok bulunamamaktardır. Mevcut stok: {urun.Stok}";
                        ViewBag.PersonelListesi = Functions.DropdownListItems.Personel();
                        ViewBag.CariListesi = Functions.DropdownListItems.Cari();
                        ViewBag.UrunListesi = Functions.DropdownListItems.Urun();
                        ViewBag.KategoriListesi = Functions.DropdownListItems.Kategori();
                        ViewBag.UrunAdi = satis.Urun.Ad;

                        return View();
                    }
                    else
                    {
                        s.Tutar = urun.SatisFiyati;
                        s.ToplamTutar = urun.SatisFiyati * urunAdet;

                        satis.UrunId = s.UrunId;
                        satis.PersonelId = s.PersonelId;
                        satis.CariId = s.CariId;
                        satis.Tutar = s.Tutar;
                        satis.ToplamTutar = s.ToplamTutar;
                        satis.Adet = s.Adet;
                        satis.Tarih = s.Tarih;
                        db.SaveChanges();

                        TempData["SatisSuccess"] = $"{urun.Ad} satışı başarıyla güncellendi";
                        return RedirectToAction("Index");
                    }
                }
            }
            else return RedirectToAction("Index");
        }

        public ActionResult Detay(int Id)
        {
            var satis = db.SatisHarekets.Where(x => x.Id == Id).FirstOrDefault();

            if (satis != null) return View(satis);
            else return RedirectToAction("Index");
        }
    }
}