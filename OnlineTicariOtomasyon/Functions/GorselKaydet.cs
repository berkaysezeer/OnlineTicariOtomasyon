using OnlineTicariOtomasyon.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace OnlineTicariOtomasyon.Functions
{
    public static class GorselKaydet
    {
        public static string PersonelGorselKaydet(HttpRequestBase Request, HttpServerUtilityBase Server, Personel personel)
        {
            string dosyaAdi = $"{personel.Ad}{personel.Soyad}-{personel.Id}";
            string uzanti = Path.GetExtension(Request.Files[0].FileName);
            string dosyaYolu = $"/Images/Personel/{dosyaAdi}{uzanti}";
            Request.Files[0].SaveAs(Server.MapPath(dosyaYolu));

            return dosyaYolu;
        }
    }
}