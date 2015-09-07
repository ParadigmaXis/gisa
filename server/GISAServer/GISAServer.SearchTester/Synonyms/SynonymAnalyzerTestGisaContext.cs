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
using GISAServer.Hibernate;

namespace GISAServer.SearchTester.Synonyms
{
    [TestFixture]
    public class SynonymAnalyzerTestGisaContext
    {
        private RAMDirectory directory;
        private IndexSearcher searcher;
        private static SynonymAnalyzer synonymAnalyzer = new SynonymAnalyzer(new MockSynomymEngine());

        [TestFixtureSetUp]
        public void FixtureSetup()
        {
            directory = new RAMDirectory();
            IndexWriter writer = new IndexWriter(directory, synonymAnalyzer, IndexWriter.MaxFieldLength.UNLIMITED);
            NivelDocumental n = new NivelDocumental();
            //var doc = new Util.NivelDocumentalToLuceneDocument(n);
            Document doc = new Document();
            doc.Add(new Field("content", "Manobras no Porto", Field.Store.NO, Field.Index.ANALYZED));
            doc.Add(new Field("content", "Registo do testamento com que faleceu Ana Amália de Brito e Cunha", Field.Store.NO, Field.Index.ANALYZED));
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
        public void TestWithQueryParser()
        {
            QueryParser qp = new QueryParser(Lucene.Net.Util.Version.LUCENE_30, "", new GISAServer.Search.NivelDocumentalSearcher.InstancePerFieldAnalyzerWrapper().instancePerFieldAnalyzerWrapper);
            Query query = qp.Parse("content:\"manobras\"");
            Assert.AreEqual(1, searcher.Search(query, 1).TotalHits, "!!!! what?");
            
            query = qp.Parse("content:\"porto\"");
            Assert.AreEqual(1, searcher.Search(query, 1).TotalHits, "!!!! *whew*");

            query = qp.Parse("content:\"manobras porto\"");
            Assert.AreEqual(0, searcher.Search(query, 1).TotalHits, "!!!! *whew*");

            query = qp.Parse("content:\"manobras no porto\"");
            Assert.AreEqual(1, searcher.Search(query, 1).TotalHits, "!!!! *whew*");
        }

        [Test]
        public void TestWithQueryParser2()
        {
            QueryParser qp = new QueryParser(Lucene.Net.Util.Version.LUCENE_30, "", new GISAServer.Search.NivelDocumentalSearcher.InstancePerFieldAnalyzerWrapper().instancePerFieldAnalyzerWrapper);
            Query query = qp.Parse("content:ana");
            Assert.AreEqual(1, searcher.Search(query, 1).TotalHits, "!!!! what?");

            query = qp.Parse("content:amalia");
            Assert.AreEqual(1, searcher.Search(query, 1).TotalHits, "!!!! *whew*");

            query = qp.Parse("content:amália");
            Assert.AreEqual(1, searcher.Search(query, 1).TotalHits, "!!!! *whew*");

            query = qp.Parse("content:ana amalia");
            Assert.AreEqual(1, searcher.Search(query, 1).TotalHits, "!!!! *whew*");
            
            query = qp.Parse("content:ana amália");
            Assert.AreEqual(1, searcher.Search(query, 1).TotalHits, "!!!! *whew*");
        }
    }
}
