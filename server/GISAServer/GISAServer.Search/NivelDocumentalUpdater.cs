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
    public class NivelDocumentalUpdater : LuceneUpdater
    {
        // Logging initializations
        private static readonly ILog log = LogManager.GetLogger(typeof(NivelDocumentalUpdater));

        public NivelDocumentalUpdater()
            : base(FSDirectory.Open(Util.NivelDocumentalPath))
        { }

        protected override IndexWriter GetIndexWriter()
        {
            return new IndexWriter(this.index, new NivelDocumentalSearcher.InstancePerFieldAnalyzerWrapper().instancePerFieldAnalyzerWrapper, IndexWriter.MaxFieldLength.UNLIMITED);
        }

        public override void CreateIndex()
        {
            using(IndexWriter indexWriter = new IndexWriter(this.index, new NivelDocumentalSearcher.InstancePerFieldAnalyzerWrapper().instancePerFieldAnalyzerWrapper, IndexWriter.MaxFieldLength.UNLIMITED))
            {
                GISAUtils.getAllNivelDocumentalIds().ToList().ForEach(id => indexWriter.AddDocument(this.GetDocument(id)));
                indexWriter.Optimize();
                indexWriter.Dispose();
            }
        }

        protected override Document GetDocument(long id)
        {
            Document doc = null;
            try
            {
                doc = Util.NivelDocumentalToLuceneDocument(new NivelDocumental(id));
            }
            catch (Exception e)
            {
                log.Error(string.Format("Error adding document, with idNivel {0} to index.", id), e);
            }
            return doc;
        }
    }
}