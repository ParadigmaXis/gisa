using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Lucene.Net.Store;

using GISAServer.Hibernate.Utils;
using GISAServer.Hibernate;
using GISAServer.Search;

namespace GISAServer.SearchTester.Locks
{
    public class MockUnidadeFisicaUpdater : UnidadeFisicaUpdater
    {
        int N { get; set; }
        string P { get; set; }
        public MockUnidadeFisicaUpdater(string pattern, int n) { P = pattern; N = n; }

        protected override Lucene.Net.Documents.Document GetDocument(long id)
        {
            var uf = new UnidadeFisica();
            uf.Id = id.ToString();
            uf.Designacao = string.Format(P, id.ToString());
            return Util.UFToLuceneDocument(uf);
        }

        public override void CreateIndex()
        {
            using (IndexWriter indexWriter = new IndexWriter(this.index, new UnidadeFisicaSearcher.InstancePerFieldAnalyzerWrapper().instancePerFieldAnalyzerWrapper, IndexWriter.MaxFieldLength.UNLIMITED))
            {
                for (int i = 0; i < N; i++)
                    indexWriter.AddDocument(this.GetDocument(i));
                indexWriter.Optimize();
                indexWriter.Dispose();
            }

            ForceUnlockIndex();
        }

        public void ClearIndex()
        {
            if (System.IO.Directory.GetFiles(this.index.Directory.FullName).Any())
            {
                try
                {
                    var analyzer = new Lucene.Net.Analysis.Standard.StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30);
                    using (var writer = new IndexWriter(this.index, analyzer, true, IndexWriter.MaxFieldLength.UNLIMITED))
                    {
                        // remove older index entries
                        writer.DeleteAll();

                        // close handles
                        analyzer.Close();
                        writer.Dispose();
                    }

                    ForceUnlockIndex();
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        public int CountAllIndexRecords() 
        {
			// validate search index
            if (!System.IO.Directory.GetFiles(this.index.Directory.FullName).Any()) return 0;

			// set up lucene searcher
            
			var searcher = new IndexSearcher(this.index, false);
            var reader = IndexReader.Open(this.index, false);
			var docs = new List<Document>();
            try
            {
                var term = reader.TermDocs();
                while (term.Next()) docs.Add(searcher.Doc(term.Doc));
            }
            catch (Exception e) { Console.WriteLine(e.ToString()); }
            finally
            {
                reader.Dispose();
                searcher.Dispose();
            }

			return docs.Count;
		}
    }
}
