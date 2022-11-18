using MedControlNet.Entities;
using MedControlNet.Models;
using MedControlNet.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MedControlNet.Controllers
{
    public class CitasController : Controller
    {

        private readonly CitasServicio _citasServicio;
        private readonly MedicosServicio _medicosServicio;

        public CitasController(CitasServicio citasServicio, MedicosServicio medicosServicio)
        {
            _citasServicio = citasServicio;
            _medicosServicio = medicosServicio;
        }

        public ActionResult Index()
        {
            var citaFormulario = TempData["form"] != null ? (CitaFormularioModelo)TempData["form"] : new CitaFormularioModelo();

            var medicosLista = _medicosServicio.ObtenerMedicos();
            var items = medicosLista.Select(medico => new SelectListItem() { 

                Selected = medico.MedicoId + "" == citaFormulario.MedicoEspecialista,
                Text = $"{medico.Nombre} ({medico.EspecialidadModel.NombreEspecialidad})",
                Value = medico.MedicoId.ToString()
            
            }).ToList();

            if (citaFormulario.FechaCita < DateTime.Now) {
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

        private string ObtenerEspecialidades(Consultorio consultorio) {
            var especialidades = consultorio.Especialidads.Select( esp => esp.NombreEspecialidad ).ToList();
            return $"{consultorio.NumeroConsultorio} ({string.Join(", ", especialidades)})";

        }

        [HttpPost]
        public ActionResult Index(CitaFormularioModelo citaModelo)
        {
            var fechaSeleccionada = citaModelo.FechaCita;


            if (NoEsHoraDeAlmuerzo(fechaSeleccionada))
            {
                TempData["MensajeError"] = $"La fecha seleccionada '{citaModelo.FechaCita}' no es válida. La hora de almuerzo es de las 12pm a 1pm ";
                TempData["form"] = citaModelo;
            }

            if (NoEsHorarioValido(fechaSeleccionada)) {
                TempData["MensajeError"] = $"La fecha seleccionada '{citaModelo.FechaCita}' no es válida. El horario de atención 8am a 5pm. ";
                TempData["form"] = citaModelo;
            }


            TempData["form"] = null;
            _citasServicio.AgregarCita(citaModelo);
            TempData["MensajeExito"] = $"La cita de {citaModelo.NombrePaciente} con cédula {citaModelo.CedulaPaciente} se ha agendado satisfactoriamente";

            return RedirectToAction("Index");
        }

        private bool NoEsHoraDeAlmuerzo(DateTime fechaSeleccionada)
        {

            return fechaSeleccionada.Hour >= 12 && fechaSeleccionada.Hour < 13;
        }

        private bool NoEsHorarioValido(DateTime fechaSeleccionada)
        {
            var now = DateTime.Now;

            var inicio = new DateTime(now.Year, now.Month, now.Day, 08, 00, 00);
            var fin = new DateTime(now.Year, now.Month, now.Day, 16, 30, 00);

            return !(fechaSeleccionada >= inicio && fechaSeleccionada <= fin);
        }
    }
}