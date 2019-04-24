using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OneCountryWebApi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult IncidentList()
        {
            return View();
        }

        public ActionResult Incident(int incidentID)
        {
            return View();
        }
    }
}
