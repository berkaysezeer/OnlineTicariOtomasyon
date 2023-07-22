using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineTicariOtomasyon.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Hata()
        {
            Response.TrySkipIisCustomErrors = true;
            return View();
        }

        public ActionResult Page404()
        {
            Response.StatusCode = 404;
            ViewBag.StatusCode = Response.StatusCode;
            Response.TrySkipIisCustomErrors = true;
            return View("Hata");
        }
    }
}