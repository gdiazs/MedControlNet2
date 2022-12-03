using MedControlNet.Entities;
using MedControlNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MedControlNet.Services
{
    public class ReportesServicio
    {
        public List<Paciente> ObtenerPacientesAtendidos()
        {
            using (var db = new MedControlNetDBEntities())
            {
                var ahora = DateTime.Now;
                var citas = db.Citas.Where(cita => cita.HoraCita <= ahora).ToList();

                return db.Citas.Where(cita => cita.HoraCita <= ahora).Select(cita => cita.Paciente).ToList();
            }
        }

        public List<string> ObtenerMedicosQueAtendieron()
        {
            using (var db = new MedControlNetDBEntities())
            {
                var ahora = DateTime.Now;
                var citas = db.Citas.Where(cita => cita.HoraCita <= ahora).ToList();

                return db.Citas.Where(cita => cita.HoraCita <= ahora)
                    .AsEnumerable()
                    .Select(cita => DarFormatoMedicoDeLaCita(cita))
                    .Distinct()
                    .ToList();
            }
        }

        public List<string> ObtenerCitasAtendidas()
        {
            using (var db = new MedControlNetDBEntities())
            {
                var ahora = DateTime.Now;
                var citas = db.Citas.Where(cita => cita.HoraCita <= ahora).ToList();

                return db.Citas.Where(cita => cita.HoraCita <= ahora)
                    .AsEnumerable()
                    .Select(cita => DarFormatoCita(cita))
                    .ToList();
            }
        }

        public List<CitaReporteJson> ObtenerCitasAtendidasSinFormato()
        {
            using (var db = new MedControlNetDBEntities())
            {
                var ahora = DateTime.Now;
                var citas = db.Citas.Where(cita => cita.HoraCita <= ahora).ToList();

                return db.Citas.Where(cita => cita.HoraCita <= ahora)
                    .Select(cita => new CitaReporteJson()
                    {
                        Paciente = cita.Paciente.Nombre,
                        HoraCita = cita.HoraCita.Value,
                        Medico = cita.Medico.Nombre,
                        Costo = cita.Costo.Value

                    })
                    .ToList();
            }
        }

        public List<Inventario> ObtenerReporteDeActivos() {
            using (var db = new MedControlNetDBEntities())
            {

                return db.Inventarios.Include("Especialidad").Include("Consultorio").ToList();
                 
            }
        }

        public List<Consultorio> ObtenerListaEspecialidadesPorConsultorios()
        {
            using (var db = new MedControlNetDBEntities())
            {
                return db.Consultorios.Include("Especialidads").ToList();
            }
        }

        public int ObtenerConteoIngresos()
        {
            using (var db = new MedControlNetDBEntities())
            {
                return db.Citas.ToList().Count;
            }
        }

        public int ObtenerConteoDeEspecialidades()
        {
            using (var db = new MedControlNetDBEntities())
            {
                return db.Especialidads.Count();
            }
        }

        public decimal ObtenerPorcentajeEspecialidades()
        {

            decimal conteoEspecialidad = this.ObtenerConteoDeEspecialidades();

            using (var db = new MedControlNetDBEntities())
            {
                decimal especialidadesAtendidas = db.Citas.Select(citas => citas.Medico.Especialidad).Count();

                decimal result = (especialidadesAtendidas / conteoEspecialidad);

                return result * 100;

            }

        }

        public List<MedicoCitasReporte> ObtenerPorcentajeAtencionPorMedico()
        {
            using (var db = new MedControlNetDBEntities())
            {
                var medicos = db.Medicos.ToList();
                decimal conteoCitas = db.Citas.Count();

                var medicoCitas = new List<MedicoCitasReporte>();

                foreach (var medico in medicos)
                {
                    decimal citasAtendidas = medico.Citas.Count();
                    decimal result = (citasAtendidas / conteoCitas) * 100;

                    medicoCitas.Add(new MedicoCitasReporte
                    {
                        Medico = medico.Nombre,
                        PorcenajeAtencion = result

                    });
                }

                return medicoCitas;

            }

        }


        private string DarFormatoCita(Cita cita)
        {
            var paciente = cita.Paciente.Nombre;
            return $"<span class='fw-bold'>Paciente:</span> {paciente}<br/> <span class='fw-bold'>hora de cita:</span> {cita.HoraCita.Value.ToLocalTime()}<br/> <span class='fw-bold'> Medico:</span> {cita.Medico.Nombre}<br/>  <span class='fw-bold'>costo:</span> CRC{cita.Costo}";
        }
        private static string DarFormatoMedicoDeLaCita(Cita cita)
        {
            return string.Format("{0} - {1} ({2})", cita.Medico.Identificacion, cita.Medico.Nombre, cita.Medico.Especialidad.NombreEspecialidad);
        }
    }
}