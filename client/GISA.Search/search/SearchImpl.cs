/*
 * Created by SharpDevelop.
 * User: Administrator
 * Date: 2008-04-04
 * Time: 11:16
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;

namespace GISA.Search
{
	/// <summary>
	/// Description of SearchImpl.
	/// </summary>
	public class SearchImpl
	{
        private static List<string> search_getResults(string path)
        {
            return HttpClient.HttpGetresults(path, "");
        }

        public static List<string> search(string query, string type)
        {
            query = query.Replace("&", "%26");
            return search_getResults(string.Format("/?f=search&q={0}&t={1}",query, type));
        }

		public static List<string> search(string query, string type, string user)
		{
            query = query.Replace("&","%26");
            return search_getResults(string.Format("/?f=search&u={0}&q={1}&t={2}", user, query, type));
		}
	}
}
