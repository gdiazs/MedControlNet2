using MedControlNet.Entities;
using MedControlNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedControlNet.Services
{
    public class CitasServicio
    {
        public void AgregarCita(CitaFormularioModelo modelo)
        {

            using (var db = new MedControlNetDBEntities())
            {

                //var pacienteGuardado = db.Pacientes.Where(p => p.Identificacion.Equals(modelo.CedulaPaciente));

                var nuevoPaciente = new Paciente()
                {
                    Identificacion = modelo.CedulaPaciente,
                    Nombre = modelo.NombrePaciente
                };


                var nuevaCita = new Cita()
                {
                    Costo = modelo.Monto,
                    HoraCita = modelo.FechaCita,
                    MedicoID = int.Parse(modelo.MedicoEspecialista),
                    Paciente = nuevoPaciente,
                    ConsultorioID = int.Parse(modelo.ConsultorioId)
                };

                if (EstaDisponibleElEspacio(modelo))
                {
                    db.Citas.Add(nuevaCita);
                    db.SaveChanges();
                }
                else
                {

                    throw new CitaExisteExcepcion($"El consultorio {modelo.ConsultorioId} ya se encuentra ocupado en esa fecha");
                }



            }
        }

        private bool EstaDisponibleElEspacio(CitaFormularioModelo modelo)
        {
            var fechaDeNuevaCita = modelo.FechaCita.ToString("yyyy-MM-dd");


            using (var db = new MedControlNetDBEntities())
            {
                int consultorioId = int.Parse(modelo.ConsultorioId);
                var citasAgendadas = db.Citas
                    .Where(cita => cita.ConsultorioID == consultorioId).ToList().Where(cita => cita.HoraCita.Value.Date == modelo.FechaCita.Date).ToList();

                var citasMismaHora = citasAgendadas.Where( cita => cita.HoraCita.Value.Hour == modelo.FechaCita.Hour).ToList();

                if (citasMismaHora.Any()) {
                    return false;
                }


            }

            return true;
        }
    }
}