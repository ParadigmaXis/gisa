using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GISA.IntGestDoc.Model;
using GISA.IntGestDoc.Model.EntidadesExternas;
using GISA.IntGestDoc.Model.EntidadesInternas;
using GISA.IntGestDoc.Controllers;

using NUnit.Framework;

namespace GISA.IntGestDoc.Tests
{
    [TestFixture]
    class TestSuggestionsFactory
    {

        List<DocumentoSimples> des = null;
        Dictionary<string, DocumentoGisa> correspDocsExistentes = null;
        Dictionary<string, Model.EntidadesInternas.Tipologia> correspTipologiasExistentes = null;
        Dictionary<string, Model.EntidadesInternas.Produtor> correspProdutoresExistentes = null;
        Dictionary<string, Model.EntidadesInternas.Ideografico> correspIdeograficosExistentes = null;
        Dictionary<string, Model.EntidadesInternas.Onomastico> correspOnomasticosExistentes = null;
        Dictionary<string, Model.EntidadesInternas.Geografico> correspGeograficosExistentes = null;
        DocumentoSimples deUm = null;
        DocumentoSimples deDois = null;
        DocumentoSimples deTres = null;
        DocumentoGisa docGisa = null;


        [SetUp]
        public void SetUp()
        {
            des = new List<DocumentoSimples>();
            correspDocsExistentes = new Dictionary<string, DocumentoGisa>();
            correspTipologiasExistentes = new Dictionary<string, Model.EntidadesInternas.Tipologia>();
            correspProdutoresExistentes = new Dictionary<string, Model.EntidadesInternas.Produtor>();
            correspIdeograficosExistentes = new Dictionary<string, Model.EntidadesInternas.Ideografico>();
            correspOnomasticosExistentes = new Dictionary<string, Model.EntidadesInternas.Onomastico>();
            correspGeograficosExistentes = new Dictionary<string, Model.EntidadesInternas.Geografico>();

            deUm = new DocumentoSimples(Sistema.DocInPorto, new DateTime(1985, 12, 6), 1);
            deDois = new DocumentoSimples(Sistema.DocInPorto, new DateTime(1965, 6, 26), 2);
            deTres = new DocumentoSimples(Sistema.DocInPorto, new DateTime(1965, 6, 26), 3);
            docGisa = new DocumentoGisa();

            deUm.NUD = "123987d";
            deUm.Processo = new DocumentoComposto(Sistema.DocInPorto, deUm.Timestamp, 1) { NUP = "123987p" };
            deUm.NumeroEspecifico = "42";
            deUm.DataCriacao = new DateTime(1979, 9, 13).ToShortDateString();
            deUm.Ideografico = new Model.EntidadesExternas.Ideografico(Sistema.DocInPorto, deUm.Timestamp);
            deUm.Ideografico.Titulo = "Igreja";
            deUm.Onomastico = new Model.EntidadesExternas.Onomastico(Sistema.DocInPorto, deUm.Timestamp);
            deUm.Onomastico.Titulo = "Manuel Silva";
            deUm.Toponimia = new Model.EntidadesExternas.Geografico(Sistema.DocInPorto, deUm.Timestamp);
            deUm.Toponimia.Titulo = "Parque das Rosas";
            deUm.Tipologia = new Model.EntidadesExternas.Tipologia(Sistema.DocInPorto, deUm.Timestamp);
            deUm.Tipologia.ID = "27";
            deUm.Tipologia.Titulo = "Alvará";

            deDois.NUD = "8734d";
            deDois.Processo = new DocumentoComposto(Sistema.DocInPorto, deDois.Timestamp, 2) { NUP = "8734p" };
            deDois.NumeroEspecifico = "15";
            deDois.DataCriacao = new DateTime(1934, 9, 11).ToShortDateString();
            deDois.Ideografico = new Model.EntidadesExternas.Ideografico(Sistema.DocInPorto, deDois.Timestamp);
            deDois.Ideografico.Titulo = "Catedral";
            deDois.Onomastico = new Model.EntidadesExternas.Onomastico(Sistema.DocInPorto, deDois.Timestamp);
            deDois.Onomastico.Titulo = "Maria Silva";
            deDois.Toponimia = new Model.EntidadesExternas.Geografico(Sistema.DocInPorto, deDois.Timestamp);
            deDois.Toponimia.Titulo = "Rua do Carmo";
            deDois.Tipologia = new Model.EntidadesExternas.Tipologia(Sistema.DocInPorto, deDois.Timestamp);
            deDois.Tipologia.Titulo = "Fotografia do Local";
            deDois.Tipologia.ID = "23";

            deTres.NUD = "12345d";
            deTres.Processo = new DocumentoComposto(Sistema.DocInPorto, deTres.Timestamp, 3) { NUP = "12345p" };
            deTres.NumeroEspecifico = "";
            deTres.DataCriacao = new DateTime(1934, 9, 11).ToShortDateString();
            deTres.Ideografico = new Model.EntidadesExternas.Ideografico(Sistema.DocInPorto, deTres.Timestamp);
            deTres.Ideografico.Titulo = "Catedral";
            deTres.Onomastico = new Model.EntidadesExternas.Onomastico(Sistema.DocInPorto, deTres.Timestamp);
            deTres.Onomastico.Titulo = "Maria Silva";
            deTres.Toponimia = new Model.EntidadesExternas.Geografico(Sistema.DocInPorto, deTres.Timestamp);
            deTres.Toponimia.Titulo = "Parque das Rosas";
            deTres.Tipologia = new Model.EntidadesExternas.Tipologia(Sistema.DocInPorto, deTres.Timestamp);
            deTres.Tipologia.ID = "27";
            deTres.Tipologia.Titulo = "Alvará";


            docGisa.Estado = TipoEstado.SemAlteracoes;
            docGisa.Codigo = "123987d";
            docGisa.DataCriacao.Valor = new DataIncompleta(new DateTime(1979, 10, 3).ToShortDateString());
            DocumentoGisa docC = new DocumentoGisa();
            docC.Id = 6;
            docC.Estado = TipoEstado.SemAlteracoes;
            docC.Titulo = "35423";
            docC.Codigo = "35423";

            var produtor = new Model.EntidadesInternas.Produtor();
            produtor.Id = 1;
            produtor.Codigo = "CMP-UCD";
            produtor.Titulo = "Unidade Central de Digitalização";
            produtor.Estado = TipoEstado.SemAlteracoes;

            var ideografico = new Model.EntidadesInternas.Ideografico();
            ideografico.Id = 2;
            ideografico.Titulo = "Igreja";
            ideografico.Estado = TipoEstado.SemAlteracoes;

            var onomastico1 = new Model.EntidadesInternas.Onomastico();
            onomastico1.Id = 3;
            onomastico1.Titulo = "Manuel Silva";
            onomastico1.Codigo = "123456789";
            onomastico1.Estado = TipoEstado.SemAlteracoes;

            var onomastico2 = new Model.EntidadesInternas.Onomastico();
            onomastico2.Id = 6;
            onomastico2.Titulo = "João Silva";
            onomastico2.Codigo = "";
            onomastico2.Estado = TipoEstado.SemAlteracoes;

            var geografico = new Model.EntidadesInternas.Geografico();
            geografico.Id = 4;
            geografico.Titulo = "Rua das Rosas";
            geografico.Estado = TipoEstado.SemAlteracoes;

            var tipologia = new Model.EntidadesInternas.Tipologia();
            tipologia.Id = 5;
            tipologia.Titulo = "Alvará";
            tipologia.Estado = TipoEstado.SemAlteracoes;

            docGisa.Produtores.Add(produtor.Titulo);
            docGisa.Ideograficos.Add(ideografico.Titulo);
            docGisa.Onomasticos.Add(onomastico1.Titulo);
            docGisa.Toponimias.Add(geografico.Titulo);
            docGisa.Tipologia = tipologia.Titulo;

            des.Add(deUm);
            des.Add(deDois);
            des.Add(deTres);

            correspDocsExistentes.Add(deUm.IDExterno, docGisa);
            correspProdutoresExistentes.Add("CMP-UCD", produtor);
            correspTipologiasExistentes.Add("27", tipologia);
            correspIdeograficosExistentes.Add("Igreja", ideografico);
            correspOnomasticosExistentes.Add("123456789", onomastico1);
            correspOnomasticosExistentes.Add("João Silva", onomastico2);
            correspGeograficosExistentes.Add("RROSAS", geografico);
        }

        [Test, Ignore]
        public void MakeSomeTestsWithTheScenarioBuiltInTheSetupMethod() 
        {
            Assert.True(false);
        }
    }
}
