using MedControlNet.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MedControlNet.Models
{
    public class InventarioModelo
    {

        public int InventarioID { set; get; }

        [Required(ErrorMessage = "Número de activo requerido")]
        public int NumeroActivo { set; get; }

        [Required(ErrorMessage = "Nombre requerido")]
        public string Nombre { set; get; }

        [Required(ErrorMessage = "Serie requerida")]
        public string Serie { set; get; }

        [Required(ErrorMessage = "Descripción requerida")]
        public string Descripcion { set; get; }

        [Required(ErrorMessage = "Fecha de compra requerida")]
        public string FechaDeCompra { set; get; }

        public List<Especialidad> Especialidades { set; get; }

        public List<Consultorio> Consultorios { set; get; }

        public string Especialidad { set; get; }

        public int EspecialidadID { set; get; }

        public string Consultorio { set; get; }

        public int ConsultorioID { set; get; }
    }
}