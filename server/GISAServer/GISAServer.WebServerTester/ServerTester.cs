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
    public class ServerTester
    {
        private Server server;

        [TestFixtureSetUp]
        public void FixtureSetup()
        {        
            this.server = new Server();
        }

        [SetUp]
        public void Setup()
        {
            this.server.StartListening();
        }

        [TearDown]
        public void TearDown()
        {
            this.server.StopListening();
        }

        [Test]
        public void Request()
        {
            this.WebRequestMethod();
        }

        [Test]
        public void OverloadingRequest()
        {         
            for (int i = 0; i < 20000; i++)
            {
                this.WebRequestMethod();
            }         
        }

        [Test]
        public void MultithreadingRequest()
        {
            this.MultiThreading(2);
        }

        [Test]
        public void OverloadingMultithreadingRequest()
        {
            this.MultiThreading(200000);            
        }

        private void WebRequestMethod()
        { 
            WebRequest request = WebRequest.Create("http://localhost:8888/?f=ping");
            Stream stream = request.GetResponse().GetResponseStream();
            StreamReader streamReader = new StreamReader(stream);
            Console.WriteLine(streamReader.ReadToEnd().Equals("pong"));
            request.GetResponse().Close();                    
        }

        private void MultiThreading(int numThreads)
        {
            List<Thread> threads = new List<Thread>(numThreads);
            for (int i = 0; i < numThreads; i++)
            {
                Thread t = new Thread(this.WebRequestMethod);
                t.Start();
                threads.Add(t);
            }

            foreach (Thread t in threads)
            {                
                t.Join();
            }
        }
    }
}
