using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;

using GISAServer.Hibernate;
using GISAServer.Hibernate.Utils;

namespace GISAServer.HibernateTester
{
    [TestFixture]
    public class UnidadeFisicaExtTester
    {
        [Test]
        public void GetUser()
        {
            IList<long> ids = GISAUtils.GetTrusteeUsersIds("leitor");

            foreach (long id in ids)
            {
                Console.WriteLine(id);
            }
            Assert.AreEqual(1, ids.Count);
        }

        [Test]
        public void GetGroup()
        {
            IList<long> ids = GISAUtils.GetTrusteeUsersIds("t.Name = 'todos'");

            foreach (long id in ids)
            {
                Console.WriteLine(id);
            }
            Console.WriteLine("ids.count: {0}", ids.Count);
            Assert.IsTrue(ids.Count>1);
        }

        [Test]
        public void GetFRDBases()
        {
            var ids = GISAUtils.GetNivelIds(string.Empty, null, new DateTime(2008,10,30));
            if (ids == null)
            {
                Assert.Fail("Parameters not specified?!");
            }

            foreach (string id in ids)
            {
                Console.WriteLine(id);
            }
            Console.WriteLine("ids.count: {0}", ids.Count);
            Assert.IsTrue(ids.Count > 1);
        }


    }
}
