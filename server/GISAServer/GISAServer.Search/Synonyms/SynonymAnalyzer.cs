using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Lucene.Net;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;

namespace GISAServer.Search.Synonyms
{
    public class SynonymAnalyzer : Analyzer
    {
        private SynonymEngine engine;
        public SynonymAnalyzer(SynonymEngine engine) { this.engine = engine; }

        public override TokenStream TokenStream(string fieldName, TextReader reader)
        {
            var result = new SynonymFilter(
                            new StopFilter(true, 
                                new ASCIIFoldingFilter(
                                    new LowerCaseFilter(
                                        new StandardFilter(
                                            new StandardTokenizer(Lucene.Net.Util.Version.LUCENE_30, reader)))), 
                                StandardAnalyzer.STOP_WORDS_SET), 
                            engine);
            return result;
        }
    }
}
