using System;
using System.Collections.Generic;
using System.Text;

namespace GISAServer.WebServer.Exceptions
{
    class InvalidFunctionException : Exception
    {
        public InvalidFunctionException() 
            : base()
        {
        }

        public InvalidFunctionException(string message)
            : base(message)
        {                       
        }

        public InvalidFunctionException(string message,Exception inner)
            : base(message,inner)
        {
        }

    }
}
