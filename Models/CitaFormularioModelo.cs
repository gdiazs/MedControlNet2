using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MedControlNet.Models
{
    public class CitaFormularioModelo
    {
        [Required(ErrorMessage = "Nombre del paciente es requerido")]
        public string NombrePaciente { set; get; }

        [Required(ErrorMessage = "Cédula del paciente requerida")]
        public string CedulaPaciente { set; get; }

        [Required(ErrorMessage = "Fecha de cita es requerida")]
        public DateTime FechaCita { set; get; }

        public string ConsultorioId { set; get; }
        public string MedicoEspecialista { set; get; }


        [Range(1, int.MaxValue, ErrorMessage = "Ingrese un monto mayor que 0")]
        public decimal Monto { set; get; }
    }
}