using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Lucene.Net.Analysis;
using Lucene.Net.Util;
using Lucene.Net.Analysis.Tokenattributes;

namespace GISAServer.Search.Synonyms
{
    public class SynonymFilter : TokenFilter
    {
        public static string TOKEN_TYPE_SYNONYM = "SYNONYM";

        private Stack<string> synonymStack;
        private SynonymEngine engine;
        private AttributeSource.State current;
        private readonly TermAttribute termAtt;
        private readonly PositionIncrementAttribute posIncrAtt;

        public SynonymFilter (TokenStream input, SynonymEngine engine) : base(input) {
            if (engine == null)
                throw new ArgumentNullException("synonymEngine");
            synonymStack = new Stack<string>();
            this.engine = engine;

            this.termAtt = (TermAttribute)AddAttribute<ITermAttribute>();
            this.posIncrAtt = (PositionIncrementAttribute)AddAttribute<IPositionIncrementAttribute>();

            //this.termAtt = this.AddAttribute<string>();
            //this.posIncrAtt = this.AddAttribute<string>();
        }

        public override bool IncrementToken()
        {
            if (synonymStack.Count > 0)
            {
                var syn = synonymStack.Pop();
                RestoreState(current);
                termAtt.SetTermBuffer(syn);
                posIncrAtt.PositionIncrement = 0;
                return true;
            }

            if (!input.IncrementToken())
                return false;

            if (addAliasesToStack())
                current = CaptureState();

            return true;
        }

        private bool addAliasesToStack()
        {
            var synonyms = engine.getAcordoOrtogWord(termAtt.Term);
            if (synonyms == null)
                return false;
            foreach (var synonym in synonyms)
                synonymStack.Push(synonym);
            return true;
        }
    }
}
