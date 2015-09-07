using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GISAServer.Search.Synonyms
{
    public interface SynonymEngine
    {
        string[] getSynonyms (string s);
        List<string> getAcordoOrtogWord(string s);
    }
}
