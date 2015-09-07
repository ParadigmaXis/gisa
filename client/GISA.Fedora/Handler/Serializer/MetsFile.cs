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

    public enum MetsAgentRole { Creator, Editor, Archivist, Preservation, Disseminator, Custodian, IPOwner, Other }
    public enum MetsAgentType { Individual, Organization, Other }
    public enum MetsLocatorType { ARK, URN, URL, PURL, HANDLE, DOI, Other }
    public enum MetsMetadataType { MARC, MODS, EAD, DC, NISOIMG, LCAV, VRA, TEIHDR, DDL, FGDC, PREMIS, Other }
    public enum MetsAmdMetadataType { Technical, Rights, Source, DigiProv }
    public enum MetsChecksumType { HAVAL, MD5, SHA256, SHA384, SHA512, TIGER, WHIRLPOOL, NONE }
    public enum MetsExtentType { BYTE, IDREF, SMIL, MIDI, SMPTE25, SMPTE24, SMPTEDF30, SMPTENDF30, SMPTEDF2997, SMPTENDF2997, TIME, TCF}

    [XmlRoot(Namespace = "http://www.loc.gov/METS/", ElementName = "mets")]
    public class MetsFile {

        private ArrayList dmdList = new ArrayList();
        private ArrayList amdList = new ArrayList();
        private ArrayList structList = new ArrayList();
        private ArrayList structLinkList = new ArrayList();

        [XmlAttribute("ID")]
        public string id = null;
        [XmlAttribute("OBJID")]
        public string objid = null;
        [XmlAttribute("LABEL")]
        public string label = null;
        [XmlAttribute("TYPE")]
        public string type = null;
        [XmlAttribute("PROFILE")]
        public string profile = null;

        [XmlElement("metsHdr")]
        public MetsHeaderSection header = null;

        [XmlElement("dmdSec")]
        public MetsDmdSection[] dmdSection {
            get {
                MetsDmdSection[] items = new MetsDmdSection[dmdList.Count];
                dmdList.CopyTo(items);
                return items;
            }
            set {
                if (value == null) return;
                MetsDmdSection[] items = (MetsDmdSection[])value;
                dmdList.Clear();
                foreach (MetsDmdSection item in items)
                    dmdList.Add(item);
            }
        }

        [XmlElement("amdSec")]
        public MetsAmdSection[] amdSection {
            get {
                MetsAmdSection[] items = new MetsAmdSection[amdList.Count];
                amdList.CopyTo(items);
                return items;
            }
            set {
                if (value == null) return;
                MetsAmdSection[] items = (MetsAmdSection[])value;
                amdList.Clear();
                foreach (MetsAmdSection item in items)
                    amdList.Add(item);
            }
        }

        [XmlElement("fileSec")]
        public MetsFileSection fileSection = null;

        [XmlElement("structMap")]
        public MetsStructMap[] structMaps {
            get {
                MetsStructMap[] items = new MetsStructMap[structList.Count];
                structList.CopyTo(items);
                return items;
            }
            set {
                if (value == null) return;
                MetsStructMap[] items = (MetsStructMap[])value;
                structList.Clear();
                foreach (MetsStructMap item in items)
                    structList.Add(item);
            }
        }

        [XmlElement("structLink")]
        public MetsStructuralLink structuralLinkSection = null;

        [XmlElement("behaviorSec")]
        public MetsBehaviorSection behaviorSection = null;

        public MetsFile() { }

        public MetsFile(string id, string objid, string label, string type, string profile) {
            this.id = id;
            this.objid = objid;
            this.label = label;
            this.type = type;
            this.profile = profile;
        }

        public void AddDmdSection(MetsDmdSection dmdSection) { if (!dmdList.Contains(dmdSection)) dmdList.Add(dmdSection); }
        public void RemoveDmdSection(MetsDmdSection dmdSection) { if (dmdList.Contains(dmdSection)) dmdList.Remove(dmdSection); }

        public void AddAmdSection(MetsAmdSection amdSection) { if (!amdList.Contains(amdSection)) amdList.Add(amdSection); }
        public void RemoveAmdSection(MetsAmdSection amdSection) { if (amdList.Contains(amdSection)) amdList.Remove(amdSection); }

        public void AddStructMap(MetsStructMap structMap) { if (!structList.Contains(structMap)) structList.Add(structMap); }
        public void RemoveStructMap(MetsStructMap structMap) { if (structList.Contains(structMap)) structList.Remove(structMap); }

        public void Serialize(string filepath) {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Encoding = Encoding.UTF8;
            settings.OmitXmlDeclaration = true;
            settings.Indent = true;

            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("mets", "http://www.loc.gov/METS/");
            ns.Add("xlink", "http://www.w3.org/TR/xlink/");

            XmlSerializer serializer = new XmlSerializer(typeof(MetsFile));
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
            ns.Add("mets", "http://www.loc.gov/METS/");
            ns.Add("xlink", "http://www.w3.org/TR/xlink/");

            MemoryStream stream = new MemoryStream();
            XmlSerializer serializer = new XmlSerializer(typeof(MetsFile));
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

    /*------------------------------------------------------------------------
     *  MetsHdr Section
     *  
     *  A MetsFile instance has one MetsHeaderSection. This class may in turn
     *  contain a list of MetsAgent and a list of MetsAlternativeIdentifier.
     * -----------------------------------------------------------------------*/

    public class MetsHeaderSection {

        private ArrayList agentList = new ArrayList();
        private ArrayList altIDList = new ArrayList();

        [XmlAttribute("ID")]
        public string id = null;
        [XmlAttribute("ADMID")]
        public string admid = null;
        [XmlAttribute("CREATEDATE")]
        public string createdate = null;
        [XmlAttribute("LASTMODDATE")]
        public string lastmoddate = null;
        [XmlAttribute("RECORDSTATUS")]
        public string recordstatus = null;

        [XmlElement("agent")]
        public MetsAgent[] agents {
            get {
                MetsAgent[] items = new MetsAgent[agentList.Count];
                agentList.CopyTo(items);
                return items;
            }
            set {
                if (value == null) return;
                MetsAgent[] items = (MetsAgent[])value;
                agentList.Clear();
                foreach (MetsAgent item in items)
                    agentList.Add(item);
            }
        }

        [XmlElement("altRecordID")]
        public MetsAlternativeIdentifier[] alternativeIDs {
            get {
                MetsAlternativeIdentifier[] items = new MetsAlternativeIdentifier[altIDList.Count];
                altIDList.CopyTo(items);
                return items;
            }
            set {
                if (value == null) return;
                MetsAlternativeIdentifier[] items = (MetsAlternativeIdentifier[])value;
                altIDList.Clear();
                foreach (MetsAlternativeIdentifier item in items)
                    altIDList.Add(item);
            }
        }

        public MetsHeaderSection() { }

        public MetsHeaderSection(string id, string admid, string createdate, string lastmoddate, string recordstatus) {
            this.id = id;
            this.admid = admid;
            this.createdate = createdate;
            this.lastmoddate = lastmoddate;
            this.recordstatus = recordstatus;
        }

        public void AddAgent(MetsAgent agent) { if(!agentList.Contains(agent)) agentList.Add(agent); }
        public void RemoveAgent(MetsAgent agent) { if (agentList.Contains(agent)) agentList.Remove(agent); }

        public void AddAltID(MetsAlternativeIdentifier altID) { if (!altIDList.Contains(altID)) altIDList.Add(altID); }
        public void RemoveAltID(MetsAlternativeIdentifier altID) { if (altIDList.Contains(altID)) altIDList.Remove(altID); }
    }

    public class MetsAgent {
        private ArrayList noteList = new ArrayList();

        [XmlAttribute("ID")]
        public string id = null;
        [XmlAttribute("ROLE")]
        public string role;
        [XmlAttribute("OTHERROLE")]
        public string otherrole = null;
        [XmlAttribute("TYPE")]
        public string type = null;
        [XmlAttribute("OTHERTYPE")]
        public string othertype = null;

        [XmlElement("name")]
        public string name = null;
        [XmlElement("note")]
        public string[] notes
        {
            get
            {
                string[] items = new string[noteList.Count];
                noteList.CopyTo(items);
                return items;
            }
            set
            {
                if (value == null) return;
                string[] items = (string[])value;
                noteList.Clear();
                foreach (string item in items)
                    noteList.Add(item);
            }
        }


        public MetsAgent() {
            role = "IPOwner";
        }

        public MetsAgent(string id, MetsAgentRole role, string otherrole, MetsAgentType type, string othertype, string name, string[] notes) {
            this.id = id;
            this.role = role.ToString().ToUpper();
            this.otherrole = otherrole;
            this.type = type.ToString().ToUpper();
            this.othertype = othertype;

            // Elements
            this.name = name;
            this.notes = notes;
        }

        public void AddNote(string note) { if (!noteList.Contains(note)) noteList.Add(note); }
        public void RemoveNote(string note) { if (noteList.Contains(note)) noteList.Remove(note); }
    }

    public class MetsAlternativeIdentifier {
        [XmlAttribute("ID")]
        public string id = null;
        [XmlAttribute("TYPE")]
        public string type = null; 

        [XmlText()]
        public string altID = null;

        public MetsAlternativeIdentifier() { }

        public MetsAlternativeIdentifier(string id, string type, string altID) {
            this.id = id;
            this.type = type;
            this.altID = altID;
        }
    }

    /*------------------------------------------------------------------------
     *  MetsDmd Section
     *  
     *  ???
     * -----------------------------------------------------------------------*/

    public class MetsDmdSection {
        [XmlAttribute("ID")]
        public string id = null;
        [XmlAttribute("GROUPID")]
        public string groupid = null;
        [XmlAttribute("ADMID")]
        public string admid = null;
        [XmlAttribute("CREATED")]
        public string created = null;
        [XmlAttribute("STATUS")]
        public string status = null;

        [XmlElement("mdRef")]
        public MetsMetadataReference refElement = null;
        [XmlElement("mdWrap")]
        public MetsMetadataWrapper wrapElement = null;

        public MetsDmdSection() { }

        public MetsDmdSection(string id, string groupid, string admid, string created, string status) {
            this.id = id;
            this.groupid = groupid;
            this.admid = admid;
            this.created = created;
            this.status = status;
        }
    }

    /*------------------------------------------------------------------------
     *  MetsAmd Section
     *  
     *  ???
     * -----------------------------------------------------------------------*/

    public class MetsAmdSection {
        private ArrayList techList = new ArrayList();
        private ArrayList rightsList = new ArrayList();
        private ArrayList sourceList = new ArrayList();
        private ArrayList digiprovList = new ArrayList();

        [XmlAttribute("ID")]
        public string id = null;

        [XmlElement("techMD")]
        public MetsAmdMetadata[] techMetadata {
            get { return Get(techList); }
            set { Set(value, techList); }
        }

        [XmlElement("rightsMD")]
        public MetsAmdMetadata[] rightsMetadata {
            get { return Get(rightsList); }
            set { Set(value, rightsList); }
        }

        [XmlElement("sourceMD")]
        public MetsAmdMetadata[] sourceMetadata {
            get { return Get(sourceList); }
            set { Set(value, sourceList); }
        }

        [XmlElement("digiprovMD")]
        public MetsAmdMetadata[] digiprovMetadata {
            get { return Get(digiprovList); }
            set { Set(value, digiprovList); }
        }

        public MetsAmdSection() { }
        public MetsAmdSection(string id) { this.id = id; }

        public void AddMetadataSection(MetsAmdMetadataType type, MetsAmdMetadata metadata) {
            switch(type) {
                case MetsAmdMetadataType.DigiProv: if(!digiprovList.Contains(metadata)) digiprovList.Add(metadata); break;
                case MetsAmdMetadataType.Rights: if (!rightsList.Contains(metadata)) rightsList.Add(metadata); break;
                case MetsAmdMetadataType.Source: if (!sourceList.Contains(metadata)) sourceList.Add(metadata); break;
                case MetsAmdMetadataType.Technical: if (!techList.Contains(metadata)) techList.Add(metadata); break;
            }
        }

        public void RemoveMetadataSection(MetsAmdMetadataType type, MetsAmdMetadata metadata) {
            switch (type) {
                case MetsAmdMetadataType.DigiProv: if (digiprovList.Contains(metadata)) digiprovList.Remove(metadata); break;
                case MetsAmdMetadataType.Rights: if (rightsList.Contains(metadata)) rightsList.Remove(metadata); break;
                case MetsAmdMetadataType.Source: if (sourceList.Contains(metadata)) sourceList.Remove(metadata); break;
                case MetsAmdMetadataType.Technical: if (techList.Contains(metadata)) techList.Remove(metadata); break;
            }
        }

        private MetsAmdMetadata[] Get(ArrayList list) {
            MetsAmdMetadata[] items = new MetsAmdMetadata[list.Count];
            list.CopyTo(items);
            return items;
        }

        private void Set(object value, ArrayList list) {
            if (value == null) return;
            MetsAmdMetadata[] items = (MetsAmdMetadata[])value;
            list.Clear();
            foreach (MetsAmdMetadata item in items)
                list.Add(item);
        }
    }

    public class MetsAmdMetadata{
        [XmlAttribute("ID")]
        public string id = null;
        [XmlAttribute("GROUPID")]
        public string groupid = null;
        [XmlAttribute("ADMID")]
        public string admid = null;
        [XmlAttribute("CREATED")]
        public string created = null;
        [XmlAttribute("STATUS")]
        public string status = null;

        [XmlElement("mdRef")]
        public MetsMetadataReference refElement = null;
        [XmlElement("mdWrap")]
        public MetsMetadataWrapper wrapElement = null;

        public MetsAmdMetadata() { }

        public MetsAmdMetadata(string id, string groupid, string admid, string created, string status) {
            this.id = id;
            this.groupid = groupid;
            this.admid = admid;
            this.created = created;
            this.status = status;
        }
    }

    /*------------------------------------------------------------------------
     *  MetsFile Section
     *  
     *  ???
     * -----------------------------------------------------------------------*/

    public class MetsFileSection {
        private ArrayList fileGroupList = new ArrayList();
        
        [XmlAttribute("ID")]
        public string id = null;

        [XmlElement("fileGrp")]
        public MetsFileGroup[] fileGroups {
            get {
                MetsFileGroup[] items = new MetsFileGroup[fileGroupList.Count];
                fileGroupList.CopyTo(items);
                return items;
            }
            set {
                if (value == null) return;
                MetsFileGroup[] items = (MetsFileGroup[])value;
                fileGroupList.Clear();
                foreach (MetsFileGroup item in items)
                    fileGroupList.Add(item);
            }
        }

        public MetsFileSection() { }
        public MetsFileSection(string id) { this.id = id; }

        public void AddFileGroupElement(MetsFileGroup fileGroup) { if (!fileGroupList.Contains(fileGroup)) fileGroupList.Add(fileGroup); }
        public void RemoveFileGroupElement(MetsFileGroup fileGroup) { if (fileGroupList.Contains(fileGroup)) fileGroupList.Remove(fileGroup); }
    }

    public class MetsFileGroup {
        private ArrayList fileList = new ArrayList();
        private ArrayList fileGroupList = new ArrayList();

        [XmlAttribute("ID")]
        public string id = null;
        [XmlAttribute("VERSDATE")]
        public string versdate = null;
        [XmlAttribute("ADMID")]
        public string admid = null;
        [XmlAttribute("USE")]
        public string use = null;

        [XmlElement("fileGrp")]
        public MetsFileGroup[] fileGroups {
            get {
                MetsFileGroup[] items = new MetsFileGroup[fileGroupList.Count];
                fileGroupList.CopyTo(items);
                return items;
            }
            set {
                if (value == null) return;
                MetsFileGroup[] items = (MetsFileGroup[])value;
                fileGroupList.Clear();
                foreach (MetsFileGroup item in items)
                    fileGroupList.Add(item);
            }
        }

        [XmlElement("file")]
        public MetsFileElement[] files {
            get {
                MetsFileElement[] items = new MetsFileElement[fileList.Count];
                fileList.CopyTo(items);
                return items;
            }
            set {
                if (value == null) return;
                MetsFileElement[] items = (MetsFileElement[])value;
                fileList.Clear();
                foreach (MetsFileElement item in items)
                    fileList.Add(item);
            }
        }

        public MetsFileGroup() { }

        public MetsFileGroup(string id, string versdate, string admid, string use) {
            this.id = id;
            this.versdate = versdate;
            this.admid = admid;
            this.use = use;
        }


        public void AddFileElement(MetsFileElement file) { if (!fileList.Contains(file)) fileList.Add(file); }
        public void RemoveFileElement(MetsFileElement file) { if (fileList.Contains(file)) fileList.Remove(file); }

        public void AddFileGroupElement(MetsFileGroup fileGroup) { if (!fileGroupList.Contains(fileGroup)) fileGroupList.Add(fileGroup); }
        public void RemoveFileGroupElement(MetsFileGroup fileGroup) { if (fileGroupList.Contains(fileGroup)) fileGroupList.Remove(fileGroup); }
    }

    public class MetsFileElement {
        private ArrayList fLocatList = new ArrayList();
        private ArrayList fContentList = new ArrayList();
        
        [XmlAttribute("ID")]
        public string id = null;
        [XmlAttribute("MIMETYPE")]
        public string mimetype = null;
        [XmlAttribute("SEQ")]
        public string seq = null;
        [XmlAttribute("SIZE")]
        public string size = null;
        [XmlAttribute("CREATED")]
        public string created = null;
        [XmlAttribute("CHECKSUM")]
        public string checksum = null;
        [XmlAttribute("CHECKSUMTYPE")]
        public string checksumtype = null;
        [XmlAttribute("OWNERID")]
        public string ownerid = null;
        [XmlAttribute("ADMID")]
        public string admid = null;
        [XmlAttribute("DMDID")]
        public string dmdid = null;
        [XmlAttribute("GROUPID")]
        public string groupid = null;
        [XmlAttribute("USE")]
        public string use = null;

        [XmlElement("FLocat")]
        public MetsXLinkElement[] fLocats {
            get {
                MetsXLinkElement[] items = new MetsXLinkElement[fLocatList.Count];
                fLocatList.CopyTo(items);
                return items;
            }
            set {
                if (value == null) return;
                MetsXLinkElement[] items = (MetsXLinkElement[])value;
                fLocatList.Clear();
                foreach (MetsXLinkElement item in items)
                    fLocatList.Add(item);
            }
        }

        [XmlElement("FContent")]
        public MetsFileContent[] fContents {
            get {
                MetsFileContent[] items = new MetsFileContent[fContentList.Count];
                fContentList.CopyTo(items);
                return items;
            }
            set {
                if (value == null) return;
                MetsFileContent[] items = (MetsFileContent[])value;
                fContentList.Clear();
                foreach (MetsFileContent item in items)
                    fContentList.Add(item);
            }
        }

        public MetsFileElement() { id = ""; }

        public MetsFileElement(string id, string mimetype, string seq, string size, string created, string checksum, MetsChecksumType checksumtype, string ownerid, string admid, string dmdid, string groupid, string use) {
            this.id = id;
            this.mimetype = mimetype;
            this.seq = seq;
            this.size = size;
            this.created = created;
            this.checksum = checksum;
            switch (checksumtype) {
                case MetsChecksumType.SHA256: this.checksumtype = "SHA-256"; break;
                case MetsChecksumType.SHA384: this.checksumtype = "SHA-384"; break;
                case MetsChecksumType.SHA512: this.checksumtype = "SHA-512"; break;
                default: this.checksumtype = checksumtype.ToString(); break;
            }
            this.ownerid = ownerid;
            this.admid = admid;
            this.dmdid = dmdid;
            this.groupid = groupid;
            this.use = use;
        }

        public void AddFLocat(MetsXLinkElement fLocat) { if (!fLocatList.Contains(fLocat)) fLocatList.Add(fLocat); }
        public void RemoveFLocat(MetsXLinkElement fLocat) { if (fLocatList.Contains(fLocat)) fLocatList.Remove(fLocat); }

        public void AddFContent(MetsFileContent fContent) { if (!fContentList.Contains(fContent)) fContentList.Add(fContent); }
        public void RemoveFContent(MetsFileContent fContent) { if (fContentList.Contains(fContent)) fContentList.Remove(fContent); }
    }

    public class MetsFileContent {
        [XmlAttribute("ID")]
        public string id = null;
        [XmlAttribute("USE")]
        public string use = null;

        [XmlElement("xmlData")]
        public XmlDocument inlineXML = null;
        [XmlElement("binData")]
        public MetsBinaryData binaryContent = null;

        public MetsFileContent() { }
        public MetsFileContent(string id, string use) { this.id = id; this.use = use; }
    }

    // NOT YET IMPLEMENTED
    public class MetsFileStream { }
    public class MetsFileTransform { }

    /*------------------------------------------------------------------------
     *  MetsStructMap Section
     *  
     *  ???
     * -----------------------------------------------------------------------*/

    public class MetsStructMap {
        private ArrayList divList = new ArrayList();

        [XmlAttribute("ID")]
        public string id = null;
        [XmlAttribute("TYPE")]
        public string type = null;
        [XmlAttribute("LABEL")]
        public string label = null;

        [XmlElement("div")]
        public MetsStructDivision[] divisions {
            get {
                MetsStructDivision[] items = new MetsStructDivision[divList.Count];
                divList.CopyTo(items);
                return items;
            }
            set {
                if (value == null) return;
                MetsStructDivision[] items = (MetsStructDivision[])value;
                divList.Clear();
                foreach (MetsStructDivision item in items)
                    divList.Add(item);
            }
        }

        public MetsStructMap() { }
        public MetsStructMap(string id, string type, string label) {
            this.id = id;
            this.type = type;
            this.label = label;
        }

        public void AddDivision(MetsStructDivision division) { if (!divList.Contains(division)) divList.Add(division); }
        public void RemoveDivision(MetsStructDivision division) { if (divList.Contains(division)) divList.Remove(division); }
    }

    public class MetsStructDivision {
        private ArrayList metsPointerList = new ArrayList();
        private ArrayList filePointerList = new ArrayList();
        private ArrayList divList = new ArrayList();

        [XmlAttribute("ID")]
        public string id = null;
        [XmlAttribute("TYPE")]
        public string type = null;
        [XmlAttribute("LABEL")]
        public string label = null;
        [XmlAttribute("DMDID")]
        public string dmdid = null;
        [XmlAttribute("ADMID")]
        public string admid = null;
        [XmlAttribute("ORDER")]
        public string order = null;
        [XmlAttribute("ORDERLABEL")]
        public string orderlabel = null;
        [XmlAttribute("CONTENTIDS")]
        public string contentids = null;
        [XmlAttribute(Namespace = "http://www.w3.org/TR/xlink/", AttributeName = "label")]
        public string xlinkLabel = null;

        [XmlElement("div")]
        public MetsStructDivision[] divisions {
            get {
                MetsStructDivision[] items = new MetsStructDivision[divList.Count];
                divList.CopyTo(items);
                return items;
            }
            set {
                if (value == null) return;
                MetsStructDivision[] items = (MetsStructDivision[])value;
                divList.Clear();
                foreach (MetsStructDivision item in items)
                    divList.Add(item);
            }
        }

        [XmlElement("fptr")]
        public MetsFilePointer[] filePointers {
            get {
                MetsFilePointer[] items = new MetsFilePointer[filePointerList.Count];
                filePointerList.CopyTo(items);
                return items;
            }
            set {
                if (value == null) return;
                MetsFilePointer[] items = (MetsFilePointer[])value;
                filePointerList.Clear();
                foreach (MetsFilePointer item in items)
                    filePointerList.Add(item);
            }
        }

        [XmlElement("mptr")]
        public MetsXLinkElement[] metsPointers {
            get {
                MetsXLinkElement[] items = new MetsXLinkElement[metsPointerList.Count];
                metsPointerList.CopyTo(items);
                return items;
            }
            set {
                if (value == null) return;
                MetsXLinkElement[] items = (MetsXLinkElement[])value;
                metsPointerList.Clear();
                foreach (MetsXLinkElement item in items)
                    metsPointerList.Add(item);
            }
        }

        public MetsStructDivision() { }

        public MetsStructDivision(string id, string type, string label, string dmdid, string admid, string order, string orderlabel, string contentids, string xlinkLabel) {
            this.id = id;
            this.type = type;
            this.label = label;
            this.dmdid = dmdid;
            this.admid = admid;
            this.order = order;
            this.orderlabel = orderlabel;
            this.contentids = contentids;
            this.xlinkLabel = xlinkLabel;
        }

        public void AddMetsPointer(MetsXLinkElement metsPointer) { if(!metsPointerList.Contains(metsPointer)) metsPointerList.Add(metsPointer); }
        public void RemoveMetsPointer(MetsXLinkElement metsPointer) { if (metsPointerList.Contains(metsPointer)) metsPointerList.Remove(metsPointer); }

        public void AddFilePointer(MetsFilePointer filePointer) { if(!filePointerList.Contains(filePointer)) filePointerList.Add(filePointer); }
        public void RemoveFilePointer(MetsFilePointer filePointer) { if (filePointerList.Contains(filePointer)) filePointerList.Remove(filePointer); }

        public void AddDivision(MetsStructDivision division) { if(!divList.Contains(division)) divList.Add(division); }
        public void RemoveDivision(MetsStructDivision division) { if (divList.Contains(division)) divList.Remove(division); }
    }

    public class MetsFilePointer {
        [XmlAttribute("ID")]
        public string id = null;
        [XmlAttribute("FILEID")]
        public string fileid = null;
        [XmlAttribute("CONTENTIDS")]
        public string contentids = null;

        [XmlElement("area")]
        public MetsArea areaDefinition = null; 
        [XmlElement("seq")]
        public MetsSeq sequenceDefinition = null;
        [XmlElement("par")]
        public MetsPar parallelDefinition = null; 

        public MetsFilePointer() { }

        public MetsFilePointer(string id, string fileid, string contentids) {
            this.id = id;
            this.fileid = fileid;
            this.contentids = contentids;
        }
    }

    public class MetsArea {
        [XmlAttribute("ID")]
        public string id = null;
        [XmlAttribute("FILEID")]
        public string fileid = null;
        [XmlAttribute("SHAPE")]
        public string shape = null;
        [XmlAttribute("COORDS")]
        public string coords = null;
        [XmlAttribute("BEGIN")]
        public string begin = null;
        [XmlAttribute("END")]
        public string end = null;
        [XmlAttribute("BETYPE")]
        public string betype = null;
        [XmlAttribute("EXTENT")]
        public string extent = null;
        [XmlAttribute("EXTYPE")]
        public string extype = null;
        [XmlAttribute("ADMID")]
        public string admid = null;
        [XmlAttribute("CONTENTIDS")]
        public string contentids = null;

        public MetsArea() { }

        public MetsArea(string id, string fileid, string shape, string coords, string begin, string end, string betype, string extent, string extype, string admid, string contentids) {
            this.id = id;
            this.fileid = fileid;
            this.shape = shape;
            this.coords = coords;
            this.begin = begin;
            this.end = end;
            this.betype = betype;
            this.extent = extent;
            this.extype = extype;
            this.admid = admid;
            this.contentids = contentids;
        }
    }

    public class MetsSeq {
        private ArrayList areaList = new ArrayList();

        [XmlAttribute("ID")]
        public string id = null;

        [XmlElement("area")]
        public MetsArea[] areas {
            get {
                MetsArea[] items = new MetsArea[areaList.Count];
                areaList.CopyTo(items);
                return items;
            }
            set {
                if (value == null) return;
                MetsArea[] items = (MetsArea[])value;
                areaList.Clear();
                foreach (MetsArea item in items)
                    areaList.Add(item);
            }
        }

        public MetsSeq() { }
        public MetsSeq(string id) { this.id = id; }

        public void AddArea(MetsArea area) { if(!areaList.Contains(area)) areaList.Add(area); }
        public void RemoveArea(MetsArea area) { if(areaList.Contains(area)) areaList.Remove(area); }
    }
    
    public class MetsPar {
        private ArrayList areaList = new ArrayList();
        private ArrayList sequenceList = new ArrayList();

        [XmlAttribute("ID")]
        public string id = null;

        [XmlElement("area")]
        public MetsArea[] areas {
            get {
                MetsArea[] items = new MetsArea[areaList.Count];
                areaList.CopyTo(items);
                return items;
            }
            set {
                if (value == null) return;
                MetsArea[] items = (MetsArea[])value;
                areaList.Clear();
                foreach (MetsArea item in items)
                    areaList.Add(item);
            }
        }

        [XmlElement("seq")]
        public MetsSeq[] sequences {
            get {
                MetsSeq[] items = new MetsSeq[sequenceList.Count];
                sequenceList.CopyTo(items);
                return items;
            }
            set {
                if (value == null) return;
                MetsSeq[] items = (MetsSeq[])value;
                sequenceList.Clear();
                foreach (MetsSeq item in items)
                    sequenceList.Add(item);
            }
        }

        public MetsPar() { }
        public MetsPar(string id) { this.id = id; }

        public void AddArea(MetsArea area) { if(!areaList.Contains(area)) areaList.Add(area); }
        public void RemoveArea(MetsArea area) { if(areaList.Contains(area)) areaList.Remove(area); }

        public void AddSequence(MetsSeq sequence) { if (!sequenceList.Contains(sequence)) sequenceList.Add(sequence); }
        public void RemoveSequence(MetsSeq sequence) { if (sequenceList.Contains(sequence)) sequenceList.Remove(sequence); }
    }

    /*------------------------------------------------------------------------
     *  MetsStructuralLink Section
     *  
     *  ???
     * -----------------------------------------------------------------------*/

    public class MetsStructuralLink {
        private ArrayList maplinkList = new ArrayList();

        [XmlElement("ID")]
        public string id = null;

        [XmlElement("smLink")]
        public MetsStructuralMapLink[] mapLinks {
            get {
                MetsStructuralMapLink[] items = new MetsStructuralMapLink[maplinkList.Count];
                maplinkList.CopyTo(items);
                return items;
            }
            set {
                if (value == null) return;
                MetsStructuralMapLink[] items = (MetsStructuralMapLink[])value;
                maplinkList.Clear();
                foreach (MetsStructuralMapLink item in items)
                    maplinkList.Add(item);
            }
        }

        public MetsStructuralLink() { }
        public MetsStructuralLink(string id) { this.id = id; }

        public void AddMapLink(MetsStructuralMapLink mapLink) { if (!maplinkList.Contains(mapLink)) maplinkList.Add(mapLink); }
        public void RemoveMapLink(MetsStructuralMapLink mapLink) { if (maplinkList.Contains(mapLink)) maplinkList.Remove(mapLink); }
    }

    public class MetsStructuralMapLink {
        [XmlAttribute("ID")]
        public string id = null;
        [XmlAttribute(Namespace = "http://www.w3.org/TR/xlink/", AttributeName = "arcrole")]
        public string arcrole = null;
        [XmlAttribute(Namespace = "http://www.w3.org/TR/xlink/", AttributeName = "title")]
        public string title = null;
        [XmlAttribute(Namespace = "http://www.w3.org/TR/xlink/", AttributeName = "show")]
        public string show = null;
        [XmlAttribute(Namespace = "http://www.w3.org/TR/xlink/", AttributeName = "actuate")]
        public string actuate = null;
        [XmlAttribute(Namespace = "http://www.w3.org/TR/xlink/", AttributeName = "to")]
        public string to = null;
        [XmlAttribute(Namespace = "http://www.w3.org/TR/xlink/", AttributeName = "from")]
        public string from = null;

        public MetsStructuralMapLink() { }

        public MetsStructuralMapLink(string id, string arcrole, string title, string show, string actuate, string to, string from) {
            this.id = id;
            this.arcrole = arcrole;
            this.title = title;
            this.show = show;
            this.actuate = actuate;
            this.to = to;
            this.from = from;
        }
    }

    /*------------------------------------------------------------------------
     *  MetsBehaviour Section
     *  
     *  ???
     * -----------------------------------------------------------------------*/

    public class MetsBehaviorSection {
        private ArrayList behaviorList = new ArrayList();

        [XmlAttribute("ID")]
        public string id = null;
        [XmlAttribute("CREATED")]
        public string created = null;
        [XmlAttribute("LABEL")]
        public string label = null;

        [XmlElement("behavior")]
        public MetsBehavior[] behaviors {
            get {
                MetsBehavior[] items = new MetsBehavior[behaviorList.Count];
                behaviorList.CopyTo(items);
                return items;
            }
            set {
                if (value == null) return;
                MetsBehavior[] items = (MetsBehavior[])value;
                behaviorList.Clear();
                foreach (MetsBehavior item in items)
                    behaviorList.Add(item);
            }
        }

        public MetsBehaviorSection() { }

        public MetsBehaviorSection(string id, string created, string label) {
            this.id = id;
            this.created = created;
            this.label = label;
        }

        public void AddBehavior(MetsBehavior behavior) { if (!behaviorList.Contains(behavior)) behaviorList.Add(behavior); }
        public void RemoveBehavior(MetsBehavior behavior) { if (behaviorList.Contains(behavior)) behaviorList.Remove(behavior); }
        
    }

    public class MetsBehavior {

        [XmlAttribute("ID")]
        public string id = null;
        [XmlAttribute("STRUCTID")]
        public string structid = null;
        [XmlAttribute("BTYPE")]
        public string btype = null;
        [XmlAttribute("CREATED")]
        public string created = null;
        [XmlAttribute("LABEL")]
        public string label = null;
        [XmlAttribute("GROUPID")]
        public string groupid = null;
        [XmlAttribute("ADMID")]
        public string admid = null;

        [XmlElement("interfaceDef")]
        public MetsXLinkElement interfaceDef = null;
        [XmlElement("mechanism")]
        public MetsXLinkElement mechanism = null;

        public MetsBehavior() { structid = ""; }

        public MetsBehavior(string id, string structid, string btype, string created, string label, string groupid, string admid) {
            this.id = id;
            this.structid = structid;
            this.btype = btype;
            this.created = created;
            this.label = label;
            this.groupid = groupid;
            this.admid = admid;
        }
    }

    /*------------------------------------------------------------------------
     *  Generic METS classes
     *  
     *  ???
     * -----------------------------------------------------------------------*/

    public class MetsMetadataReference {
        [XmlAttribute("ID")]
        public string id = null;
        [XmlAttribute("MIMETYPE")]
        public string mimetype = null;
        [XmlAttribute("LABEL")]
        public string label = null;
        [XmlAttribute("XPTR")]
        public string xptr = null;
        [XmlAttribute("LOCTYPE")]
        public string loctype = null;
        [XmlAttribute("OTHERLOCTYPE")]
        public string otherloctype = null;
        [XmlAttribute("MDTYPE")]
        public string mdtype = null;
        [XmlAttribute("OTHERMDTYPE")]
        public string othermdtype = null;

        [XmlText()]
        public string reference = null;

        public MetsMetadataReference() { }

        public MetsMetadataReference(string reference, string id, string mimetype, string label, string xptr, MetsLocatorType loctype, string otherloctype, MetsMetadataType mdtype, string othermdtype) {
            this.id = id;
            this.mimetype = mimetype;
            this.label = label;
            this.xptr = xptr;
            this.loctype = loctype.ToString().ToUpper();
            this.otherloctype = otherloctype;
            this.mdtype = mdtype.ToString().ToUpper();
            this.othermdtype = othermdtype;
            this.reference = reference;
        }
    }

    public class MetsMetadataWrapper {
        [XmlAttribute("ID")]
        public string id = null;
        [XmlAttribute("MIMETYPE")]
        public string mimetype = null;
        [XmlAttribute("LABEL")]
        public string label = null;
        [XmlAttribute("MDTYPE")]
        public string mdtype = null;
        [XmlAttribute("OTHERMDTYPE")]
        public string othermdtype = null;

        [XmlElement("xmlData")]
        public XmlDocument inlineXML = null;
        [XmlElement("binData")]
        public MetsBinaryData binaryContent = null;

        public MetsMetadataWrapper() {
            this.mdtype = "";
        }

        public MetsMetadataWrapper(string id, string mimetype, string label, MetsMetadataType mdtype, string othermdtype) {
            this.id = id;
            this.mimetype = mimetype;
            this.label = label;
            this.mdtype = mdtype.ToString().ToUpper();
            this.othermdtype = othermdtype;
        }
    }

    public class MetsBinaryData {
        [XmlText()]
        public string binaryData;

        public MetsBinaryData() { }
        public MetsBinaryData(string binaryData) { this.binaryData = binaryData; }
    }

    public class MetsXLinkElement {

        [XmlAttribute("ID")]
        public string id = null;
        [XmlAttribute("LABEL")]
        public string label = null;
        [XmlAttribute("LOCTYPE")]
        public string loctype = null;
        [XmlAttribute("OTHERLOCTYPE")]
        public string otherloctype = null;
        [XmlAttribute(Namespace = "http://www.w3.org/TR/xlink/", AttributeName = "href")]
        public string href = null;
        [XmlAttribute(Namespace = "http://www.w3.org/TR/xlink/", AttributeName = "role")]
        public string role = null;
        [XmlAttribute(Namespace = "http://www.w3.org/TR/xlink/", AttributeName = "arcrole")]
        public string arcrole = null;
        [XmlAttribute(Namespace = "http://www.w3.org/TR/xlink/", AttributeName = "title")]
        public string title = null;
        [XmlAttribute(Namespace = "http://www.w3.org/TR/xlink/", AttributeName = "show")]
        public string show = null;
        [XmlAttribute(Namespace = "http://www.w3.org/TR/xlink/", AttributeName = "actuate")]
        public string actuate = null;

        public MetsXLinkElement() { }

        public MetsXLinkElement(string id, string label, MetsLocatorType loctype, string otherloctype, string href, string role, string arcrole, string title, string show, string actuate) {
            this.id = id;
            this.label = label;
            this.loctype = loctype.ToString().ToUpper();
            this.otherloctype = otherloctype;
            this.href = href;
            this.role = role;
            this.arcrole = arcrole;
            this.title = title;
            this.show = show;
            this.actuate = actuate;
        }
    }
}
