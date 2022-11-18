using MedControlNet.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedControlNet.Models
{
    public class MedicoCitasReporte
    {
        public Medico Medico { set; get;}
        public decimal PorcenajeAtencion { set; get; }
    }
}