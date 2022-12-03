using MedControlNet.Models;
using MedControlNet.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;


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

        [HttpGet]
        public ActionResult ObtenerArchivo(string nombre)
        {

            var citasAtendidas = _reportesServicio.ObtenerCitasAtendidas();

            var timestamp = DateTime.Now.ToFileTimeUtc();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", $"attachment;filename = {nombre}_{timestamp}.json");
            Response.ContentType = "application/json";

            switch (nombre)
            {
                case "pacientes":
                    var pacientes = _reportesServicio.ObtenerPacientesAtendidos();
                    var vistaPacientes = pacientes.Select(paciente => paciente.Identificacion + " - " + paciente.Nombre).ToList();
                    Response.Write(JsonConvert.SerializeObject(vistaPacientes));
                    break;

                case "medicos":
                    Response.Write(JsonConvert.SerializeObject(_reportesServicio.ObtenerMedicosQueAtendieron().ToList()));
                    break;

                case "citas":
                    var citas = _reportesServicio.ObtenerCitasAtendidasSinFormato();
                    Response.Write(JsonConvert.SerializeObject(citas.ToList()));
                    break;

                case "consultorios":
                    var consultorios = _reportesServicio.ObtenerListaEspecialidadesPorConsultorios()
                        .Select(consultorio => new ConsultoriosReporteJson()
                        {
                            Consultorio = consultorio.ConsultorioID + "",
                            Especialidades = consultorio.Especialidads.Select(especialidad => especialidad.NombreEspecialidad).ToList()
                        });
                    Response.Write(JsonConvert.SerializeObject(consultorios.ToList()));
                    break;

                case "conteo":

                    Response.Write(JsonConvert.SerializeObject(new
                    {
                        CitasIngresadas = _reportesServicio.ObtenerConteoIngresos(),
                        ConteoCitas = citasAtendidas.Count
                    }));
                    break;

                case "estadisticas":

                    Response.Write(JsonConvert.SerializeObject(new
                    {
                        PorcentajeDeAtencionPorMedico = _reportesServicio.ObtenerPorcentajeAtencionPorMedico(),
                        PorcentajeEspecialidades = _reportesServicio.ObtenerPorcentajeEspecialidades()
                    }));
                    break;
            }

            Response.End();
            Response.Close();

            return new EmptyResult();
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