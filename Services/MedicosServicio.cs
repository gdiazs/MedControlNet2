using MedControlNet.Entities;
using MedControlNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedControlNet.Services
{
    public class MedicosServicio
    {
        public List<Medico> ObtenerMedicos() {
            using (var entities = new MedControlNetDBEntities()) { 
                return entities.Medicos.ToList();
            }
        } 

        public void AgregarMedico(MedicoModelo medicoModelo)
        {

            var nuevoMedico = new Medico 
            { 
                Identificacion = medicoModelo.Identificacion,
                EspecialidadID = medicoModelo.EspecialidadID,   
                Nombre = medicoModelo.Nombre,
                EsTemporal = medicoModelo.EsTemporal,
            };

            using (var entities = new MedControlNetDBEntities())
            {
                entities.Medicos.Add(nuevoMedico);
                entities.SaveChanges();
            }
        }
    }
}