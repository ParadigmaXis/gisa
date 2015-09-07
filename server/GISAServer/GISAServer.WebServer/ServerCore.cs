using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Threading;
using System.Web;
using GISAServer.WebServer.Exceptions;
using GISAServer.Search;
using log4net;

using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip.Compression;


namespace GISAServer.WebServer
{
    enum UpdateType
    {
        NivelDocumental,
        NivelDocumentalInternet,
        NivelDocumentalComProdutores,
        UnidadeFisica,
        Produtor,
        Assunto,
        Tipologia
    }

    public class ServerCore
    {        
        // Logging initializations
        private static readonly ILog log = LogManager.GetLogger(typeof(ServerCore));

        private bool atomic;
        
        private QueueUpdater updates;

        // searchers
        private TipologiasSearcher tSearcher; // = new TipologiasSearcher("", "");
        private AssuntosSearcher aSearcher; // = new AssuntosSearcher("", "");
        private ProdutorSearcher pSearcher; // = new ProdutorSearcher("", "");
        private UnidadeFisicaSearcher ufSearcher; // = new UnidadeFisicaSearcher("", "");
        private NivelDocumentalSearcher ndSearcher; // = new NivelDocumentalSearcher("", "");
        private NivelDocumentalInternetSearcher ndiSearcher; // = new NivelDocumentalInternetSearcher("", "");
        private NivelDocumentalComProdutoresSearcher ndcpSearcher; // = new NivelDocumentalComProdutoresSearcher("", "");
        private Search.Cache.QueryCache ndCache = new Search.Cache.QueryCache();
        
        private Thread workerUnidadesFisicas;
        private Thread workerNiveisDocumentais;
        private Thread workerNiveisDocumentaisInternet;
        private Thread workerNiveisDocumentaisComProdutores;
        private Thread workerProdutores;
        private Thread workerAssuntos;
        private Thread workerTipologias;

        public ServerCore()
        {
            
            atomic = false;
                        
            try
            {
                // Debug messages            
                log.Debug("Initializing the update queue...");

                this.updates = new QueueUpdater(true);
                this.workerUnidadesFisicas = new Thread(this.updates.UpdateUnidadesFisicas);
                this.workerNiveisDocumentais = new Thread(this.updates.UpdateNiveisDocumentais);
                this.workerNiveisDocumentaisInternet = new Thread(this.updates.UpdateNiveisDocumentaisInternet);
                this.workerNiveisDocumentaisComProdutores = new Thread(this.updates.UpdateNiveisDocumentaisComProdutores);
                this.workerProdutores = new Thread(this.updates.UpdateProdutores);
                this.workerAssuntos = new Thread(this.updates.UpdateAssuntos);
                this.workerTipologias = new Thread(this.updates.UpdateTipologias);

                this.workerUnidadesFisicas.Start();
                this.workerNiveisDocumentais.Start();
                this.workerNiveisDocumentaisInternet.Start();
                this.workerNiveisDocumentaisComProdutores.Start();
                this.workerProdutores.Start();
                this.workerAssuntos.Start();
                this.workerTipologias.Start();

                // Debug messages           
                log.Debug("Update queue running!");
            }
            catch (Exception e)
            {
                log.Fatal("Error starting the update queue.", e);
                throw;
            }                        
                       
        }

