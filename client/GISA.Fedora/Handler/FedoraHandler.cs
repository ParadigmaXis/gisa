using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Xml;

using GISA.Fedora.FedoraHandler.Fedora.APIA;
using GISA.Fedora.FedoraHandler.Fedora.APIM;

namespace GISA.Fedora.FedoraHandler
{
    public enum Quality {Tiny, Low, Medium, High};
    public enum DocumentType {Simple, Complex};
    public enum ObjectState {Active, Inactive, Deleted, Unchanged};
    public enum IngestResult { Success, Error, Partial };

    public class FedoraConnection {
        private Uri host;
        private FedoraAPIAService service;
        private FedoraAPIMService manager;
        private string serverNamespace;
        private string gisaOperator;

        // Constantes para definição de mensagens de log de modificação de um objecto
        private const string IMAGES_NEW = "Adição de novas imagens";
        private const string IMAGES_ORDER = "Alteração na ordenação das imagens";
        private const string IMAGES_DELETE = "Remoção de imagens";
        private const string DOCUMENT_THEME = "Alteração da lista de assuntos";
        private const string DOCUMENT_TYPE = "Atualização da tipologia";
        private const string DOCUMENT_TITLE = "Atualização do título";
        private const string DOCUMENT_ORDER = "Alteração na ordenação documental";
        private const string DOCUMENT_CHILD_NEW = "Adição de novos sub-documentos";
        private const string DOCUMENT_CHILD_DELETE = "Remoção de sub-documentos";
        private const string METADATA_CHILD = "Atualização de metadados num sub-documento";
        private const string METADATA_DEFAULT = "Atualização de metadados";

        public FedoraConnection(Uri host, string gisa_operator) { 
            this.host = host;
            this.gisaOperator = gisa_operator;
        }

        public bool Connect(string userName, string passWord) {
            try {
                CredentialCache credCache = new CredentialCache();
                credCache.Add(host, "Basic", new NetworkCredential(userName, passWord));

                // Ligação à API-A
                service = new FedoraAPIAService();
                service.Url = host.ToString() + "/services/access";
                service.Timeout = 240000;
                service.PreAuthenticate = true;
                service.Credentials = credCache;

                // Ligação à API-M
                manager = new FedoraAPIMService();
                manager.Url = host.ToString() + "/services/management";
                manager.Timeout = 120000;
                manager.PreAuthenticate = true;
                manager.Credentials = credCache;

                serverNamespace = service.describeRepository().repositoryPIDNamespace;
                return true; 
            }  catch (Exception ex) {
                Trace.WriteLine(ex.ToString());
                return false; 
            }
        }

        public bool AddObject(ObjDigital objDigital)
        {
            try 
            { 
                string newPid = manager.getNextPID("1", serverNamespace)[0];
                if (objDigital.pid == "-1") objDigital.pid = newPid;
                FoxmlExporter foxml;
                if (objDigital.GetType() == typeof(ObjDigSimples)) foxml = new FoxmlExporter(objDigital as ObjDigSimples, gisaOperator, newPid, serverNamespace);
                else foxml = new FoxmlExporter(objDigital as ObjDigComposto, gisaOperator, newPid, ((ObjDigComposto)objDigital).objSimples, serverNamespace);

                if (Ingest(foxml, newPid) != null)
                {
                    Trace.WriteLine(newPid + " foi ingerido.");
                    objDigital.pid = newPid;
                    objDigital.state = State.unchanged;
                    return true;
                }

                Trace.WriteLine(newPid + " não foi ingerido.");
                return false;
            }
            catch (Exception ex) 
            { 
                Trace.WriteLine(ex.ToString());
                return false;
            }
        }

        public bool PokeObject(ObjDigital obj)
        {
            try
            {
                manager.modifyObject(obj.pid, null, null, null, "Poke no objecto para refrescamento.");
                obj.state = State.unchanged;
                return true;
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
                return false;
            }
        }

        public bool ChangeState(ObjDigital obj, ObjectState newState, bool updateObject)
        {
            string stringNewState = null;
            switch(newState) 
            {
                case ObjectState.Active: stringNewState = "A"; break;
                case ObjectState.Inactive: stringNewState = "I"; break;
                case ObjectState.Deleted: stringNewState = "D"; break;
                default: stringNewState = null; break;
            }

            try
            {
                manager.modifyObject(obj.pid, stringNewState, null, null, "Estado do objecto alterado para '" + stringNewState + "'");
                if(updateObject)
                    obj.state = State.unchanged;
                return true;
            }
            catch (Exception ex) 
            {
                Trace.WriteLine(ex.ToString());
                return false;
            }
        }

