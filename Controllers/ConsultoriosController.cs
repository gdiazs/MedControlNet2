using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MedControlNet.Entities;

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
            Consultorio consultorio = db.Consultorios.Find(id);
            if (consultorio == null)
            {
                return HttpNotFound();
            }
            return View(consultorio);
        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "ConsultorioID,NumeroConsultorio")] Consultorio consultorio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(consultorio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(consultorio);
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
