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

namespace GISAServer.SearchTester.StandardAnalyzer
{
    [TestFixture]
    public class StandardAnalyzerTest
    {
        private RAMDirectory directory;
        private IndexSearcher searcher;
        private IndexWriter writer;
        private Lucene.Net.Analysis.Standard.StandardAnalyzer standardAnalyzer;

        [TestFixtureSetUp]
        public void FixtureSetup()
        {
            directory = new RAMDirectory();
            standardAnalyzer = new Lucene.Net.Analysis.Standard.StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30);
            writer = new IndexWriter(directory, standardAnalyzer, IndexWriter.MaxFieldLength.UNLIMITED);
            AddDocuments(new List<string>() { "[Rosa]", "Rosa", "Olá" });
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
                doc.Add(new Field("term", term, Field.Store.NO, Field.Index.ANALYZED));
                writer.AddDocument(doc);
            });
        }

        [Test]
        public void TestTerms1() // "[Rosa]", "Rosa"
        {
            QueryParser qp = new QueryParser(Lucene.Net.Util.Version.LUCENE_30, "", standardAnalyzer);
            Query query = qp.Parse("term:\"Rosa\"");
            Assert.AreEqual(2, searcher.Search(query, 2).TotalHits);

            query = qp.Parse("term:\"\\[Rosa\\]\"");
            Assert.AreEqual(2, searcher.Search(query, 2).TotalHits);

            query = qp.Parse("term:Rosa");
            Assert.AreEqual(2, searcher.Search(query, 2).TotalHits);

            // usando o standard analyzer os '[' ']' são suprimidos daí os 2 documentos serem sempre retornados em qualquer uma das situações

            //query = qp.Parse("term:[Rosa]");
            //hits = searcher.Search(query);
            //Assert.AreEqual(1, hits.Length());
        }

        [Test]
        public void TestTerms2() // "Olá", "Rosa"
        {
            QueryParser qp = new QueryParser(Lucene.Net.Util.Version.LUCENE_30, "", standardAnalyzer);
            Query query = qp.Parse("term:Olá");
            Assert.AreEqual(1, searcher.Search(query, 1).TotalHits);

            query = qp.Parse("term:olá");
            Assert.AreEqual(1, searcher.Search(query, 1).TotalHits);

            query = qp.Parse("term:Ola");
            Assert.AreEqual(0, searcher.Search(query, 1).TotalHits);

            query = qp.Parse("term:ola");
            Assert.AreEqual(0, searcher.Search(query, 1).TotalHits);

            // o standart analyzer retorna resultados independentemente de os termos terem ou não maiúsculas
        }
    }
}
