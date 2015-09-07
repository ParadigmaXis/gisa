using System;

using System.IO;

using System.Net;
using System.Threading;

using System.Collections.Generic;

using NUnit.Framework;

using GISAServer.WebServer;

namespace GISAServer.WebServerTester
{
    [TestFixture]
    public class ServerCoreTester
    {
        private ServerCore server;

        [TestFixtureSetUp]
        public void FixtureSetup()
        {        
            this.server = new ServerCore();
        }              
        
    }
}
