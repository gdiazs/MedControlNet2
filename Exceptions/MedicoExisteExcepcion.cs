using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MedControlNet.Exceptions
{
    public class MedicoExisteExcepcion : Exception
    {



        public MedicoExisteExcepcion(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}