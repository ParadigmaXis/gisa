using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Xml;

namespace GISA.Fedora.FedoraHandler
{
    public class DublinCoreExporter {
        private string title;
        private string type;
        private string[] subjects;
        private List<string> ids;
        private List<string> parts = new List<string>();
        private bool daddy;

        public DublinCoreExporter(ObjDigSimples doc) {
            this.title = doc.titulo;
            this.type = doc.tipologia;
            this.ids = new List<string>() { doc.pid, doc.gisa_id };
            if(doc.parentDocumentTitle != null) 
                this.parts.Add(doc.parentDocumentTitle);
            this.subjects = doc.assuntos.ToArray();
            this.daddy = false;
        }

        public DublinCoreExporter(ObjDigComposto doc)
        {
            this.title = doc.titulo;
            this.type = doc.tipologia;
            this.ids = new List<string>() { doc.pid, doc.gisa_id };
            this.parts.AddRange(doc.objSimples.Select(simples => simples.titulo));
            this.subjects = doc.assuntos.ToArray();
            this.daddy = true;
        }

        public XmlDocument DublinCore {
            get {
                var prefix = this.daddy ? "hasPart " : "isPartOf ";

                DublinCoreFile dc = new DublinCoreFile();
                dc.AddElement(DublinCoreElement.Title, title);
                dc.AddElement(DublinCoreElement.Type, type);
                foreach (string id in ids) dc.AddElement(DublinCoreElement.Identifier, id);
                foreach (string name in parts) dc.AddElement(DublinCoreElement.Relation, prefix + name);
                if (this.subjects != null && this.subjects.Count() > 0)
                {
                    foreach(string subject in this.subjects) dc.AddElement(DublinCoreElement.Subject, subject);
                }
                return dc.Serialize();
            }
        }
    }

    public class MetsExporter {
        private string userName;
        private MetsFile mets;
        private List<ObjDigSimples> sonPIDs = new List<ObjDigSimples>();

        public MetsExporter(ObjDigComposto document, string owner, string[] logMessage)
        {
            this.userName = owner;
            this.sonPIDs = document.objSimples;
            DoTheMETS(document, logMessage);
        }

        public MetsExporter(ObjDigSimples document, string owner, string[] logMessage)
        {
            this.userName = owner;
            DoTheMETS(document, logMessage);
        }

        public XmlDocument METS {
            get { return mets.Serialize(); }
        }

        public void Export(string path) {
            mets.Serialize(path);
        }

