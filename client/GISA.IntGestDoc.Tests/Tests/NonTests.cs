using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

using NUnit.Framework;
using GISA.IntGestDoc.Tests.TestData;
using GISA.Webservices.ProdDocInPortoWebService;
using GISA.Webservices.ToponimiaWS;

namespace GISA.IntGestDoc.Tests.NonTests
{
    [TestFixture]
    class SerializationUtilities
    {

        [Test]
        public void LoadHistoricDocuments()
        {
            Assert.AreEqual(WebServiceDataSerialization.GetWSRecordsFromSerializedSource(@"TestData\HistoricalData\20100503-dipExamples.bin").Count, 100);
        }

        [Test,
        Description("Grava para disco dados obtidos dos webservices da CMP"),
        Ignore("Usado apenas para experimentar as chamadas ao webservice.")]
        public void GetWebServiceData()
        {
            string username = "username";
            string password = "password";
            WebServiceDataSerialization.SerializeWSRecords("dipExamples.bin");
            WebServiceDataSerialization.RetrieveConteudoAndWriteToFile(username, password, "I/915/08/CMP");
            WebServiceDataSerialization.RetrieveAnexosAndWriteToFiles(username, password, "13-11-2000 18:49:43,696875000", long.MaxValue);
            WebServiceDataSerialization.SerializeWSToponimias(username, password, "13-11-2000 18:49:43,696875000", "dipMoradasExamples.bin");
        }
    }
}
