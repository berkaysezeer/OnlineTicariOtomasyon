using OnlineTicariOtomasyon.Models;
using OnlineTicariOtomasyon.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineTicariOtomasyon.Controllers
{
    public class IstatistikController : Controller
    {
        Context db = new Context();
        // GET: Istatistik
        public ActionResult Index()
        {
            var cariler = db.Caris.ToList();
            var urunler = db.Uruns.Where(x => x.Sil == false).ToList();
            var personeller = db.Personels.Where(x => x.Sil == false).ToList();
            var kategoriler = db.Kategoris.Where(x => x.Sil == false).ToList();
            var markalar = db.Markas.Where(x => x.Sil == false).ToList();
            var faturalar = db.Faturas.ToList();
            var satisHareket = db.SatisHarekets.ToList();

            //Toplam Cari
            ViewBag.d1 = cariler.Count();
            //Ürün Sayısı
            ViewBag.d2 = urunler.Count();
            //Personel sayısı
            ViewBag.d3 = personeller.Count();
            //kategori sayısı
            ViewBag.d4 = kategoriler.Count();
            //toplam stok
            ViewBag.d5 = urunler.Sum(x => x.Stok);
            //marka sayısı
            ViewBag.d6 = markalar.Count();
            //kritik stok
            ViewBag.d7 = urunler.Where(x => x.Stok < 20).Count();
            //max fiyatlı ürün
            ViewBag.d8 = $"₺{Convert.ToInt32(urunler.Select(x => x.SatisFiyati).Max())}";
            //min fiyatlı ürün
            ViewBag.d9 = $"₺{Convert.ToInt32(urunler.Select(x => x.SatisFiyati).Min())}";
            //en çok satan marka
            //ViewBag.d12 = markalar.Where(m => m.Id == (urunler
            //                         .GroupBy(x => x.MarkaId)
            //                         .OrderByDescending(x => x.Count())
            //                         .Select(x => x.Key)
            //                         .FirstOrDefault())).Select(m => m.Ad).FirstOrDefault();

            //en çok satan marka
            ViewBag.d12 = urunler.Where(u => u.Id == satisHareket.GroupBy(x => x.UrunId).OrderByDescending(x => x.Sum(y => y.Adet)).Select(x => x.Key).FirstOrDefault()).Select(u => u.Marka.Ad).FirstOrDefault();

            //buzdolabı sayısı
            ViewBag.d10 = urunler.Where(x => x.Ad.Contains("Buzdolabı")).Count();
            //laptop sayısı
            ViewBag.d11 = urunler.Where(x => x.Ad.Contains("Laptop")).Count();
            //en çok satan ürün
            ViewBag.d13 = urunler.Where(u => u.Id == satisHareket
                                                    .GroupBy(x => x.UrunId)
                                                    .OrderByDescending(x => x.Sum(y => y.Adet))
                                                    .Select(x => x.Key)
                                                    .FirstOrDefault())
                .Select(u => u.Ad).FirstOrDefault();

            //kasadaki tutar
            ViewBag.d14 = $"₺{Convert.ToInt32(satisHareket.Sum(x => x.ToplamTutar))}";
            DateTime bugun = DateTime.Today;
            //bugünkü satışlar
            ViewBag.d15 = $"₺{Convert.ToInt32(satisHareket.Where(x => x.Tarih == bugun).Sum(x => (decimal?)x.ToplamTutar))}";
            //bugünkü kasa
            ViewBag.d16 = $"₺{Convert.ToInt32(satisHareket.Where(x => x.Tarih == bugun).Sum(x => (decimal?)x.ToplamTutar))}";

            return View();
        }

        public ActionResult OzetTablolar()
        {
            return View();
        }

        public PartialViewResult PartialKategoriler()
        {
            return PartialView();
        }

        public PartialViewResult PartialCariler()
        {
            var cariler = (from x in db.Caris
                           where (x.Sil == false)
                           group x by x.Sehir into g
                           select new OzetTabloViewModel
                           {
                               Sehir = g.Key,
                               Adet = g.Count()
                           }).ToList().OrderByDescending(x => x.Adet);

            ViewBag.Toplam = cariler.Sum(x => x.Adet);

            return PartialView(cariler);
        }

        public PartialViewResult PartialUrunler()
        {
            var urunler = db.Uruns.Where(x => x.Sil == false).OrderBy(x => x.Ad).ToList();
            return PartialView(urunler);
        }

        //Lamda ile anonim tip kullanımı
        public PartialViewResult PartialDepartman()
        {
            var departman = db.Personels.Where(x => x.Sil == false)
                .GroupBy(x => x.Departman.Ad)
                .Select(x => new PersonelDepartmanViewModel { Adet = x.Count(), Departman = x.Key })
                .ToList();
            return PartialView(departman);
        }
    }
}
