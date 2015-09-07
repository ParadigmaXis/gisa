using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using GISA.Webservices.DocInPorto;
using GISA.Webservices.ProdDocInPortoWebService;

namespace GISA.IntGestDoc.Tests.TestData
{
    public static class DocInPortoRecords
    {
        public static List<DocumentoInfoArquivoGeral> CreateDocInPortoRecordsList1()
        {
            DocumentoInfoArquivoGeral dipr = new DocumentoInfoArquivoGeral();
            dipr.NUD = "1";
            dipr.NUP = "P/1";
            dipr.DATAREGISTO = new DateTime(2000, 10, 14).ToString("dd-MM-yyyy HH:mm:ss,FFFFFFF");
            dipr.ID_TIPOREGISTO = "1";
            dipr.TIPOREGISTO = "Licença de obra";
            dipr.NUMEROESP = "asd1";
            dipr.UNIDADEORGANICA_NOMEMECANOGRAFICO = "CMP1";
            dipr.ASSUNTO = "assunto1";
            dipr.ENTIDADE_NIF = "1";
            dipr.ENTIDADE_NOME = "manuel";
            dipr.CODMORADA = "xpto";
            dipr.DATA_ARQUIVOGERAL = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss,FFFFFFF");
            dipr.NR_POLICIA = "47";
            dipr.CODIGOPOSTAL = "4200-678";
            dipr.CONFIDENCIALIDADE = enConfidencialidade.enTipoConfidencialidadePublico;

            return new List<DocumentoInfoArquivoGeral>() { dipr };
        }

        public static List<DocumentoInfoArquivoGeral> CreateDocInPortoRecordsList2()
        {
            DocumentoInfoArquivoGeral dipr = new DocumentoInfoArquivoGeral();
            dipr.NUD = "2";
            dipr.DATAREGISTO = new DateTime(2000, 2, 14).ToString("dd-MM-yyyy HH:mm:ss,FFFFFFF");
            dipr.ID_TIPOREGISTO = "2";
            dipr.TIPOREGISTO = "Licença de utilização";
            dipr.NUMEROESP = "asd2";
            dipr.UNIDADEORGANICA_NOMEMECANOGRAFICO = "CMP2";
            dipr.ASSUNTO = "assunto2";
            dipr.ENTIDADE_NIF = "254875";
            dipr.ENTIDADE_NOME = "maria";
            dipr.CODMORADA = "xyz";
            dipr.DATA_ARQUIVOGERAL = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss,FFFFFFF");
            dipr.NUP = "2a";
            dipr.NR_POLICIA = "46";
            dipr.CODIGOPOSTAL = "4200-345";
            dipr.CONFIDENCIALIDADE = enConfidencialidade.enTipoConfidencialidadeConfidencial;

            return new List<DocumentoInfoArquivoGeral>() { dipr };
        }

        public static List<DocumentoInfoArquivoGeral> CreateDocInPortoRecordsList3()
        {
            DocumentoInfoArquivoGeral dipr1 = new DocumentoInfoArquivoGeral();
            dipr1.NUD = "3";
            dipr1.ID_TIPOREGISTO = "3";
            dipr1.TIPOREGISTO = "TIPOREGISTO genérico";
            dipr1.NUMEROESP = "asd3";
            dipr1.UNIDADEORGANICA_NOMEMECANOGRAFICO = "CMP";
            dipr1.ASSUNTO = "Assunto genérico";
            dipr1.ENTIDADE_NIF = "3";
            dipr1.ENTIDADE_NOME = "Joaquim";
            dipr1.CODMORADA = "abc";
            dipr1.DATA_ARQUIVOGERAL = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss,FFFFFFF");
            dipr1.DATAREGISTO = DateTime.Now.ToShortDateString();
            dipr1.NUP = "3a";
            dipr1.NR_POLICIA = "45";
            dipr1.CODIGOPOSTAL = "4200-012";

            DocumentoInfoArquivoGeral dipr2 = new DocumentoInfoArquivoGeral();
            dipr2.NUD = "4";
            dipr2.ID_TIPOREGISTO = "3";
            dipr2.TIPOREGISTO = "TIPOREGISTO genérico";
            dipr2.NUMEROESP = "asd4";
            dipr2.UNIDADEORGANICA_NOMEMECANOGRAFICO = "CMP";
            dipr2.ASSUNTO = "Assunto genérico";
            dipr2.ENTIDADE_NIF = "3";
            dipr2.ENTIDADE_NOME = "Joaquim";
            dipr2.CODMORADA = "abc";
            dipr2.DATA_ARQUIVOGERAL = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss,FFFFFFF");
            dipr2.DATAREGISTO = DateTime.Now.ToShortDateString();
            dipr2.NUP = "4a";
            dipr2.NR_POLICIA = "44";
            dipr2.CODIGOPOSTAL = "4200-789";

            return new List<DocumentoInfoArquivoGeral>() { dipr1, dipr2 };
        }

