using MedControlNet.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MedControlNet.Models
{
    public class ConsultorioModelo
    {

        public int ConsultorioID { get; set; }

        public int NumeroConsultorio { get; set; }

        public ICollection<Especialidad> EspecialidadesConsultorio { set; get; }

        public List<SelectListItem> TodasLasEspecialidades { set; get; }

        public int NuevaEspecialidad { set; get; }   

    }
}