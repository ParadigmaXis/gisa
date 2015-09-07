using System;
using System.Collections.Generic;
using System.Text;

using Lucene.Net.Analysis.Standard;
using Lucene.Net.Store;

namespace GISAServer.Search
{
    public class TipologiasSearcher : LuceneSearcher
    {
        public TipologiasSearcher(string orderBy, string defaultField)
            : base(FSDirectory.Open(Util.TipologiasPath), orderBy, defaultField, new Synonyms.SynonymAnalyzer(new Synonyms.XmlSynonymEngine()))
        { }

        public new string Search(string searchText)
        {
            return string.Join(" ", base.Search(searchText).ToArray());
        }

    }
}