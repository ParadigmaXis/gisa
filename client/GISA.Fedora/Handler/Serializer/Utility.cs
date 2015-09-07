using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Xml;

namespace GISA.Fedora.FedoraHandler
{
    public static class Utility
    {
        public static XmlDocument GetRemoteXml(string url)
        {
            try
            {
                XmlTextReader myReader = new XmlTextReader(url);
                XmlDocument mySourceDoc = new XmlDocument();
                mySourceDoc.Load(myReader);
                myReader.Close();
                return mySourceDoc;
            }
            catch { return null; }
        }

        public static string GetResizableImgElement(string url)
        {
            // Doctype tem de ir, ou o max-width não funciona
            string doctype = "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">";
            return String.Format("{0} <body style=\"background-color: #565656;\"><img src=\"{1}\" style=\"max-width: 100%;\" alt=\"\"/></body>", doctype, url);
        }

        public static string GetMIME(string url, out bool found) {
            try
            {
                HttpWebRequest request = WebRequest.Create(new Uri(url)) as HttpWebRequest;
                request.Timeout = 3000;
                request.Method = "HEAD";

                try
                {
                    using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                    {
                        string responseType = response.ContentType;
                        response.Close();
                        found = true;
                        return responseType;
                    }
                }
                catch
                {
                    System.Diagnostics.Trace.WriteLine("GetMime: " + url);
                    found = false;
                    return "application/octet-stream";
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine("Não foi possivel obter a imagem do url: " + url);
                Trace.WriteLine(e);
                found = false;
                return "application/octet-stream";
            }
        }

        public static string Now()
        {
            return System.DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ", System.Globalization.CultureInfo.InvariantCulture);
        }

        public static bool RemoteFileExists(string url)
        {
            try
            {
                HttpWebRequest request = WebRequest.Create(new Uri(url)) as HttpWebRequest;
                request.Timeout = 3000;
                request.Method = "HEAD";

                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    bool result = response.StatusCode == HttpStatusCode.OK;
                    response.Close();
                    return result;
                }
            }
            catch (Exception e) 
            {
                Trace.WriteLine(url);
                Trace.WriteLine(e.ToString());
                return false; 
            }
        }
    }
}
