using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GISA.Webservices.DocInPortoRecords
{
    // Example classe for example purposes only
    public class DocumentoInfoArquivoGeral
    {
        public string NUD { get; set; }
        public string NUMEROESP { get; set; }
        public string DATA_ARQUIVOGERAL { get; set; }
        public string ID_TIPOREGISTO { get; set; }
        public string TIPOREGISTO { get; set; }
        public string DATAREGISTO { get; set; }
        public string NUP { get; set; }
        public string UNIDADEORGANICA_NOMEMECANOGRAFICO { get; set; }
        public string ASSUNTO { get; set; } // é o nosso Ideográfico (tipicamente só haverá 1)
        public string ENTIDADE_NIF { get; set; } // NIF
        public string ENTIDADE_NOME { get; set; } // é o nosso onomástico (pode ser mais que 1)

        // campos que aparecerão no caso das licenças de obra
        public string CODMORADA { get; set; }
        public string NR_POLICIA { get; set; }
        public string CODIGOPOSTAL { get; set; }

        //public string LocalObra { get; set; } // é o nosso geográfico (pode ser mais que 1)
        //public string TipoProcesso { get; set; }
        //public long IDProdutor { get; set; }
        //public long IDAssunto { get; set; } // é o nosso Ideográfico (tipicamente só haverá 1)
        
        public string[] ARRAYANEXOS { get; set; }
        

        public DocumentoInfoArquivoGeral() { }
    }
}
