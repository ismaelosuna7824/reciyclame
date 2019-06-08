using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecyclameV2.Clases
{
    public class ConnectionStringException : Exception
    {
        public ConnectionStringException()
            : this("La cadena de conexión está vacía.")
        {

        }

        public ConnectionStringException(string message)
            : base(message)
        {

        }
        public ConnectionStringException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
        protected ConnectionStringException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {

        }
    }
}
