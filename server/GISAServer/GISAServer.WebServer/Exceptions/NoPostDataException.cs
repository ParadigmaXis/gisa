using System;
using System.Collections.Generic;
using System.Text;

namespace GISAServer.WebServer.Exceptions
{
    class NoPostDataException : Exception
    {
        public NoPostDataException()
            : base()
        {
        }

        public NoPostDataException(string message)
            : base(message)
        {            
        }

        public NoPostDataException(string message, Exception inner)
            : base(message, inner)
        {         
        }
    }
}
