using MedControlNet.Entities;
using MedControlNet.Models;
using MedControlNet.Services;
using NLog;
using System;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;

using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages.Html;
using SelectListItem = System.Web.Mvc.SelectListItem;

namespace MedControlNet.Controllers
{
    public class CitasController : Controller
    {
        private static readonly Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        private readonly CitasServicio _citasServicio;
        private readonly MedicosServicio _medicosServicio;

        public CitasController(CitasServicio citasServicio, MedicosServicio medicosServicio)
        {
            _citasServicio = citasServicio;
            _medicosServicio = medicosServicio;
        }

        public ActionResult Index()
        {

            ModelState.Merge(TempData["modelState"] == null ? ModelState : (System.Web.Mvc.ModelStateDictionary)TempData["modelState"]);

            var citaFormulario = TempData["form"] != null ? (CitaFormularioModelo)TempData["form"] : new CitaFormularioModelo();

            var medicosLista = _medicosServicio.ObtenerMedicos();
            var items = medicosLista.Select(medico => new SelectListItem()
            {

                Selected = medico.MedicoId + "" == citaFormulario.MedicoEspecialista,
                Text = $"{medico.Nombre} ({medico.EspecialidadModel.NombreEspecialidad})",
                Value = medico.MedicoId.ToString()

            }).ToList();

            if (citaFormulario.FechaCita < DateTime.Now)
            {
                citaFormulario.FechaCita = DateTime.Now;
            }


            items.Insert(0, new SelectListItem()
            {
                Text = "Seleccione",
                Value = "-1"
            });
            var citaModelo = new CitaModelo()
            {
                medicos = items,
                consultorios = null,
                CitaFormularioModelo = citaFormulario
            };

            return View(citaModelo);
        }


        [HttpPost]
        public ActionResult Index(CitaFormularioModelo citaModelo)
        {

            if (ModelState.IsValid)
            {
                var fechaSeleccionada = citaModelo.FechaCita;


                if (NoEsHoraDeAlmuerzo(fechaSeleccionada))
                {
                    TempData["MensajeError"] = $"La fecha seleccionada '{citaModelo.FechaCita}' no es válida. La hora de almuerzo es de las 12pm a 1pm ";
                    TempData["form"] = citaModelo;
                    return RedirectToAction("Index");
                }

                if (NoEsHorarioValido(fechaSeleccionada))
                {
                    TempData["MensajeError"] = $"La fecha seleccionada '{citaModelo.FechaCita}' no es válida. El horario de atención 8am a 5pm. ";
                    TempData["form"] = citaModelo;
                    return RedirectToAction("Index");
                }

                try
                {
                    TempData["form"] = null;
                    _citasServicio.AgregarCita(citaModelo);
                    TempData["MensajeExito"] = $"La cita de {citaModelo.NombrePaciente} con cédula {citaModelo.CedulaPaciente} se ha agendado satisfactoriamente";
                }
                catch (CitaExisteExcepcion ex)
                {

                    TempData["MensajeError"] = ex.Message;
                    TempData["form"] = citaModelo;
                    TempData["modelState"] = ModelState;
                }

                return RedirectToAction("Index");
            }

            TempData["MensajeError"] = "Hay campos con errores";
            TempData["form"] = citaModelo;
            TempData["modelState"] = ModelState;
            return RedirectToAction("Index");
        }

        private bool NoEsHoraDeAlmuerzo(DateTime fechaSeleccionada)
        {

            return fechaSeleccionada.Hour >= 12 && fechaSeleccionada.Hour < 13;
        }

        private bool NoEsHorarioValido(DateTime fechaSeleccionada)
        {


            return !(fechaSeleccionada.Hour >= 9 && fechaSeleccionada.Hour < 17);
        }

    }
}