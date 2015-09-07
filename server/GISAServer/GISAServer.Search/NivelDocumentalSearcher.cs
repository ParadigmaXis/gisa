using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using GISAServer.Hibernate.Utils;

using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Search;
using Lucene.Net.Store;

namespace GISAServer.Search
{
    public class NivelDocumentalSearcher : LuceneSearcher
    {
        private Cache.QueryCache queryCacher; 

        public class InstancePerFieldAnalyzerWrapper
        {
            public Analyzer instancePerFieldAnalyzerWrapper { get; set; }
            public InstancePerFieldAnalyzerWrapper()
            {
                var analyzer = new Lucene.Net.Analysis.PerFieldAnalyzerWrapper(new Synonyms.SynonymAnalyzer(new Synonyms.XmlSynonymEngine()));
                analyzer.AddAnalyzer("cota", new Lucene.Net.Analysis.KeywordAnalyzer());
                analyzer.AddAnalyzer("codigo", new Lucene.Net.Analysis.KeywordAnalyzer());
                instancePerFieldAnalyzerWrapper = analyzer;
            }
        }

        public NivelDocumentalSearcher(string orderBy, string defaultField, Cache.QueryCache c)
            : base(FSDirectory.Open(Util.NivelDocumentalPath), orderBy, defaultField, new InstancePerFieldAnalyzerWrapper().instancePerFieldAnalyzerWrapper)
        {
            this.queryCacher = c;
        }

        public string Search(string searchText, long idTrustee)
        {
            var results = this.queryCacher.SearchInCache(idTrustee, searchText);
            if (results != null) return string.Join(" ", results.ToArray());

            string operador = GISAUtils.buildOperatorSearchString(ref searchText);
            DateTime? inicio;
            DateTime? fim;
            searchText = GISAUtils.buildDataInicialDataFinalSearchString(searchText, out inicio, out fim);
            List<string> nivelIds = GISAUtils.GetNivelIds(operador, inicio, fim);

            List<string> luceneResults;
            if (nivelIds == null) //no parameter was actually filled for GetNivelIds()
            {
                luceneResults = base.Search(searchText);
            }
            else
            {
                if (nivelIds.Count > 0)
                {
                    if (searchText.Length > 0)
                    {
                        string a = GISAUtils.buildList_Id_OR_Id(searchText, nivelIds);
                        luceneResults = base.Search(a);
                    }
                    else
                        luceneResults = nivelIds;
                }
                else
                    luceneResults = new List<string>();
            }

            var c = DateTime.Now.Ticks;
            var ret = Util.FilterByReadPermission(luceneResults, idTrustee);
            var b = new TimeSpan(DateTime.Now.Ticks - c).ToString();

            queryCacher.Add(idTrustee, searchText, luceneResults);

            return string.Join(" ", ret.ToArray());
        }
    }
}
