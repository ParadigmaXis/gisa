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

    public enum FoxmlControlGroup { InlineXML, ManagedContent, ExternallyReferenceContent, RedirectedContent }
    public enum FoxmlDatastreamState { Active, Inactive, Deleted }
    public enum FoxmlChecksumTypes { DISABLED, DEFAULT, MD5, SHA1, SHA256, SHA384, SHA512 };

    [XmlRoot(Namespace = "info:fedora/fedora-system:def/foxml#", ElementName = "digitalObject")]
    public class FoxmlDigitalObject
    {
        private ArrayList datastreamList;

        [XmlAttribute("VERSION")]
        public string version;
        [XmlAttribute("PID")]
        public string pid;

        [XmlElement("objectProperties")]
        public FoxmlObjectProperties properties;
        [XmlElement("datastream")]
        public FoxmlDatastream[] datastreams {
            get {
                FoxmlDatastream[] items = new FoxmlDatastream[datastreamList.Count];
                datastreamList.CopyTo(items);
                return items;
            }
            set {
                if (value == null) return;
                FoxmlDatastream[] items = (FoxmlDatastream[])value;
                datastreamList.Clear();
                foreach (FoxmlDatastream item in items)
                    datastreamList.Add(item);
            }
        }

        public FoxmlDigitalObject() {
            this.pid = null;
            this.version = null;
            properties = new FoxmlObjectProperties();
            datastreamList = new ArrayList();
        }

        public FoxmlDigitalObject(string version, string pid) {
            this.version = version;
            this.pid = pid;
            properties = new FoxmlObjectProperties();
            datastreamList = new ArrayList();
        }

        public void AddDatastream(FoxmlDatastream datastream) {
            if(!datastreamList.Contains(datastream))
                datastreamList.Add(datastream);
        }

        public void RemoveDatastream(FoxmlDatastream datastream) {
            if(datastreamList.Contains(datastream))
                datastreamList.Remove(datastream);
        }

        public void Serialize(string filepath) {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Encoding = Encoding.UTF8;
            settings.OmitXmlDeclaration = false;
            settings.Indent = true;

            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("foxml", "info:fedora/fedora-system:def/foxml#");
            ns.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance");
            ns.Add("schemaLocation", "info:fedora/fedora-system:def/foxml# http://www.fedora.info/definitions/1/0/foxml1-1.xsd");

            XmlSerializer serializer = new XmlSerializer(typeof(FoxmlDigitalObject));
            XmlWriter textWriter = XmlTextWriter.Create(@filepath, settings);
            serializer.Serialize(textWriter, this, ns);
            textWriter.Close();
        }

        public XmlDocument Serialize() {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Encoding = Encoding.UTF8;
            settings.OmitXmlDeclaration = false;
            settings.Indent = true;

            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("foxml", "info:fedora/fedora-system:def/foxml# http://www.fedora.info/definitions/1/0/foxml1-1.xsd");

            MemoryStream stream = new MemoryStream();
            XmlSerializer serializer = new XmlSerializer(typeof(FoxmlDigitalObject));
            XmlWriter textWriter = XmlTextWriter.Create(stream, settings);
            serializer.Serialize(textWriter, this, ns);
            textWriter.Flush();
            stream.Position = 0;

            XmlDocument xml = new XmlDocument();
            xml.Load(stream);
            textWriter.Close();

            return xml;
        }
    }

    public class FoxmlObjectProperties 
    {
        private ArrayList propertiesList;

        [XmlElement("property")]
        public FoxmlProperty[] properties {
            get {
                FoxmlProperty[] items = new FoxmlProperty[propertiesList.Count];
                propertiesList.CopyTo(items);
                return items;
            }
            set {
                if (value == null) return;
                FoxmlProperty[] items = (FoxmlProperty[])value;
                propertiesList.Clear();
                foreach (FoxmlProperty item in items)
                    propertiesList.Add(item);
            } 
        }


        public FoxmlObjectProperties() {
            propertiesList = new ArrayList();
        }

        public void AddProperty(FoxmlProperty property) {
            if(!propertiesList.Contains(property))
                propertiesList.Add(property);
        }

        public void RemoveProperty(FoxmlProperty property) {
            if (propertiesList.Contains(property))
                propertiesList.Remove(property);
        }
    }

    public class FoxmlProperty 
    {
        [XmlAttribute("NAME")]
        public string name;
        [XmlAttribute("VALUE")]
        public string value;

        public FoxmlProperty() {
            name = null;
            value = null;
        }

        public FoxmlProperty(string name, string value) {
            this.name = name;
            this.value = value;
        }
    }

    public class FoxmlDatastream {
        private ArrayList versionsList;

        [XmlAttribute("ID")]
        public string id;
        [XmlAttribute("STATE")]
        public string state;
        [XmlAttribute("CONTROL_GROUP")]
        public string controlGroup;
        [XmlAttribute("VERSIONABLE")]
        public bool versionable;

        [XmlElement("datastreamVersion")]
        public FoxmlDatastreamVersion[] versions {
            get {
                FoxmlDatastreamVersion[] items = new FoxmlDatastreamVersion[versionsList.Count];
                versionsList.CopyTo(items);
                return items;
            }
            set {
                if (value == null) return;
                FoxmlDatastreamVersion[] items = (FoxmlDatastreamVersion[])value;
                versionsList.Clear();
                foreach (FoxmlDatastreamVersion item in items)
                    versionsList.Add(item);
            }
        }

        public FoxmlDatastream() {
            id = null;
            state = null;
            controlGroup = null;
            versionable = false;
            versionsList = new ArrayList();
        }

        public FoxmlDatastream(string id, FoxmlDatastreamState state, FoxmlControlGroup controlGroup, bool versionable) {
            this.id = id;
            switch (state) {
                case FoxmlDatastreamState.Active: this.state = "A"; break;
                case FoxmlDatastreamState.Deleted: this.state = "D"; break;
                case FoxmlDatastreamState.Inactive: this.state = "I"; break;
            }
            switch(controlGroup) {
                case FoxmlControlGroup.ExternallyReferenceContent: this.controlGroup = "E"; break;
                case FoxmlControlGroup.InlineXML: this.controlGroup = "X"; break;
                case FoxmlControlGroup.ManagedContent: this.controlGroup = "M"; break;
                case FoxmlControlGroup.RedirectedContent: this.controlGroup = "R"; break;
            }
            this.versionable = versionable;
            this.versionsList = new ArrayList();
        }

        public void AddVersion(FoxmlDatastreamVersion version) {
            if (!versionsList.Contains(version))
                versionsList.Add(version);
        }

        public void RemoveVersion(FoxmlDatastreamVersion version) {
            if (versionsList.Contains(version))
                versionsList.Remove(version);
        }
    }

    public class FoxmlContentDigest
    {
        [XmlAttribute("TYPE")]
        public string type;
        [XmlAttribute("DIGEST")]
        public string digest;

        public FoxmlContentDigest()
        {
            type = null;
            digest = null;
        }

        public FoxmlContentDigest(FoxmlChecksumTypes type, string digest)
        {
            switch (type)
            {
                case FoxmlChecksumTypes.DEFAULT: this.type = "MD5"; break;
                case FoxmlChecksumTypes.DISABLED: this.type = "DISABLED"; break;
                case FoxmlChecksumTypes.MD5: this.type = "MD5"; break;
                case FoxmlChecksumTypes.SHA1: this.type = "SHA-1"; break;
                case FoxmlChecksumTypes.SHA256: this.type = "SHA-256"; break;
                case FoxmlChecksumTypes.SHA384: this.type = "SHA-384"; break;
                case FoxmlChecksumTypes.SHA512: this.type = "SHA-512"; break;
                default: this.type = "DISABLED"; break;
            }
            this.digest = digest;
        }
    }

    public class FoxmlDatastreamVersion {
        [XmlAttribute("ID")]
        public string id;
        [XmlAttribute("FORMAT_URI")]
        public string formatURI;
        [XmlAttribute("MIMETYPE")]
        public string mimeType;
        [XmlAttribute("LABEL")]
        public string label;
        [XmlAttribute("SIZE")]
        public string size = null;
        [XmlAttribute("CREATED")]
        public string creationTime = null;

        // Optional digest element
        [XmlElement("contentDigest")]
        public FoxmlContentDigest contentDigest = null;

        // Optional elements. These should bet all set to null, except for the desired one
        [XmlElement("xmlContent")]
        public XmlDocument inlineXML = null;
        [XmlElement("contentLocation")]
        public FoxmlContentLocation contentLocation = null;

        public FoxmlDatastreamVersion() {
            id = null;
            formatURI = null;
            mimeType = null;
            label = null;
            size = null;
            creationTime = null;
        }

        public FoxmlDatastreamVersion(string id, string formatURI, string mimeType, string label, string size, string creationTime) {
            this.id = id;
            this.formatURI = formatURI;
            this.mimeType = mimeType;
            this.label = label;
            this.size = size;
            this.creationTime = creationTime;
        }
    }

    public class FoxmlContentLocation {
        [XmlAttribute("REF")]
        public string url;
        [XmlAttribute("TYPE")]
        public string type;

        public FoxmlContentLocation() {
            url = null;
            type = null;
        }

        public FoxmlContentLocation(string url, string type) {
            this.url = url;
            this.type = type;
        }
    }
}