        public ServerState GetState(string pid)
        {
            FieldSearchQuery query = new FieldSearchQuery();
            FieldSearchQueryConditions conditions = new FieldSearchQueryConditions();
            Condition c = new Condition();
            c.@operator = ComparisonOperator.eq;
            c.property = "pid";
            c.value = pid;
            conditions.condition = new Condition[] { c };
            query.Item = conditions;

            FieldSearchResult results = service.findObjects(new string[] { "pid", "state" }, "1", query);
            if (results != null && results.resultList.Length == 1)
            {
                switch (results.resultList[0].state)
                {
                    case "A": return ServerState.Active;
                    case "I": return ServerState.Inactive;
                    case "D": return ServerState.Deleted;
                    default: return ServerState.Unknown;
                }
            } else return ServerState.Unknown;
        }

        public RepositoryInfo GetRepositoryInfo() {
            try { return service.describeRepository(); } 
            catch (Exception ex) { Trace.WriteLine(ex.ToString()); }
            return null;
        }

        /* 
         * 
         *  THIS METHOD EXISTS TO DEAL WITH VERY LARGE FILES THAT NEED TO BE PULLED FROM THE REPOSITORY
         *  It builds the Fedora URL for a specific datastream so that the ImageHelper can get it. It is possible to fetch the version defined by timestamp
         *  If the timestamp is null, the url for the most recent version is returned
         * 
         */
        public string GetUrlForDatastream(string pid, string datastreamId, string timestamp)
        {
            string url = String.Format("{0}objects/{1}/datastreams/{2}/content", this.host, pid, datastreamId);
            if (!String.IsNullOrEmpty(timestamp))
                url += String.Format("?asOfDateTime={0}", System.Web.HttpUtility.UrlEncode(timestamp));

            Debug.WriteLine("URL for datastream " + datastreamId + " in PID " + pid + ": " + url);

            return url;
        }

        public MIMETypedStream GetDisseminatorForPid(string pid, string method, Quality quality) {            
            try 
            { 
                ObjectMethodsDef[] methods = service.listMethods(pid, null);
                ObjectMethodsDef getPdf = methods.FirstOrDefault(x => x.methodName == "getPDF");
                if (getPdf != null) {
                    Property[] props = { new Property() };
                    props[0].name = "parm1";
                    props[0].value = quality.ToString();
                    return service.getDissemination(getPdf.PID, getPdf.serviceDefinitionPID, getPdf.methodName, props, null);
                } else {
                    Trace.WriteLine("Disseminador de PDFs não encontrado para o objecto " + pid + ".");
                }
            } 
            catch (Exception ex) 
            { 
                Trace.WriteLine(ex.ToString()); 
            }
            return null;
        }

