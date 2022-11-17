using MedControlNet.Entities;
using MedControlNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MedControlNet.Services
{
    public class ConsultoriosServicio
    {

        public ConsultorioModelo ObtenerConsutultorioPorId(int? consultorioId) 
        {

            using (var db = new MedControlNetDBEntities()) 
            {

                var consultorio = db.Consultorios.Find(consultorioId);


                var listaEspecialidades = consultorio.Especialidads.Select(especialidad => especialidad.EspecialidadID).ToList();

                var especialidades = db.Especialidads
                    .Where(especialidad => !listaEspecialidades.Contains(especialidad.EspecialidadID)).ToList();

                var items = especialidades
                    .Select(especialidad => new SelectListItem() { Value = especialidad.EspecialidadID + "", Text = especialidad.NombreEspecialidad })
                    .ToList();


                var consultorioModelo = new ConsultorioModelo()
                {
                    ConsultorioID = consultorio.ConsultorioID,
                    NumeroConsultorio = consultorio.NumeroConsultorio.Value,
                    EspecialidadesConsultorio = consultorio.Especialidads,
                    TodasLasEspecialidades = items

                };
                return consultorioModelo;
            }

        }

        public Consultorio ActualizarConsultorio(ConsultorioModelo consultorioModelo) 
        {
            using (var db = new MedControlNetDBEntities())
            {
                var consultorio = db.Consultorios.Find(consultorioModelo.ConsultorioID);
                consultorio.NumeroConsultorio = consultorioModelo.NumeroConsultorio;
                db.SaveChanges();
                return consultorio;
            }

        }

        public void AgregarEspecialidad(ConsultorioModelo consultorioModelo) 
        {
            using (var db = new MedControlNetDBEntities()) {

                var consultorio = db.Consultorios.Find(consultorioModelo.ConsultorioID);
                var especialidad = db.Especialidads.Find(consultorioModelo.NuevaEspecialidad);
                consultorio.Especialidads.Add(especialidad);

                db.SaveChanges();
            }
        }

        public void RemoverEspecialidad(int? especialidadId, int? consultorioId) 
        {
            using (var db = new MedControlNetDBEntities())
            {
                var consultorio = db.Consultorios.Find(consultorioId);
                var especialidad = db.Especialidads.Find(especialidadId);

                consultorio.Especialidads.Remove(especialidad);

                db.SaveChanges();
            }

        }

        public List<Consultorio> ObtenerConsultorios()
        {
            using (var db = new MedControlNetDBEntities()) {
                return db.Consultorios.ToList();
            }
        }
    }
}