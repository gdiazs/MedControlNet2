using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedControlNet.Models
{
    public class MedicoModelo
    {
        public string Identificacion { set; get; }

        public string Nombre { set; get; }

        public int EspecialidadID { set; get; }

        public bool EsTemporal { set; get; }
    }
}