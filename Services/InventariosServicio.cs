using MedControlNet.Entities;
using MedControlNet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedControlNet.Services
{
    public class InventariosServicio
    {
        public List<InventarioModelo> ObtenerEquiposMedicos()
        {
            using (var db = new MedControlNetDBEntities())
            {
                return db.Inventarios.ToList().Select(objeto => new InventarioModelo()
                {
                    InventarioID = objeto.InventarioID,
                    Consultorio = objeto.Consultorio.NumeroConsultorio + "",
                    Descripcion = objeto.Descripcion,
                    Especialidad = objeto.Especialidad.NombreEspecialidad,
                    FechaDeCompra = objeto.FechaDeCompra.Value.ToString("yyyy-MM-dd"),
                    Nombre = objeto.Nombre,
                    NumeroActivo = objeto.NumeroActivo.Value,
                    Serie = objeto.Serie,
                    Consultorios = db.Consultorios.ToList(),
                    Especialidades = db.Especialidads.ToList()

                }).ToList();
            }
        }

        public InventarioModelo ObtenerEquipoMedicoPorId(int id)
        {
            using (var db = new MedControlNetDBEntities())
            {
                var objeto = db.Inventarios.Find(id);
                return new InventarioModelo()
                {
                    InventarioID = objeto.InventarioID,
                    Consultorio = objeto.Consultorio.NumeroConsultorio + "",
                    Descripcion = objeto.Descripcion,
                    Especialidad = objeto.Especialidad.NombreEspecialidad,
                    FechaDeCompra = objeto.FechaDeCompra.Value.ToString("yyyy-MM-dd"),
                    Nombre = objeto.Nombre,
                    NumeroActivo = objeto.NumeroActivo.Value,
                    Serie = objeto.Serie,
                    Consultorios = db.Consultorios.ToList(),
                    Especialidades = db.Especialidads.ToList()

                };
            }
        }

        public InventarioModelo GenerarNuevoInventarioModelo()
        {
            using (var db = new MedControlNetDBEntities())
            {
                return new InventarioModelo()
                {
                    Consultorios = db.Consultorios.ToList(),
                    Especialidades = db.Especialidads.ToList()

                };
            }
        }

        public void GuardarObjeto(InventarioModelo inventarioModelo)
        {

            using (var db = new MedControlNetDBEntities())
            {
                var nuevoObjeto = new Inventario()
                {
                    NumeroActivo = inventarioModelo.NumeroActivo,
                    ConsultorioID = int.Parse(inventarioModelo.Consultorio),
                    Descripcion = inventarioModelo.Descripcion,
                    FechaDeCompra = DateTime.Parse(inventarioModelo.FechaDeCompra),
                    Nombre = inventarioModelo.Nombre,
                    Serie = inventarioModelo.Serie,
                    EspecialidadID = int.Parse(inventarioModelo.Especialidad)
                };

                db.Inventarios.Add(nuevoObjeto);
                db.SaveChanges();
            }

        }

        public void ActualizarObjeto(InventarioModelo inventarioModelo)
        {

            using (var db = new MedControlNetDBEntities())
            {
                var objetoEncontado = db.Inventarios.Find(inventarioModelo.InventarioID);
                objetoEncontado.NumeroActivo = inventarioModelo.NumeroActivo;
                objetoEncontado.ConsultorioID = int.Parse(inventarioModelo.Consultorio);
                objetoEncontado.Descripcion = inventarioModelo.Descripcion;
                objetoEncontado.FechaDeCompra = DateTime.Parse(inventarioModelo.FechaDeCompra);
                objetoEncontado.Nombre = inventarioModelo.Nombre;
                objetoEncontado.Serie = inventarioModelo.Serie;
                objetoEncontado.EspecialidadID = int.Parse(inventarioModelo.Especialidad);
                db.SaveChanges();
            }

        }

        public void BorrarObjeto(int id)
        {

            using (var db = new MedControlNetDBEntities()) {

                var objetoEncontrado = db.Inventarios.Find(id);
                db.Inventarios.Remove(objetoEncontrado);
                db.SaveChanges();
            }
        }
    }
}