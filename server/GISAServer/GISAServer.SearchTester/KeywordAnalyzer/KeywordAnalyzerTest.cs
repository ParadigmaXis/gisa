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

namespace GISAServer.SearchTester.KeywordAnalyzer
{
    /* 
     NOTA IMPORTANTE: ler http://trac/gisa/wiki/desenvolvimento/clientes#Pesquisa
     */

    [TestFixture]
    public class KeywordAnalyzerTest
    {
        private RAMDirectory directory;
        private IndexSearcher searcher;
        private IndexWriter writer;
        private static SynonymAnalyzer synonymAnalyzer = new SynonymAnalyzer(new Synonyms.MockSynomymEngine());
        private QueryParser qp = new QueryParser(Lucene.Net.Util.Version.LUCENE_30, "", new Lucene.Net.Analysis.KeywordAnalyzer());

        [TestFixtureSetUp]
        public void FixtureSetup()
        {
            directory = new RAMDirectory();
            writer = new IndexWriter(directory, new Lucene.Net.Analysis.KeywordAnalyzer(), IndexWriter.MaxFieldLength.UNLIMITED);
            //writer = new IndexWriter(directory, synonymAnalyzer, true);
            Document doc = new Document();
            doc.Add(new Field("cota", "E:A.123/2008", Field.Store.NO, Field.Index.NOT_ANALYZED));
            writer.AddDocument(doc);

            doc = new Document();
            doc.Add(new Field("cota", "A/03/02 - Cx 22 -Lv. 25 [Ref. 217]", Field.Store.NO, Field.Index.NOT_ANALYZED));
            writer.AddDocument(doc);

            doc = new Document();
            doc.Add(new Field("cota", "A/03/01-Cx.15-Lv.16[Ref. 207]", Field.Store.NO, Field.Index.NOT_ANALYZED));
            writer.AddDocument(doc);

            doc = new Document();
            doc.Add(new Field("cota", "A/03/02 - Cx 22 -Lv. 25 [Ref. 217]", Field.Store.NO, Field.Index.NOT_ANALYZED));
            writer.AddDocument(doc);

            doc = new Document();
            doc.Add(new Field("cota", "D-CMP/7(27)", Field.Store.NO, Field.Index.NOT_ANALYZED));
            writer.AddDocument(doc);
            doc = new Document();
            doc.Add(new Field("cota", "D-CMP/7(28)", Field.Store.NO, Field.Index.NOT_ANALYZED));
            writer.AddDocument(doc);
            doc = new Document();
            doc.Add(new Field("cota", "D-CMP/7(29)", Field.Store.NO, Field.Index.NOT_ANALYZED));
            writer.AddDocument(doc);

            doc = new Document();
            doc.Add(new Field("cota", "cota teste", Field.Store.NO, Field.Index.NOT_ANALYZED));
            writer.AddDocument(doc);

            doc = new Document();
            doc.Add(new Field("codigoRef", "POP_280_1971_P", Field.Store.NO, Field.Index.NOT_ANALYZED));
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
        public void TestCotaScenario1()
        {
            //QueryParser qp = new QueryParser("", new GISAServer.Search.UnidadeFisicaSearcher.InstancePerFieldAnalyzerWrapper().instancePerFieldAnalyzerWrapper);
            
            // IMPOERTANTE: ler http://trac/gisa/wiki/desenvolvimento/clientes#Pesquisaporcotasecódigosdereferênciaver2819

            // casos que encontra
            string[] search_with_hits = {   "cota:(*cota?teste*)",      // "cota teste"
                                            "cota:(*cota?tes\\**)",     // "cota tes*"
                                            "cota:(*\\*ota?tes\\**)",   // "*ota tes*"
                                            "cota:(cota*)"              // cota*"
                                        };
            string assert_with_hits = "({0}) Haven't found cota teste: {1}";

            // casos que não encontra
            string[] search_without_hits = {    "cota:(cota teste)",        // cota teste
                                                "cota:(cota)",              // cota
                                                "cota:(cota tes*)",         // cota tes*
                                                "cota:(*ota tes*)",         // *ota tes*
                                                "cota:(\"cota*\")",         // "cota*"
                                                "cota:(teste)"              // teste
                                           };
            string assert_without_hits = "({0}) Found cota teste: {1}";

            Query query = null;
            qp.AllowLeadingWildcard = true;

            for (int i = 0; i < search_with_hits.Length; i++)
            {
                query = qp.Parse(search_with_hits[i]);
                Assert.AreEqual(1, searcher.Search(query, 1).TotalHits, string.Format(assert_with_hits, i + 1, search_with_hits[i]));
            }

            for (int i = 0; i < search_without_hits.Length; i++)
            {
                query = qp.Parse(search_without_hits[i]);
                Assert.AreEqual(0, searcher.Search(query, 1).TotalHits, string.Format(assert_without_hits, i + 1, search_without_hits[i]));
            }
        }

        [Test]
        public void TestCotaScenario2()
        {
            //QueryParser qp = new QueryParser("", new GISAServer.Search.UnidadeFisicaSearcher.InstancePerFieldAnalyzerWrapper().instancePerFieldAnalyzerWrapper);
            QueryParser qp = new QueryParser(Lucene.Net.Util.Version.LUCENE_30, "", new Lucene.Net.Analysis.KeywordAnalyzer());
            Query query = qp.Parse("cota:\"E\\:A.123/2008\"");
            Assert.AreEqual(1, searcher.Search(query, 1).TotalHits, "(1) Haven't found E:A.123/2008");

            query = qp.Parse("cota:(E\\:A.123/2008)");
            Assert.AreEqual(1, searcher.Search(query, 1).TotalHits, "(2) Haven't found E:A.123/2008");

            query = qp.Parse("cota:\"E\\:A.123/2\"");
            Assert.AreEqual(0, searcher.Search(query, 1).TotalHits, "(3) Found E:A.123/2008");

            query = qp.Parse("cota:\"E\\:A.123/2*\"");
            Assert.AreEqual(0, searcher.Search(query, 1).TotalHits, "(4) Found E:A.123/2008");

            query = qp.Parse("cota:\"E\\:A*\"");
            Assert.AreEqual(0, searcher.Search(query, 1).TotalHits, "(5) Found E:A.123/2008");

            qp.AllowLeadingWildcard = true;
            query = qp.Parse("cota:(E\\:A.123/2008)");
            Assert.AreEqual(1, searcher.Search(query, 1).TotalHits, "(6) Haven't found E:A.123/2008");

            /*TODO: esta pesquisa funciona no cliente mas ainda não percebi porque não funciona no teste... */
            query = qp.Parse("cota:E\\:A.123/20*");
            Assert.AreEqual(1, searcher.Search(query, 1).TotalHits, "(7) Haven't found E:A.123/2008");
        }

        [Test]
        public void TestCotaScenario3()
        {
            //QueryParser qp = new QueryParser("", new GISAServer.Search.UnidadeFisicaSearcher.InstancePerFieldAnalyzerWrapper().instancePerFieldAnalyzerWrapper);
            QueryParser qp = new QueryParser(Lucene.Net.Util.Version.LUCENE_30, "", new Lucene.Net.Analysis.KeywordAnalyzer());
            Query query = qp.Parse("cota:D-CMP/7(27)");
            var hits = searcher.Search(query, 1).TotalHits;
            Assert.AreEqual(0, hits, string.Format("(1) Was ment not to match (hits: {0})", hits));

            query = qp.Parse("cota:D\\-CMP/7(27)");
            hits = searcher.Search(query, 1).TotalHits;
            Assert.AreEqual(0, hits, string.Format("(2) Was ment not to match (hits: {0})", hits));

            query = qp.Parse("cota:D-CMP/7\\(27\\)");
            hits = searcher.Search(query, 1).TotalHits;
            Assert.AreEqual(1, hits, string.Format("(3) Was ment to match with D-CMP/7(27) (hits: {0})", hits));

            query = qp.Parse("cota:D\\-CMP/7\\(27\\)");
            hits = searcher.Search(query, 1).TotalHits;
            Assert.AreEqual(1, hits, string.Format("(4) Was ment to match with D-CMP/7(27) (hits: {0})", hits));

            query = qp.Parse("cota:\"D\\-CMP/7\\(27\\)\"");
            hits = searcher.Search(query, 1).TotalHits;
            Assert.AreEqual(1, hits, string.Format("(5) Was ment to match with D-CMP/7(27) (hits: {0})", hits));

            /*TODO: estas pesquisas funcionam no cliente mas ainda não percebi porque não funcionam no teste... */
            query = qp.Parse("cota:D\\-CMP/7*");
            hits = searcher.Search(query, 3).TotalHits;
            Assert.AreEqual(3, hits, string.Format("(6) Was ment to have 3 matches (hits: {0})", hits));

            query = qp.Parse("cota:D\\-*");
            hits = searcher.Search(query, 3).TotalHits;
            Assert.AreEqual(3, hits, string.Format("(7) Was ment to have 3 matches (hits: {0})", hits));
        }

        [Test]
        public void TestCodigoReferenciaScenario1()
        {
            /*TermQuery tq = new TermQuery(new Term("content", "hops"));
            Hits hits = searcher.Search(tq);
            Assert.AreEqual(1, hits);

            PhraseQuery pq = new PhraseQuery();
            pq.Add(new Term("content", "fox"));
            pq.Add(new Term("content", "hops"));
            hits = searcher.Search(tq);
            Assert.AreEqual(1, hits);*/
        }
    }
}
