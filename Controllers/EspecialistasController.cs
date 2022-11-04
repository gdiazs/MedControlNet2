using MedControlNet.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MedControlNet.Controllers
{
    public class EspecialistasController : Controller
    {
        private readonly MedicosServicio _medicosServicio;

        public EspecialistasController(MedicosServicio medicosServicio)
        {
            _medicosServicio = medicosServicio;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}