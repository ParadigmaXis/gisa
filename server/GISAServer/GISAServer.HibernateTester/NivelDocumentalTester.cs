using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using GISAServer.Hibernate;
using GISAServer.Hibernate.Exceptions;
using GISAServer.Hibernate.Utils;

namespace GISAServer.HibernateTester
{
    [TestFixture]    
    public class NivelDocumentalTester
    {                
        [Test]
        [ExpectedException(typeof(InvalidIdException))]
        public void GetAnInvalidNivelDocumental()
        {
            NivelDocumental nd = new NivelDocumental(0);
        }

        
        [Test]
        [ExpectedException(typeof(InvalidIdException))]
        public void GetANegativeIdNivelDocumental()
        {
            NivelDocumental nd = new NivelDocumental(-1);
        }
        
        [Test]        
        public void GetAGivenNivelDocumental()
        {
            NivelDocumental nd = new NivelDocumental(250699);
        }

        [Test]        
        public void GetARandomDocument()
        {            
            IList<long> ids = GISAUtils.getAllNivelDocumentalIds();

            if (ids.Count > 0)
            {
                Random random = new Random();
                int pos = random.Next(0, ids.Count - 1);

                long initMemory = GC.GetTotalMemory(true);
                NivelDocumental doc = new NivelDocumental(ids[pos]);
                long usedMemory = (GC.GetTotalMemory(true) - initMemory) / 1024;

                Console.WriteLine("One Document Memory: " + usedMemory.ToString() + "KB");
                Assert.IsNotNull(doc);
            }
        }

        [Test]
        public void GetAllNivelDocumental()
        {
            try
            {
                IList<long> ids = GISAUtils.getAllNivelDocumentalIds();

                long initMemory = GC.GetTotalMemory(true);
                foreach (long id in ids)
                {
                    NivelDocumental doc = new NivelDocumental(id);
                    Assert.IsNotNull(doc);
                }
                long usedMemory = (GC.GetTotalMemory(true) - initMemory) / 1024;

                GC.Collect();
                GC.WaitForPendingFinalizers();

                Console.WriteLine("All Documents Memory: " + usedMemory.ToString() + "KB");
            }
            catch (InsufficientMemoryException)
            {
                Console.WriteLine("It's too much for me...!");
            }
        }
    }
}
