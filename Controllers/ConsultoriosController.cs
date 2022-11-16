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

namespace MedControlNet.Controllers
{
    public class ConsultoriosController : Controller
    {
        private MedControlNetDBEntities db = new MedControlNetDBEntities();

        public ActionResult Index()
        {
            return View(db.Consultorios.ToList());
        }



        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var consultorio = db.Consultorios.Find(id);


            var listaEspecialidades = consultorio.Especialidads.Select(especialidad => especialidad.EspecialidadID).ToList();

            var especialidades = db.Especialidads
                .Where( especialidad => !listaEspecialidades.Contains(especialidad.EspecialidadID)).ToList();

            var items = especialidades
                .Select(especialidad => new SelectListItem() { Value = especialidad.EspecialidadID + "", Text = especialidad.NombreEspecialidad })
                .ToList();


            var consultorioModelo = new ConsultorioModelo()
            {
                ConsultorioID = consultorio.ConsultorioID,
                NumeroConsultorio = consultorio.NumeroConsultorio.Value,
                EspecialidadesConsultorio = consultorio.Especialidads,
                TodasLasEspecialidades = items

            };
            return View(consultorioModelo);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "ConsultorioID,NumeroConsultorio")] ConsultorioModelo consultorioModelo)
        {
            var consultorio = db.Consultorios.Find(consultorioModelo.ConsultorioID);
            if (ModelState.IsValid)
            {

                consultorio.NumeroConsultorio = consultorioModelo.NumeroConsultorio;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(consultorio);
        }

        [HttpPost]
        public ActionResult AgregarEspecialidad(ConsultorioModelo consultorioModelo) {

            var consultorio = db.Consultorios.Find(consultorioModelo.ConsultorioID);
            var especialidad = db.Especialidads.Find(consultorioModelo.NuevaEspecialidad);
            consultorio.Especialidads.Add(especialidad);

            db.SaveChanges();

            return RedirectToAction($"Edit/{consultorioModelo.ConsultorioID}");
        }

        public ActionResult EliminarEspecialidad(int? especialidadId, int? consultorioId) {

            var consultorio = db.Consultorios.Find(consultorioId);
            var especialidad = db.Especialidads.Find(especialidadId);

            consultorio.Especialidads.Remove(especialidad);

            db.SaveChanges();

            return RedirectToAction($"Edit/{consultorioId}");
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
