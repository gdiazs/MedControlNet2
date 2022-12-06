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
        public void AgregarCita(CitaFormularioModelo modelo) {

            using (var db = new MedControlNetDBEntities()) {

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

                if (EstaDisponibleElEspacio(modelo)) {
                    db.Citas.Add(nuevaCita);
                    db.SaveChanges();
                }


            
            }
        }

        private bool EstaDisponibleElEspacio(CitaFormularioModelo modelo)
        {




            return true;
        }
    }
}