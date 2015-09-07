using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Lucene.Net;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;

namespace GISAServer.Search.CustomAnalyzer
{
    public class CustomAnalyzer : StandardAnalyzer
    {
        public CustomAnalyzer() : base(Lucene.Net.Util.Version.LUCENE_30) { }
        public override TokenStream TokenStream(string fieldName, System.IO.TextReader reader)
        {
            var result = new StopFilter(true,
                            new ASCIIFoldingFilter(
                                new LowerCaseFilter(
                                    new StandardFilter(
                                        new StandardTokenizer(Lucene.Net.Util.Version.LUCENE_30, reader)))),
                            StandardAnalyzer.STOP_WORDS_SET);
            return result;
        }
    }
}
