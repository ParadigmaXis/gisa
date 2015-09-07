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
using GISAServer.Search.Synonyms;
using NUnit.Framework;

namespace GISAServer.SearchTester.Synonyms
{
    [TestFixture]
    public class SynonymAnalyzerTest
    {
        private RAMDirectory directory;
        private IndexSearcher searcher;
        private static SynonymAnalyzer synonymAnalyzer = new SynonymAnalyzer(new MockSynomymEngine());

        [TestFixtureSetUp]
        public void FixtureSetup()
        {
            directory = new RAMDirectory();
            IndexWriter writer = new IndexWriter(directory, synonymAnalyzer, IndexWriter.MaxFieldLength.UNLIMITED);
            Document doc = new Document();
            doc.Add(new Field("content", "The quick brown fox jumps over the lazy dogs", Field.Store.NO, Field.Index.ANALYZED));
            writer.AddDocument(doc);
            writer.Dispose();

            searcher = new IndexSearcher(directory);
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            searcher.Dispose();
        }

        [Test]
        public void TestSearchByAPI()
        {
            TermQuery tq = new TermQuery(new Term("content", "hops"));
            Assert.AreEqual(1, searcher.Search(tq, 1).TotalHits);

            PhraseQuery pq = new PhraseQuery();
            pq.Add(new Term("content", "fox"));
            pq.Add(new Term("content", "hops"));
            Assert.AreEqual(1, searcher.Search(tq, 1).TotalHits);
        }

        [Test]
        public void TestWithQueryParser()
        {
            QueryParser qp = new QueryParser(Lucene.Net.Util.Version.LUCENE_30, "", synonymAnalyzer);
            Query query = qp.Parse("content:\"fox jumps\"");
            Assert.AreEqual(1, searcher.Search(query, 1).TotalHits, "!!!! what?");

            qp = new QueryParser(Lucene.Net.Util.Version.LUCENE_30, "", new Lucene.Net.Analysis.Standard.StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30));
            Assert.AreEqual(1, searcher.Search(query, 1).TotalHits, "!!!! *whew*");

            query = qp.Parse("content:\"fox hops\"");
            Assert.AreEqual(1, searcher.Search(query, 1).TotalHits, "!!!! *whew*");

            query = qp.Parse("content:(\"fox hops\")");
            Assert.AreEqual(1, searcher.Search(query, 1).TotalHits, "!!!! *whew*");
        }
    }
}
