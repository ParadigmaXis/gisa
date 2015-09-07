using System;
using System.Collections.Generic;
using System.Text;

using Lucene.Net.Analysis.Standard;
using Lucene.Net.Store;

using GISAServer.Search.Synonyms;

namespace GISAServer.Search
{
    public class AssuntosSearcher : LuceneSearcher
    {
        public AssuntosSearcher(string orderBy, string defaultField)
            : base(FSDirectory.Open(Util.AssuntosPath), orderBy, defaultField, new Synonyms.SynonymAnalyzer(new Synonyms.XmlSynonymEngine()))
        { }

        public new string Search(string searchText)
        {
            return string.Join(" ", base.Search(searchText).ToArray());
        }
    }
}