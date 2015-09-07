using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Search;
using Lucene.Net.Store;

using GISAServer.Hibernate.Utils;

namespace GISAServer.Search
{
    public class NivelDocumentalInternetSearcher : LuceneSearcher
    {
        public NivelDocumentalInternetSearcher(string orderBy, string defaultField)
            : base(FSDirectory.Open(Util.NivelDocumentalInternetPath), orderBy, defaultField, new Synonyms.SynonymAnalyzer(new Synonyms.XmlSynonymEngine()))
        { }

        public new string Search(string searchText)
        {            
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

            return string.Join(" ", luceneResults.ToArray());
        }
    }
}
