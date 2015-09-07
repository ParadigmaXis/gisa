using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;

using Lucene.Net.Analysis;

namespace GISAServer.Search.Synonyms
{
    public class XmlSynonymEngine : SynonymEngine
    {
        //this will contains a list, of lists of words that go together
        private List<ReadOnlyCollection<string>> SynonymGroups =
            new List<ReadOnlyCollection<string>>();

        /*public XmlSynonymEngine(string xmlSynonymFilePath)
        {
            // create an xml document object, and load it from the specified file.
            XmlDocument Doc = new XmlDocument();
            Doc.Load(xmlSynonymFilePath);

            // get all the <group> nodes
            var groupNodes = Doc.SelectNodes("/synonyms/group");

            //enumerate groups
            foreach (XmlNode g in groupNodes)
            {
                //get all the <syn> elements from the group nodes.
                XmlNodeList synNodes = g.SelectNodes("child::syn");

                //create a list that will hold the items for this group
                List<string> synonymGroupList = new List<string>();

                //enumerate then and add them to the list,
                //and add each synonym group to the list
                foreach (XmlNode synNode in g)
                {
                    synonymGroupList.Add(synNode.InnerText.Trim());
                }

                //add single synonm group to the list of synonm groups.
                SynonymGroups.Add(new ReadOnlyCollection<string>(synonymGroupList));
            }

            // clear the xml document
            Doc = null;
        }*/

        Dictionary<string, HashSet<string>> terms = new Dictionary<string, HashSet<string>>();
        public XmlSynonymEngine()
        {

            var _assembly = Assembly.GetExecutingAssembly();
            var file = new StreamReader(_assembly.GetManifestResourceStream("GISAServer.Search.Synonyms.synonyms.txt"));

            string line = "";
            //var file = new StreamReader(xmlSynonymFilePath);
            try
            {
                while ((line = file.ReadLine()) != null)
                {
                    var wordForms = line.Split(';');

                    System.Diagnostics.Debug.Assert(wordForms.Length == 2);

                    AddSynomymTerms(wordForms[0], wordForms[1]);
                    AddSynomymTerms(wordForms[1], wordForms[0]);
                }
            }
            catch (Exception) { }
            finally
            {
                file.Close();
            }

            /*// create an xml document object, and load it from the specified file.
            XmlDocument Doc = new XmlDocument();
            Doc.Load(xmlSynonymFilePath);

            // get all the <group> nodes
            var groupNodes = Doc.SelectNodes("/synonyms/group");

            //enumerate groups
            foreach (XmlNode g in groupNodes)
            {
                //get all the <syn> elements from the group nodes.
                XmlNodeList synNodes = g.SelectNodes("child::syn");

                //create a list that will hold the items for this group
                List<string> synonymGroupList = new List<string>();

                //enumerate then and add them to the list,
                //and add each synonym group to the list
                foreach (XmlNode synNode in g)
                {
                    synonymGroupList.Add(synNode.InnerText.Trim());
                }

                //add single synonm group to the list of synonm groups.
                SynonymGroups.Add(new ReadOnlyCollection<string>(synonymGroupList));
            }

            // clear the xml document
            Doc = null;*/
        }

        private void AddSynomymTerms(string term1, string term2)
        {
            if (!terms.ContainsKey(term1))
                terms.Add(term1, new HashSet<string>() { term1, term2 });
            else if(!terms[term1].Contains(term2))
                terms[term1].Add(term2);
        }

        #region ISynonymEngine Members

        public string[] getSynonyms(string word)
        {
            //enumerate all the synonym groups
            foreach (var synonymGroup in SynonymGroups)
            {
                //if the word is a part of the group return 
                //the group as the results.
                if (synonymGroup.Contains(word))
                {
                    //gonna use a read only collection for security purposes
                    return synonymGroup.ToArray();
                }
            }

            return null;
        }

        public List<string> getAcordoOrtogWord(string word)
        {
            if (!terms.ContainsKey(word)) return null;
            return terms[word].ToList();
        }

        #endregion
    }
}