        public ObjDigital GetStructureForPid(string pid, string originalTimestamp, bool loadVersions)
        {
            XmlDocument metsDoc;
            XmlDocument dcDoc;
            XmlNamespaceManager nameSpaceManager;
            XmlNamespaceManager dcNameSpaceManager;
            string versionTimestamp = null;
            try {
                versionTimestamp = manager.getDatastream(pid, "METS", originalTimestamp).createDate;

                MIMETypedStream stream = service.getDatastreamDissemination(pid, "METS", originalTimestamp);
                metsDoc = GetXmlFromStream(stream);
                nameSpaceManager = new XmlNamespaceManager(metsDoc.NameTable);
                nameSpaceManager.AddNamespace("mets", "http://www.loc.gov/METS/");
                nameSpaceManager.AddNamespace("xlink", "http://www.w3.org/TR/xlink/");

                // Temos de pedir o DC mais próximo da versão mais actualizada do METS (logo passamos o versionTimestamp)
                MIMETypedStream dcStream = service.getDatastreamDissemination(pid, "DC", versionTimestamp);
                dcDoc = GetXmlFromStream(dcStream);
                dcNameSpaceManager = new XmlNamespaceManager(dcDoc.NameTable);
                dcNameSpaceManager.AddNamespace("dc", "http://purl.org/dc/elements/1.1/");
                dcNameSpaceManager.AddNamespace("oai_dc", "http://www.openarchives.org/OAI/2.0/oai_dc/");
                dcNameSpaceManager.AddNamespace("xsi", "http://www.w3.org/2001/XMLSchema-instance");

            } catch (Exception ex) { throw new Exception("Datastreams METS e/ou DC inválidos para o objecto " + pid + ".", ex); }

            try {
                XmlNode structMap = metsDoc.SelectSingleNode("mets:mets/mets:structMap", nameSpaceManager);
                XmlNode mainDiv = structMap.SelectSingleNode("mets:div", nameSpaceManager);
                XmlNode recordID = metsDoc.SelectSingleNode("mets:mets/mets:metsHdr/mets:altRecordID", nameSpaceManager);

                string type;
                try { type = structMap.Attributes["TYPE"].Value.ToString().ToUpper();
                } catch { type = "PHYSICAL"; }
                ObjDigital newDocumentType = null;
                if (type == "LOGICAL")
                {
                    newDocumentType = new ObjDigComposto();
                    newDocumentType.state = State.unchanged;
                    newDocumentType.serverState = GetState(pid);
                    ObjDigSimples[] parts = new ObjDigSimples[mainDiv.SelectNodes("mets:div", nameSpaceManager).Count];

                    foreach (XmlNode subDiv in mainDiv.SelectNodes("mets:div", nameSpaceManager))
                    {
                        // Ler objecto directamente
                        XmlNode fptr = subDiv.SelectSingleNode("mets:fptr", nameSpaceManager);
                        ObjDigSimples objSimples = GetStructureForPid(fptr.Attributes["FILEID"].Value, originalTimestamp, true) as ObjDigSimples;
                        parts[int.Parse(subDiv.Attributes["ORDER"].Value) - 1] = objSimples;
                    }

                    ((ObjDigComposto)newDocumentType).objSimples.AddRange(parts);
                }
                else { 
                    newDocumentType = new ObjDigSimples();
                    newDocumentType.state = State.unchanged;
                    newDocumentType.serverState = GetState(pid);
                    Anexo[] parts = new Anexo[mainDiv.SelectNodes("mets:div", nameSpaceManager).Count];

                    foreach (XmlNode subDiv in mainDiv.SelectNodes("mets:div", nameSpaceManager))
                    {
                        XmlNode fptr = subDiv.SelectSingleNode("mets:fptr", nameSpaceManager);
                        Anexo anexo = new Anexo();

                        anexo.pid = pid;
                        anexo.dataStreamID = fptr.Attributes["FILEID"].Value;
                        anexo.mimeType = subDiv.Attributes["TYPE"].Value;
                        parts[int.Parse(subDiv.Attributes["ORDER"].Value) - 1] = anexo;

                        try
                        {
                            Datastream data = manager.getDatastream(pid, anexo.dataStreamID, null);
                            anexo.url = data.location;
                            anexo.checksum = data.checksum;
                        }
                        catch (Exception ex) { Trace.WriteLine(ex.ToString()); throw new Exception("URL de datastream " + anexo.dataStreamID + " no objecto " + pid + " não foi encontrado.", ex);  }
                    }

                    ((ObjDigSimples)newDocumentType).nextDatastreamId = service.listDatastreams(pid, null).Count(ds => ds.ID.Contains("IMG")) + 1;
                    ((ObjDigSimples)newDocumentType).fich_associados.AddRange(parts);

                    if(loadVersions)
                        ((ObjDigSimples)newDocumentType).historico.AddRange(GetHistoric(pid));
                    
                }

                newDocumentType.pid = pid;
                newDocumentType.version = versionTimestamp;
                newDocumentType.titulo = mainDiv.Attributes["LABEL"].Value;
                if (mainDiv.Attributes["TYPE"] != null)
                    newDocumentType.tipologia = mainDiv.Attributes["TYPE"].Value;
                if (recordID != null)  // há objetos que não têm gisa_id
                    newDocumentType.gisa_id = recordID.InnerText;
                else
                    newDocumentType.gisa_id = "";

                XmlNodeList assuntos = dcDoc.SelectNodes("oai_dc:dc/dc:subject", dcNameSpaceManager);
                if (assuntos.Count > 0)
                {
                    List<string> listAssuntos = new List<string>();
                    foreach (XmlNode node in assuntos) listAssuntos.Add(node.InnerText);
                    newDocumentType.assuntos = listAssuntos;
                }

                return newDocumentType;
            }
            catch (Exception ex) { Trace.WriteLine(ex.ToString()); throw new Exception("Erro ao analisar objecto " + pid + ".", ex); }
        }

