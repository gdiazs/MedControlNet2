using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MedControlNet.Models
{
    public class MedicoModelo
    {
        public int MedicoId { set; get; }

        [Required(ErrorMessage = "Cédula es requerida")]
        public string Identificacion { set; get; }

        [Required(ErrorMessage = "Nombre es requerido")]
        public string Nombre { set; get; }

        [Required(ErrorMessage = "Debe escoger una especialidad")]
        public int EspecialidadID { set; get; }

        public EspecialidadModel EspecialidadModel { set; get; }

        public bool EsTemporal { set; get; }
    }
}