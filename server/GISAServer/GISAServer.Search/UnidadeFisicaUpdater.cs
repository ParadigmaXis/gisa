using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using Lucene.Net.Analysis.Standard;
using Lucene.Net.Index;
using Lucene.Net.Documents;
using Lucene.Net.Store;

using GISAServer.Hibernate;
using GISAServer.Hibernate.Utils;

using log4net;

using GISAServer.Search.Synonyms;

namespace GISAServer.Search
{
    public class UnidadeFisicaUpdater : LuceneUpdater
    {
        // Logging initializations
        private static readonly ILog log = LogManager.GetLogger(typeof(UnidadeFisicaUpdater));

        public UnidadeFisicaUpdater()
            : base(FSDirectory.Open(Util.UnidadeFisicaPath))
        { }

        protected override IndexWriter GetIndexWriter()
        {
            return new IndexWriter(this.index, new UnidadeFisicaSearcher.InstancePerFieldAnalyzerWrapper().instancePerFieldAnalyzerWrapper, IndexWriter.MaxFieldLength.UNLIMITED);
        }

        public override void CreateIndex()
        {
            using (IndexWriter indexWriter = new IndexWriter(this.index, new UnidadeFisicaSearcher.InstancePerFieldAnalyzerWrapper().instancePerFieldAnalyzerWrapper, IndexWriter.MaxFieldLength.UNLIMITED))
            {
                GISAUtils.getAllUnidadesFisicasIds().ToList().ForEach(id => indexWriter.AddDocument(this.GetDocument(id)));
                indexWriter.Optimize();
                indexWriter.Dispose();
            }
        }

        protected override Document GetDocument(long idNivel)
        {
            Document doc = null;
            try
            {             
                doc = Util.UFToLuceneDocument(new UnidadeFisica(idNivel));              
            }
            catch (Exception e)
            {
                log.Error(string.Format("Error adding document, with idNivel {0} to index.", idNivel), e);
            }
            return doc;
        }
    }
}