using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Lucene.Net.Store;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Lucene.Net.Documents;
using Lucene.Net.QueryParsers;

using GISAServer.Search.CustomAnalyzer;

using NUnit.Framework;

namespace GISAServer.SearchTester.CustomAnalyzer
{
    [TestFixture]
    class CustomAnalyzerPhraseTest
    {
        private RAMDirectory directory;
        private IndexSearcher searcher;
        private IndexWriter writer;
        private GISAServer.Search.CustomAnalyzer.CustomAnalyzer customdAnalyzer;
        private readonly string QUERYFIELD = "content";

        [TestFixtureSetUp]
        public void FixtureSetup()
        {
            directory = new RAMDirectory();
            customdAnalyzer = new GISAServer.Search.CustomAnalyzer.CustomAnalyzer();
            writer = new IndexWriter(directory, customdAnalyzer, IndexWriter.MaxFieldLength.UNLIMITED);
            AddDocuments(new List<string>() { 
                "Hello world", 
                "hello tests", 
                "hi lucene and tests",
                "Licença de obra n.º: 465/1927"});
            writer.Dispose();

            searcher = new IndexSearcher(directory);
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            searcher.Dispose();
        }

        private void AddDocuments(List<string> terms)
        {
            terms.ForEach(term =>
            {
                Document doc = new Document();
                doc.Add(new Field(QUERYFIELD, term, Field.Store.NO, Field.Index.ANALYZED));
                writer.AddDocument(doc);
            });
        }

        private Query GetQuery(string q)
        {
            QueryParser qp = new QueryParser(Lucene.Net.Util.Version.LUCENE_30, QUERYFIELD, customdAnalyzer);
            return qp.Parse(q);
        }

        [Test]
        public void Test1()
        {
            var query = GetQuery("hello");
            Assert.AreEqual(2, searcher.Search(query, 2).TotalHits);
        }

        [Test]
        public void Test2()
        {
            var query = GetQuery("\"hello world\"");
            Assert.AreEqual(1, searcher.Search(query, 1).TotalHits);
        }

        [Test]
        public void Test3()
        {
            var query = GetQuery("\"lucene and\"");
            Assert.AreEqual(1, searcher.Search(query, 1).TotalHits);
        }

        [Test]
        public void Test4()
        {
            var query = GetQuery("\"Licença de obra n.º: 465/1927\"");
            Assert.AreEqual(1, searcher.Search(query, 1).TotalHits);
        }
    }
}
