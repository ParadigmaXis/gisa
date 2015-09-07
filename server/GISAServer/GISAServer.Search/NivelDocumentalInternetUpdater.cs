using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Store;

using GISAServer.Hibernate.Utils;
using GISAServer.Hibernate;

using log4net;

using GISAServer.Search.Synonyms;

namespace GISAServer.Search
{
    public class NivelDocumentalInternetUpdater : LuceneUpdater
    {
        // Logging initializations
        private static readonly ILog log = LogManager.GetLogger(typeof(NivelDocumentalInternetUpdater));

        public NivelDocumentalInternetUpdater()
            : base(FSDirectory.Open(Util.NivelDocumentalInternetPath))
        { }

        protected override IndexWriter GetIndexWriter()
        {
            return new IndexWriter(this.index, new SynonymAnalyzer(new XmlSynonymEngine()), IndexWriter.MaxFieldLength.UNLIMITED);
        }

        public override void CreateIndex()
        {
            using(IndexWriter indexWriter = new IndexWriter(this.index, new SynonymAnalyzer(new XmlSynonymEngine()), IndexWriter.MaxFieldLength.UNLIMITED))
            {
                GISAUtils.getAllNivelDocumentalInternetIds().ToList().ForEach(id => indexWriter.AddDocument(this.GetDocument(id)));
                indexWriter.Optimize();
                indexWriter.Dispose();
            }
        }

        protected override Document GetDocument(long id)
        {
            Document doc = null;
            try
            {
                doc = Util.NivelDocumentalInternetToLuceneDocument(new NivelDocumentalInternet(id));
            }
            catch (Exception e)
            {
                log.Error(string.Format("Error adding document, with idNivel {0} to index.", id), e);
            }
            return doc;
        }

        public void MyUpdate(List<string> idsNivel)
        {
            //GISAUtils.GetIdsNiveisDocumentaisInternet(idsNivel);
            //this.Update(idsNivel);

            var ticks = DateTime.Now.Ticks;
            this.OptimizedUpdate(GISAUtils.GetIdsNiveisDocumentaisInternet(idsNivel));
            log.Debug("UpdateManyIdsNiveisDocumentaisInternet: " + new TimeSpan(DateTime.Now.Ticks - ticks).ToString());
        }
    }
}