        // retorna o histórico de um pid por ordem decrescente (do mais recente para o mais antigo) da data de registo
        public List<Historico> GetHistoric(string pid)
        {
            List<Historico> result = new List<Historico>();
            
            foreach (Datastream ds in manager.getDatastreamHistory(pid, "METS")) {
                XmlDocument metsDoc;
                XmlNamespaceManager nameSpaceManager;

                MIMETypedStream stream = service.getDatastreamDissemination(pid, "METS", ds.createDate);
                metsDoc = GetXmlFromStream(stream);
                nameSpaceManager = new XmlNamespaceManager(metsDoc.NameTable);
                nameSpaceManager.AddNamespace("mets", "http://www.loc.gov/METS/");
                nameSpaceManager.AddNamespace("xlink", "http://www.w3.org/TR/xlink/");

                // Get author and log message from METS file
                string user = "";
                List<string> logMsg = new List<string>();
                XmlNode headerNode = metsDoc.SelectSingleNode("//mets:agent", nameSpaceManager);
                
                if (headerNode != null)
                {
                    if (headerNode.SelectSingleNode("mets:name", nameSpaceManager) != null) user = headerNode.SelectSingleNode("mets:name", nameSpaceManager).InnerText;

                    if (headerNode.SelectNodes("mets:note", nameSpaceManager) != null)
                    {
                        foreach(XmlNode node in headerNode.SelectNodes("mets:note", nameSpaceManager)) {
                            logMsg.Add(node.InnerText);
                        }
                    }
                }

                result.Add(new Historico(ds.createDate, user, logMsg.ToArray()));
            }
            return result;
        }

        public IngestResult Ingest(ObjDigital obj)
        {
            if (obj.GetType() == typeof(ObjDigComposto))
            {
                var docComposto = obj as ObjDigComposto;
                bool updateComplex = false;
                bool allUnchanged = true;
                IngestResult result = IngestResult.Success;
                List<ObjDigSimples> toRemove = new List<ObjDigSimples>();

                //if (obj.state == State.notFound)
                //    return IngestResult.Error;

                //if (docComposto.objSimples.Count(od => od.state == State.notFound) > 0)
                //    return IngestResult.Error;

                foreach (var sd in docComposto.objSimples)
                {
                    // So olhamos para objectos que foram alterados
                    if (sd.state != State.unchanged)
                    {
                        allUnchanged = false;
                        if (AtomicIngest(sd)) 
                        {
                            updateComplex = true;
                            if (sd.state == State.deleted) toRemove.Add(sd);
                        }
                        else result = IngestResult.Partial;
                    }
                }

                if (allUnchanged && obj.state == State.unchanged) return IngestResult.Success;
                else if (updateComplex || (obj.state != State.unchanged))
                {
                    // Se houverem documentos que foram marcados como removidos, remover da lista do objecto composto
                    toRemove.ForEach(doc => docComposto.objSimples.Remove(doc));

                    // Tentar actualizar complexo
                    if (AtomicIngest(obj)) return result;
                    else return IngestResult.Error;
                }
                else return IngestResult.Error;
            }
            else
            {
                // Documento simples solto
                if (obj.state != State.notFound && AtomicIngest(obj)) return IngestResult.Success;
                else return IngestResult.Error;
            }
        }

        private bool AtomicIngest(ObjDigital objDigital)
        {
            if (objDigital.state == State.modified)
            {
                return Update(objDigital);
            }
            else if (objDigital.state == State.added)
            {
                return AddObject(objDigital);
            }
            else if (objDigital.state == State.deleted)
            {
                return ChangeState(objDigital, ObjectState.Deleted, false);
            }
            else if (objDigital.state == State.poked)
            {
                return PokeObject(objDigital);
            }
            else return true;
        }

        private string Ingest(FoxmlExporter xml, string identifier)
        {
            StringWriter sw = new StringWriter();
            XmlTextWriter xw = new XmlTextWriter(sw);
            xml.FOXML.WriteTo(xw);
            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            byte[] obj = encoding.GetBytes(sw.ToString());
            string newPid = null;

            try
            {
                newPid = manager.ingest(obj, "info:fedora/fedora-system:FOXML-1.1", "Ingestão do documento " + identifier);
                return newPid;
            }
            catch (Exception ex) { 
                Trace.WriteLine(ex.ToString());
                return null;
            }
        }

