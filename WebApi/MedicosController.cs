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

namespace MedControlNet.WebApi
{
    public class MedicosController : ApiController
    {
        [HttpGet]
        public List<Medico> Get() {
            var m = new Medico();
            m.MedicoID = 1;
            m.Identificacion = "1";

            var list = new List<Medico>();
            list.Add(m);

            return list;
        }
    }
}