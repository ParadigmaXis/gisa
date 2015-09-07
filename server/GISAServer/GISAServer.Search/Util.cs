using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Lucene.Net.Documents;
using Lucene.Net.Analysis;
using GISAServer.Hibernate;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using NHibernate;
using GISAServer.Hibernate.Utils;
using Lucene.Net.Analysis.Standard;

namespace GISAServer.Search
{
    public class Util
    {
        private static readonly string IndexDir = "index";

        public static readonly string NivelDocumentalPath = AppDomain.CurrentDomain.BaseDirectory + Path.DirectorySeparatorChar + IndexDir + System.IO.Path.DirectorySeparatorChar + "NivelDocumental";
        public static readonly string NivelDocumentalInternetPath = AppDomain.CurrentDomain.BaseDirectory + Path.DirectorySeparatorChar + IndexDir + System.IO.Path.DirectorySeparatorChar + "NivelDocumentalInternet";
        public static readonly string NivelDocumentalComProdutoresPath = AppDomain.CurrentDomain.BaseDirectory + Path.DirectorySeparatorChar + IndexDir + System.IO.Path.DirectorySeparatorChar + "NivelDocumentalComProdutores";
        public static readonly string UnidadeFisicaPath = AppDomain.CurrentDomain.BaseDirectory + Path.DirectorySeparatorChar + IndexDir + System.IO.Path.DirectorySeparatorChar + "UnidadeFisica";
        public static readonly string ProdutorPath = AppDomain.CurrentDomain.BaseDirectory + Path.DirectorySeparatorChar + IndexDir + System.IO.Path.DirectorySeparatorChar + "Produtor";
        public static readonly string AssuntosPath = AppDomain.CurrentDomain.BaseDirectory + Path.DirectorySeparatorChar + IndexDir + System.IO.Path.DirectorySeparatorChar + "Assuntos";
        public static readonly string TipologiasPath = AppDomain.CurrentDomain.BaseDirectory + Path.DirectorySeparatorChar + IndexDir + System.IO.Path.DirectorySeparatorChar + "Tipologias";

