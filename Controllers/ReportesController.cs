using MedControlNet.Models;
using MedControlNet.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MedControlNet.Controllers
{
    public class ReportesController : Controller
    {

        private readonly ReportesServicio _reportesServicio;

        public ReportesController(ReportesServicio reportesServicio)
        {
            _reportesServicio = reportesServicio;
        }

        // GET: Reportes
        public ActionResult Index()
        {
            var citasAtendidas = _reportesServicio.ObtenerCitasAtendidas();
            var reportes = GetReportes(citasAtendidas);

            return View(reportes);
        }

        private ReportesModelo GetReportes(List<string> citasAtendidas)
        {
            return new ReportesModelo()
            {
                Pacientes = _reportesServicio.ObtenerPacientesAtendidos(),
                Medicos = _reportesServicio.ObtenerMedicosQueAtendieron(),
                Citas = citasAtendidas,
                Consultorios = _reportesServicio.ObtenerListaEspecialidadesPorConsultorios(),
                ConteoIngresos = _reportesServicio.ObtenerConteoIngresos(),
                ConteoCitas = citasAtendidas.Count,
                PorcentajeDeAtencionPorMedico = _reportesServicio.ObtenerPorcentajeAtencionPorMedico(),
                PorcentajeEspecialidades = _reportesServicio.ObtenerPorcentajeEspecialidades()
            };
        }
    }
}