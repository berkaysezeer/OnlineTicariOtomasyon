using OnlineTicariOtomasyon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineTicariOtomasyon.Functions
{
    public static class DropdownListItems
    {
        static Context db = new Context();

        public static IEnumerable<SelectListItem> Kategori()
        {
            IEnumerable<SelectListItem> kategoriler = new SelectList(db.Kategoris.Where(x => x.Sil == false), "Id", "Ad");
            return kategoriler;
        }

        public static IEnumerable<SelectListItem> Marka()
        {
            IEnumerable<SelectListItem> markalar = new SelectList(db.Markas.Where(x => x.Sil == false), "Id", "Ad");
            return markalar;
        }

        public static IEnumerable<SelectListItem> Departman()
        {
            IEnumerable<SelectListItem> departmanlar = new SelectList(db.Departmans.Where(x => x.Sil == false), "Id", "Ad");
            return departmanlar;
        }

        public static IEnumerable<SelectListItem> Urun()
        {
            IEnumerable<SelectListItem> urunler = new SelectList(db.Uruns.Where(x => x.Sil == false), "Id", "Ad");
            return urunler;
        }

        public static IEnumerable<SelectListItem> Urun(int KategoriId)
        {
            IEnumerable<SelectListItem> urunler = new SelectList(db.Uruns.Where(x => x.Sil == false && x.KategoriId == KategoriId), "Id", "Ad");
            return urunler;
        }

        public static IEnumerable<SelectListItem> Personel()
        {
            var personeller = db.Personels
                            .Where(x => x.Sil == false)
                            .Select(x => new
                            {
                                Id = x.Id,
                                Ad = x.Ad + " " + x.Soyad.ToString()
                            });

            IEnumerable<SelectListItem> listPersoneller = new SelectList(personeller, "Id", "Ad");
            return listPersoneller;
        }

        public static IEnumerable<SelectListItem> Cari()
        {
            var cariler = db.Caris
                            .Where(x => x.Sil == false)
                            .Select(x => new
                            {
                                Id = x.Id,
                                Ad = x.Ad + " " + x.Soyad.ToString()
                            });

            IEnumerable<SelectListItem> listCariler = new SelectList(cariler, "Id", "Ad");
            return listCariler;
        }

        public static IEnumerable<SelectListItem> CariMesaj(int cariId)
        {
            var cariler = db.Caris
                            .Where(x => x.Sil == false && x.Id != cariId)
                            .Select(x => new
                            {
                                Id = x.Id,
                                Ad = x.Ad + " " + x.Soyad + " (" + x.Eposta + ") "
                            });

            IEnumerable<SelectListItem> listCariler = new SelectList(cariler, "Id", "Ad");
            return listCariler;
        }
    }
}