        public static Document NivelDocumentalToLuceneDocument(NivelDocumental nd)
        {
            Field fieldId = new Field("id", nd.Id, Field.Store.YES, Field.Index.NOT_ANALYZED);
            Field fieldCodigo = new Field("codigo", nd.Codigo.ToLower(), Field.Store.NO, Field.Index.NOT_ANALYZED);
            Field fieldDesignacaoTipoNivelRelacionado = new Field("designacaoTipoNivelRelacionado", nd.DesignacaoTipoNivelRelacionado, Field.Store.NO, Field.Index.ANALYZED);
            Field fieldDesignacaoSorted = new Field("designacaoSorted", nd.DesignacaoTipoNivelRelacionado.ToLower(), Field.Store.YES, Field.Index.NOT_ANALYZED);
            Field fieldNotaDoArquivista = new Field("notaDoArquivista", nd.NotaDoArquivista, Field.Store.NO, Field.Index.ANALYZED);
            Field fieldRegrasOuConvencoes = new Field("regrasOuConvencoes", nd.RegrasOuConvencoes, Field.Store.NO, Field.Index.ANALYZED);
            Field fieldHistoriaAdministrativa = new Field("historiaAdministrativa", nd.HistoriaAdministrativa, Field.Store.NO, Field.Index.ANALYZED);
            Field fieldHistoriaCustodial = new Field("historiaCustodial", nd.HistoriaCustodial, Field.Store.NO, Field.Index.ANALYZED);
            Field fieldFonteImediataDeAquisicao = new Field("fonteImediataDeAquisicao", nd.FonteImediataDeAquisicao, Field.Store.NO, Field.Index.ANALYZED);
            Field fieldDesignacaoTipoTradicaoDocumental = new Field("designacaoTipoTradicaoDocumental", nd.DesignacaoTipoTradicaoDocumental, Field.Store.NO, Field.Index.ANALYZED);
            Field fieldInicioTexto = new Field("inicioTexto", nd.InicioTexto, Field.Store.NO, Field.Index.ANALYZED);
            Field fieldInicioAtribuida = new Field("inicioAtribuida", nd.InicioAtribuida, Field.Store.NO, Field.Index.ANALYZED);
            Field fieldFimTexto = new Field("fimTexto", nd.FimTexto, Field.Store.NO, Field.Index.ANALYZED);
            Field fieldFimAtribuida = new Field("fimAtribuida", nd.FimAtribuida, Field.Store.NO, Field.Index.ANALYZED);
            Field fieldExistenciaDeOriginais = new Field("existenciaDeOriginais", nd.ExistenciaDeOriginais, Field.Store.NO, Field.Index.ANALYZED);
            Field fieldExistenciaDeCopias = new Field("existenciaDeCopias", nd.ExistenciaDeCopias, Field.Store.NO, Field.Index.ANALYZED);
            Field fieldUnidadesRelacionadas = new Field("unidadesRelacionadas", nd.UnidadesRelacionadas, Field.Store.NO, Field.Index.ANALYZED);
            Field fieldNotaDePublicacao = new Field("notaDePublicacao", nd.NotaDePublicacao, Field.Store.NO, Field.Index.ANALYZED);
            Field fieldConteudoInformacional = new Field("conteudo", nd.ConteudoInformacional, Field.Store.NO, Field.Index.ANALYZED);
            Field fieldIncorporacao = new Field("incorporacao", nd.Incorporacao, Field.Store.NO, Field.Index.ANALYZED);
            Field fieldDesignacaoTipoOrdenacao = new Field("designacaoTipoOrdenacao", nd.DesignacaoTipoOrdenacao, Field.Store.NO, Field.Index.ANALYZED);
            Field fieldNotaGeral = new Field("notaGeral", nd.NotaGeral, Field.Store.NO, Field.Index.ANALYZED);
            Field fieldEstatutoLegal = new Field("estatutoLegal", nd.EstatutoLegal, Field.Store.NO, Field.Index.ANALYZED);
            Field fieldCondicaoDeAcesso = new Field("condicaoDeAcesso", nd.CondicaoDeAcesso, Field.Store.NO, Field.Index.ANALYZED);
            Field fieldCondicaoDeReproducao = new Field("condicaoDeReproducao", nd.CondicaoDeReproducao, Field.Store.NO, Field.Index.ANALYZED);
            Field fieldAuxiliarDePesquisa = new Field("auxiliarDePesquisa", nd.AuxiliarDePesquisa, Field.Store.NO, Field.Index.ANALYZED);
            Field fieldNotasExplicativas = new Field("notasExplicativas", nd.NotasExplicativas, Field.Store.NO, Field.Index.ANALYZED);
            Field fieldChavesColectividade = new Field("chavesColectividade", nd.ChavesColectividade, Field.Store.NO, Field.Index.ANALYZED);
            Field fieldRegrasConvencoes = new Field("regrasConvencoes", nd.RegrasConvencoes, Field.Store.NO, Field.Index.ANALYZED);
            Field fieldObservacoesControloAuto = new Field("observacoesControloAuto", nd.ObservacoesControloAuto, Field.Store.NO, Field.Index.ANALYZED);
            Field fieldDescHistoricas = new Field("descHistoricas", nd.DescHistoricas, Field.Store.NO, Field.Index.ANALYZED);
            Field fieldDescZonasGeograficas = new Field("descZonasGeograficas", nd.DescZonasGeograficas, Field.Store.NO, Field.Index.ANALYZED);
            Field fieldDescEstatutosLegais = new Field("descEstatutosLegais", nd.DescEstatutosLegais, Field.Store.NO, Field.Index.ANALYZED);
            Field fieldDescOcupacoesActividades = new Field("descOcupacoesActividades", nd.DescOcupacoesActividades, Field.Store.NO, Field.Index.ANALYZED);
            Field fieldDescEnquadramentosLegais = new Field("descEnquadramentosLegais", nd.DescEnquadramentosLegais, Field.Store.NO, Field.Index.ANALYZED);
            Field fieldDescEstruturasInternas = new Field("descEstruturasInternas", nd.DescEstruturasInternas, Field.Store.NO, Field.Index.ANALYZED);
            Field fieldDescContextosGerais = new Field("descContextosGerais", nd.DescContextosGerais, Field.Store.NO, Field.Index.ANALYZED);
            Field fieldDesignacoesTipoNoticiaAuto = new Field("designacoesTipoNoticiaAuto", nd.DesignacoesTipoNoticiaAuto, Field.Store.NO, Field.Index.ANALYZED);
            Field fieldTermosDeIndexacao = new Field("assunto", nd.TermosDeIndexacao, Field.Store.NO, Field.Index.ANALYZED);
            Field fieldTipologiaInformacional = new Field("tipologiaInformacional", nd.TipologiaInformacional, Field.Store.NO, Field.Index.ANALYZED);
            Field fieldDesignacoesTipoEntidadeProdutora = new Field("designacoesTipoEntidadeProdutora", nd.DesignacoesTipoEntidadeProdutora, Field.Store.NO, Field.Index.ANALYZED);
            Field fieldEntidadeProdutora = new Field("entidadeProdutora", nd.EntidadeProdutora, Field.Store.NO, Field.Index.ANALYZED);
            Field fieldAutor = new Field("autor", nd.Autor, Field.Store.NO, Field.Index.ANALYZED);
            Field fieldDesignacoesTipoFormaSuporteAcond = new Field("designacoesTipoFormaSuporteAcond", nd.DesignacoesTipoFormaSuporteAcond, Field.Store.NO, Field.Index.ANALYZED);
            Field fieldDesignacoesTipoMaterialDeSuporte = new Field("designacoesTipoMaterialDeSuporte", nd.DesignacoesTipoMaterialDeSuporte, Field.Store.NO, Field.Index.ANALYZED);
            Field fieldDesignacoesTipoTecnicasDeRegisto = new Field("designacoesTipoTecnicasDeRegisto", nd.DesignacoesTipoTecnicasDeRegisto, Field.Store.NO, Field.Index.ANALYZED);
            Field fieldDesignacoesTipoEstadoDeConservacao = new Field("designacoesTipoEstadoDeConservacao", nd.DesignacoesTipoEstadoDeConservacao, Field.Store.NO, Field.Index.ANALYZED);
            Field fieldLanguageNamesEnglish = new Field("languageNamesEnglish", nd.LanguageNamesEnglish, Field.Store.NO, Field.Index.ANALYZED);
            Field fieldScriptNamesEnglish = new Field("scriptNamesEnglish", nd.ScriptNamesEnglish, Field.Store.NO, Field.Index.ANALYZED);
            Field fieldFrequencia = new Field("frequencia", nd.Frequencia, Field.Store.NO, Field.Index.ANALYZED);
            Field fieldPreservar = new Field("preservar", nd.Preservar, Field.Store.NO, Field.Index.ANALYZED);
            Field fieldPublicar = new Field("publicar", nd.Publicar, Field.Store.NO, Field.Index.ANALYZED);
            Field fieldObservacoesSFRDAvaliacao = new Field("observacoesSFRDAvaliacao", nd.ObservacoesSFRDAvaliacao, Field.Store.NO, Field.Index.ANALYZED);
            Field fieldIdAutoEliminacao = new Field("idAutoEliminacao", nd.IdAutoEliminacao, Field.Store.NO, Field.Index.ANALYZED);
            Field fieldDesignacaoTipoSubDensidade = new Field("designacaoTipoSubDensidade", nd.DesignacaoTipoSubDensidade, Field.Store.NO, Field.Index.ANALYZED);
            Field fieldDesignacaoTipoDensidade = new Field("designacaoTipoDensidade", nd.DesignacaoTipoDensidade, Field.Store.NO, Field.Index.ANALYZED);
            Field fieldDesignacaoTipoPertinencia = new Field("designacaoTipoPertinencia", nd.DesignacaoTipoPertinencia, Field.Store.NO, Field.Index.ANALYZED);
            Field fieldPonderacao = new Field("ponderacao", nd.Ponderacao, Field.Store.NO, Field.Index.ANALYZED);
            Field fieldDesignacaoAutoEliminacao = new Field("designacaoAutoEliminacao", nd.DesignacaoAutoEliminacao, Field.Store.NO, Field.Index.ANALYZED);
            Field fieldDesignacaoNivelDesignado = new Field("titulo", nd.DesignacaoNivelDesignado, Field.Store.NO, Field.Index.ANALYZED);
            Field fieldDesignacaoNivelSorted = new Field("designacaoNivelSorted", nd.DesignacaoNivelDesignado.ToLower(), Field.Store.YES, Field.Index.ANALYZED);
            Field fieldAgrupador = new Field("agrupador", nd.Agrupador, Field.Store.NO, Field.Index.ANALYZED);
            List<Field> fieldCotas = new List<Field>();
            foreach (string cota in nd.Cota)
                fieldCotas.Add(new Field("cota", cota.ToLower(), Field.Store.NO, Field.Index.NOT_ANALYZED));
            Field fieldNumImagens = new Field("objetos", nd.NumImagens, Field.Store.NO, Field.Index.NOT_ANALYZED);
            Field fieldNumODsNaoPub = new Field("objetosNaoPublicados", nd.NumODsNaoPublicados, Field.Store.NO, Field.Index.NOT_ANALYZED);
            Field fieldNumODsPub = new Field("objetosPublicados", nd.NumODsPublicados, Field.Store.NO, Field.Index.NOT_ANALYZED);
            Field fieldInicioProducao = new Field("inicioProducao", ToLuceneDate(nd.DataInicioProd,true), Field.Store.YES, Field.Index.NOT_ANALYZED);
            Field fieldFimProducao = new Field("fimProducao", ToLuceneDate(nd.DataFimProd, false), Field.Store.YES, Field.Index.NOT_ANALYZED);
            Field fieldIdUpper = new Field("idUpper", nd.IdUpper, Field.Store.NO, Field.Index.ANALYZED);
            Field fieldIdControloAutoridade = new Field("idControloAutoridade", nd.IdsControlosAutoridade, Field.Store.NO, Field.Index.ANALYZED);
            Field allFields = new Field("all", nd.ToString(), Field.Store.NO, Field.Index.ANALYZED);
            Field fieldExiste = new Field("existe", "sim", Field.Store.NO, Field.Index.ANALYZED);
            Field _CodigoCodigo = new Field("Codigo_Codigo", nd.Codigo_Codigo, Field.Store.NO, Field.Index.ANALYZED);

            #region Field defs.: Licencas de obra

            // LicencaObra
            Field _TipoObra = new Field("tipoObra", nd.TipoObra, Field.Store.NO, Field.Index.ANALYZED);
            Field _PHSimNao = new Field("LicencaObra_PHSimNao", nd.PHSimNao, Field.Store.NO, Field.Index.ANALYZED);
            Field _PHTexto = new Field("LicencaObra_PHTexto", nd.PHTexto, Field.Store.NO, Field.Index.ANALYZED);
            Field _PropriedadeHorizontal = new Field("propriedadeHorizontal", nd.PHCompleto, Field.Store.NO, Field.Index.ANALYZED);
            Field _CodigosAtestadoHabitabilidade = new Field("atestado", nd.CodigosAtestadoHabitabilidade, Field.Store.NO, Field.Index.ANALYZED);
            Field _Datas_LicencaObraDataLicencaConstrucao = new Field("Datas_LicencaObraDataLicencaConstrucao", nd.Datas_LicencaObraDataLicencaConstrucao, Field.Store.NO, Field.Index.ANALYZED);
            Field _NumPolicia_LicencaObraLocalizacaoObraActual = new Field("numPoliciaAtual", nd.NumPolicia_LicencaObraLocalizacaoObraActual, Field.Store.NO, Field.Index.ANALYZED);
            Field _Termo_LicencaObraLocalizacaoObraActual = new Field("localAtual", nd.Termo_LicencaObraLocalizacaoObraActual, Field.Store.NO, Field.Index.ANALYZED);
            Field _NumPolicia_LicencaObraLocalizacaoObraAntiga = new Field("numPoliciaAntigo", nd.NumPolicia_LicencaObraLocalizacaoObraAntiga, Field.Store.NO, Field.Index.ANALYZED);
            Field _NomeLocal_LicencaObraLocalizacaoObraAntiga = new Field("localAntigo", nd.NomeLocal_LicencaObraLocalizacaoObraAntiga, Field.Store.NO, Field.Index.ANALYZED);
            Field _Nome_LicencaObraRequerentes = new Field("requerente", nd.Nome_LicencaObraRequerentes, Field.Store.NO, Field.Index.ANALYZED);
            Field _Termo_LicencaObraTecnicoObra = new Field("tecnico", nd.Termo_LicencaObraTecnicoObra, Field.Store.NO, Field.Index.ANALYZED);

            #endregion

            Document doc = new Document();
            doc.Add(fieldId);
            doc.Add(fieldCodigo);
            doc.Add(fieldDesignacaoTipoNivelRelacionado);
            doc.Add(fieldDesignacaoSorted);
            doc.Add(fieldNotaDoArquivista);
            doc.Add(fieldRegrasOuConvencoes);
            doc.Add(fieldHistoriaAdministrativa);
            doc.Add(fieldHistoriaCustodial);
            doc.Add(fieldFonteImediataDeAquisicao);
            doc.Add(fieldDesignacaoTipoTradicaoDocumental);
            doc.Add(fieldInicioTexto);
            doc.Add(fieldInicioAtribuida);
            doc.Add(fieldFimTexto);
            doc.Add(fieldFimAtribuida);
            doc.Add(fieldExistenciaDeOriginais);
            doc.Add(fieldExistenciaDeCopias);
            doc.Add(fieldUnidadesRelacionadas);
            doc.Add(fieldNotaDePublicacao);
            doc.Add(fieldConteudoInformacional);
            doc.Add(fieldIncorporacao);
            doc.Add(fieldDesignacaoTipoOrdenacao);
            doc.Add(fieldNotaGeral);
            doc.Add(fieldEstatutoLegal);
            doc.Add(fieldCondicaoDeAcesso);
            doc.Add(fieldCondicaoDeReproducao);
            doc.Add(fieldAuxiliarDePesquisa);
            doc.Add(fieldNotasExplicativas);
            doc.Add(fieldChavesColectividade);
            doc.Add(fieldRegrasConvencoes);
            doc.Add(fieldObservacoesControloAuto);
            doc.Add(fieldDescHistoricas);
            doc.Add(fieldDescZonasGeograficas);
            doc.Add(fieldDescEstatutosLegais);
            doc.Add(fieldDescOcupacoesActividades);
            doc.Add(fieldDescEnquadramentosLegais);
            doc.Add(fieldDescEstruturasInternas);
            doc.Add(fieldDescContextosGerais);
            doc.Add(fieldDesignacoesTipoNoticiaAuto);
            doc.Add(fieldTermosDeIndexacao);
            doc.Add(fieldTipologiaInformacional);
            doc.Add(fieldDesignacoesTipoEntidadeProdutora);
            doc.Add(fieldEntidadeProdutora);
            doc.Add(fieldAutor);
            doc.Add(fieldDesignacoesTipoFormaSuporteAcond);
            doc.Add(fieldDesignacoesTipoMaterialDeSuporte);
            doc.Add(fieldDesignacoesTipoTecnicasDeRegisto);
            doc.Add(fieldDesignacoesTipoEstadoDeConservacao);
            doc.Add(fieldLanguageNamesEnglish);
            doc.Add(fieldScriptNamesEnglish);
            doc.Add(fieldFrequencia);
            doc.Add(fieldPreservar);            
            doc.Add(fieldPublicar);
            doc.Add(fieldObservacoesSFRDAvaliacao);
            doc.Add(fieldIdAutoEliminacao);
            doc.Add(fieldDesignacaoTipoSubDensidade);
            doc.Add(fieldDesignacaoTipoDensidade);
            doc.Add(fieldDesignacaoTipoPertinencia);
            doc.Add(fieldPonderacao);
            doc.Add(fieldDesignacaoAutoEliminacao);
            doc.Add(fieldDesignacaoNivelDesignado);
            doc.Add(fieldDesignacaoNivelSorted);
            fieldCotas.ForEach(fieldCota => doc.Add(fieldCota));
            doc.Add(fieldAgrupador);
            doc.Add(fieldNumImagens);
            doc.Add(fieldNumODsNaoPub);
            doc.Add(fieldNumODsPub);
            doc.Add(fieldInicioProducao);
            doc.Add(fieldFimProducao);
            doc.Add(fieldIdUpper);
            doc.Add(fieldIdControloAutoridade);
            doc.Add(allFields);
            doc.Add(fieldExiste);
            doc.Add(_CodigoCodigo);

            #region Add fields: Licencas de obra

            doc.Add(_TipoObra);
            doc.Add(_PHSimNao);
            doc.Add(_PHTexto);
            doc.Add(_PropriedadeHorizontal);
            doc.Add(_CodigosAtestadoHabitabilidade);
            doc.Add(_Datas_LicencaObraDataLicencaConstrucao);
            doc.Add(_Termo_LicencaObraLocalizacaoObraActual);
            doc.Add(_NumPolicia_LicencaObraLocalizacaoObraActual);
            doc.Add(_NomeLocal_LicencaObraLocalizacaoObraAntiga);
            doc.Add(_NumPolicia_LicencaObraLocalizacaoObraAntiga);
            doc.Add(_Nome_LicencaObraRequerentes);
            doc.Add(_Termo_LicencaObraTecnicoObra);

            #endregion

            return doc;
        }

