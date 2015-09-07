using System;
using System.Collections.Generic;
using System.Text;

using Lucene.Net.Store;

using GISAServer.Search.Synonyms;

namespace GISAServer.Search
{
    public class NivelDocumentalComProdutoresSearcher : LuceneSearcher
    {
        public NivelDocumentalComProdutoresSearcher(string orderBy, string defaultField)
            : base(FSDirectory.Open(Util.NivelDocumentalComProdutoresPath), orderBy, defaultField, new SynonymAnalyzer(new XmlSynonymEngine()))
        { }

        public new string Search(string searchText)
        {
            return string.Join(" ", base.Search(searchText).ToArray());
        }
    }
}
