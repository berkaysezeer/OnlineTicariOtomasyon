using OnlineTicariOtomasyon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineTicariOtomasyon.Controllers
{
    public class GaleriController : Controller
    {
        Context db = new Context();

        [Authorize]
        public ActionResult Index()
        {
            var urunler = db.Uruns.Where(x => x.Sil == false).ToList();
            return View(urunler);
        }
    }
}