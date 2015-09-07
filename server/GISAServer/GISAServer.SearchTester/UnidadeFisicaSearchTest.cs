using System;

using System.IO;

using System.Net;
using System.Threading;

using System.Collections.Generic;

using NUnit.Framework;

using GISAServer.Search;

namespace GISAServer.SearchTester
{
    [TestFixture]
    public class UnidadeFisicaSearchTest
    {
        private UnidadeFisicaSearcher searcher;

        [TestFixtureSetUp]
        public void FixtureSetup()
        {
            this.searcher = new UnidadeFisicaSearcher("", "");   
        }        

        [Test]
        public void Request()
        {            
            //Console.WriteLine(this.searcher.GetTrusteeQuery("2007", "12", "12", "2008", "12", "31", ""));
        }
    }
}
