using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedControlNet.Models
{
    public class CitaFormularioModelo
    {
        public string NombrePaciente { set; get; }
        public string CedulaPaciente { set; get; }

        public DateTime FechaCita { set; get; }

        public string ConsultorioId { set; get; }
        public string MedicoEspecialista { set; get; }

        public decimal Monto { set; get; }
    }
}