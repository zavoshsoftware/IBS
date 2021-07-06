using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IBS.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return RedirectToAction("login", "Account");
        }
        public ActionResult Play()
        {
            return View();
        }
        public ActionResult TestRange()
        {
            return View();
        }
    }
}