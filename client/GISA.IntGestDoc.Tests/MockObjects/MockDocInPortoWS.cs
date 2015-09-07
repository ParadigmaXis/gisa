using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GISA.Webservices.ProdDocInPortoWebService;
using GISA.Webservices.DocInPorto;
using GISA.IntGestDoc.Controllers;
using GISA.IntGestDoc.Tests.TestData;


namespace GISA.IntGestDoc.Tests.MockObjects
{
    public class MockDocInPortoWS : DocInPortoWS
    {
        public override List<DocumentoInfoArquivoGeral> GetDocumentosEnviadosParaArquivoGeral(DateTime timeStamp, int maxDocs)
        {
            return WebServiceDataSerialization.GetWSRecordsFromSerializedSource(@"TestData\HistoricalData\20100503-dipExamples.bin");
        }

        public override List<MoradaRecord> GetMoradas(IEnumerable<string> iEnumerable)
        {
            return WebServiceDataSerialization.GetWSToponimiasFromSerializedSource(@"TestData\HistoricalData\20100503-dipMoradasExamples.bin");
        }
    }
}