        private bool Update(ObjDigital objDigital)
        {
            bool success = true;
            bool update = false;
            var original = objDigital.original;
            List<string> changeset = new List<string>();

            // Se objecto não tem modificações, não fazer nada
            if (objDigital.state != State.modified) return true;

            // Mesmo pid?
            if (original.pid != objDigital.pid || original.GetType() != objDigital.GetType())
            {
                Trace.WriteLine("Objecto a atualizar não tem o mesmo PID do objecto original.");
                return false;
            }

            // Se for simples, verificar imagens
            if (objDigital.GetType() == typeof(ObjDigSimples))
            {
                // Verificar se é um simples e se mexemos nas imagens
                var simpleOriginal = original as ObjDigSimples;
                var simpleChanged = objDigital as ObjDigSimples;

                // Detectar alterações na ordem (pelo menos dos documentos que se mantiverem da versão anterior)
                for (int i = 0; i < simpleOriginal.fich_associados.Count; i++)
                {
                    if (i + 1 > simpleChanged.fich_associados.Count) break;
                    if (simpleChanged.fich_associados[i].url != simpleOriginal.fich_associados[i].url)
                    {
                        AddChange(IMAGES_ORDER, out update, changeset);
                        break;
                    }

                }

                // Adicionar novas datastreams
                if (simpleChanged.fich_associados.Exists(fich => fich.dataStreamID == null))
                {
                    AddChange(IMAGES_NEW, out update, changeset);
                    foreach (Anexo anexo in simpleChanged.fich_associados.Where(anx => anx.dataStreamID == null))
                    {
                        try
                        {
                            anexo.dataStreamID = manager.addDatastream(simpleChanged.pid, "IMG" + simpleChanged.nextDatastreamId, null, null, true, anexo.mimeType, null, anexo.url, "E", "A", "MD5", null, "Adição de imagem");
                            simpleChanged.nextDatastreamId++;
                        }
                        catch (Exception ex)
                        {
                            Trace.WriteLine(ex.ToString());
                            return false;
                        }
                    }
                }

                // Detectar remoções feitas no objecto
                if (!simpleOriginal.fich_associados.TrueForAll(anexoOriginal => simpleChanged.fich_associados.Exists(anexoNovo => anexoNovo.url == anexoOriginal.url && anexoNovo.dataStreamID == anexoOriginal.dataStreamID)))
                {
                    changeset.Add(IMAGES_DELETE);
                    update = true;
                }

                /* if (simpleOriginal.fich_associados.Count > simpleChanged.fich_associados.Count)
                {
                    changeset.Add(IMAGES_DELETE);
                    update = true;
                } */
            } 
            else if (objDigital.GetType() == typeof(ObjDigComposto))
            {
                // Se for um composto, verificar também se alteramos o título / tipologia de um dos simples
                var compostoOriginal = original as ObjDigComposto;
                var compostoChanged = objDigital as ObjDigComposto;

                // Vemos se o composto tem o mesmo número de objectos simples que o original
                if (compostoChanged.objSimples.Count == compostoOriginal.objSimples.Count)
                {
                    for (int i = 0; i < compostoChanged.objSimples.Count; i++)
                    {
                        ObjDigSimples simples = compostoChanged.objSimples[i];
                        ObjDigSimples simplesOriginal = compostoOriginal.objSimples[i];

                        if (simples.pid != simplesOriginal.pid)
                        {
                            AddChange(DOCUMENT_ORDER, out update, changeset);
                            break;
                        }
                    }

                    // Verificamos se algum dos simples associados a este composto teve o seu título ou tipologia alterada
                    foreach (ObjDigSimples simples in compostoChanged.objSimples)
                    {
                        ObjDigSimples simplesOriginal = simples.original as ObjDigSimples;
                        if (simplesOriginal == null || simples.titulo != simplesOriginal.titulo || simples.tipologia != simplesOriginal.tipologia)
                        {
                            AddChange(METADATA_CHILD, out update, changeset);
                            break;
                        }
                    }

                    // Actualizações ao estado de publicação não ficam registados como nova versão
                    if (compostoChanged.publicado != compostoOriginal.publicado)
                        PokeObject(compostoChanged);
                    else
                    {
                        foreach (ObjDigSimples simples in compostoChanged.objSimples)
                        {
                            ObjDigSimples simplesOriginal = simples.original as ObjDigSimples;
                            if (simplesOriginal != null && simples.publicado != simplesOriginal.publicado)
                            {
                                PokeObject(compostoChanged);
                                break;
                            }
                        }
                    }
                }
                else
                {
                    // Neste caso, ou adicionamos novos sub-documentos, ou os removemos
                    if (compostoChanged.objSimples.Count > compostoOriginal.objSimples.Count) AddChange(DOCUMENT_CHILD_NEW, out update, changeset);
                    else AddChange(DOCUMENT_CHILD_DELETE, out update, changeset);
                    update = true;
                }
            }

            // Verificar alteração na lista de assuntos
            if (original.assuntos.Count != objDigital.assuntos.Count || !objDigital.assuntos.TrueForAll(assunto => original.assuntos.Contains(assunto)))
                AddChange(DOCUMENT_THEME, out update, changeset);

            // A tipologia foi alterada?
            if (original.tipologia != objDigital.tipologia)
                AddChange(DOCUMENT_TYPE, out update, changeset);

            // O título foi alterado?
            if (original.titulo != objDigital.titulo)
                AddChange(DOCUMENT_TITLE, out update, changeset);

            if (update)
            {
                if (changeset.Count == 0) changeset.Add(METADATA_DEFAULT);

                // Refazer DC e METS
                string newStamp;
                success = UpdateMetadata(objDigital, changeset.ToArray(), out newStamp);

                // Actualizar versão, se tudo correu bem
                if (success)
                {
                    objDigital.version = newStamp;
                    objDigital.state = State.unchanged;
                }
            }

            return success;
        }

