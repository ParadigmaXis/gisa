using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GISA.Fedora.FedoraHandler
{
    public enum State { added, modified, deleted, unchanged, poked, notFound }
    public enum ServerState { Active, Inactive, Deleted, Unknown }
    public abstract class ObjDigital
    {
        public string pid = "";
        public string gisa_id = "";
        public string titulo = "";
        public string tipologia = "";
        public List<string> assuntos = new List<string>();
        public bool isDeleted = false;
        public bool publicado = false;
        public State state = State.unchanged;
        public ServerState serverState = ServerState.Unknown;
        public ObjDigital original;
        public string version;

        public abstract ObjDigital Clone();
    }

    public class ObjDigSimples : ObjDigital
    {
        public List<Anexo> fich_associados = new List<Anexo>();
        public List<Historico> historico = new List<Historico>();
        public int nextDatastreamId;
        public string parentDocumentTitle;
        public long guiorder;

        public override ObjDigital Clone()
        {
            var theOriginal = new ObjDigSimples();
            theOriginal.titulo = this.titulo;
            theOriginal.tipologia = this.tipologia;
            theOriginal.assuntos = new List<string>(this.assuntos);
            theOriginal.gisa_id = this.gisa_id;
            theOriginal.pid = this.pid;
            theOriginal.publicado = this.publicado;
            theOriginal.parentDocumentTitle = this.parentDocumentTitle;
            theOriginal.fich_associados = new List<Anexo>();
            theOriginal.version = this.version;
            theOriginal.nextDatastreamId = this.nextDatastreamId;
            this.fich_associados.ForEach(anexo => theOriginal.fich_associados.Add(anexo.Clone()));
            this.historico.ForEach(entry => theOriginal.historico.Add(entry.Clone()));
            return theOriginal;
        }
    }

    public class ObjDigComposto : ObjDigital
    {
        public List<ObjDigSimples> objSimples = new List<ObjDigSimples>();

        public override ObjDigital Clone()
        {
            var theOriginal = new ObjDigComposto();
            theOriginal.titulo = this.titulo;
            theOriginal.tipologia = this.tipologia;
            theOriginal.assuntos = new List<string>(this.assuntos);
            theOriginal.gisa_id = this.gisa_id;
            theOriginal.pid = this.pid;
            theOriginal.publicado = this.publicado;
            theOriginal.version = this.version;
            theOriginal.objSimples = new List<ObjDigSimples>();
            this.objSimples.ForEach(objSimples => {
                var newClone = objSimples.Clone() as ObjDigSimples;
                theOriginal.objSimples.Add(newClone);
                objSimples.original = newClone;
            });
            return theOriginal;
        }
    }

    public class Anexo {
        public string pid;
        public string dataStreamID;
        public string mimeType;
        public string url;
        public string checksum;

        public bool isIngested { get { if (String.IsNullOrEmpty(this.dataStreamID)) return false; return true; } }

        public Anexo Clone()
        {
            Anexo result = new Anexo();
            result.pid = this.pid;
            result.dataStreamID = this.dataStreamID;
            result.mimeType = this.mimeType;
            result.url = this.url;
            result.checksum = this.checksum;
            return result;
        }
    }

    public class Historico
    {
        public string timestamp;
        public string user;
        public string[] message;

        public Historico(string timestamp, string user, string[] message)
        {
            this.timestamp = timestamp;
            this.user = user;
            this.message = message;
        }

        public Historico Clone()
        {
            return new Historico(this.timestamp, this.user, this.message);
        }

        public override string ToString()
        {
            string description = "";

            if (message.Length == 1) description = String.IsNullOrEmpty(message[0]) ? "" : message[0];
            else description = String.Format("Feitas {0} alterações", message.Length);

            return String.Format("{0}{1}", description, String.IsNullOrEmpty(user) ? "" : " (por " + user + ")");
        }

        public string FullDescription
        {
            get {
                StringBuilder builder = new StringBuilder();
                foreach (string note in message) builder.AppendFormat(" - {0}\n", note);
                return builder.ToString();
            }
        }

        public string Timestamp { 
            get 
            {
                try
                {
                    DateTime converted = DateTime.ParseExact(timestamp, "yyyy-MM-dd'T'HH:mm:ss.fff'Z'", CultureInfo.InvariantCulture);
                    return "Em " + converted.ToString("F");
                }
                catch { return timestamp; }
            } 
        }
    }
    
}
