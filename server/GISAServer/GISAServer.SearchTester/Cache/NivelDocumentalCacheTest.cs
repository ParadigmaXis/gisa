using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using GISAServer.Search.Cache;

namespace GISAServer.SearchTester.Cache
{
    [TestClass]
    public class NivelDocumentalCacheTest
    {
        [TestMethod]
        public void TestSearchInCacheMatch()
        {
            var user = 1;
            var query = "text:a";
            var results = new List<string>() { "1", "2" };

            var queryCache = new QueryCache();
            queryCache.Add(user, query, results);
            var res = queryCache.SearchInCache(user, query);

            Assert.AreEqual(results, res);
            Assert.AreNotEqual(results, new List<string>() {"3", "4"});
        }

        [TestMethod]
        public void TestSearchInCacheNotMatch()
        {
            var user = 1;
            var query1 = "text:a";
            var query2 = "text:b";
            var results = new List<string>() { "1", "2" };

            var queryCache = new QueryCache();
            queryCache.Add(user, query1, results);

            var res = queryCache.SearchInCache(user, query2);

            Assert.IsNull(res);
        }

        [TestMethod]
        public void TestDeleteOldQueries()
        {
            var user = 1;
            var queries = new List<string>() { "text:a", "text:b", "text:c", "text:d" };
            var results = new List<string>() { "1", "2" };

            var queryCache = new QueryCache() { LIMIT = 3, VALID_TIME_SECONDS = 5 };
            foreach (var query in queries)
            {
                queryCache.Add(user, query, results);
                Thread.Sleep(500);
            }
            Assert.IsNull(queryCache.SearchInCache(user, "text:a"));
            var res = queryCache.SearchInCache(user, "text:b");
            Assert.IsNotNull(res);
            Assert.AreEqual(results, res);
        }
    }
}