        private static void AddChange(string changeMessage, out bool update, List<string> changeset)
        {
            changeset.Add(changeMessage);
            update = true;
        }

        private bool UpdateMetadata(ObjDigital obj, string[] logMessage, out string newTimestamp) 
        {
            DublinCoreExporter dcExporter;
            MetsExporter metsExporter;
            newTimestamp = null;

            try
            {
                // Create new versions of XML for METS and DC
                if (obj.GetType() == typeof(ObjDigComposto))
                {
                    var objComposto = obj as ObjDigComposto;
                    dcExporter = new DublinCoreExporter(objComposto);
                    metsExporter = new MetsExporter(objComposto, gisaOperator, logMessage);
                }
                else
                {
                    var objSimples = obj as ObjDigSimples;
                    dcExporter = new DublinCoreExporter(objSimples);
                    metsExporter = new MetsExporter(objSimples, gisaOperator, logMessage);
                }
            }
            catch (Exception ex)
            {
                Trace.Write("Erro ao recriar DC/METS do " + obj.pid + ": ");
                Trace.WriteLine(ex.ToString());
                return false;
            }

            // Reingerir o DC e o METS
            try { 
                manager.modifyDatastreamByValue(obj.pid, "DC", null, null, "text/xml", null, ConvertToBytes(dcExporter.DublinCore), "MD5", null, "Atualização de metadados", false);
                newTimestamp = manager.modifyDatastreamByValue(obj.pid, "METS", null, null, "text/xml", null, ConvertToBytes(metsExporter.METS), "MD5", null, "Atualização de metadados", false);
                return true;
            } catch(Exception ex) {
                Trace.Write("Erro ao reingerir DC/METS do " + obj.pid + ": ");
                Trace.WriteLine(ex.ToString());
                return false;
            }
        }

        private XmlDocument GetXmlFromStream(MIMETypedStream stream) {
            if (stream.MIMEType == "text/xml") {
                XmlDocument doc = new XmlDocument();
                doc.Load(new MemoryStream(stream.stream));
                return doc;
            }
            return null;
        }

        private XmlDocument GetXmlFromStream(byte[] bytes)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(new MemoryStream(bytes));
            return doc;
        }

        public byte[] ConvertToBytes(XmlDocument doc)
        {
            Encoding encoding = Encoding.UTF8;
            byte[] docAsBytes = encoding.GetBytes(doc.OuterXml);
            return docAsBytes;
        }

        public bool CheckIntegrity(string pid, string datastreamId, string currentChecksum)
        {
            try
            {
                string result = manager.compareDatastreamChecksum(pid, datastreamId, null);
                return currentChecksum == result;

            } catch(Exception ex)
            {
                Trace.Write("Erro ao verificar integridade do " + pid + ": ");
                Trace.WriteLine(ex.ToString());
                return false;
            }
        }

        public ObjectFields[] Search(string[] fields, int pageSize, FieldSearchQuery query)
        {
            ObjectFields[] objs;
            try
            {
                FieldSearchResult results = service.findObjects(fields, pageSize.ToString(), query);
                objs = results.resultList;

                while (results.listSession != null && results.listSession.token != null)
                {
                    results = service.resumeFindObjects(results.listSession.token);
                    objs = objs.Concat(results.resultList).ToArray();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao obter ficheiros com o critério de pesquisa: " + query.ToString(), ex);
            }
            return objs;
        }
    }
}