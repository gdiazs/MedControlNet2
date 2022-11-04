using MedControlNet.Entities;
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
    }
}