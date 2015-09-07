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
using GISAServer.Search.Synonyms;

using log4net;

namespace GISAServer.Search
{
    public class AssuntosUpdater : LuceneUpdater
    {
        // Logging initializations
        private static readonly ILog log = LogManager.GetLogger(typeof(AssuntosUpdater));

        public AssuntosUpdater()
            : base(FSDirectory.Open(Util.AssuntosPath))
        { }

        protected override IndexWriter GetIndexWriter()
        {
            return new IndexWriter(this.index, new SynonymAnalyzer(new XmlSynonymEngine()), IndexWriter.MaxFieldLength.UNLIMITED);
        }

        public override void CreateIndex()
        {
            using(IndexWriter indexWriter = new IndexWriter(this.index, new SynonymAnalyzer(new XmlSynonymEngine()), IndexWriter.MaxFieldLength.UNLIMITED )) 
            {
                GISAUtils.getAllAssuntosIds().ToList().ForEach(id => indexWriter.AddDocument(this.GetDocument(id)));
                indexWriter.Optimize();
                indexWriter.Dispose();
            }
        }

        protected override Document GetDocument(long id)
        {
            Document doc = null;
            try
            {
                 doc = Util.AssuntosToLuceneDocument(new Assunto(id));
            }
            catch (Exception e)
            {
                log.Error(string.Format("Error adding 'assunto', with id {0} to index.", id), e);
            }
            return doc;
        }
    }
}