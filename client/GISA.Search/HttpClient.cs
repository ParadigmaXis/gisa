using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Diagnostics;

namespace GISA.Search
{
    public class HttpClient
    {
        private static string searchServer;

        public static string SearchServer
        {
            get { return searchServer; }
            set { searchServer = value; }
        }

        public static List<string> HttpGetresults(string path, string query)
        {
            string url = HttpClient.SearchServer + path;
            HttpWebRequest oRequest = (HttpWebRequest)WebRequest.Create(url);            
            
            byte[] postBytes = Encoding.UTF8.GetBytes(query);
            oRequest.Method = "POST";
            oRequest.ContentType = "application/x-www-form-urlencoded";
            oRequest.ContentLength = postBytes.Length;
            Stream dataStream = null;            
            try
            {
                dataStream = oRequest.GetRequestStream();
            }
            finally
            {
                if (dataStream != null)
                {
                    dataStream.Write(postBytes, 0, postBytes.Length);
                    dataStream.Close();
                }
            }
            HttpWebResponse oResponse = null;
            try
            {
                oResponse = (HttpWebResponse)oRequest.GetResponse();
            }
            catch (WebException e)
            {
                HttpStatusCode intCode = 0;
                if (oResponse != null)
                    intCode = oResponse.StatusCode;

                Stream errstream = e.Response.GetResponseStream();
                string errsr = "";
                if (errstream != null)
                {
                    StreamReader errreader = new StreamReader(errstream);
                    errsr = errreader.ReadToEnd();
                }
                string errmessage = "Error in GISA Server" + Environment.NewLine;
                if (intCode != 0)
                    errmessage += "HttpStatusCode: " + intCode.ToString() + Environment.NewLine;
                if (errsr != "")
                    errmessage += "StreamResponse: " + errsr + Environment.NewLine + "End-of-stream-response" + Environment.NewLine;

                throw new Exception(errmessage, e);
            }
            StreamReader reader = new StreamReader(oResponse.GetResponseStream());
            string sr = reader.ReadToEnd();
            string[] srParts = sr.Split(' ');
            
            List<string> ret = new List<string>();
            foreach(string str in srParts)
            {
                if (str.Length > 0)
                {
                    ret.Add(str);
                }
            }
            return ret;

        }
    }
}
