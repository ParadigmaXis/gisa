using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GISA.Webservices.ProdDocInPortoWebService;
using System.Runtime.Serialization;
using System.IO;

namespace GISA.Webservices.DocInPortoWebservice
{
    public partial class DocumentoInfoArquivoGeral : ICloneable
    {
        public object Clone()
        {
            IFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, this);
                stream.Seek(0, SeekOrigin.Begin);
                return (DocumentoInfoArquivoGeral)formatter.Deserialize(stream);
            }
        }
    }

}
