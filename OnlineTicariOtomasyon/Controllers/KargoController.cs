using OnlineTicariOtomasyon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineTicariOtomasyon.Controllers
{
    public class KargoController : Controller
    {
        Context db = new Context();
        // GET: Kargo
        
        public ActionResult Index()
        {
            var kargolar = db.Kargos.OrderByDescending(x => x.Tarih).ToList();
            return View(kargolar);
        }

        [HttpGet]
        
        public ActionResult KargoDetay(string id)
        {
            var kargo = db.Kargos.FirstOrDefault(x => x.TakipKodu == id);
            return View(kargo);
        }
    }

}