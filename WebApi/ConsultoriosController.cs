using MedControlNet.Entities;
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

    public class ConsultoriosController : ApiController
    {

        private readonly MedicosServicio _medicosServicio;
        private readonly EspecialidadesServicio _especialidadesServicio;

        public ConsultoriosController(MedicosServicio medicosServicio, EspecialidadesServicio especialidadesServicio)
        {
            _medicosServicio = medicosServicio;
            _especialidadesServicio = especialidadesServicio;
        }

        [HttpGet]
        public List<ConsultoriosEspecialidadModelo> Get(int medicoId) 
        {
            var medico = _medicosServicio.ObtenerMedicoPorId(medicoId);

            return _especialidadesServicio.ObtenerEspecialidadPorId(medico.EspecialidadID.Value).Consultorios
                .Select( consultorio => new ConsultoriosEspecialidadModelo() { ConsultorioID = consultorio.ConsultorioID,
                    NumeroConsultorio = consultorio.NumeroConsultorio.Value } )
                .ToList();
            
        }

    }
}