using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecyclameV2.Clases
{
    public class LoginException : Exception
    {
        public LoginException()
        {

        }
        public LoginException(string message)
            : base(message)
        {

        }
        public LoginException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
        protected LoginException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {

        }
    }
}
