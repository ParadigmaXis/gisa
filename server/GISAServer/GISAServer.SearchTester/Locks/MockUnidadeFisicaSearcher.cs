using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using GISAServer.Hibernate;
using GISAServer.Hibernate.Utils;

using GISAServer.Search;

using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Store;

namespace GISAServer.SearchTester.Locks
{
    class MockUnidadeFisicaSearcher : LuceneSearcher
    {
        public class InstancePerFieldAnalyzerWrapper
        {
            public Analyzer instancePerFieldAnalyzerWrapper { get; set; }
            public InstancePerFieldAnalyzerWrapper()
            {
                var analyzer = new Lucene.Net.Analysis.PerFieldAnalyzerWrapper(new GISAServer.Search.Synonyms.SynonymAnalyzer(new GISAServer.Search.Synonyms.XmlSynonymEngine()));
                analyzer.AddAnalyzer("cota", new Lucene.Net.Analysis.KeywordAnalyzer());
                instancePerFieldAnalyzerWrapper = analyzer;
            }
        }
        public MockUnidadeFisicaSearcher(string orderBy, string defaultField)
            : base(FSDirectory.Open(Util.UnidadeFisicaPath), orderBy, defaultField, new InstancePerFieldAnalyzerWrapper().instancePerFieldAnalyzerWrapper)
        {
            
        }

        public void IsIndexLocked()
        {
            var lockFilePath = Path.Combine(Util.UnidadeFisicaPath, "write.lock");
            Console.WriteLine("Index lock? " + File.Exists(lockFilePath));
        }
    }
}
