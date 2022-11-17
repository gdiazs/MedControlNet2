using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MedControlNet.Entities;
using MedControlNet.Models;
using MedControlNet.Services;

namespace MedControlNet.Controllers
{
    public class ConsultoriosController : Controller
    {

        private readonly ConsultoriosServicio _consultoriosServicio;

        public ConsultoriosController(ConsultoriosServicio consultoriosServicio)
        {
            _consultoriosServicio = consultoriosServicio;
        }

        public ActionResult Index()
        {
            return View(_consultoriosServicio.ObtenerConsultorios());
        }



        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(_consultoriosServicio.ObtenerConsutultorioPorId(id));

        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "ConsultorioID,NumeroConsultorio")] ConsultorioModelo consultorioModelo)
        {
           
            if (ModelState.IsValid)
            {

                _consultoriosServicio.ActualizarConsultorio(consultorioModelo);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult AgregarEspecialidad(ConsultorioModelo consultorioModelo) {

            _consultoriosServicio.AgregarEspecialidad(consultorioModelo);

            return RedirectToAction($"Edit/{consultorioModelo.ConsultorioID}");
        }

        public ActionResult EliminarEspecialidad(int? especialidadId, int? consultorioId) {

            _consultoriosServicio.RemoverEspecialidad(especialidadId, consultorioId);

            return RedirectToAction($"Edit/{consultorioId}");
        }

    }
}
