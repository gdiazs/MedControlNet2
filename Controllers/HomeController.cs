using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MedControlNet.Controllers
{
    public class HomeController : Controller
    {

        private static readonly Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public ActionResult Index()
        {
            Console.WriteLine("hi Console");
            Logger.Info("Hola mundo");
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}