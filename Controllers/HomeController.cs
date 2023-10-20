using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace ProyectoProgra2.Controllers
{
    public class HomeController : Controller
    {
        [Authorize][OutputCache(NoStore = true, Location = OutputCacheLocation.None)]
        public ActionResult Index()
        {
            return View();
        }
        [Authorize][OutputCache(NoStore = true, Location = OutputCacheLocation.None)]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [Authorize][OutputCache(NoStore = true, Location = OutputCacheLocation.None)]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}