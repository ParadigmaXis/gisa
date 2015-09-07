using System;

namespace GISAServer.WebServer.Exceptions
{
    /// <summary>
    /// Raised when an initialization step fails.
    /// </summary>
    public class ServerInitializationException : Exception
    {
        public ServerInitializationException()
            : base()
        { }

        public ServerInitializationException(string message)
            : base(message)
        { }

        public ServerInitializationException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
