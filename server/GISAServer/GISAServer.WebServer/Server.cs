using System;

using System.Configuration;

using System.Net;
using System.Threading;
using System.Security;

using log4net;

using GISAServer.WebServer.Exceptions;

namespace GISAServer.WebServer
{
    public class Server
    {
        // Logging initializations
        private static readonly ILog log = LogManager.GetLogger(typeof(Server));

        /// <summary>
        /// The default prefix for listen on.
        /// </summary>
        public static string DEFAUL_TPREFIX = "http://+:8888/";

        /// <summary>
        /// The max number of active threads.
        /// </summary>
        public static int MAX_THREADS = 20;

        private Thread listeningThread;
        private int workingThreads;
        private AutoResetEvent waitHandle;
        private object locker;

        private AutoResetEvent activeThreadsWaitHandle;

        private HttpListener listener;
        private ServerCore serverCore;

        /// <summary>
        /// Server class initialization.
        /// </summary>
        /// <exception cref="ServerInitializationException"/>        
        public Server()
        {
            // Debug messages
            log.Debug("Initializing server...");

            // Debug messages                            
            log.Debug("Getting requested path...");
            if (ConfigurationManager.AppSettings.HasKeys())
            {
                Server.DEFAUL_TPREFIX = ConfigurationManager.AppSettings["Listener Prefix"];
            }
            else 
            {
                log.Warn("Error getting app settings for listen prfixes. Using default: " + Server.DEFAUL_TPREFIX);
            }

            try
            {
                this.listeningThread = new Thread(Listen);
                this.listener = new HttpListener();
                this.serverCore = new ServerCore();

                this.workingThreads = 0;
                this.waitHandle = new AutoResetEvent(false);
                this.locker = new object();

                this.activeThreadsWaitHandle = new AutoResetEvent(false);

                // Debug messages
                log.Debug("Server initialized!");
            }
            catch (Exception e)
            {
                log.Fatal("Server class initialization failed!", e);
                throw new ServerInitializationException("Server class initialization failed", e);
            }          
        }                
                
        /// <summary>
        /// Start the listener server in a 
        /// separated thread.
        /// </summary>
        /// <exception cref="ServerInitializationException">
        /// If some of the initialization step fails.
        /// </exception>
        public void StartListening()
        {
            // Debug messages
            log.Debug("Starting listeneres...");

            try
            {
                // Initialize HttpListener
                this.listener.Prefixes.Add(Server.DEFAUL_TPREFIX);
                this.listener.Start();

                // Debug messages            
                if (log.IsDebugEnabled)
                {
                    foreach (string prefix in listener.Prefixes)
                    {
                        log.DebugFormat("HttpListener started listening on: {0}!", prefix);
                    }
                }

                // Debug messages
                log.Debug("Starting listening thread...");

                // Initialize the listener thread            
                this.listeningThread.Start();

                // Debug messages
                log.Debug("Listening thread started...");
            }
            catch (ArgumentNullException e)
            {
                log.Fatal("Error adding prefixes!", e);
                throw new ServerInitializationException("Error adding prefixes", e);
            }
            catch (ArgumentException e)
            {
                log.Fatal("Error adding prefixes!", e);
                throw new ServerInitializationException("Error adding prefixes", e);
            }
            catch (ObjectDisposedException e)
            {
                log.Fatal("Error adding prefixes or starting listening!", e);
                throw new ServerInitializationException("Error adding prefixes", e);
            }
            catch (HttpListenerException e)
            {
                log.Fatal("Error adding prefixes or starting listening!", e);
                throw new ServerInitializationException("Error adding prefixes or starting listening", e);
            }
            catch (Exception e)
            {
                log.Fatal("Error starting the listening thread!", e);
                throw new ServerInitializationException("Error starting the listening thread", e);
            }
        }

        /// <summary>
        /// Stops the server activity.
        /// </summary>
        public void StopListening()
        {
            // Debug messages
            log.Debug("Starting the shutdown sequence...");

            try
            {
                Monitor.Enter(this.locker);
                while (this.workingThreads > 0)
                {
                    Monitor.Exit(this.locker);
                    this.waitHandle.WaitOne();
                    Monitor.Enter(this.locker);
                }
                Monitor.Exit(locker);
            }
            catch (ArgumentNullException e)
            {
                log.Error("Error getting the lock, the server will stop abruptly!", e);
            }
            catch (Exception e)
            {
                log.Error("Error waiting for thread, the server will stop abruptly!", e);
            }
            finally
            {
                this.serverCore.StopListening();
                
                //Stop listener thread 
                try
                {
                    this.listeningThread.Abort();
                }
                catch (SecurityException ex)
                {
                    log.Error("Error stoping the listener thread!", ex);
                }
                catch (ThreadStateException ex)
                {
                    log.Error("Error stoping the listener thread!", ex);
                }
                finally
                {
                    // Stop the HttpListener                        
                    this.listener.Stop();

                    // Debug messages
                    log.Debug("Shutdown sequence completed!");
                }
            }
        }

        private void Listen()
        {
            // Debug messages
            log.Debug("Starting listening...");

            int activeThreads = 0;
            while (activeThreads < Server.MAX_THREADS)
            {
                try
                {
                    ThreadPool.QueueUserWorkItem(new WaitCallback(ServerFunction));
                    activeThreads++;
                    if (activeThreads == Server.MAX_THREADS)
                    {
                        activeThreads--;
                        this.activeThreadsWaitHandle.WaitOne();
                    }
                }
                catch (ApplicationException e)
                {
                    log.Error("Error getting threads from pool!", e);
                }
                catch (OutOfMemoryException e)
                {
                    log.Error("Error getting threads from pool!", e);
                }
                catch (ArgumentNullException e)
                {
                    log.Error("Error getting threads from pool - Server unstable!", e);
                }
                catch (ObjectDisposedException e)
                {
                    log.Fatal("Impossible to wait for thread - Server unstable!", e);
                }
                catch (AbandonedMutexException e)
                {
                    log.Fatal("Impossible to wait for thread - Server unstable!", e);
                }
                catch (ThreadAbortException)
                { 
                    // Thread abort forced to stop the server
                }
                catch (Exception e)
                {
                    log.Fatal("Unhandle exception - Server unstable!", e);
                }
            }

            // Debug messages
            log.Debug("Listening ended!");

        }
        
        private void ServerFunction(object state)
        {
            // Debug messages
            log.Debug("Starting getting the context...");

            try
            {
                HttpListenerContext context = this.listener.GetContext();

                lock (locker)
                {
                    this.workingThreads++;                    
                }

                this.serverCore.ServerFunction(context); // TODO: take it out from locker

                lock (locker)
                {
                    this.workingThreads--;
                    if (this.workingThreads == 0)
                    {
                        this.waitHandle.Set();
                    }
                }

                this.activeThreadsWaitHandle.Set();

            }
            catch (HttpListenerException e)
            {
                // Debug messages
                log.Debug("Ended getting the context: ", e);
            }
            catch (InvalidOperationException e)
            {
                log.Error("Error getting context!", e);
            }            
            catch (Exception e)
            {
                log.Error("Error in the server function!", e);
            }
            finally
            {
                // Debug messages
                log.Debug("Ended getting the context!");
            }
        }        
    }
}