        //TODO: document the exceptions
        public void ServerFunction(HttpListenerContext context)        
        {            
            HttpListenerRequest request = context.Request;
            HttpListenerResponse response = context.Response;

            if (!atomic)
            {
                // Debug messages
                if (log.IsDebugEnabled)
                {
                    log.Debug("Server free from atomic operations!");
                    log.Debug("Getting requested path...");
                }

                // Request analysis - only accept url equals to '/'
                string path = HttpUtility.UrlDecode(request.Url.AbsolutePath);
                if (!path.Equals("/"))
                {
                    sendResponse(response, "", HttpStatusCode.NotFound);
                    log.Error("Invalid url: " + path);
                }
                else
                {
                    string queryString = "";
                    string function = "";
                    string orderBy = "";
                    string defaultField = "";
                    string type = "";
                    string user = "";

                    // Debug messages
                    if (log.IsDebugEnabled)
                    {
                        log.Debug("Valid path request!");
                        log.Debug("Getting query parameters...");
                    }

                    Dictionary<string, object> parameters = GetQueryParams(request.Url.Query);

                    // Debug messages
                    if (log.IsDebugEnabled)
                    {
                        log.Debug("Parameters: " + request.Url.Query);
                        log.Debug("Catch every parameter...");
                    }

                    try
                    {
                        queryString = ReadValue(parameters, "q", "Query");
                        orderBy = ReadValue(parameters, "orderBy", "OrderBy");
                        defaultField = ReadValue(parameters, "defaultField", "defaultField");
                        function = ReadValue(parameters, "f", "Function");
                        if (function == null)
                        {
                            log.Error("Invalid function");
                            sendResponse(response, "Invalid function", HttpStatusCode.BadRequest);
                        }
                        user = ReadValue(parameters, "u", "User");
                        type = ReadValue(parameters, "t", "type");
                        if (user == "" && (type == "nivelDocumental" || type == ""))
                        {
                            log.Error("Invalid user");
                            sendResponse(response, "Invalid user", HttpStatusCode.BadRequest);
                        }
                    }
                    catch (Exception e)
                    {
                        log.Error("Error getting parameters", e);
                        sendResponse(response, "", HttpStatusCode.PreconditionFailed);
                        return;
                    }

                    switch (function)
                    {
                        case "search":
                            try
                            {
                                string responseString;
                                switch (type)
                                {
                                    case "tipologias":
                                        tSearcher = new TipologiasSearcher(orderBy, defaultField); //
                                        tSearcher.OrderBy = orderBy;
                                        responseString = tSearcher.Search(queryString);
                                        tSearcher.Close(); //
                                        break;                                                                  
                                    case "assuntos":
                                        aSearcher = new AssuntosSearcher(orderBy, defaultField); //
                                        aSearcher.OrderBy = orderBy;
                                        responseString = aSearcher.Search(queryString);
                                        aSearcher.Close(); //
                                        break;                                 
                                    case "produtor":
                                        pSearcher = new ProdutorSearcher(orderBy, defaultField); //
                                        pSearcher.OrderBy = orderBy;
                                        responseString = pSearcher.Search(queryString);
                                        pSearcher.Close(); //
                                        break;
                                    case "unidadeFisica":
                                        ufSearcher = new UnidadeFisicaSearcher(orderBy, defaultField); //
                                        ufSearcher.OrderBy = orderBy;
                                        responseString = ufSearcher.Search(queryString);
                                        ufSearcher.Close(); //
                                        break;
                                    case "nivelDocumental":
                                        ndSearcher = new NivelDocumentalSearcher(orderBy, defaultField, ndCache); //
                                        ndSearcher.OrderBy = orderBy;
                                        responseString = ndSearcher.Search(queryString, System.Convert.ToInt64(user));
                                        ndSearcher.Close(); //
                                        break;
                                    case "nivelDocumentalInternet":
                                        ndiSearcher = new NivelDocumentalInternetSearcher(orderBy, defaultField); //
                                        ndiSearcher.OrderBy = orderBy;
                                        responseString = ndiSearcher.Search(queryString);
                                        ndiSearcher.Close(); //
                                        break;
                                    case "nivelDocumentalComProdutores":
                                        ndcpSearcher = new NivelDocumentalComProdutoresSearcher(orderBy, defaultField); //
                                        ndcpSearcher.OrderBy = orderBy;
                                        responseString = ndcpSearcher.Search(queryString);
                                        ndcpSearcher.Close(); //
                                        break;
                                    default:
                                        throw new Exception(string.Format("Invalid search type: {0}", type));
                                }

                                sendResponse(response, responseString, HttpStatusCode.OK);
                            }                                     
                            catch (Exception e)
                            {
                                log.Error("Error searching for: " + queryString, e);
                                sendResponse(response, "Sintaxe inválida!", HttpStatusCode.BadRequest);
                            }
                            break;
                        case "update":
                            try
                            {

                                switch (type)
                                {
                                    case "tipologias":
                                        Update(request, UpdateType.Tipologia);
                                        break;                                    
                                    case "assuntos":
                                        Update(request, UpdateType.Assunto);
                                        break;                                    
                                    case "produtor":
                                        Update(request, UpdateType.Produtor);
                                        break;
                                    case "unidadeFisica":
                                        Update(request, UpdateType.UnidadeFisica);
                                        break;
                                    case "nivelDocumental":
                                    case "":
                                        Update(request, UpdateType.NivelDocumental);
                                        break;
                                    case "nivelDocumentalInternet":
                                        Update(request, UpdateType.NivelDocumentalInternet);
                                        break;
                                    case "nivelDocumentalComProdutores":
                                        Update(request, UpdateType.NivelDocumentalComProdutores);
                                        break;
                                    default:
                                        throw new Exception(string.Format("Invalid search type: {0}", type));

                                }


                                sendResponse(response, "", HttpStatusCode.OK);
                            }
                            // It's not an error it's a feature :)
                            catch (NoPostDataException e)
                            {
                                log.Error(e.Message, e);
                                sendResponse(response, "", HttpStatusCode.OK);
                            }
                            catch (Exception e)
                            {
                                log.Error("Error updating the index", e);
                                sendResponse(response, "", HttpStatusCode.InternalServerError);
                            }
                            break;
                        case "createIndex":
                            try
                            {
                                atomic = true;
                                sendResponse(response, "", HttpStatusCode.OK);

                                // Debug messages                                            
                                log.Debug("Creating all indexes...");

                                UnidadeFisicaUpdater ufUpdater = new UnidadeFisicaUpdater();
                                ufUpdater.Optimize();
                                NivelDocumentalUpdater ndUpdater = new NivelDocumentalUpdater();
                                ndUpdater.Optimize();

                                // Debug messages                                            
                                log.Debug("Indexes created.");
                            }
                            catch (Exception ex)
                            {
                                log.Error("Error creating the index", ex);
                            }
                            finally
                            {
                                atomic = false;
                            }
                            break;
                        case "ping":
                            try
                            {
                                sendResponse(response, "pong", HttpStatusCode.OK);
                                log.Debug("Ping response sent");
                            }
                            catch (Exception e)
                            {
                                log.Error("Ping response fail!", e);
                            }
                            break;
                        default:
                            try
                            {
                                log.Info("Invalid Function: " + function);
                                sendResponse(response, "", HttpStatusCode.NotImplemented);
                            }
                            catch (Exception e)
                            {
                                log.Error("Default command error.", e);
                            }
                            break;
                    }
                }
            }
            else
            {
                try
                {
                    log.Debug("Service call while in atomic state.");
                    sendResponse(response, "Atomic function", HttpStatusCode.ServiceUnavailable);
                }
                catch (Exception e)
                {
                    log.Error("Error sending 'atomic' information to client", e);
                }
            }            
        }