        public static Document NivelDocumentalInternetToLuceneDocument(NivelDocumentalInternet nd)
        {
            Field fieldId = new Field("id", nd.Id, Field.Store.YES, Field.Index.NOT_ANALYZED);
            Field fieldCodigo = new Field("codigo", nd.Codigo, Field.Store.NO, Field.Index.ANALYZED);
            Field fieldProdutor = new Field("produtor", nd.EntidadeProdutora, Field.Store.NO, Field.Index.ANALYZED);
            Field fieldDesignacaoTipoNivelRelacionado = new Field("designacaoTipoNivelRelacionado", nd.DesignacaoTipoNivelRelacionado, Field.Store.NO, Field.Index.ANALYZED);
            Field fieldDesignacaoSorted = new Field("designacaoSorted", nd.DesignacaoTipoNivelRelacionado.ToLower(), Field.Store.YES, Field.Index.NOT_ANALYZED);
            Field fieldConteudoInformacional = new Field("conteudo", nd.ConteudoInformacional, Field.Store.NO, Field.Index.ANALYZED);
            Field fieldTermosDeIndexacao = new Field("assunto", nd.TermosDeIndexacao, Field.Store.NO, Field.Index.ANALYZED);
            Field fieldTipologiaInformacional = new Field("tipologiaInformacional", nd.TipologiaInformacional, Field.Store.NO, Field.Index.ANALYZED);
            Field fieldAutor = new Field("autor", nd.Autor, Field.Store.NO, Field.Index.ANALYZED);
            Field fieldPublicar = new Field("publicar", nd.Publicar, Field.Store.NO, Field.Index.ANALYZED);
            Field fieldDesignacaoNivelDesignado = new Field("titulo", nd.DesignacaoNivelDesignado, Field.Store.NO, Field.Index.ANALYZED);
            Field fieldDesignacaoNivelDesignadoSorted = new Field("tituloSorted", nd.DesignacaoNivelDesignado.ToLower(), Field.Store.YES, Field.Index.NOT_ANALYZED);
            Field fieldNotaGeral = new Field("notaGeral", nd.NotaGeral, Field.Store.NO, Field.Index.ANALYZED);
            Field fieldNumImagens = new Field("objetos", nd.NumImagens, Field.Store.NO, Field.Index.NOT_ANALYZED);
            Field fieldInicioProducao = new Field("inicioProducao", ToLuceneDate(nd.DataInicioProd, true), Field.Store.YES, Field.Index.NOT_ANALYZED);
            Field fieldFimProducao = new Field("fimProducao", ToLuceneDate(nd.DataFimProd, false), Field.Store.YES, Field.Index.NOT_ANALYZED);
            Field fieldIdControloAutoridade = new Field("idControloAutoridade", nd.IdsControlosAutoridade, Field.Store.NO, Field.Index.ANALYZED);
            Field fieldIdUpper = new Field("idUpper", nd.IdUpper, Field.Store.NO, Field.Index.ANALYZED);
            Field fieldExiste = new Field("existe", "sim", Field.Store.NO, Field.Index.ANALYZED);
            
            #region Field defs.: Licencas de obra

            // LicencaObra
            Field _TipoObra = new Field("tipoObra", nd.TipoObra, Field.Store.NO, Field.Index.ANALYZED);
            Field _PHTexto = new Field("LicencaObra_PHTexto", nd.PHTexto, Field.Store.NO, Field.Index.ANALYZED);
            Field _CodigosAtestadoHabitabilidade = new Field("atestado", nd.CodigosAtestadoHabitabilidade, Field.Store.NO, Field.Index.ANALYZED);
            Field _NumPolicia_LicencaObraLocalizacaoObraActual = new Field("numPoliciaAtual", nd.NumPolicia_LicencaObraLocalizacaoObraActual, Field.Store.NO, Field.Index.ANALYZED);
            Field _Termo_LicencaObraLocalizacaoObraActual = new Field("localAtual", nd.Termo_LicencaObraLocalizacaoObraActual, Field.Store.NO, Field.Index.ANALYZED);
            Field _NumPolicia_LicencaObraLocalizacaoObraAntiga = new Field("numPoliciaAntigo", nd.NumPolicia_LicencaObraLocalizacaoObraAntiga, Field.Store.NO, Field.Index.ANALYZED);
            Field _NomeLocal_LicencaObraLocalizacaoObraAntiga = new Field("localAntigo", nd.NomeLocal_LicencaObraLocalizacaoObraAntiga, Field.Store.NO, Field.Index.ANALYZED);
            Field _Nome_LicencaObraRequerentes = new Field("requerente", nd.Nome_LicencaObraRequerentes, Field.Store.NO, Field.Index.ANALYZED);
            Field _Termo_LicencaObraTecnicoObra = new Field("tecnico", nd.Termo_LicencaObraTecnicoObra, Field.Store.NO, Field.Index.ANALYZED);

            #endregion

            Field fieldGI = new Field("all", 
                    nd.Id + " " + 
                    nd.DesignacaoNivelDesignado + " " + 
                    nd.Codigo + " " + 
                    nd.InicioAno + " " + 
                    nd.FimAno + " " + 
                    nd.TipologiaInformacional + " " + 
                    nd.TermosDeIndexacao + " " + 
                    nd.ConteudoInformacional + " " + 
                    nd.Autor + " " + 
                    nd.TipoObra + " " + 
                    nd.PHTexto + " " + 
                    nd.CodigosAtestadoHabitabilidade + " " + 
                    nd.Termo_LicencaObraLocalizacaoObraActual + " " + 
                    nd.NomeLocal_LicencaObraLocalizacaoObraAntiga + " " + 
                    nd.Nome_LicencaObraRequerentes + " " +
                    nd.Termo_LicencaObraTecnicoObra + " " + 
                    nd.EntidadeProdutora + " " +
                    nd.IdsControlosAutoridade + " " +
                    nd.IdUpper, Field.Store.NO, Field.Index.ANALYZED); 

            Document doc = new Document();
            doc.Add(fieldId);
            doc.Add(fieldCodigo);
            doc.Add(fieldProdutor);
            doc.Add(fieldDesignacaoTipoNivelRelacionado);
            doc.Add(fieldDesignacaoSorted);
            doc.Add(fieldConteudoInformacional);
            doc.Add(fieldTermosDeIndexacao);
            doc.Add(fieldTipologiaInformacional);
            doc.Add(fieldAutor);
            doc.Add(fieldNumImagens);
            doc.Add(fieldPublicar);
            doc.Add(fieldDesignacaoNivelDesignado);
            doc.Add(fieldDesignacaoNivelDesignadoSorted);
            doc.Add(fieldNotaGeral);
            doc.Add(fieldExiste);
            doc.Add(fieldInicioProducao);
            doc.Add(fieldFimProducao);
            doc.Add(fieldIdControloAutoridade);
            doc.Add(fieldIdUpper);
            doc.Add(fieldGI);

            #region Add fields: Licencas de obra

            doc.Add(_TipoObra);
            doc.Add(_PHTexto);
            doc.Add(_CodigosAtestadoHabitabilidade);
            doc.Add(_Termo_LicencaObraLocalizacaoObraActual);
            doc.Add(_NumPolicia_LicencaObraLocalizacaoObraActual);
            doc.Add(_NomeLocal_LicencaObraLocalizacaoObraAntiga);
            doc.Add(_NumPolicia_LicencaObraLocalizacaoObraAntiga);
            doc.Add(_Nome_LicencaObraRequerentes);
            doc.Add(_Termo_LicencaObraTecnicoObra);

            #endregion

            return doc;
        }

