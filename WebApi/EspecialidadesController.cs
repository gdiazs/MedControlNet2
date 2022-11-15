using MedControlNet.Models;
using MedControlNet.Services;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MedControlNet.WebApi
{

    public class EspecialidadesController : ApiController
    {

        private static readonly Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        private readonly EspecialidadesServicio _especialidadesServicio;

        public EspecialidadesController(EspecialidadesServicio especialidadesServicio)
        {
            _especialidadesServicio = especialidadesServicio;
        }


        [HttpGet]
        public List<EspecialidadModel> Get()
        {

            Logger.Info("Obteniendo todos las especialidades");

            var especialidades = _especialidadesServicio.ObtenerEspecialidades();

            Logger.Info($"Cantidad de especialidades {especialidades.Count}");

            return especialidades;
        }

    }
}