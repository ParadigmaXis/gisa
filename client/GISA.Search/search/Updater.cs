using System;
using System.IO;
using System.Net;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Linq;

namespace GISA.Search
{
    public class Updater
    {
        private static string appData = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
        private static DirectoryInfo di = new DirectoryInfo(appData + @"\ParadigmaXis\GISA");

        private static string updateQueueFileNivelDocumentalComProdutores = di.FullName + @"\updateQueueNivelDocumentalComProdutores.txt";
        private static string updateQueueFileProdutores = di.FullName + @"\updateQueueProdutores.txt";
        private static string updateQueueFileAssuntos = di.FullName + @"\updateQueueAssuntos.txt";
        private static string updateQueueFileTipologias = di.FullName + @"\updateQueueTipologias.txt";
        private static string updateQueueFileUnidadesFisicas = di.FullName + @"\updateQueueUnidadesFisicas.txt";
        private static string updateQueueFileNivelDocumental = di.FullName + @"\updateQueue.txt";

        public static void updateNivelDocumentalComProdutores(long id)
        {
            updateNivelDocumentalComProdutores(new List<string>() { id.ToString() });
        }

        public static void updateNivelDocumentalComProdutores(List<string> ids)
        {
            update(ids, updateQueueFileNivelDocumentalComProdutores, "nivelDocumentalComProdutores");
        }


        public static void updateProdutor(long id)
        {
            updateProdutor(new List<string>() { id.ToString() });
        }

        public static void updateProdutor(List<string> ids)
        {
            update(ids, updateQueueFileProdutores, "produtor");
        }


        public static void updateAssunto(long id)
        {
            updateAssunto(new List<string>() { id.ToString() });
        }

        public static void updateAssunto(List<string> ids)
        {
            update(ids, updateQueueFileAssuntos, "assuntos");
        }


        public static void updateTipologia(long id)
        {
            updateTipologia(new List<string>() { id.ToString() });
        }

        public static void updateTipologia(List<string> ids)
        {
            update(ids, updateQueueFileTipologias, "tipologias");
        }

        public static void updateUnidadeFisica(long nUfID)
        {
            updateUnidadeFisica(new List<string>() { nUfID.ToString() });
        }

        public static void updateUnidadeFisica(List<string> nUfIDs)
        {
            update(nUfIDs, updateQueueFileUnidadesFisicas, "unidadeFisica");
        }


        public static void updateNivelDocumental(long id)
        {
            updateNivelDocumental(new List<string>() { id.ToString() });
        }

        public static void updateNivelDocumental(List<string> ids)
        {
            update(ids, updateQueueFileNivelDocumental, "nivelDocumental");
            update(ids, updateQueueFileNivelDocumental, "nivelDocumentalInternet");
        }

        private static void update(List<string> ids, string updateQueueFile, string updateType)
        {
            // Turn server to yellow
            // Make the request or write to file
            // Wait for answer or end if server is unavailable
            // Turn server back to green again

            Debug.Assert(updateType.Length > 0, "Invalid search type");

            var idsToUpdate = new List<string>();
            idsToUpdate.AddRange(ids);
            
            if (File.Exists(updateQueueFile))
            {
                StreamReader sr = File.OpenText(updateQueueFile);
                string idString = sr.ReadLine();
                sr.Close();
                File.Delete(updateQueueFile);

                string[] idsArray = idString.Split(' ');
                idsToUpdate.AddRange(idsArray);
            }

            idsToUpdate = removeDuplicates(idsToUpdate);

            if (idsToUpdate.Count > 0)
            {
                string idString = "";
                try
                {
                    idString = string.Join(" ", idsToUpdate.ToArray());
                    HttpClient.HttpGetresults("/?f=update&t=" + updateType, idString);
                }
                catch (Exception e)
                {                    
                    Trace.WriteLine(e.ToString());

                    // Save the ids that were unable no update
                    StreamWriter sw = File.AppendText(updateQueueFile);
                    sw.Write(idString);
                    sw.Close();

                    //throw new UpdateServerException();
                }
            }
        }

        static List<string> removeDuplicates(List<string> inputList)
        {
            HashSet<string> finalList = new HashSet<string>(inputList);
            return finalList.ToList();
        }
    }
}
