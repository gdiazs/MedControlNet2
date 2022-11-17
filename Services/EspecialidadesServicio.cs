using MedControlNet.Entities;
using MedControlNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedControlNet.Services
{
    public class EspecialidadesServicio
    {

        public List<EspecialidadModel> ObtenerEspecialidades()
        {

            using (var entities = new MedControlNetDBEntities())
            {
                return entities.Especialidads.Select( especialidad => new EspecialidadModel {
                    EspecialidadID = especialidad.EspecialidadID,
                    NombreEspecialidad = especialidad.NombreEspecialidad
                
                }).ToList();
            }
        }

        public Especialidad ObtenerEspecialidadPorId(int id)
        {

            using (var entities = new MedControlNetDBEntities())
            {
                return entities.Especialidads.Include("Consultorios").FirstOrDefault( especialidad => especialidad.EspecialidadID.Equals(id) );
            }
        }
    }
}