        public static Document NivelDocumentalComProdutoresToLuceneDocument(NivelDocumentalComProdutores ndcp)
        {
            Field fieldId = new Field("id", ndcp.IdDocumento.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED);
            Field fieldExiste = new Field("existe", "sim", Field.Store.NO, Field.Index.NOT_ANALYZED);
            Field fieldProdutor = new Field("produtor", ndcp.Produtor, Field.Store.NO, Field.Index.ANALYZED);
            Field fieldProdutorAutorizado = new Field("produtorAutorizado", ndcp.ProdutorAutorizado, Field.Store.NO, Field.Index.ANALYZED);

            Document doc = new Document();
            doc.Add(fieldId);
            doc.Add(fieldExiste);
            doc.Add(fieldProdutor);
            doc.Add(fieldProdutorAutorizado);

            return doc;
        }
        
        private static string ToLuceneDate(string s, bool isInicio)
        {
            if (s.Length == 8)
            {
                s = s.Replace('?', isInicio ? '0' : '9');
            }
            else
            {
                s = isInicio ? "00000000" : "99999999";
            }

            return s;
        }

        public static Document UFToLuceneDocument(UnidadeFisica uf) {            
            Field id = new Field("id", uf.Id, Field.Store.YES, Field.Index.NOT_ANALYZED);
            Field existe = new Field("existe", "sim", Field.Store.NO, Field.Index.NOT_ANALYZED);
            Field numero = new Field("numero", uf.Numero, Field.Store.NO, Field.Index.ANALYZED);
            Field designacao = new Field("designacao", uf.Designacao, Field.Store.NO, Field.Index.ANALYZED);
            Field cota = new Field("cota", uf.Cota.ToLower(), Field.Store.NO, Field.Index.NOT_ANALYZED);
            Field conteudoInformacional = new Field("conteudoInformacional", uf.ConteudoInformacional, Field.Store.NO, Field.Index.ANALYZED);
            Field tipoUnidadeFisica = new Field("tipoUnidadeFisica", uf.TipoUnidadeFisica, Field.Store.NO, Field.Index.ANALYZED);            
            Field guiaIncorporacao = new Field("guiaIncorporacao", uf.GuiaIncorporacao, Field.Store.NO, Field.Index.ANALYZED);
            Field codigoBarras = new Field("codigoBarras", uf.CodigoBarras, Field.Store.NO, Field.Index.ANALYZED);
            Field eliminado = new Field("eliminado", uf.Eliminado, Field.Store.NO, Field.Index.NOT_ANALYZED);
            Field fieldInicioProducao = new Field("dataProducaoInicio", ToLuceneDate(uf.DataInicioProd, true), Field.Store.YES, Field.Index.NOT_ANALYZED);
            Field fieldFimProducao = new Field("dataProducaoFim", ToLuceneDate(uf.DataFimProd,false), Field.Store.YES, Field.Index.NOT_ANALYZED);

            Document doc = new Document();
            doc.Add(id);
            doc.Add(existe);
            doc.Add(numero);
            doc.Add(designacao);
            doc.Add(cota);
            doc.Add(conteudoInformacional);
            doc.Add(tipoUnidadeFisica);
            doc.Add(guiaIncorporacao);
            doc.Add(codigoBarras);
            doc.Add(eliminado);
            doc.Add(fieldInicioProducao);
            doc.Add(fieldFimProducao);

            return doc;
        }

