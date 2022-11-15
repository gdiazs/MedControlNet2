using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
using MedControlNet.Entities;
using MedControlNet.Models;
using MedControlNet.Services;
using NLog;

namespace MedControlNet.WebApi
{
    public class MedicosController : ApiController
    {

        private static readonly Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        private readonly MedicosServicio _medicosServicios;

        public MedicosController(MedicosServicio medicosServicios)
        {
            _medicosServicios = medicosServicios;
        }

        [HttpGet]
        public List<MedicoModelo> Obtener() {

            Logger.Info("Obteniendo todos los médicos");

            var medicos = _medicosServicios.ObtenerMedicos();

            Logger.Info($"Cantidad de médicos {medicos.Count}");

            return medicos;
        }

        [HttpPost]
        public MedicoModelo Agregar(MedicoModelo medico) {
            Logger.Info($"Se va a agregar el siguiente médico {medico.Nombre}");
           return _medicosServicios.AgregarMedico(medico);
        }
    }
}