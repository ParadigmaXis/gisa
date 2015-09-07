using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using NUnit.Framework;

using GISA.IntGestDoc.Model;
using GISA.Webservices.DocInPorto;
using GISA.IntGestDoc.Model.EntidadesExternas;
using GISA.IntGestDoc.Model.EntidadesInternas;
using GISA.IntGestDoc.Controllers;
using GISA.Webservices.ProdDocInPortoWebService;
using GISA.IntGestDoc.Tests.TestData;
using GISA.IntGestDoc.Tests.MockObjects;

namespace GISA.IntGestDoc.Tests
{
    [TestFixture]
    class TestMapDocInPortoRecordsToEntidadesExternas
    {
        [Test]
        public void TestMapDocInPortoRecordToDocumentoExterno1()
        {
            var mrecords = DocInPortoRecords.CreateMoradaRecordsList1();
            var diprecords = DocInPortoRecords.CreateDocInPortoRecordsList1();
            DocumentoInfoArquivoGeral diprecord = diprecords[0];

            var serv = new MockDocInPortoWS();
            List<DocumentoExterno> des = serv.ToDocumentosExternos(diprecords, mrecords);
            
            DocumentoComposto dc = (DocumentoComposto)des[0];
            DocumentoSimples ds = (DocumentoSimples)des[1];
            Assert.AreEqual(diprecord.NUD, ds.NUD);
            Assert.AreEqual(diprecord.NUMEROESP, ds.NumeroEspecifico);
            Assert.AreEqual(diprecord.DATA_ARQUIVOGERAL, ds.Timestamp.ToString("dd-MM-yyyy HH:mm:ss,FFFFFFF"));

            Assert.AreEqual(diprecord.NUP, dc.NUP);
            Assert.AreEqual(diprecord.DATAREGISTO, dc.DataInicio);
            Assert.AreEqual(null, dc.Confidencialidade);
            Assert.AreEqual(diprecord.ASSUNTO, dc.Tipologia.Titulo);
            Assert.Contains(diprecord.ENTIDADE_NOME, dc.RequerentesOuProprietariosIniciais.ToList());
            Assert.Contains("Rua do XPTO", dc.LocalizacoesObraDesignacaoActual.Select(g => g.LocalizacaoObraDesignacaoActual.Titulo).ToList());
            Assert.AreEqual(0, dc.TecnicosDeObra.Count);
        }

        [Test]
        public void TestMapDocInPortoRecordToDocumentoExterno2()
        {
            var serv = new MockDocInPortoWS();
            var diprecords = DocInPortoRecords.CreateDocInPortoRecordsList1();
            var mrecords = DocInPortoRecords.CreateMoradaRecordsList1();
            var diprecord = diprecords[0];
            var docsExts = serv.ToDocumentosExternos(diprecords, mrecords);

            Assert.AreEqual(2, docsExts.Count);
            Assert.IsInstanceOf(typeof(DocumentoComposto), docsExts[0]);
            Assert.IsInstanceOf(typeof(DocumentoSimples), docsExts[1]);
            
            var docExtSimples = (DocumentoSimples)docsExts[1];
            Assert.AreEqual(Sistema.DocInPorto, docExtSimples.Sistema);
            Assert.True(docExtSimples.DataCriacao.Equals(diprecord.DATAREGISTO));
            Assert.AreEqual(diprecord.NUD, docExtSimples.NUD);
            Assert.AreEqual(diprecord.NUP, docExtSimples.Processo.NUP);
            Assert.AreEqual(diprecord.NUMEROESP, docExtSimples.NumeroEspecifico);
            Assert.AreEqual(diprecord.DATA_ARQUIVOGERAL, docExtSimples.Timestamp.ToString("dd-MM-yyyy HH:mm:ss,FFFFFFF"));

            Assert.IsNotNull(docExtSimples.Tipologia);
            Assert.AreEqual(diprecord.TIPOREGISTO, docExtSimples.Tipologia.Titulo);

            Assert.IsNotNull(docExtSimples.Onomastico);
            Assert.AreEqual(diprecord.ENTIDADE_NOME, docExtSimples.Onomastico.Titulo);

            Assert.IsNotNull(docExtSimples.Ideografico);
            Assert.AreEqual(diprecord.ASSUNTO, docExtSimples.Ideografico.Titulo);

            Assert.IsNotNull(docExtSimples.Toponimia);
            Assert.AreEqual(mrecords.Single(mrecord => mrecord.CodigoMorada == diprecord.CODMORADA).Nome, docExtSimples.Toponimia.Titulo);
        }

        [Test]
        public void TestMapDocInPortoRecordToDocumentoExternoWithDocumentoIdentity()
        {
            var diprecords = new List<DocumentoInfoArquivoGeral>();
            diprecords.AddRange(DocInPortoRecords.CreateDocInPortoRecordsList3());
            diprecords.AddRange(DocInPortoRecords.CreateDocInPortoRecordsList3());
            var serv = new MockDocInPortoWS();
            var docsExts = serv.ToDocumentosExternos(diprecords, new List<MoradaRecord>());

            Assert.AreEqual(2, docsExts.OfType<DocumentoComposto>().Count());
            Assert.AreEqual(2, docsExts.OfType<DocumentoSimples>().Count());
        }

