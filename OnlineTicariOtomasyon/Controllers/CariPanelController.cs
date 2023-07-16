using OnlineTicariOtomasyon.Models;
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

        [Authorize]
        public ActionResult Index()
        {
            int cariId = Convert.ToInt32(Session["CariId"]);
            var cari = db.Caris.FirstOrDefault(x => x.Id == cariId);
            ViewBag.CariBilgisi = $"{cari.Ad} {cari.Soyad}";

            return View(cari);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Profil()
        {
            int cariId = Convert.ToInt32(Session["CariId"]);
            var cari = db.Caris.FirstOrDefault(x => x.Id == cariId);
            return View(cari);
        }

        [HttpPost]
        public ActionResult BilgiGuncelle(Cari c)
        {
            var cari = db.Caris.FirstOrDefault(x => x.Id == c.Id);

            if (cari != null)
            {
                if (!ModelState.IsValid)
                    return RedirectToAction("Profil", "CariPanel");
                else
                {
                    cari.Ad = c.Ad;
                    cari.Soyad = c.Soyad;
                    cari.Eposta = c.Eposta;
                    cari.Sehir = c.Sehir;

                    db.SaveChanges();
                    return RedirectToAction("Index", "CariPanel");
                }
            }
            else return RedirectToAction("Profil", "CariPanel");
        }

        [HttpGet]
        [Authorize]
        public ActionResult Siparisler()
        {
            int cariId = Convert.ToInt32(Session["CariId"]);
            var siparisler = db.SatisHarekets.Where(x => x.CariId == cariId).OrderByDescending(x=>x.Tarih).ToList();

            return View(siparisler);
        }

    }
}