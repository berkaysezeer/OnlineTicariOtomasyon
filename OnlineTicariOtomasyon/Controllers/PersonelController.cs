﻿using OnlineTicariOtomasyon.Functions;
using OnlineTicariOtomasyon.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineTicariOtomasyon.Controllers
{
    [Authorize(Roles = "Yönetici")]
    public class PersonelController : Controller
    {
        Context db = new Context();


        public ActionResult Index()
        {
            var personeller = db.Personels.Where(x => x.Sil == false).ToList();
            return View(personeller);
        }

        [HttpGet]

        public ActionResult Ekle()
        {
            ViewBag.DepartmanListesi = DropdownListItems.Departman();
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
                        ViewBag.DepartmanListesi = DropdownListItems.Departman();
                        return View();
                    }
                    else
                    {
                        if (db.Personels.FirstOrDefault(x => x.Sil == false && x.Eposta == personel.Eposta) is null)
                        {
                            string guid = Guid.NewGuid().ToString();
                            personel.Guid = guid;
                            personel.Gorsel = GorselKaydet.PersonelGorselKaydet(Request, Server, guid);
                            string encSifre = DataSecurity.Encrypt(personel.Sifre);
                            personel.Sifre = encSifre;

                            db.Personels.Add(personel);
                            db.SaveChanges();

                            TempData["PersonelSuccess"] = $"{personel.Ad} {personel.Soyad} başarıyla eklendi";

                            return RedirectToAction("Index");
                        }
                        else
                        {
                            TempData["DangerPersonel"] = "Girilen personel kaydı sistemde kayıtlı";
                            ViewBag.DepartmanListesi = DropdownListItems.Departman();
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

        public ActionResult Duzenle(int Id)
        {
            ViewBag.DepartmanListesi = DropdownListItems.Departman();
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
                    ViewBag.DepartmanListesi = DropdownListItems.Departman();
                    ViewBag.PersonelBilgisi = db.Personels.Where(x => x.Id == p.Id).Select(x => x.Ad + " " + x.Soyad).FirstOrDefault();
                    return View(personel);
                }
                else
                {
                    if (db.Personels.FirstOrDefault(x => x.Sil == false && x.Eposta == p.Eposta) is null || p.Eposta == personel.Eposta)
                    {
                        if (Request.Files.Count > 0)
                        {
                            string gorsel = GorselKaydet.PersonelGorselKaydet(Request, Server, personel.Guid);
                            if (!string.IsNullOrEmpty(gorsel)) personel.Gorsel = gorsel;
                        }

                        personel.Ad = p.Ad;
                        personel.Soyad = p.Soyad;
                        personel.DepartmanId = p.DepartmanId;
                        personel.Eposta = p.Eposta;
                        personel.CepTelefonu = p.CepTelefonu;
                        personel.Adres = p.Adres;

                        db.SaveChanges();

                        TempData["PersonelSuccess"] = $"{personel.Ad} {personel.Soyad} başarıyla düzenlendi";

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["DangerPersonel"] = "Girilen personel kaydı sistemde kayıtlı";
                        ViewBag.DepartmanListesi = DropdownListItems.Departman();
                        ViewBag.PersonelBilgisi = db.Personels.Where(x => x.Id == p.Id).Select(x => x.Ad + " " + x.Soyad).FirstOrDefault();
                        return View(personel);
                    }
                }
            }
            else return RedirectToAction("Index");


        }
    }
}