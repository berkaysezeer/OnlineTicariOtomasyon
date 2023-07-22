using OnlineTicariOtomasyon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineTicariOtomasyon.Controllers
{
    public class FaturaController : Controller
    {
        Context db = new Context();
        // GET: Fatura

        
        public ActionResult Index()
        {
            var faturalar = db.Faturas.ToList();
            return View(faturalar);
        }

        [HttpGet]
        
        public ActionResult Ekle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Ekle(Fatura fatura)
        {
            if (fatura != null)
            {
                if (!ModelState.IsValid) return View();
                else
                {
                    db.Faturas.Add(fatura);
                    db.SaveChanges();
                    TempData["FaturaSuccess"] = $"{fatura.Id} numaralı fatura başarıyla oluşturuldu";
                    return RedirectToAction("Index");
                }
            }
            else return View();
        }

        
        public ActionResult Detay(int Id)
        {
            var fatura = db.Faturas.Where(x => x.Id == Id).FirstOrDefault();

            if (fatura != null)
            {
                ViewBag.FaturaNo = Id;
                var faturaKalemleri = db.FaturaKalems.Where(x => x.FaturaId == Id).ToList();

                return View(faturaKalemleri);
            }
            else return RedirectToAction("Index");

        }

        [HttpGet]
        
        public ActionResult Duzenle(int Id)
        {
            var fatura = db.Faturas.Where(x => x.Id == Id).FirstOrDefault();
            return View(fatura);
        }

        [HttpPost]
        public ActionResult Duzenle(Fatura f)
        {
            var fatura = db.Faturas.Where(x => x.Id == f.Id).FirstOrDefault();

            if (fatura != null)
            {
                if (!ModelState.IsValid)
                {
                    return View(fatura);
                }
                else
                {
                    fatura.Saat = f.Saat;
                    fatura.SeriNo = f.SeriNo;
                    fatura.SiraNo = f.SiraNo;
                    fatura.Tarih = f.Tarih;
                    fatura.TeslimAlan = f.TeslimAlan;
                    fatura.TeslimEden = f.TeslimEden;
                    fatura.VergiDairesi = f.VergiDairesi;
                    fatura.VergiNumarasi = f.VergiNumarasi;
                    db.SaveChanges();

                    TempData["FaturaSuccess"] = $"{fatura.Id} numaralı fatura başarıyla düzenlendi";

                    return RedirectToAction("Index");
                }
            }
            else return View(fatura);
        }

        [HttpGet]
        
        public ActionResult KalemEkle(int? Id)
        {
            if (Id == null || Id == 0) return RedirectToAction("Index");
            else
            {
                ViewBag.FaturaSiraNo = db.Faturas.Where(x => x.Id == Id).Select(x => x.SiraNo).FirstOrDefault();
                ViewBag.FaturaId = Id;
                return View();
            }
        }

        [HttpPost]
        public ActionResult KalemEkle(FaturaKalem fk, int Id)
        {
            if (fk is FaturaKalem)
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.FaturaSiraNo = db.Faturas.Where(x => x.Id == Id).Select(x => x.SiraNo);
                    ViewBag.FaturaId = Id;
                    return View();
                }
                else
                {
                    fk.FaturaId = Id;
                    db.FaturaKalems.Add(fk);
                    db.SaveChanges();

                    TempData["FaturaKalemSuccess"] = $"{Id} numaralı fatura için yeni bir kalem eklendi.";

                    return RedirectToAction($"Detay/{Id}");
                }
            }
            else return RedirectToAction($"Detay/{Id}");
        }

        [HttpPost]
        public ActionResult KalemEkleModal(FaturaKalem fk)
        {
            if (fk is FaturaKalem)
            {
                if (!ModelState.IsValid)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    db.FaturaKalems.Add(fk);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            else return RedirectToAction("Index");
        }
    }
}