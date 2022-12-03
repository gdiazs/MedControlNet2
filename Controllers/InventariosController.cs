using MedControlNet.Models;
using MedControlNet.Services;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MedControlNet.Controllers
{
    public class InventariosController : Controller
    {

        private readonly InventariosServicio _inventariosServicio;

        public InventariosController(InventariosServicio inventariosServicio)
        {
            _inventariosServicio = inventariosServicio;
        }

        // GET: Inventarios
        public ActionResult Index()
        {
            return View(_inventariosServicio.ObtenerEquiposMedicos());
        }

        public ActionResult Create()
        {
            return View(_inventariosServicio.GenerarNuevoInventarioModelo());
        }

        public ActionResult Edit(int id)
        {
            return View(_inventariosServicio.ObtenerEquipoMedicoPorId(id));
        }

        [HttpPost]
        public ActionResult Edit(InventarioModelo inventarioModelo)
        {
            if (ModelState.IsValid)
            {
                _inventariosServicio.ActualizarObjeto(inventarioModelo);

                return RedirectToAction("Index");
            }
            var modelData = _inventariosServicio.GenerarNuevoInventarioModelo();
            inventarioModelo.Especialidades = modelData.Especialidades;
            inventarioModelo.Consultorios = modelData.Consultorios;

            return View(inventarioModelo);
        }
        public ActionResult Delete(int id)
        {
            _inventariosServicio.BorrarObjeto(id);
            return RedirectToAction("Index");
        }


        [HttpPost]
        public ActionResult Create(InventarioModelo inventarioModelo)
        {
            if (ModelState.IsValid)
            {
                _inventariosServicio.GuardarObjeto(inventarioModelo);

               return RedirectToAction("Index");
            }
            var modelData = _inventariosServicio.GenerarNuevoInventarioModelo();
            inventarioModelo.Especialidades = modelData.Especialidades;
            inventarioModelo.Consultorios = modelData.Consultorios;

            return View(inventarioModelo);

        }
    }
}