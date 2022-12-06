using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedControlNet.Models
{
    public class CitaReporteJson
    {
        public string Paciente { get; internal set; }
        public DateTime HoraCita { get; internal set; }
        public decimal Costo { get; internal set; }
        public string Medico { get; internal set; }
    }
}