        [Test]
        public void TestMapDocInPortoRecordToDocumentoExternoWithTipologiaIdentity()
        {
            var diprecords = new List<DocumentoInfoArquivoGeral>();
            diprecords.AddRange(DocInPortoRecords.CreateDocInPortoRecordsList3());
            diprecords.AddRange(DocInPortoRecords.CreateDocInPortoRecordsList1());
            var serv = new MockDocInPortoWS();
            var docsExts = serv.ToDocumentosExternos(diprecords, new List<MoradaRecord>());
            var docExtSimples1a = (DocumentoSimples)docsExts[1];
            var docExtSimples1b = (DocumentoSimples)docsExts[3];
            var docExtSimples3 = (DocumentoSimples)docsExts[5];

            Assert.AreEqual(docExtSimples1a.Tipologia, docExtSimples1b.Tipologia);
            Assert.AreSame(docExtSimples1a.Tipologia, docExtSimples1b.Tipologia);

            Assert.AreNotEqual(docExtSimples1a.Tipologia, docExtSimples3.Tipologia);
            Assert.AreNotSame(docExtSimples1a.Tipologia, docExtSimples3.Tipologia);
        }

        [Test]
        public void TestMapDocInPortoRecordToDocumentoExternoWithOnomasticoIdentity()
        {
            var diprecords = new List<DocumentoInfoArquivoGeral>();
            diprecords.AddRange(DocInPortoRecords.CreateDocInPortoRecordsList3());
            diprecords.AddRange(DocInPortoRecords.CreateDocInPortoRecordsList1());
            var serv = new MockDocInPortoWS();
            var docsExts = serv.ToDocumentosExternos(diprecords, new List<MoradaRecord>());
            var docExtSimples1 = (DocumentoSimples)docsExts[1];
            var docExtSimples2 = (DocumentoSimples)docsExts[3];
            var docExtSimples3 = (DocumentoSimples)docsExts[5];

            Assert.AreEqual(docExtSimples1.Onomastico, docExtSimples2.Onomastico);
            Assert.AreSame(docExtSimples1.Onomastico, docExtSimples2.Onomastico);

            Assert.AreNotEqual(docExtSimples1.Onomastico, docExtSimples3.Onomastico);
            Assert.AreNotSame(docExtSimples1.Onomastico, docExtSimples3.Onomastico);
        }

        [Test]
        public void TestMapDocInPortoRecordToDocumentoExternoWithIdeograficoIdentity()
        {
            var diprecords = new List<DocumentoInfoArquivoGeral>();
            diprecords.AddRange(DocInPortoRecords.CreateDocInPortoRecordsList3());
            diprecords.AddRange(DocInPortoRecords.CreateDocInPortoRecordsList1());
            var serv = new MockDocInPortoWS();
            var docsExts = serv.ToDocumentosExternos(diprecords, new List<MoradaRecord>());
            var docExtSimples1a = (DocumentoSimples)docsExts[1];
            var docExtSimples1b = (DocumentoSimples)docsExts[3];
            var docExtSimples2 = (DocumentoSimples)docsExts[5];

            Assert.AreEqual(docExtSimples1a.Ideografico, docExtSimples1b.Ideografico);
            Assert.AreSame(docExtSimples1a.Ideografico, docExtSimples1b.Ideografico);

            Assert.AreNotEqual(docExtSimples1a.Ideografico, docExtSimples2.Ideografico);
            Assert.AreNotSame(docExtSimples1a.Ideografico, docExtSimples2.Ideografico);
        }

        [Test]
        public void TestMapDocInPortoRecordToDocumentoExternoWithToponimiaIdentity()
        {
            var diprecords = new List<DocumentoInfoArquivoGeral>();
            diprecords.AddRange(DocInPortoRecords.CreateDocInPortoRecordsList3());
            diprecords.AddRange(DocInPortoRecords.CreateDocInPortoRecordsList1());
            var serv = new MockDocInPortoWS();
            var docsExts = serv.ToDocumentosExternos(diprecords, new List<MoradaRecord>());
            var docExtSimples1a = (DocumentoSimples)docsExts[1];
            var docExtSimples1b = (DocumentoSimples)docsExts[3];
            var docExtSimples3 = (DocumentoSimples)docsExts[5];

            Assert.AreEqual(docExtSimples1a.Toponimia, docExtSimples1b.Toponimia);
            Assert.AreSame(docExtSimples1a.Toponimia, docExtSimples1b.Toponimia);

            Assert.AreNotEqual(docExtSimples1a.Toponimia, docExtSimples3.Toponimia);
            Assert.AreNotSame(docExtSimples1a.Toponimia, docExtSimples3.Toponimia);
        }

        [Test]
        public void TestMapDocInPortoRecordToDocumentoExterno_Confidencialidade()
        {
            DocumentoInfoArquivoGeral c1 = DocInPortoRecords.CreateDocInPortoRecordsList1()[0];
            DocumentoInfoArquivoGeral c2 = DocInPortoRecords.CreateDocInPortoRecordsList1()[0];
            DocumentoInfoArquivoGeral c3 = DocInPortoRecords.CreateDocInPortoRecordsList1()[0];
            c1.NUD = "1";
            c1.NUP = "1";
            c1.CONFIDENCIALIDADE = enConfidencialidade.enTipoConfidencialidadePublico;
            c2.NUD = "2";
            c2.NUP = "2";
            c2.CONFIDENCIALIDADE = enConfidencialidade.enTipoConfidencialidadeConfidencial;
            c3.NUD = "3";
            c3.NUP = "3";
            c3.CONFIDENCIALIDADE = enConfidencialidade.enTipoConfidencialidadeRestrito;

            var diprecords = new List<DocumentoInfoArquivoGeral>() {c1, c2, c3};

            var serv = new MockDocInPortoWS();
            var docsExts = serv.ToDocumentosExternos(diprecords, new List<MoradaRecord>());
            Assert.IsNull(((DocumentoComposto)docsExts[0]).Confidencialidade);
            Assert.AreEqual("Confidencial", ((DocumentoComposto)docsExts[2]).Confidencialidade);
            Assert.AreEqual("Restrito", ((DocumentoComposto)docsExts[4]).Confidencialidade);
        }
    }
}
