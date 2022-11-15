using MedControlNet.Entities;
using MedControlNet.Exceptions;
using MedControlNet.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace MedControlNet.Services
{
    public class MedicosServicio
    {
        public List<MedicoModelo> ObtenerMedicos() {
            using (var entities = new MedControlNetDBEntities()) {


                return entities.Medicos.Select( medico => new MedicoModelo {
                    MedicoId = medico.MedicoID,
                    Identificacion = medico.Identificacion,
                    EspecialidadID = medico.Especialidad.EspecialidadID,
                    Nombre = medico.Nombre,
                    EspecialidadModel = new EspecialidadModel {
                        EspecialidadID = medico.Especialidad.EspecialidadID,
                        NombreEspecialidad = medico.Especialidad.NombreEspecialidad
                    }

                } ).ToList();
            }
        } 

        public MedicoModelo AgregarMedico(MedicoModelo medicoModelo)
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
                try
                {
                    entities.SaveChanges();
                }
                catch (DbUpdateException ex) {
                    if (ex.InnerException.InnerException.Message.Contains("UNIQUE KEY")) {
                        throw new MedicoExisteExcepcion($"El médico con cédula {nuevoMedico.Identificacion} ya existe", ex.InnerException.InnerException);
                    }
                }
               
            }


            using (var entities = new MedControlNetDBEntities())
            {
                var medico = entities.Medicos.Where((especialista) => especialista.Identificacion.Equals(especialista.Identificacion)).First();

                return new MedicoModelo
                {
                    MedicoId = medico.MedicoID,
                    Identificacion = medico.Identificacion,
                    EspecialidadID = medico.Especialidad.EspecialidadID,
                    Nombre = medico.Nombre,
                    EspecialidadModel = new EspecialidadModel
                    {
                        EspecialidadID = medico.Especialidad.EspecialidadID,
                        NombreEspecialidad = medico.Especialidad.NombreEspecialidad
                    }
                };

            }


        }
    }
}