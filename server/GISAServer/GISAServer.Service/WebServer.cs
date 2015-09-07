using System;
using System.Diagnostics;
using System.ServiceProcess;

using log4net;

using GISAServer.WebServer;
using GISAServer.WebServer.Exceptions;

namespace GISAServer.Service
{
    /// <summary>
    /// Describes the service that encapsulates
    /// the GISA Server.
    /// 
    /// Tries to start the server giving one 
    /// try to exceptions, in the second just
    /// stops.
    /// </summary>
    public partial class Service : ServiceBase
    {
        // Logging initializations
        private static readonly ILog log = LogManager.GetLogger(typeof(Service));

        private Server server;

        /// <summary>
        /// Initializes the service class.
        /// </summary>
        /// <exception cref="ServerInitializationException"/>
        public Service()
        {
            InitializeComponent();

            try
            {
                this.server = new Server();
            }
            catch (ServerInitializationException e)
            {
                log.Error(e.Message, e);

                // Debug messages
                log.Debug("Error initializing Server class, trying again...", e);

                try
                {
                    this.server = new Server();
                }
                catch (ServerInitializationException ex)
                {
                    log.Fatal(ex.Message, ex);
                    throw ex;
                }
            }
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                this.server.StartListening();
            }
            catch (ServerInitializationException e)
            {
                log.Error(e.Message, e);
                
                // Debug messages
                log.Debug("Error starting listener, trying again...", e);

                try
                {
                    server.StartListening();
                }
                catch (ServerInitializationException ex)
                {
                    log.Fatal(ex.Message, ex);
                    throw ex;
                }
            }
        }

        protected override void OnStop()
        {
            server.StopListening();
        }

#if DEBUG
        static void Main()
        {
            Service service = new Service();
            service.OnStart(null);
            Console.ReadKey();
            service.OnStop();
        }
#else
        static void Main()
        {
            try
            {
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

                ServiceBase[] servicesToRun
                    = new ServiceBase[] 
			{ 
				new Service() 
			};

                ServiceBase.Run(servicesToRun);
            }
            catch (Exception e)
            {
                log.Error("Error in the service initialization!", e);
                string SourceName = "WindowsService.ExceptionLog";
                if (!EventLog.SourceExists(SourceName))
                {
                    EventLog.CreateEventSource(SourceName, "Application");
                }
 
                EventLog eventLog = new EventLog();
                eventLog.Source = SourceName;
                string message = string.Format("Exception: {0} \n\nStack: {1}", e.Message, e.StackTrace);
                eventLog.WriteEntry(message, EventLogEntryType.Error);
            }
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            log.Fatal(e.ToString());
        }
#endif
    }
}
