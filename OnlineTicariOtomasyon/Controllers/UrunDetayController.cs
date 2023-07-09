using OnlineTicariOtomasyon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineTicariOtomasyon.Controllers
{
    public class UrunDetayController : Controller
    {
        Context db = new Context();
        // GET: UrunDetay
        public ActionResult Index(int Id)
        {
            var urun = db.Uruns.FirstOrDefault(x => x.Id == Id);
            return View(urun);
        }

        public ActionResult Deneme()
        {
            var viewModel = new ViewModel();
            viewModel.Uruns = db.Uruns.Where(x => x.Sil == false).ToList();
            viewModel.Markas = db.Markas.Where(x => x.Sil == false).ToList();

            return View(viewModel);
        }
    }
}