        private string ReadValue(Dictionary<string, object> parameters, string param, string paramTitle)
        {
            object value;
            string val = null;
            parameters.TryGetValue(param, out value);

            if (value is string)
                val = (string)value;

            // Debug messages
            if (log.IsDebugEnabled)
                log.Debug(paramTitle + ": " + val);

            return val;
        }

        private void Update(HttpListenerRequest request, UpdateType type)
        {
            if (!request.HasEntityBody)
            {
                throw new NoPostDataException("No post data");
            }

            // Debug messages
            if (log.IsDebugEnabled)
            {
                log.Debug("Updating ids...");
                log.Debug("Transforming the I/O stuff...");
            }

            System.IO.Stream body = request.InputStream;
            System.Text.Encoding encoding = request.ContentEncoding;
            System.IO.StreamReader reader = new System.IO.StreamReader(body, encoding);
            string ids = reader.ReadToEnd();

            // Debug messages
            if (log.IsDebugEnabled)
            {
                log.Debug("IO proccesing done!");
                log.Debug("IDs to update: " + ids);
                log.Debug("Spliting ids...");
            }
            
            string[] idList = ids.Split(' '); // The clients MUST send ids with a space separator

            // Debug messages
            if (log.IsDebugEnabled)
            {
                log.Debug("Ids splited!");
                log.Debug("IDs to update: " + idList.Length);
                log.Debug("Locking 'updates' and adding ids...");
            }

            System.Collections.Queue queue = null;
            EventWaitHandle ewh = null;
            switch (type)
            {
                case UpdateType.Tipologia:
                    queue = this.updates.Tipologias;
                    ewh = this.updates.WHTipologias;
                    break;
                case UpdateType.Assunto:
                    queue = this.updates.Assuntos;
                    ewh = this.updates.WHAssuntos;
                    break;
                case UpdateType.Produtor:
                    queue = this.updates.Produtores;
                    ewh = this.updates.WHProdutores;
                    break;
                case UpdateType.NivelDocumental:
                    queue = this.updates.NiveisDocumentais;
                    ewh = this.updates.WHNiveisDocumentais;
                    break;
                case UpdateType.NivelDocumentalInternet:
                    queue = this.updates.NiveisDocumentaisInternet;
                    ewh = this.updates.WHNiveisDocumentaisInternet;
                    break;
                case UpdateType.NivelDocumentalComProdutores:
                    queue = this.updates.NiveisDocumentaisComProdutores;
                    ewh = this.updates.WHNiveisDocumentaisComProdutores;
                    break;
                case UpdateType.UnidadeFisica:
                    queue = this.updates.UnidadesFisicas;
                    ewh = this.updates.WHUnidadesFisicas;
                    break;
            }

            foreach (string idS in idList)
            {
                long id = -1;
                long.TryParse(idS, out id);
                if (id != -1 && !queue.Contains(id))
                {
                    queue.Enqueue(id);
                }
            }
            ewh.Set();

                                  
            // Debug messages
            if (log.IsDebugEnabled)
            {
                log.Debug("IDs added!");                
                log.Debug("Thread sinalized!");
            }            
        }

