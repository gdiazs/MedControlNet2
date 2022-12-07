using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
using MedControlNet.Entities;
using MedControlNet.Exceptions;
using MedControlNet.Models;
using MedControlNet.Services;
using Newtonsoft.Json;
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
        public HttpResponseMessage Agregar(MedicoModelo medico) {
            Logger.Info($"Se va a agregar el siguiente médico {medico.Nombre}");

            try
            {
                if (ModelState.IsValid)
                {
                    return Request
                        .CreateResponse<MedicoModelo>(HttpStatusCode.OK,
                        _medicosServicios.AgregarMedico(medico));
                }
                else {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
            }
            catch (MedicoExisteExcepcion  ex) {
                var mensajeError = new MensajeError()
                {
                    CodigoHttp = HttpStatusCode.BadRequest,
                    Mensaje = ex.Message
                };
                throw new HttpResponseException( new HttpResponseMessage() { 
                    Content = new StringContent( JsonConvert.SerializeObject(mensajeError)),
                    StatusCode = HttpStatusCode.BadRequest
                });
            }
        }
    }
}