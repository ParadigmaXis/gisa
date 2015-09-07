using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

using NUnit.Framework;
using GISA.Webservices.DocInPorto;
using GISA.IntGestDoc.Tests.TestData;
using GISA.Webservices.ProdDocInPortoWebService;
using GISA.Webservices.ToponimiaWS;

namespace GISA.IntGestDoc.Tests.TestData
{
    public static class WebServiceDataSerialization
    {
        public static void SerializeWSRecords(string filename)
        {
            var dipWS = new GISA.IntGestDoc.Controllers.DocInPortoWS();
            Stream stream = File.Open(filename, FileMode.Create);
            var diags = dipWS.GetDocumentosEnviadosParaArquivoGeral(DateTime.MinValue, int.MaxValue);
            BinaryFormatter bFormatter = new BinaryFormatter();
            bFormatter.Serialize(stream, diags.ToArray());
            stream.Close();
        }

        public static string RetrieveConteudoAndWriteToFile(string username, string password, string nud) 
        {
            ServicoDocumentos sd = new ServicoDocumentos();
            sd.Credentials = new NetworkCredential(username, password);
            ConteudoInfo conteudo = sd.ConsultarConteudoDocumento(nud);
            var stream = new System.IO.FileStream(conteudo.NOMEFICHEIRO, System.IO.FileMode.CreateNew);
            stream.Write(conteudo.FICHEIRO, 0, conteudo.FICHEIRO.Length);
            stream.Close();
            return conteudo.NOMEFICHEIRO;
        }

        public static string[] RetrieveAnexosAndWriteToFiles(string username, string password, string timestamp, long limit)
        {
            List<string> filenames = new List<string>();
            ServicoDocumentos sd = new ServicoDocumentos();
            sd.Credentials = new NetworkCredential(username, password);
            DocumentoInfoArquivoGeral[] diags = 
                sd.ListaDocumentosArquivoGeral(timestamp, limit);
            foreach (var d in diags)
            {
                foreach (var a in d.ARRAYCONTEUDOS.Where(a => a != null))
                {
                    ConteudoInfo conteudo = sd.ConsultarAnexoDocumento(d.NUD, a.NOMEFICHEIRO);
                    filenames.Add(conteudo.NOMEFICHEIRO);
                    var stream = new System.IO.FileStream(conteudo.NOMEFICHEIRO, System.IO.FileMode.CreateNew);
                    stream.Write(conteudo.FICHEIRO, 0, conteudo.FICHEIRO.Length);
                    stream.Close();
                }

            }
            return filenames.ToArray();
        }

        public static void SerializeWSToponimias(string username, string password, string timestamp, string filename)
        {
            var dipWS = new Controllers.DocInPortoWS();
            var diags = dipWS.GetDocumentosEnviadosParaArquivoGeral(DateTime.MinValue, int.MaxValue);
            //Pre_ToponimiaWS.ToponimiaWS tp = new Pre_ToponimiaWS.ToponimiaWS();
            ToponimiaWS tp = new ToponimiaWS();
            tp.Credentials = new NetworkCredential(username, password);
            var moradas = new Dictionary<string, MoradaRecord>();
            foreach (var d in diags)
            {
                if (d.CODMORADA == null) continue;
                var top = tp.SeleccionaToponimia(d.CODMORADA);
                moradas[d.CODMORADA] = new MoradaRecord() {CodigoMorada = top.Tables[0].Rows[0]["CODMORADA"].ToString(), Nome = top.Tables[0].Rows[0]["MORADA"].ToString()};
            }

            Stream stream = File.Open(filename, FileMode.Create);
            BinaryFormatter bFormatter = new BinaryFormatter();
            bFormatter.Serialize(stream, moradas.Values.ToArray());
            stream.Close();
        }

        public static List<DocumentoInfoArquivoGeral> GetWSRecordsFromSerializedSource(String filename)
        {
            List<DocumentoInfoArquivoGeral> ret = new List<DocumentoInfoArquivoGeral>();

            DocumentoInfoArquivoGeral[] docs = null;
            Stream stream = File.Open(filename, FileMode.Open);
            BinaryFormatter bFormatter = new BinaryFormatter();
            docs = (DocumentoInfoArquivoGeral[])bFormatter.Deserialize(stream);
            stream.Close();

            if (docs != null)
            {
                ret.AddRange(docs.ToList<DocumentoInfoArquivoGeral>());
            }
            return ret;
        }

        public static List<MoradaRecord> GetWSToponimiasFromSerializedSource(String filename)
        {
            List<MoradaRecord> ret = new List<MoradaRecord>();

            MoradaRecord[] moradas = null;
            Stream stream = File.Open(filename, FileMode.Open);
            BinaryFormatter bFormatter = new BinaryFormatter();
            moradas = (MoradaRecord[])bFormatter.Deserialize(stream);
            stream.Close();

            if (moradas != null)
            {
                ret.AddRange(moradas.ToList<MoradaRecord>());
            }
            return ret;
        }
    }
}