        // teste onde documentos e controlos de autoridade já existem no Gisa (sem correspondência)
        // NOTA: requer que os controlos de autoridade já existam na base de dados mas o documento não
        public static List<DocumentoInfoArquivoGeral> CreateDocInPortoRecordsList4()
        {
            DocumentoInfoArquivoGeral dipr = new DocumentoInfoArquivoGeral();
            dipr.NUD = "5";
            dipr.NUMEROESP = "NroGisa1";
            dipr.DATA_ARQUIVOGERAL = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss,FFFFFFF");
            dipr.ID_TIPOREGISTO = "5";
            dipr.TIPOREGISTO = "TIPOREGISTO Gisa";
            dipr.DATAREGISTO = DateTime.Now.ToShortDateString();
            dipr.NUP = "5a";
            dipr.UNIDADEORGANICA_NOMEMECANOGRAFICO = "ProdGisa";
            dipr.ASSUNTO = "Assunto Gisa";
            dipr.ENTIDADE_NIF = "5";
            dipr.ENTIDADE_NOME = "ENTIDADE_NOME Gisa";
            dipr.CODMORADA = "xpto";
            dipr.NR_POLICIA = "42";
            dipr.CODIGOPOSTAL = "4200-123";

            return new List<DocumentoInfoArquivoGeral>() { dipr };
        }

        // teste onde se tenta criar um controlo de autoridade com um termo que já é usado mas não como forma 
        // autorizada
        // NOTA: requer que exista um controlo de autoridade que tenha o termo "Assunto Paralelo" em qualquer uma
        //       forma excepto Forma Autorizada;
        //       requer que o documento não exista na base de dados
        public static List<DocumentoInfoArquivoGeral> CreateDocInPortoRecordsList5()
        {
            DocumentoInfoArquivoGeral dipr = new DocumentoInfoArquivoGeral();
            dipr.NUD = "6";
            dipr.ID_TIPOREGISTO = "5";
            dipr.TIPOREGISTO = "TIPOREGISTO Gisa";
            dipr.NUMEROESP = "NroGisa2";
            dipr.UNIDADEORGANICA_NOMEMECANOGRAFICO = "ProdGisa";
            dipr.ASSUNTO = "Assunto Paralelo";
            dipr.ENTIDADE_NIF = "6";
            dipr.ENTIDADE_NOME = "ENTIDADE_NOME Gisa";
            dipr.CODMORADA = "xyz";
            dipr.DATA_ARQUIVOGERAL = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss,FFFFFFF");
            dipr.DATAREGISTO = DateTime.Now.ToShortDateString();
            dipr.NUP = "6a";
            dipr.NR_POLICIA = "43";
            dipr.CODIGOPOSTAL = "4200-456";


            return new List<DocumentoInfoArquivoGeral>() { dipr };
        }

        public static List<MoradaRecord> CreateMoradaRecordsList1()
        {
            List<MoradaRecord> mrecords = new List<MoradaRecord>();
            mrecords.Add(new MoradaRecord() { CodigoMorada = "xpto", Nome = "Rua do XPTO" });
            mrecords.Add(new MoradaRecord() { CodigoMorada = "xyz", Nome = "Rua do XYZ" });
            mrecords.Add(new MoradaRecord() { CodigoMorada = "abc", Nome = "Rua do ABC" });
            return mrecords;
        }
    }
}
