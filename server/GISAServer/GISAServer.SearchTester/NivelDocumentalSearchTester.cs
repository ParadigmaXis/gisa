using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GISAServer.Search;

using NUnit.Framework;

namespace GISAServer.SearchTester
{
    [TestFixture]
    public class NivelDocumentalSearchTester
    {
        private NivelDocumentalSearcher searcher;

        [TestFixtureSetUp]
        public void FixtureSetup()
        {
            this.searcher = new NivelDocumentalSearcher("id", "", new Search.Cache.QueryCache());
        }

        [Test]
        public void SearchHasUniqueValues()
        {
            long idTrustee = 0;
            string textIds = this.searcher.Search("porto", idTrustee);
            string[] ids = textIds.Split(' ');

            Console.WriteLine(textIds);
            
            List<long> listIds = new List<long>();
            foreach (string id in ids)
            {
                long longId = -1;
                long.TryParse(id, out longId);
                if (longId != -1 && !listIds.Contains(longId))
                {
                    listIds.Add(longId);
                }
                else
                {
                    Assert.Fail();
                }

            }
        }

        [Test]
        public void SearchAnaAmalia()
        {
            // válido para os dados da CMP e pressopõe que os índices estejam criados
            long idTrustee = 10;
            string textIds = this.searcher.Search("ana amália", idTrustee);
            var ids = textIds.Split(' ').ToList();
            //Assert.AreEqual(1, ids.Count(id => id.Equals("5927")));
            Assert.AreEqual(-1, ids.Count());
        }
    }
}
