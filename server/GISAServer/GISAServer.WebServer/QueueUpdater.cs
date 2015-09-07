using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using GISAServer.Search;

using log4net;

namespace GISAServer.WebServer
{
    
    class QueueUpdater
    {
        // Logging initializations
        private static readonly ILog log = LogManager.GetLogger(typeof(QueueUpdater));

        private bool isWorking;

        private EventWaitHandle whUnidadesFisicas = new AutoResetEvent(false);
        private EventWaitHandle whNiveisDocumentais = new AutoResetEvent(false);
        private EventWaitHandle whNiveisDocumentaisInternet = new AutoResetEvent(false);
        private EventWaitHandle whNiveisDocumentaisComProdutores = new AutoResetEvent(false);
        private EventWaitHandle whProdutores = new AutoResetEvent(false);
        private EventWaitHandle whAssuntos = new AutoResetEvent(false);
        private EventWaitHandle whTipologias = new AutoResetEvent(false);

        private Queue niveisDocumentais;
        private Queue niveisDocumentaisInternet;
        private Queue niveisDocumentaisComProdutores;
        private Queue unidadesFisicas;
        private Queue produtores;
        private Queue assuntos;
        private Queue tipologias;

        private NivelDocumentalUpdater nivelDocumentalUpdater;
        private NivelDocumentalInternetUpdater nivelDocumentalInternetUpdater;
        private NivelDocumentalComProdutoresUpdater nivelDocumentalComProdutoresUpdater;
        private UnidadeFisicaUpdater unidadeFisicaUpdater;
        private ProdutorUpdater produtorUpdater;
        private AssuntosUpdater assuntosUpdater;
        private TipologiasUpdater tipologiasUpdater;

        public bool IsWorking
        {
            get { return isWorking; }
            set { this.isWorking = value; }
        }

        public EventWaitHandle WHUnidadesFisicas
        {
            get { return this.whUnidadesFisicas; }
        }

        public EventWaitHandle WHNiveisDocumentais
        {
            get { return this.whNiveisDocumentais; }
        }

        public EventWaitHandle WHNiveisDocumentaisInternet
        {
            get { return this.whNiveisDocumentaisInternet; }
        }

        public EventWaitHandle WHNiveisDocumentaisComProdutores
        {
            get { return this.whNiveisDocumentaisComProdutores; }
        }

        public EventWaitHandle WHProdutores
        {
            get { return this.whProdutores; }
        }

        public EventWaitHandle WHAssuntos
        {
            get { return this.whAssuntos; }
        }

        public EventWaitHandle WHTipologias
        {
            get { return this.whTipologias; }
        }

        public Queue NiveisDocumentais
        {
            get { return this.niveisDocumentais; }
        }

        public Queue NiveisDocumentaisInternet
        {
            get { return this.niveisDocumentaisInternet; }
        }

        public Queue NiveisDocumentaisComProdutores
        {
            get { return this.niveisDocumentaisComProdutores; }
        }

        public Queue UnidadesFisicas
        {
            get { return this.unidadesFisicas; }
        }

        public Queue Produtores
        {
            get { return this.produtores; }
        }

        public Queue Assuntos
        {
            get { return this.assuntos; }
        }

