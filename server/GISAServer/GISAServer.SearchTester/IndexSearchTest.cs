using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Lucene.Net.Store;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Lucene.Net.Documents;
using Lucene.Net.QueryParsers;
using Lucene.Net.Analysis.Standard;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GISAServer.SearchTester
{
    [TestClass]
    public class IndexSearchTest
    {
        private RAMDirectory directory;
        private IndexSearcher searcher;
        private IndexWriter writer;
        private Lucene.Net.Analysis.Standard.StandardAnalyzer standardAnalyzer;

        private void FixtureSetup()
        {
            directory = new RAMDirectory();
            standardAnalyzer = new Lucene.Net.Analysis.Standard.StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30);
            writer = new IndexWriter(directory, standardAnalyzer, IndexWriter.MaxFieldLength.UNLIMITED);
            Document doc = new Document();
            doc.Add(new Field("content", "The quick brown fox jumps over the lazy dogs", Field.Store.NO, Field.Index.ANALYZED));
            writer.AddDocument(doc);
            writer.Dispose();

            searcher = new IndexSearcher(directory);
        }

        [TestMethod]
        public void Test1()
        {
            FixtureSetup();

            QueryParser qp = new QueryParser(Lucene.Net.Util.Version.LUCENE_30, "", standardAnalyzer);
            Query query = qp.Parse("content:fox");
            Assert.AreEqual(1, searcher.Search(query, 2).TotalHits);


        }

        class MyCollector : Collector
        {
            public override bool AcceptsDocsOutOfOrder
            {
                get { throw new NotImplementedException(); }
            }

            public override void Collect(int doc)
            {
                throw new NotImplementedException();
            }

            public override void SetNextReader(IndexReader reader, int docBase)
            {
                throw new NotImplementedException();
            }

            public override void SetScorer(Scorer scorer)
            {
                throw new NotImplementedException();
            }
        }

    }
}
