using System;
using System.Runtime.Serialization;

namespace MedControlNet.Services
{
    [Serializable]
    internal class CitaExisteExcepcion : Exception
    {
        public CitaExisteExcepcion()
        {
        }

        public CitaExisteExcepcion(string message) : base(message)
        {
        }

        public CitaExisteExcepcion(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CitaExisteExcepcion(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}