        private void DoTheMETS(ObjDigital obj, string[] logMessage) {

            var idsGisa = new string[] {obj.gisa_id};
            var title = obj.titulo;
            var type = obj.tipologia;

            // Ficheiro METS
            mets = new MetsFile(null, null, title, type, null);

            // Header - criador, hora de criação, e IDs alternativos
            mets.header = new MetsHeaderSection(null, null, Utility.Now(), null, null);
            mets.header.AddAgent(new MetsAgent(null, MetsAgentRole.Archivist, null, MetsAgentType.Individual, null, userName, logMessage));
            foreach (string s in idsGisa.Where(id => id.Length > 0))
            {
                string typeID = s.Substring(0, s.IndexOf(':'));
                mets.header.AddAltID(new MetsAlternativeIdentifier(null, typeID, s));
            }

            // Criar a secção de Ficheiros
            mets.fileSection = new MetsFileSection(null);
            MetsFileGroup fileGrp = new MetsFileGroup(null, null, null, "Conteúdos");
            mets.fileSection.AddFileGroupElement(fileGrp);

            MetsStructMap structa = null;
            MetsStructDivision largeFile = null;
            if (obj.GetType() == typeof(ObjDigSimples))
            {
                ObjDigSimples objSimples = obj as ObjDigSimples;
                structa = new MetsStructMap(null, "PHYSICAL", "Estruturação em imagens do " + title);
                largeFile = new MetsStructDivision(null, type, title, null, null, null, null, null, null); 

                for(int i = 0; i < objSimples.fich_associados.Count; i++)
                {
                    Anexo ficheiro = objSimples.fich_associados[i];

                    if (ficheiro.dataStreamID == null)
                    {
                        ficheiro.dataStreamID = "IMG" + objSimples.nextDatastreamId;
                        objSimples.nextDatastreamId++;
                    }
                    string uName = ficheiro.dataStreamID;

                    MetsFileElement file = new MetsFileElement(uName, ficheiro.mimeType, (i + 1).ToString(), null, null, null, MetsChecksumType.NONE, null, null, null, null, null);
                    file.AddFLocat(new MetsXLinkElement(null, null, MetsLocatorType.Other, "Nome de Datastream", uName, null, null, null, null, null));
                    fileGrp.AddFileElement(file);

                    MetsStructDivision div = new MetsStructDivision(null, ficheiro.mimeType, title, null, null, (i + 1).ToString(), null, null, null);
                    div.AddFilePointer(new MetsFilePointer(null, uName, null));
                    largeFile.AddDivision(div);
                }
            }
            else
            {
                ObjDigComposto objComposto = obj as ObjDigComposto;
                structa = new MetsStructMap(null, "LOGICAL", "Estruturação em partes do " + title);
                largeFile = new MetsStructDivision(null, type, title, null, null, null, null, null, null);
                foreach (ObjDigSimples sd in objComposto.objSimples) {
                    // Apenas queremos fazer isto se o objecto não estiver marcado como apagado
                    if (sd.state != State.deleted)
                    {
                        string pid = sd.pid;
                        MetsStructDivision div = new MetsStructDivision(null, sd.tipologia, sd.titulo, null, null, (objComposto.objSimples.IndexOf(sd) + 1).ToString(), null, null, null);
                        div.AddFilePointer(new MetsFilePointer(null, pid, null));
                        largeFile.AddDivision(div);
                    }
                }
            }

            structa.AddDivision(largeFile);
            mets.AddStructMap(structa);
        }
    }

    public class FoxmlExporter {
        private string userName, pid, nmspace;
        private FoxmlDigitalObject foxml;
        private List<ObjDigSimples> sonPIDs = new List<ObjDigSimples>();

        public FoxmlExporter(ObjDigComposto document, string owner, string pid, List<ObjDigSimples> sonPIDs, string nmspace) {
            this.userName = owner;
            this.pid = pid;
            this.sonPIDs = sonPIDs;
            this.nmspace = nmspace;
            this.DoTheFOXML(document);
        }

        public FoxmlExporter(ObjDigSimples document, string owner, string pid, string nmspace)
        {
            this.userName = owner;
            this.pid = pid;
            this.nmspace = nmspace;
            this.DoTheFOXML(document);
        }

