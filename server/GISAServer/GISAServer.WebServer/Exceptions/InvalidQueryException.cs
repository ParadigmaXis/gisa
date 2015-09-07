using System;
using System.Collections.Generic;
using System.Text;

namespace GISAServer.WebServer.Exceptions
{
    class InvalidQueryException : Exception
    {
        public InvalidQueryException()
            : base()
        {
        }

        public InvalidQueryException(string message)
            : base(message)
        {            
        }

        public InvalidQueryException(string message,Exception inner)
            : base(message, inner)
        {         
        }
    }
}
