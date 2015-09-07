using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Configuration;

using GISA.Webservices.ProdDocInPortoWebService;

namespace GISA.Webservices.DocInPorto
{
    public class DocInPortoHelper
    {
        private static string CMPUsername
        {
            get
            {
                return ConfigurationManager.AppSettings["GISA.CMPUsername"];
            }
        }
        private static string CMPPassword
        {
            get
            {
                return ConfigurationManager.AppSettings["GISA.CMPPassword"];
            }
        }
        public static string getDocInPortoAnexo(string SourceLocation, string NUD)
        {
            byte[] result = null;
            string sUrl = string.Empty;

            try
            {
                ServicoDocumentos sd = new ServicoDocumentos();
                sd.Credentials = new NetworkCredential(CMPUsername, CMPPassword);

                Debug.WriteLine("Obter imagem do documento...");
                Debug.WriteLine("NUD: " + NUD);
                Debug.WriteLine("Nome do ficheiro: " + SourceLocation);

                var a = sd.ConsultarAnexoDocumento(NUD, SourceLocation);
                result = a.FICHEIRO;

                Debug.WriteLine("Imagem obtida!");

                Debug.WriteLine("A gerar ficheiro...");

                string gisaTempPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\ParadigmaXis\\GISA";
                string fileName = Guid.NewGuid().ToString() + "_" + SourceLocation;

                FileStream createPdf = new FileStream(gisaTempPath + "\\" + fileName, FileMode.Create);
                createPdf.Write(result, 0, result.Length);
                createPdf.Close();
                Debug.WriteLine("Ficheiro gerado!");

                sUrl = gisaTempPath + "\\" + fileName;
            }
            catch (System.Web.Services.Protocols.SoapException ex)
            {
                MessageBox.Show("Ocorreu um erro ao obter o ficheiro." + System.Environment.NewLine + System.Environment.NewLine +
                    ex.Message, "Obter ficheiro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Trace.WriteLine(ex.Message + System.Environment.NewLine + ex.StackTrace);
            }
            catch (Exception e)
            {
                MessageBox.Show("Servidor inacessível.", "Servidor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Trace.WriteLine(e.ToString());
            }

            return sUrl;
        }

        public static string getDocInPortoConteudo(string NUD)
        {
            byte[] result = null;
            string sUrl = string.Empty;

            try
            {
                ServicoDocumentos sd = new ServicoDocumentos();
                sd.Credentials = new NetworkCredential(CMPUsername, CMPPassword);

                Debug.WriteLine("Obter imagem do documento...");

                Debug.WriteLine("Obter imagem do documento...");
                Debug.WriteLine("NUD: " + NUD);

                var b = sd.ConsultarConteudoDocumento(NUD);
                var SourceLocation = b.NOMEFICHEIRO;
                result = b.FICHEIRO;

                Debug.WriteLine("Imagem obtida!");

                Debug.WriteLine("A gerar ficheiro...");

                string gisaTempPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\ParadigmaXis\\GISA";
                string fileName = Guid.NewGuid().ToString() + "_" + SourceLocation;

                FileStream createPdf = new FileStream(gisaTempPath + "\\" + fileName, FileMode.Create);
                createPdf.Write(result, 0, result.Length);
                createPdf.Close();
                Debug.WriteLine("Ficheiro gerado!");

                sUrl = gisaTempPath + "\\" + fileName;
            }
            catch (System.Web.Services.Protocols.SoapException ex)
            {
                MessageBox.Show("Ocorreu um erro ao obter o ficheiro." + System.Environment.NewLine + System.Environment.NewLine +
                    ex.Message, "Obter ficheiro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Trace.WriteLine(ex.Message + System.Environment.NewLine + ex.StackTrace);
            }
            catch (Exception e)
            {
                MessageBox.Show("Servidor inacessível.", "Servidor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Trace.WriteLine(e.ToString());
            }

            return sUrl;
        }

        public static List<DocumentoInfoArquivoGeral> GetListaDocumentosArquivoGeral(DateTime timeStamp, int maxDocs)
        {
            var docs = default(DocumentoInfoArquivoGeral[]);
            try
            {
                ServicoDocumentos sd = new ServicoDocumentos();
                sd.Credentials = new NetworkCredential(DocInPortoHelper.CMPUsername, DocInPortoHelper.CMPPassword);
                System.Diagnostics.Debug.WriteLine("Filtro documentos: " + timeStamp.ToString("dd-MM-yyyy HH:mm:ss,FFFFFFF"));
                docs = sd.ListaDocumentosArquivoGeral(timeStamp.ToString("dd-MM-yyyy HH:mm:ss,FFFFFFF"), maxDocs); //Example: "13-11-2009 18:49:43,696875000");
            }
            catch (System.Web.Services.Protocols.SoapException e)
            {
                System.Diagnostics.Trace.WriteLine("DocInPorto: " + e.Message);
                MessageBox.Show("Não foi possível obter os documentos para integração." + System.Environment.NewLine + "Ocorreu um erro no servidor.", "Obter documentos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (System.Net.WebException e)
            {
                System.Diagnostics.Trace.WriteLine("DocInPorto: " + e.Message);
                if (e.Status == WebExceptionStatus.NameResolutionFailure)
                    MessageBox.Show("Não foi possível obter os documentos para integração." + System.Environment.NewLine + "O servidor está inacessível.", "Obter documentos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show("Não foi possível obter os documentos para integração." + System.Environment.NewLine + "Ocorreu um erro inesperado no servidor.", "Obter documentos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.ToString());
                MessageBox.Show("Não foi possível obter os documentos para integração." + System.Environment.NewLine + "Ocorreu um erro inesperado.", "Obter documentos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return docs == null ? new List<DocumentoInfoArquivoGeral>() : docs.ToList<DocumentoInfoArquivoGeral>();
        }

        public static List<MoradaRecord> GetMoradas(IEnumerable<string> iEnumerable)
        {
            var moradas = new Dictionary<string, MoradaRecord>();
            try
            {
                var tp = new ToponimiaWS.ToponimiaWS();
                tp.Credentials = new NetworkCredential(DocInPortoHelper.CMPUsername, DocInPortoHelper.CMPPassword);
                iEnumerable.Where(val => val != null).ToList().ForEach(codMorada =>
                {
                    var top = tp.SeleccionaToponimia(codMorada);
                    if (top.Tables.Count == 0 || top.Tables[0].Rows.Count == 0)
                        moradas[codMorada] = new MoradaRecord() { CodigoMorada = codMorada, Nome = codMorada };
                    else
                        moradas[codMorada] = new MoradaRecord() { CodigoMorada = top.Tables[0].Rows[0]["CODMORADA"].ToString(), Nome = top.Tables[0].Rows[0]["MORADA"].ToString() };
                });
            }
            catch (System.Web.Services.Protocols.SoapException e)
            {
                System.Diagnostics.Trace.WriteLine("DocInPorto: " + e.Message);
                MessageBox.Show("Não foi possível obter as moradas para integração." + System.Environment.NewLine + "Ocorreu um erro no servidor.", "Obter moradas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                moradas = null;
            }
            catch (WebException ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.ToString());
                MessageBox.Show("Não foi possível obter as moradas para integração." + System.Environment.NewLine + "Ocorreu um erro inesperado.", "Obter moradas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                moradas = null;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.ToString());
                MessageBox.Show("Não foi possível obter as moradas para integração." + System.Environment.NewLine + "Ocorreu um erro inesperado.", "Obter moradas", MessageBoxButtons.OK, MessageBoxIcon.Error);
                moradas = null;
            }

            return moradas != null ? moradas.Values.ToList() : null;
        }
    }
}