        private void DoTheFOXML(ObjDigital obj) {
            var title = obj.titulo;

            // Construir os headers
            foxml = new FoxmlDigitalObject("1.1", this.pid);
            foxml.properties = new FoxmlObjectProperties();
            foxml.properties.AddProperty(new FoxmlProperty("info:fedora/fedora-system:def/model#state", "A"));
            foxml.properties.AddProperty(new FoxmlProperty("info:fedora/fedora-system:def/model#label", title));

            // Add the DC datastream
            DublinCoreExporter dcXml;
            MetsExporter metsXml;
            if (obj.GetType() == typeof(ObjDigSimples))
            {
                ObjDigSimples objSimples = obj as ObjDigSimples;
                metsXml = new MetsExporter(objSimples, userName, new string[] { "Ingestão do documento " + this.pid } );
                dcXml = new DublinCoreExporter(obj as ObjDigSimples);
            }
            else
            {
                ObjDigComposto objComposto = obj as ObjDigComposto;
                metsXml = new MetsExporter(objComposto, userName, new string[] { "Ingestão do documento " + this.pid } );
                dcXml = new DublinCoreExporter(obj as ObjDigComposto);
            }

            FoxmlDatastream dcStream = new FoxmlDatastream("DC", FoxmlDatastreamState.Active, FoxmlControlGroup.InlineXML, true);
            FoxmlDatastreamVersion dcStream1 = new FoxmlDatastreamVersion("DC.0", "http://www.openarchives.org/OAI/2.0/oai_dc/", "text/xml", "Metadados em Dublin Core", null, null);
            dcStream1.inlineXML = dcXml.DublinCore;
            dcStream1.contentDigest = new FoxmlContentDigest(FoxmlChecksumTypes.DEFAULT, null);
            dcStream.AddVersion(dcStream1);
            foxml.AddDatastream(dcStream);

            // Add the RELS-EXT datastream
            FoxmlDatastream relsStream = new FoxmlDatastream("RELS-EXT", FoxmlDatastreamState.Active, FoxmlControlGroup.InlineXML, true);
            FoxmlDatastreamVersion relsStream1 = new FoxmlDatastreamVersion("RELS-EXT.0", "info:fedora/fedora-system:FedoraRELSExt-1.0", "application/rdf+xml", "RDF Statements about this object", null, null);
            relsStream1.inlineXML = GetRdf(this.pid, this.nmspace);
            relsStream.AddVersion(relsStream1);
            foxml.AddDatastream(relsStream);

            // Add the METS datastream for this document
            FoxmlDatastream metsStream = new FoxmlDatastream("METS", FoxmlDatastreamState.Active, FoxmlControlGroup.InlineXML, true);
            FoxmlDatastreamVersion metsStream1 = new FoxmlDatastreamVersion("METS.0", "http://www.loc.gov/METS/", "text/xml", "Metadados adicionais em METS", null, null);
            metsStream1.inlineXML = metsXml.METS;
            metsStream1.contentDigest = new FoxmlContentDigest(FoxmlChecksumTypes.DEFAULT, null);
            metsStream.AddVersion(metsStream1);
            foxml.AddDatastream(metsStream);

            // Add the content datastreams
            FoxmlDatastream datastream;
            FoxmlDatastreamVersion dsversion;
            if (obj.GetType() == typeof(ObjDigSimples))
            {
                int cnt = 1;
                ObjDigSimples objSimples = obj as ObjDigSimples;

                foreach (Anexo s in objSimples.fich_associados)
                {
                    string uName = s.dataStreamID;
                    datastream = new FoxmlDatastream(uName, FoxmlDatastreamState.Active, FoxmlControlGroup.ExternallyReferenceContent, true);
                    dsversion = new FoxmlDatastreamVersion(uName + ".0", null, s.mimeType, null, null, null);
                    dsversion.contentLocation = new FoxmlContentLocation(s.url, "URL");
                    dsversion.contentDigest = new FoxmlContentDigest(FoxmlChecksumTypes.DEFAULT, null);
                    datastream.AddVersion(dsversion);
                    foxml.AddDatastream(datastream);
                    cnt++;
                }
            } 
        }

        public XmlDocument FOXML { get { return foxml.Serialize(); } }
        public void Export(string filepath) { foxml.Serialize(filepath); }

        private XmlDocument GetRdf(string pid, string nmspace) {
            XmlDocument doc = new XmlDocument();
            doc.InnerXml = "<rdf:RDF xmlns:rdf=\"http://www.w3.org/1999/02/22-rdf-syntax-ns#\">\n" +
                           "  <rdf:Description rdf:about=\"info:fedora/" + pid + "\">" +
                           "    <hasModel xmlns=\"info:fedora/fedora-system:def/model#\" rdf:resource=\"info:fedora/" + nmspace + ":getPdfCModel\"></hasModel>" +
                           "    <hasModel xmlns=\"info:fedora/fedora-system:def/model#\" rdf:resource=\"info:fedora/" + nmspace + ":getThumbnailCModel\"></hasModel>" +
                           "  </rdf:Description>" +
                           "</rdf:RDF>";
            return doc;
        }
    }
}
