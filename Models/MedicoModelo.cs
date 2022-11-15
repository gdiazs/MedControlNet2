using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedControlNet.Models
{
    public class MedicoModelo
    {
        public int MedicoId { set; get; }

        public string Identificacion { set; get; }

        public string Nombre { set; get; }

        public int EspecialidadID { set; get; }

        public EspecialidadModel EspecialidadModel { set; get; }

        public bool EsTemporal { set; get; }
    }
}