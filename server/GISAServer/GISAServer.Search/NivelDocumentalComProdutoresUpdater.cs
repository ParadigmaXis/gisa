using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    public class NivelDocumentalComProdutoresUpdater : LuceneUpdater
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(NivelDocumentalComProdutoresUpdater));

        public NivelDocumentalComProdutoresUpdater()
            : base(FSDirectory.Open(Util.NivelDocumentalComProdutoresPath)) { }

        protected override IndexWriter GetIndexWriter()
        {
            return new IndexWriter(this.index, new SynonymAnalyzer(new XmlSynonymEngine()), IndexWriter.MaxFieldLength.UNLIMITED);
        }

        public override void CreateIndex()
        {
            using(IndexWriter indexWriter = new IndexWriter(this.index, new SynonymAnalyzer(new XmlSynonymEngine()), IndexWriter.MaxFieldLength.UNLIMITED))
            {
                GISAUtils.getAllNivelDocumentalComProdutoresIds().ToList().ForEach(id => indexWriter.AddDocument(this.GetDocument(id)));
                indexWriter.Optimize();
                indexWriter.Dispose();
            }
        }

        protected override Document GetDocument(long id)
        {
            Document doc = null;
            try
            {
                doc = Util.NivelDocumentalComProdutoresToLuceneDocument(new NivelDocumentalComProdutores(id));
            }
            catch (Exception e)
            {
                log.Error(string.Format("Error adding document with creators, with idNivel {0} to index.", id), e);
            }
            return doc;
        }

        public void UpdateManyByProdutor(long idProdutor)
        {
            var ticks = DateTime.Now.Ticks;
            this.OptimizedUpdate(GISAUtils.GetIdsNiveisDocumentaisComProdutores(idProdutor));
            log.Debug("UpdateManyByProdutor: " + new TimeSpan(DateTime.Now.Ticks - ticks).ToString());
        }
    }
}