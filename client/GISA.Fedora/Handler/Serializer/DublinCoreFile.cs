using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Collections;
using System.IO;

namespace GISA.Fedora.FedoraHandler
{

    public enum DublinCoreElement { Title, Creator, Subject, Description, Publisher, Contributor, Date, Type, Format, Identifier, Source, Language, Relation, Coverage, Rights };

    [XmlRoot(Namespace = "http://www.openarchives.org/OAI/2.0/oai_dc/", ElementName = "dc")]
    public class DublinCoreFile {

        private ArrayList titles = new ArrayList();
        private ArrayList creators = new ArrayList();
        private ArrayList subjects = new ArrayList();
        private ArrayList descriptions = new ArrayList();
        private ArrayList publishers = new ArrayList();
        private ArrayList contributors = new ArrayList();
        private ArrayList dates = new ArrayList();
        private ArrayList types = new ArrayList();
        private ArrayList formats = new ArrayList();
        private ArrayList identifiers = new ArrayList();
        private ArrayList sources = new ArrayList();
        private ArrayList languages = new ArrayList();
        private ArrayList relations = new ArrayList();
        private ArrayList coverages = new ArrayList();
        private ArrayList rights = new ArrayList();

        [XmlElement(Namespace = "http://purl.org/dc/elements/1.1/", ElementName = "title")]
        public string[] titleList {
            get { return Get(titles); }
            set { Set(value, titles); }
        }

        [XmlElement(Namespace = "http://purl.org/dc/elements/1.1/", ElementName = "creator")]
        public string[] creatorList {
            get { return Get(creators); }
            set { Set(value, creators); }
        }

        [XmlElement(Namespace = "http://purl.org/dc/elements/1.1/", ElementName = "subject")]
        public string[] subjectList {
            get { return Get(subjects); }
            set { Set(value, subjects); }
        }

        [XmlElement(Namespace = "http://purl.org/dc/elements/1.1/", ElementName = "description")]
        public string[] descriptionList {
            get { return Get(descriptions); }
            set { Set(value, descriptions); }
        }

        [XmlElement(Namespace = "http://purl.org/dc/elements/1.1/", ElementName = "publisher")]
        public string[] publisherList {
            get { return Get(publishers); }
            set { Set(value, publishers); }
        }

        [XmlElement(Namespace = "http://purl.org/dc/elements/1.1/", ElementName = "contributor")]
        public string[] contributorList {
            get { return Get(contributors); }
            set { Set(value, contributors); }
        }

        [XmlElement(Namespace = "http://purl.org/dc/elements/1.1/", ElementName = "date")]
        public string[] dateList {
            get { return Get(dates); }
            set { Set(value, dates); }
        }

        [XmlElement(Namespace = "http://purl.org/dc/elements/1.1/", ElementName = "type")]
        public string[] typeList {
            get { return Get(types); }
            set { Set(value, types); }
        }

        [XmlElement(Namespace = "http://purl.org/dc/elements/1.1/", ElementName = "format")]
        public string[] formatList {
            get { return Get(formats); }
            set { Set(value, formats); }
        }

        [XmlElement(Namespace = "http://purl.org/dc/elements/1.1/", ElementName = "identifier")]
        public string[] identifierList {
            get { return Get(identifiers); }
            set { Set(value, identifiers); }
        }

        [XmlElement(Namespace = "http://purl.org/dc/elements/1.1/", ElementName = "source")]
        public string[] sourceList {
            get { return Get(sources); }
            set { Set(value, sources); }
        }

        [XmlElement(Namespace = "http://purl.org/dc/elements/1.1/", ElementName = "language")]
        public string[] languageList {
            get { return Get(languages); }
            set { Set(value, languages); }
        }

        [XmlElement(Namespace = "http://purl.org/dc/elements/1.1/", ElementName = "relation")]
        public string[] relationList {
            get { return Get(relations); }
            set { Set(value, relations); }
        }

        [XmlElement(Namespace = "http://purl.org/dc/elements/1.1/", ElementName = "coverage")]
        public string[] coverageList {
            get { return Get(coverages); }
            set { Set(value, coverages); }
        }

        [XmlElement(Namespace = "http://purl.org/dc/elements/1.1/", ElementName = "rights")]
        public string[] rightsList {
            get { return Get(rights); }
            set { Set(value, rights); }
        }

        public DublinCoreFile() { }

        public void AddElement(DublinCoreElement element, string value) {
            if (!String.IsNullOrEmpty(value))
            {
                switch (element)
                {
                    case DublinCoreElement.Contributor: contributors.Add(value); break;
                    case DublinCoreElement.Coverage: coverages.Add(value); break;
                    case DublinCoreElement.Creator: creators.Add(value); break;
                    case DublinCoreElement.Date: dates.Add(value); break;
                    case DublinCoreElement.Description: descriptions.Add(value); break;
                    case DublinCoreElement.Format: formats.Add(value); break;
                    case DublinCoreElement.Identifier: identifiers.Add(value); break;
                    case DublinCoreElement.Language: languages.Add(value); break;
                    case DublinCoreElement.Publisher: publishers.Add(value); break;
                    case DublinCoreElement.Relation: relations.Add(value); break;
                    case DublinCoreElement.Rights: rights.Add(value); break;
                    case DublinCoreElement.Source: sources.Add(value); break;
                    case DublinCoreElement.Subject: subjects.Add(value); break;
                    case DublinCoreElement.Title: titles.Add(value); break;
                    case DublinCoreElement.Type: types.Add(value); break;
                }
            }
        }

        public void Serialize(string filepath) {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Encoding = Encoding.UTF8;
            settings.OmitXmlDeclaration = true;
            settings.Indent = true;

            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("oai_dc", "http://www.openarchives.org/OAI/2.0/oai_dc/");
            ns.Add("dc", "http://purl.org/dc/elements/1.1/");

            XmlSerializer serializer = new XmlSerializer(typeof(DublinCoreFile));
            XmlWriter textWriter = XmlTextWriter.Create(@filepath, settings);
            serializer.Serialize(textWriter, this, ns);
            textWriter.Close();
        }

        public XmlDocument Serialize() {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Encoding = Encoding.UTF8;
            settings.OmitXmlDeclaration = true;
            settings.Indent = true;

            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("oai_dc", "http://www.openarchives.org/OAI/2.0/oai_dc/");
            ns.Add("dc", "http://purl.org/dc/elements/1.1/");

            MemoryStream stream = new MemoryStream();
            XmlSerializer serializer = new XmlSerializer(typeof(DublinCoreFile));
            XmlWriter textWriter = XmlTextWriter.Create(stream, settings);
            serializer.Serialize(textWriter, this, ns);
            textWriter.Flush();
            stream.Position = 0;

            XmlDocument xml = new XmlDocument();
            xml.Load(stream);
            textWriter.Close();
            
            return xml;
        }

        private string[] Get(ArrayList list) {
            string[] items = new string[list.Count];
            list.CopyTo(items);
            return items;
        }

        private void Set(object value, ArrayList list) {
            if (value == null) return;
            string[] items = (string[])value;
            list.Clear();
            foreach (string item in items)
                list.Add(item);
        }
    }
}
