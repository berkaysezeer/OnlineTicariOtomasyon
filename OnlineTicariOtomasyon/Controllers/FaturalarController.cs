using OnlineTicariOtomasyon.Models;
using OnlineTicariOtomasyon.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineTicariOtomasyon.Controllers
{
    public class FaturalarController : Controller
    {
        Context db = new Context();
        // GET: Faturalar
        public ActionResult Index()
        {
            var faturalar = db.Faturas.OrderByDescending(x => x.Id).ToList();
            var faturaKelemleri = db.FaturaKalems.OrderByDescending(x => x.Id).ToList();
            var viewModel = new FaturaViewModel() { Faturas = faturalar, FaturaKalems = faturaKelemleri };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Ekle(JsonFatura data)
        {
            var fatura = new Fatura()
            {
                SiraNo = data.SiraNo,
                SeriNo = data.SeriNo,
                Tarih = data.Tarih,
                Saat = data.Saat,
                TeslimAlan = data.TeslimAlan,
                TeslimEden = data.TeslimEden,
                VergiDairesi = data.VergiDairesi,
                VergiNumarasi = data.VergiNumarasi,
                ToplamTutar = data.FaturaKalems.Sum(x => x.Tutar)
            };

            db.Faturas.Add(fatura);

            foreach (var kalem in data.FaturaKalems)
            {
                FaturaKalem faturaKalem = new FaturaKalem();
                faturaKalem.Aciklama = kalem.Aciklama;
                faturaKalem.Adet = kalem.Adet;
                faturaKalem.BirimFiyat = kalem.BirimFiyat;
                faturaKalem.Tutar = kalem.Tutar;
                faturaKalem.FaturaId = fatura.Id;
                db.FaturaKalems.Add(faturaKalem);

            }

            db.SaveChanges();

            return Json("Fatura başarıyla eklendi", JsonRequestBehavior.AllowGet);
        }

        public class JsonFatura
        {
            public string SiraNo { get; set; }
            public string SeriNo { get; set; }
            public string VergiDairesi { get; set; }
            public string VergiNumarasi { get; set; }
            public string TeslimEden { get; set; }
            public string TeslimAlan { get; set; }
            public decimal ToplamTutar { get; set; }
            public DateTime Tarih { get; set; }
            public string Saat { get; set; }
            public List<FaturaKalem> FaturaKalems { get; set; }
        }
    }
}