        public static Document ProdutorToLuceneDocument(Produtor produtor) {
            Field id = new Field("id", produtor.Id.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED);
            Field formaAutorizada = new Field("formaAutorizada", produtor.FormaAutorizada, Field.Store.YES, Field.Index.ANALYZED);
            Field tipoNivel = new Field("tipoNivel", produtor.TipoNivel, Field.Store.NO, Field.Index.ANALYZED);
            Field designacao = new Field("designacao", produtor.Designacao, Field.Store.NO, Field.Index.ANALYZED);
            Field validado = new Field("validado", produtor.Validado, Field.Store.NO, Field.Index.ANALYZED);

            Document doc = new Document();
            doc.Add(id);
            doc.Add(formaAutorizada);
            doc.Add(tipoNivel);
            doc.Add(designacao);
            doc.Add(validado);

            return doc;
        }

        public static Document AssuntosToLuceneDocument(Assunto assunto)
        {
            Field id = new Field("id", assunto.Id, Field.Store.YES, Field.Index.NOT_ANALYZED);
            Field formaAutorizada = new Field("formaAutorizada", assunto.FormaAutorizada, Field.Store.YES, Field.Index.ANALYZED);
            Field tipoNoticia = new Field("tipoNoticia", assunto.TipoNoticia, Field.Store.NO, Field.Index.ANALYZED);
            Field designacao = new Field("designacao", assunto.Designacao, Field.Store.NO, Field.Index.ANALYZED);
            Field validado = new Field("validado", assunto.Validado, Field.Store.NO, Field.Index.ANALYZED);

            Document doc = new Document();
            doc.Add(id);
            doc.Add(formaAutorizada);
            doc.Add(tipoNoticia);
            doc.Add(designacao);
            doc.Add(validado);

            return doc;
        }

