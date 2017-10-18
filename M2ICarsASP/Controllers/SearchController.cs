using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace M2ICarsASP.Controllers
{
    public class SearchController : Controller
    {
        // GET: Index
        public ActionResult Index()
        {
            return View();
        }

        // GET: Search
        [ValidateAntiForgeryToken]
        public string Search(string departure, string arrival)
        {
            return $"Demande de chauffeur pour un trajet de {departure} jusque {arrival}";
        }
    }
}