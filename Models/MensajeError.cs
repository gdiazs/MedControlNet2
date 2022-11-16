using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace MedControlNet.Models
{
    public class MensajeError
    {
        public HttpStatusCode CodigoHttp { get; set; }
        public string Mensaje { get; set; }
    }
}