        public static Document TipologiasToLuceneDocument(Tipologia tipologia)
        {
            Field id = new Field("id", tipologia.Id, Field.Store.YES, Field.Index.NOT_ANALYZED);
            Field formaAutorizada = new Field("formaAutorizada", tipologia.FormaAutorizada, Field.Store.YES, Field.Index.ANALYZED);
            Field designacao = new Field("designacao", tipologia.Designacao, Field.Store.NO, Field.Index.ANALYZED);
            Field validado = new Field("validado", tipologia.Validado, Field.Store.NO, Field.Index.ANALYZED);

            Document doc = new Document();
            doc.Add(id);
            doc.Add(formaAutorizada);
            doc.Add(designacao);
            doc.Add(validado);

            return doc;
        }

        #region Permissões
        
        public static List<string> FilterByReadPermission(List<string> ids, long idTrustee)
        {
            ISession session = null;
            var res = new List<string>();
            try
            {
                session = GISAUtils.SessionFactory.OpenSession();

                using (SqlCommand command = new SqlCommand(string.Empty, (SqlConnection)session.Connection))
                {
                    GISAUtils.ImportIDs(ids.ToArray(), (SqlConnection)session.Connection);
                    command.CommandText = "CREATE TABLE #effective (IDNivel BIGINT PRIMARY KEY, IDUpper BIGINT, Ler TINYINT)";
                    command.ExecuteNonQuery();

                    command.CommandText = "INSERT INTO #effective SELECT ID, ID, null FROM #temp ORDER BY #temp.seq_nr";
                    command.ExecuteNonQuery();

                    command.CommandText = "sp_getEffectiveReadPermissions";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@IDTrustee", SqlDbType.BigInt);
                    command.Parameters[0].Value = idTrustee;
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();

                    command.CommandType = CommandType.Text;
                    command.CommandText = "SELECT IDNivel FROM #effective INNER JOIN #temp ON #temp.ID = #effective.IDNivel WHERE Ler = 1 ORDER BY #temp.seq_nr";
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                        res.Add(reader.GetInt64(0).ToString());
                    reader.Close();
                }
            }
            catch (Exception) { throw; }
            finally
            {
                if (session != null) session.Close();
            }
            return res;
        }

        #endregion
    }
}
