using MedControlNet.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedControlNet.Models
{
    public class ReportesModelo
    {
        public List<Paciente> Pacientes { set; get; }
        public List<string> Medicos { set; get; }

        public List<string> Citas { set; get; }

        public List<Consultorio> Consultorios { set; get; }
        public int ConteoIngresos { get; internal set; }
        public int ConteoCitas { get; internal set; }
        public decimal PorcentajeEspecialidades { get; internal set; }
        public List<MedicoCitasReporte> PorcentajeDeAtencionPorMedico { get; internal set; }

        public List<Inventario> Activos { set; get; }
    }
}