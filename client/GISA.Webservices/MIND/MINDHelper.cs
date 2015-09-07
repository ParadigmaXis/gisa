using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GISA.Webservices.MIND
{
    public class MINDHelper
    {
        private const int nTamanho = 3;

        public static string getMINDImageIdFromUrl(string link)
        {
            if (Uri.IsWellFormedUriString(link, UriKind.RelativeOrAbsolute))
            {
                Uri url = new Uri(Uri.UnescapeDataString(link));

                int start = url.Query.IndexOf("Documento=");

                if (start != -1)
                {
                    string substring = url.Query.Substring(start + 10);
                    int end = substring.IndexOf("&");

                    if (end != -1) return substring.Substring(0, end);
                    else return substring;
                }
                else return null;
            }
            return null;
        }

        public static bool IsValidDocument(string fullLocationIdentifier)
        {
            string strDocumento = getMINDImageIdFromUrl(fullLocationIdentifier);
            int nDocIDXarq = 0;
            bool teste = false;

            XarqDigitalizacaoWebService ws = new XarqDigitalizacaoWebService();

            try
            {
                Debug.WriteLine(string.Format("A verificar se o documento {0} é válido...", strDocumento));
                teste = ws.IsValidDocument(ref strDocumento, out nDocIDXarq);
            }
            catch (System.Net.WebException)
            {
                MessageBox.Show("Servidor inacessível.", "Imagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return teste;
        }

        public static string getMINDDocument(string SourceLocation)
        {
            string strDocumento = MINDHelper.getMINDImageIdFromUrl(SourceLocation);
            int nDocIDXarq = 0;            
            byte[] result = null;
            string sUrl = string.Empty;

            XarqDigitalizacaoWebService ws = new XarqDigitalizacaoWebService();

            try
            {
                Debug.WriteLine(string.Format("A verificar se o documento {0} é válido...", strDocumento));
                bool teste = ws.IsValidDocument(ref strDocumento, out nDocIDXarq);
                
                if (!teste) 
                    return sUrl;

                Debug.WriteLine("Documento válido!");

                Debug.WriteLine("Obter imagem do documento...");
                result = ws.GetPDFDocument(nDocIDXarq, nTamanho);

                Debug.WriteLine("Imagem obtida!");

                Debug.WriteLine("A gerar pdf da imagem obtida...");

                string gisaTempPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\ParadigmaXis\\GISA";
                string pdfFileName = Guid.NewGuid().ToString() + ".pdf";

                FileStream createPdf = new FileStream(gisaTempPath + "\\" + pdfFileName, FileMode.Create);
                createPdf.Write(result, 0, result.Length);
                createPdf.Close();
                Debug.WriteLine("Pdf gerado!");

                sUrl = gisaTempPath + "\\" + pdfFileName;
            }
            catch (System.Net.WebException)
            {
                MessageBox.Show("Servidor inacessível.", "Imagem", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return sUrl;
        }

        public static void DeleteTempFiles()
        {
            string gisaTempPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\ParadigmaXis\\GISA";
            string[] files = Directory.GetFiles(gisaTempPath, "*.pdf");
            bool retVal;
            foreach (string file in files)
            {
                FileInfo info = new FileInfo(file);
                retVal = FileInUse(info.FullName);
                
                if (!retVal)
                    File.Delete(file);
            }
        }

        static bool FileInUse(string path)
        {
            string message = string.Empty;
            try
            {
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                {
                }
                return false;
            }
            catch (IOException)
            {
                return true;
            }
        }
    }
}