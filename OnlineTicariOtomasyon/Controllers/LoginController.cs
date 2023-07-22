using OnlineTicariOtomasyon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace OnlineTicariOtomasyon.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        Context db = new Context();

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public PartialViewResult PartialMusteriKayit()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult CariEkle(Cari cari)
        {
            if (cari != null)
            {
                db.Caris.Add(cari);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            else return View();
        }

        [HttpPost]
        public ActionResult CariGiris(Cari c)
        {
            string encSifre = Functions.DataSecurity.Encrypt(c.Sifre);
            var cari = db.Caris.FirstOrDefault(x => x.Eposta == c.Eposta && x.Sifre == encSifre && x.Sil == false);

            if (cari != null)
            {
                FormsAuthentication.SetAuthCookie(cari.Eposta, false);
                Session["CariId"] = cari.Id;
                Session["KisiBilgisi"] = $"{cari.Ad} {cari.Soyad}";
                return RedirectToAction("Index", "CariPanel");
            }
            else return RedirectToAction("Index", "Login");
        }

        [HttpPost]
        public ActionResult PersonelGiris(Personel p)
        {
            string encSifre = Functions.DataSecurity.Encrypt(p.Sifre);
            var personel = db.Personels.FirstOrDefault(x => x.Eposta == p.Eposta && x.Sifre == encSifre && x.Sil == false);

            if (personel != null)
            {
                FormsAuthentication.SetAuthCookie(personel.Eposta, false);
                Session["PersonelId"] = personel.Id;
                Session["KisiBilgisi"] = $"{personel.Ad} {personel.Soyad}";
                return RedirectToAction("Index", "Satis");
            }
            else return RedirectToAction("Index", "Login");
        }
    }
}