        public Queue Tipologias
        {
            get { return this.tipologias; }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="isWorking"></param>
        public QueueUpdater(bool isWorking)
        {
            this.isWorking = isWorking;

            this.niveisDocumentais = Queue.Synchronized(new Queue());
            this.niveisDocumentaisInternet = Queue.Synchronized(new Queue());
            this.niveisDocumentaisComProdutores = Queue.Synchronized(new Queue());
            this.unidadesFisicas = Queue.Synchronized(new Queue());
            this.produtores = Queue.Synchronized(new Queue());
            this.assuntos = Queue.Synchronized(new Queue());
            this.tipologias = Queue.Synchronized(new Queue());
        }

        public void UpdateNiveisDocumentais()
        {
            if (this.nivelDocumentalUpdater == null)
            {
                this.nivelDocumentalUpdater = new NivelDocumentalUpdater();
            }
            while (this.isWorking)
            {
                if (this.niveisDocumentais.Count > 0)
                {
                    try
                    {
                        var ids = this.niveisDocumentais.Cast<long>().ToList();
                        var idsStr = new StringBuilder();
                        ids.ForEach(id => idsStr.Append(id.ToString() + " "));
                        this.niveisDocumentais.Clear();
                        this.nivelDocumentalUpdater.Update(ids);
                        log.DebugFormat("NivelDocumental IDs updated: {0}", idsStr.ToString());
                    }
                    catch (InvalidOperationException e)
                    {
                        log.Error("Dequeueing an empty queue!", e);
                    }
                    catch (Exception e)
                    {
                        log.Error("Dequeueing an empty queue!", e);
                    }
                }
                else if (this.niveisDocumentais.Count == 0 && this.isWorking)
                {
                    this.whNiveisDocumentais.WaitOne();
                }                
            }
            this.nivelDocumentalUpdater.Optimize();
        }

        public void UpdateNiveisDocumentaisInternet()
        {
            if (this.nivelDocumentalInternetUpdater == null)
            {
                this.nivelDocumentalInternetUpdater = new NivelDocumentalInternetUpdater();
            }
            while (this.isWorking)
            {
                if (this.niveisDocumentaisInternet.Count > 0)
                {
                    try
                    {
                        var ids = this.niveisDocumentaisInternet.Cast<long>().Select(id => id.ToString()).ToList();
                        var idsStr = new StringBuilder();
                        ids.ForEach(id => idsStr.Append(id.ToString() + " "));
                        this.niveisDocumentaisInternet.Clear();
                        this.nivelDocumentalInternetUpdater.MyUpdate(ids);
                        log.DebugFormat("NivelDocumentalInternet IDs updated: {0}", idsStr.ToString());
                    }
                    catch (InvalidOperationException e)
                    {
                        log.Error("Dequeueing an empty queue!", e);
                    }
                    catch (Exception e)
                    {
                        log.Error("Dequeueing an empty queue!", e);
                    }
                }
                else if (this.niveisDocumentaisInternet.Count == 0 && this.isWorking)
                {
                    this.whNiveisDocumentaisInternet.WaitOne();
                }
            }
            this.nivelDocumentalInternetUpdater.Optimize();
        }

        public void UpdateNiveisDocumentaisComProdutores()
        {
            if (this.nivelDocumentalComProdutoresUpdater == null)
            {
                this.nivelDocumentalComProdutoresUpdater = new NivelDocumentalComProdutoresUpdater();
            }
            while (this.isWorking)
            {
                if (this.niveisDocumentaisComProdutores.Count > 0)
                {
                    try
                    {
                        long id = (long)this.niveisDocumentaisComProdutores.Dequeue();
                        this.nivelDocumentalComProdutoresUpdater.UpdateManyByProdutor(id);
                        log.DebugFormat("NivelDocumentalComProdutores with ProdutorID {0} updated!", id);

                    }
                    catch (InvalidOperationException e)
                    {
                        log.Error("Dequeueing an empty queue!", e);
                    }
                    catch (Exception e)
                    {
                        log.Error("Dequeueing an empty queue!", e);
                    }
                }
                else if (this.niveisDocumentaisComProdutores.Count == 0 && this.isWorking)
                {
                    this.whNiveisDocumentaisComProdutores.WaitOne();
                }
            }
            this.nivelDocumentalComProdutoresUpdater.Optimize();
        }

        public void UpdateUnidadesFisicas()
        {
            if (this.unidadeFisicaUpdater == null)
            {
                this.unidadeFisicaUpdater = new UnidadeFisicaUpdater();
            }
            while (this.isWorking)
            {
                if (this.unidadesFisicas.Count > 0)
                {
                    try
                    {
                        long id = (long)this.unidadesFisicas.Dequeue();
                        this.unidadeFisicaUpdater.Update(id);
                        log.DebugFormat("Unidade Física with ID {0} updated!", id);
                    }
                    catch (InvalidOperationException e)
                    {
                        log.Error("Dequeueing an empty queue!", e);
                    }
                    catch (Exception e)
                    {
                        log.Error("Dequeueing an empty queue!", e);
                    }
                }
                else if (this.unidadesFisicas.Count == 0 && this.isWorking)
                {
                    this.whUnidadesFisicas.WaitOne();
                }
            }
            this.unidadeFisicaUpdater.Optimize();           
        }

        public void UpdateProdutores()
        {
            if (this.produtorUpdater == null)
            {
                this.produtorUpdater = new ProdutorUpdater();
            }
            while (this.isWorking)
            {
                if (this.produtores.Count > 0)
                {
                    try
                    {
                        long id = (long)this.produtores.Dequeue();
                        this.produtorUpdater.Update(id);
                        log.DebugFormat("Produtor with ID {0} updated!", id);
                    }
                    catch (InvalidOperationException e)
                    {
                        log.Error("Dequeueing an empty queue!", e);
                    }
                    catch (Exception e)
                    {
                        log.Error("Dequeueing an empty queue!", e);
                    }
                }
                else if (this.produtores.Count == 0 && this.isWorking)
                {
                    this.whProdutores.WaitOne();
                }
            }
            this.produtorUpdater.Optimize();
        }

        public void UpdateAssuntos()
        {
            if (this.assuntosUpdater == null)
            {
                this.assuntosUpdater = new AssuntosUpdater();
            }
            while (this.isWorking)
            {
                if (this.assuntos.Count > 0)
                {
                    try
                    {
                        long id = (long)this.assuntos.Dequeue();
                        this.assuntosUpdater.Update(id);
                        log.DebugFormat("Assunto with ID {0} updated!", id);
                    }
                    catch (InvalidOperationException e)
                    {
                        log.Error("Dequeueing an empty queue!", e);
                    }
                    catch (Exception e)
                    {
                        log.Error("Dequeueing an empty queue!", e);
                    }
                }
                else if (this.assuntos.Count == 0 && this.isWorking)
                {
                    this.whAssuntos.WaitOne();
                }
            }
            this.assuntosUpdater.Optimize();
        }

        public void UpdateTipologias()
        {
            if (this.tipologiasUpdater == null)
            {
                this.tipologiasUpdater = new TipologiasUpdater();
            }
            while (this.isWorking)
            {
                if (this.tipologias.Count > 0)
                {
                    try
                    {
                        long id = (long)this.tipologias.Dequeue();
                        this.tipologiasUpdater.Update(id);
                        log.DebugFormat("Tipologia with ID {0} updated!", id);
                    }
                    catch (InvalidOperationException e)
                    {
                        log.Error("Dequeueing an empty queue!", e);
                    }
                    catch (Exception e)
                    {
                        log.Error("Dequeueing an empty queue!", e);
                    }
                }
                else if (this.tipologias.Count == 0 && this.isWorking)
                {
                    this.whTipologias.WaitOne();
                }
            }
            this.tipologiasUpdater.Optimize();
        }
    }
}
