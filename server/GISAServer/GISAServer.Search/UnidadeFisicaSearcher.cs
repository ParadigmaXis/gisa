using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using GISAServer.Hibernate;
using GISAServer.Hibernate.Utils;

using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Lucene.Net.Store;


namespace GISAServer.Search
{
    public class UnidadeFisicaSearcher : LuceneSearcher
    {
        public class InstancePerFieldAnalyzerWrapper
        {
            public Analyzer instancePerFieldAnalyzerWrapper { get; set; }
            public InstancePerFieldAnalyzerWrapper()
            {
                var analyzer = new Lucene.Net.Analysis.PerFieldAnalyzerWrapper(new Synonyms.SynonymAnalyzer(new Synonyms.XmlSynonymEngine()));
                analyzer.AddAnalyzer("cota", new Lucene.Net.Analysis.KeywordAnalyzer());
                instancePerFieldAnalyzerWrapper = analyzer;
            }
        }
        public UnidadeFisicaSearcher(string orderBy, string defaultField)
            : base(FSDirectory.Open(Util.UnidadeFisicaPath), orderBy, defaultField, new InstancePerFieldAnalyzerWrapper().instancePerFieldAnalyzerWrapper)
        {
            
        }

        public new string Search(string searchText)
        {
            string operador = GISAUtils.buildOperatorSearchString(ref searchText);
            DateTime? inicio = null;
            DateTime? fim = null;
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
                    {
                        luceneResults = nivelIds;
                    }
                }
                else
                {
                    luceneResults = new List<string>();
                }
            }

            Dictionary<string, string> test = new Dictionary<string, string>();

            List<string> ret = new List<string>();
            foreach (string id in luceneResults)
            {
                if (test.ContainsKey(id)) {continue;}
                
                test.Add(id, id);
                ret.Add(id);
            }

            return string.Join(" ", ret.ToArray());
        }        
    }
}
