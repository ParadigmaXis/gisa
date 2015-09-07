using System;
using System.Collections.Generic;
using System.Text;

namespace GISAServer.Hibernate.Exceptions
{
    public class InvalidIdException : Exception
    {
        public InvalidIdException()
            : base()
        {
        }

        public InvalidIdException(string message)
            : base(message)
        {
        }
    }
}