        private void sendResponse(HttpListenerResponse response,string responseString, HttpStatusCode status)
        {
            try
            {
                response.ContentEncoding = System.Text.Encoding.UTF8;
                response.ContentType = "text/plain; charset=utf-8";
                response.StatusCode = (int)status;
                // Construct a response.            
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                // Get a response stream and write the response to it.
                response.ContentLength64 = buffer.Length;
                System.IO.Stream output = response.OutputStream;

                output.Write(buffer, 0, buffer.Length);
                output.Close();
            }
            catch (System.Net.HttpListenerException ex)
            { 
                log.Error("The client closed the connection", ex);
            }
            catch (Exception ex)
            {
                log.Error("Error sending response to client", ex);
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
            }
        }

        private Dictionary<string, object> GetQueryParams(string query)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            NameValueCollection nvc = HttpUtility.ParseQueryString(query);
            foreach (string key in nvc.Keys)
            {
                switch (key)
                {
                    case "@expand":
                        List<string> expd = new List<string>();
                        foreach (string path in nvc[key].Split(' '))
                            if (path != string.Empty)
                                expd.Add(path);
                        result.Add(key, expd);
                        break;
                    default: result.Add(key, nvc[key]); break;
                }
            }

            return result;
        }
     
        public void StopListening()
        {
            this.updates.IsWorking = false;
            this.updates.WHNiveisDocumentais.Set();
            this.updates.WHUnidadesFisicas.Set();
            this.workerNiveisDocumentais.Join();
            this.workerUnidadesFisicas.Join();
        }                 
    }
}
