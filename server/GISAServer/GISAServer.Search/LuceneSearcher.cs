using System;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Lucene.Net.QueryParsers;
using Lucene.Net.Store;

using GISAServer.Search.Synonyms;

namespace GISAServer.Search
{
    public class LuceneSearcher
    {
        public string OrderBy { get; set; }
        private IndexSearcher indexSearcher;
        private QueryParser queryParser;
        private Analyzer Analyzer { get; set; }
        private string defaultField = "";

        public LuceneSearcher(Directory index, string orderBy, string defaultField, Analyzer analyzer)
        {
            this.OrderBy = orderBy;
            this.Analyzer = analyzer;
            this.defaultField = string.IsNullOrEmpty(defaultField) ? "all" : defaultField;
            this.indexSearcher = new IndexSearcher(index, true);
            InitQueryParser();
        }

        protected void InitQueryParser()
        {
            this.queryParser = new QueryParser(Lucene.Net.Util.Version.LUCENE_30, this.defaultField, this.Analyzer);
            this.queryParser.AllowLeadingWildcard = true;
            this.queryParser.DefaultOperator = QueryParser.Operator.AND;
        }

        public List<string> Search(string searchText)
        {
            var query = this.queryParser.Parse(searchText);
            var res = this.indexSearcher.Search(query, int.MaxValue);
            return res.ScoreDocs.Select(hit => this.indexSearcher.Doc(hit.Doc).Get("id")).ToList();
        }

        public void Close()
        {
            if (this.indexSearcher != null)
                this.indexSearcher.Dispose();
        }
    }
}
