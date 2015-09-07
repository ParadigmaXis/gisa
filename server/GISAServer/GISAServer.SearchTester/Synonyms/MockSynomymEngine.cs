using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GISAServer.Search.Synonyms;

namespace GISAServer.SearchTester.Synonyms
{
    public class MockSynomymEngine : SynonymEngine
    {
        private static Dictionary<string, List<string>> dict = new Dictionary<string,List<string>>()
        {
            {"quick", new List<string> {"fast", "speedy"}},
            {"jumps", new List<string> {"leaps", "hops"}},
            {"over", new List<string> {"above"}},
            {"lazy", new List<string> {"apathetic", "sluggish"}},
            {"dogs", new List<string> {"canines", "pooches"}},
        };

        public string[] getSynonyms(string s) { return null; }

        public List<string> getAcordoOrtogWord(string s)
        {
            if (!dict.ContainsKey(s)) return null;
            return dict[s];
        }
    }
}
