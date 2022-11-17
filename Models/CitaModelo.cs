using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MedControlNet.Models
{
    public class CitaModelo
    {
        public IEnumerable<SelectListItem> medicos { set; get; }
        public IEnumerable<SelectListItem> consultorios { set; get; }
    }
}