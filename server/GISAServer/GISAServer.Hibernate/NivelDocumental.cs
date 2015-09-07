using System.Collections.Generic;
using System.Linq;
using System.Text;
using GISAServer.Hibernate.Exceptions;
using GISAServer.Hibernate.Objects;
using GISAServer.Hibernate.Utils;
using log4net;
using NHibernate;
using NHibernate.Criterion;
using System;

namespace GISAServer.Hibernate
{
	/// <summary>
    /// NivelDocumental has all relevant
    /// information of a Nivel Documental.    
	/// </summary>
	public class NivelDocumental
    {
        // Logging initializations
        private static readonly ILog log = LogManager.GetLogger(typeof(NivelDocumental));        

        # region Properties and initializations
 
        // Nivel
        private string id = "";
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        private string codigo = "";
        public string Codigo 
        { 
            get { return codigo; } 
            set { codigo = value; } 
        }        
        
        // TipoNivelRelacionado
        private string designacaoTipoNivelRelacionado = "";
        public string DesignacaoTipoNivelRelacionado 
        { 
            get { return designacaoTipoNivelRelacionado; } 
            set { designacaoTipoNivelRelacionado = value; } 
        }
        
        // FRDBase
        private string notaDoArquivista = "";
        public string NotaDoArquivista
        {
            get { return notaDoArquivista; }
            set { notaDoArquivista = value; }
        }

        private string regrasOuConvencoes = "";
        public string RegrasOuConvencoes
        {
            get { return regrasOuConvencoes; }
            set { regrasOuConvencoes = value; }
        }

        // SFRDContexto
        private string historiaAdministrativa = "";
        public string HistoriaAdministrativa
        {
            get { return historiaAdministrativa; }
            set { historiaAdministrativa = value; }
        }

        private string historiaCustodial = "";
        public string HistoriaCustodial
        {
            get { return historiaCustodial; }
            set { historiaCustodial = value; }
        }

        private string fonteImediataDeAquisicao = "";
        public string FonteImediataDeAquisicao
        {
            get { return fonteImediataDeAquisicao; }
            set { fonteImediataDeAquisicao = value; }
        }

        // TipoTradicaoDocumental
        private string designacaoTipoTradicaoDocumental = ""; // lista
        public string DesignacaoTipoTradicaoDocumental
        {
            get { return designacaoTipoTradicaoDocumental; }
            set { designacaoTipoTradicaoDocumental = value; }
        }

        // SFRDDatasProducao
        private string dataInicioProd = "00000000";
        public string DataInicioProd
        {
            get { return dataInicioProd; }
            set { dataInicioProd = value; }
        }

        private string dataFimProd = "99999999";
        public string DataFimProd
        {
            get { return dataFimProd; }
            set { dataFimProd = value; }
        }

        private string inicioTexto = "";
        public string InicioTexto
        {
            get { return inicioTexto; }
            set { inicioTexto = value; }
        }

        private string inicioAno = "";
        public string InicioAno
        {
            get { return inicioAno; }
            set { inicioAno = value; }
        }

        private string inicioMes = "";
        public string InicioMes
        {
            get { return inicioMes; }
            set { inicioMes = value; }
        }

        private string inicioDia = "";
        public string InicioDia
        {
            get { return inicioDia; }
            set { inicioDia = value; }
        }

        private string inicioAtribuida = "";
        public string InicioAtribuida
        {
            get { return inicioAtribuida; }
            set { inicioAtribuida = value; }
        }

        private string fimTexto = "";
        public string FimTexto
        {
            get { return fimTexto; }
            set { fimTexto = value; }
        }

        private string fimAno = "";
        public string FimAno
        {
            get { return fimAno; }
            set { fimAno = value; }
        }

        private string fimMes = "";
        public string FimMes
        {
            get { return fimMes; }
            set { fimMes = value; }
        }

        private string fimDia = "";
        public string FimDia
        {
            get { return fimDia; }
            set { fimDia = value; }
        }

        private string fimAtribuida = "";
        public string FimAtribuida
        {
            get { return fimAtribuida; }
            set { fimAtribuida = value; }
        }

        // SFRDDocumentacaoAssociada
        private string existenciaDeOriginais = "";
        public string ExistenciaDeOriginais
        {
            get { return existenciaDeOriginais; }
            set { existenciaDeOriginais = value; }
        }

        private string existenciaDeCopias = "";
        public string ExistenciaDeCopias
        {
            get { return existenciaDeCopias; }
            set { existenciaDeCopias = value; }
        }

        private string unidadesRelacionadas = "";
        public string UnidadesRelacionadas
        {
            get { return unidadesRelacionadas; }
            set { unidadesRelacionadas = value; }
        }

        private string notaDePublicacao = "";
        public string NotaDePublicacao
        {
            get { return notaDePublicacao; }
            set { notaDePublicacao = value; }
        }

        // SFRDConteudoEEstrutura
        private string conteudoInformacional = "";
        public string ConteudoInformacional
        {
            get { return conteudoInformacional; }
            set { conteudoInformacional = value; }
        }

        private string incorporacao = "";
        public string Incorporacao
        {
            get { return incorporacao; }
            set { incorporacao = value; }
        }
        
        // TipoOrdenacao
        private string designacaoTipoOrdenacao = ""; // lista
        public string DesignacaoTipoOrdenacao
        {
            get { return designacaoTipoOrdenacao; }
            set { designacaoTipoOrdenacao = value; }
        }
        
        // SFRDNotaGeral
        private string notaGeral = "";
        public string NotaGeral
        {
            get { return notaGeral; }
            set { notaGeral = value; }
        }
        
        //SFRDCondicaoDeAcesso
        private string estatutoLegal = "";
        public string EstatutoLegal
        {
            get { return estatutoLegal; }
            set { estatutoLegal = value; }
        }

        private string condicaoDeAcesso = "";
        public string CondicaoDeAcesso
        {
            get { return condicaoDeAcesso; }
            set { condicaoDeAcesso = value; }
        }

        private string condicaoDeReproducao = "";
        public string CondicaoDeReproducao
        {
            get { return condicaoDeReproducao; }
            set { condicaoDeReproducao = value; }
        }

        private string auxiliarDePesquisa = "";
        public string AuxiliarDePesquisa
        {
            get { return auxiliarDePesquisa; }
            set { auxiliarDePesquisa = value; }
        }
        
        // ControloAuto
        private string notasExplicativas = ""; // lista
        public string NotasExplicativas
        {
            get { return notasExplicativas; }
            set { notasExplicativas = value; }
        }

        private string chavesColectividade = ""; // lista
        public string ChavesColectividade
        {
            get { return chavesColectividade; }
            set { chavesColectividade = value; }
        }

        private string regrasConvencoes = ""; // lista
        public string RegrasConvencoes
        {
            get { return regrasConvencoes; }
            set { regrasConvencoes = value; }
        }

        private string observacoesControloAuto = ""; // lista
        public string ObservacoesControloAuto
        {
            get { return observacoesControloAuto; }
            set { observacoesControloAuto = value; }
        }

        private string descHistoricas = ""; // lista
        public string DescHistoricas
        {
            get { return descHistoricas; }
            set { descHistoricas = value; }
        }

        private string descZonasGeograficas = ""; // lista
        public string DescZonasGeograficas
        {
            get { return descZonasGeograficas; }
            set { descZonasGeograficas = value; }
        }

        private string descEstatutosLegais = ""; // lista
        public string DescEstatutosLegais
        {
            get { return descEstatutosLegais; }
            set { descEstatutosLegais = value; }
        }

        private string descOcupacoesActividades = ""; // lista
        public string DescOcupacoesActividades
        {
            get { return descOcupacoesActividades; }
            set { descOcupacoesActividades = value; }
        }

        private string descEnquadramentosLegais = ""; // lista
        public string DescEnquadramentosLegais
        {
            get { return descEnquadramentosLegais; }
            set { descEnquadramentosLegais = value; }
        }

        private string descEstruturasInternas = ""; // lista
        public string DescEstruturasInternas
        {
            get { return descEstruturasInternas; }
            set { descEstruturasInternas = value; }
        }

        private string descContextosGerais = ""; // lista
        public string DescContextosGerais
        {
            get { return descContextosGerais; }
            set { descContextosGerais = value; }
        }

        private string descOutraInformacaoRelevante = "";
        public string DescOutraInformacaoRelevante
        {
            get { return descOutraInformacaoRelevante; }
            set { descOutraInformacaoRelevante = value; }
        }

        // TipoNoticiaAuto
        private string designacoesTipoNoticiaAuto = ""; // lista       
        public string DesignacoesTipoNoticiaAuto
        {
            get { return designacoesTipoNoticiaAuto; }
            set { designacoesTipoNoticiaAuto = value; }
        }
        
        // Dicionario
        private string termos = ""; // lista
        public string Termos
        {
            get { return termos; }
            set { termos = value; }
        }

        private string termosDeIndexacao = ""; // lista
        public string TermosDeIndexacao
        {
            get { return termosDeIndexacao; }
            set { termosDeIndexacao = value; }
        }

        private string tipologiaInformacional = ""; // lista
        public string TipologiaInformacional
        {
            get { return tipologiaInformacional; }
            set { tipologiaInformacional = value; }
        }

        // TipoEntidadeProdutora
        private string designacoesTipoEntidadeProdutora = ""; // lista
        public string DesignacoesTipoEntidadeProdutora
        {
            get { return designacoesTipoEntidadeProdutora; }
            set { designacoesTipoEntidadeProdutora = value; }
        }

        private string autor = "";
        public string Autor
        {
            get { return autor; }
            set { autor = value; }
        }

        private string entidadeProdutora = "";
        public string EntidadeProdutora
        {
            get { return entidadeProdutora; }
            set { entidadeProdutora = value; }
        }

        private string idsControlosAutoridade = ""; // lista
        public string IdsControlosAutoridade { get { return idsControlosAutoridade; } set { idsControlosAutoridade = value; } }

        // TipoFormaSuporteAcond
        private string designacoesTipoFormaSuporteAcond = ""; // lista
        public string DesignacoesTipoFormaSuporteAcond
        {
            get { return designacoesTipoFormaSuporteAcond; }
            set { designacoesTipoFormaSuporteAcond = value; }
        }
        
        // TipoMaterialDeSuporte
        private string designacoesTipoMaterialDeSuporte = ""; // lista
        public string DesignacoesTipoMaterialDeSuporte
        {
            get { return designacoesTipoMaterialDeSuporte; }
            set { designacoesTipoMaterialDeSuporte = value; }
        }

        // TipoTecnicasDeRegisto
        private string designacoesTipoTecnicasDeRegisto = ""; // lista
        public string DesignacoesTipoTecnicasDeRegisto
        {
            get { return designacoesTipoTecnicasDeRegisto; }
            set { designacoesTipoTecnicasDeRegisto = value; }
        }

        // TipoEstadoDeConservacao;
        private string designacoesTipoEstadoDeConservacao = ""; // lista
        public string DesignacoesTipoEstadoDeConservacao
        {
            get { return designacoesTipoEstadoDeConservacao; }
            set { designacoesTipoEstadoDeConservacao = value; }
        }

        // Iso639
        private string languageNamesEnglish = ""; // lista
        public string LanguageNamesEnglish
        {
            get { return languageNamesEnglish; }
            set { languageNamesEnglish = value; }
        }

        // Iso15924
        private string scriptNamesEnglish = ""; // lista
        public string ScriptNamesEnglish
        {
            get { return scriptNamesEnglish; }
            set { scriptNamesEnglish = value; }
        }

        // SFRDAvaliacao    
        private string frequencia = "";
        public string Frequencia
        {
            get { return frequencia; }
            set { frequencia = value; }
        }

        private string preservar = "";
        public string Preservar
        {
            get { return preservar; }
            set { preservar = value; }
        }        

        private string publicar = "";
        public string Publicar
        {
            get { return publicar; }
            set { publicar = value; }
        }

        private string observacoesSFRDAvaliacao = "";
        public string ObservacoesSFRDAvaliacao
        {
            get { return observacoesSFRDAvaliacao; }
            set { observacoesSFRDAvaliacao = value; }
        }

        private string idAutoEliminacao = "";
        public string IdAutoEliminacao
        {
            get { return idAutoEliminacao; }
            set { idAutoEliminacao = value; }
        }
        
        // TipoSubDensidade
        private string designacaoTipoSubDensidade = "";
        public string DesignacaoTipoSubDensidade
        {
            get { return designacaoTipoSubDensidade; }
            set { designacaoTipoSubDensidade = value; }
        }
        
        // TipoDensidade
        private string designacaoTipoDensidade = "";
        public string DesignacaoTipoDensidade
        {
            get { return designacaoTipoDensidade; }
            set { designacaoTipoDensidade = value; }
        }
        
        // TipoPertinencia
        private string designacaoTipoPertinencia = "";
        public string DesignacaoTipoPertinencia
        {
            get { return designacaoTipoPertinencia; }
            set { designacaoTipoPertinencia = value; }
        }

        private string ponderacao = "";
        public string Ponderacao
        {
            get { return ponderacao; }
            set { ponderacao = value; }
        }
        
        // AutoEliminacao
        private string designacaoAutoEliminacao = "";
        public string DesignacaoAutoEliminacao
        {
            get { return designacaoAutoEliminacao; }
            set { designacaoAutoEliminacao = value; }
        }
        
        // NivelDesignado
        private string designacaoNivelDesignado = "";
        public string DesignacaoNivelDesignado
        {
            get { return designacaoNivelDesignado; }
            set { designacaoNivelDesignado = value; }
        }
    	    
        // SFRDUFCota
        private List<string> cota = new List<string>();
        public List<string> Cota
        {
            get { return cota; }
            set { cota = value; }
        }

        // SFRDAgrupador
        private string agrupador = "";
        public string Agrupador
        {
            get { return agrupador; }
            set { agrupador = value; }
        }

        // SFRDImagem
        private string numImagens = "0";
        public string NumImagens
        {
            get { return numImagens; }
            set { numImagens = value; }
        }

        private string numODsNaoPublicados = "0";
        public string NumODsNaoPublicados
        {
            get { return numODsNaoPublicados; }
            set { numODsNaoPublicados = value; }
        }

        private string numODsPublicados = "0";
        public string NumODsPublicados
        {
            get { return numODsPublicados; }
            set { numODsPublicados = value; }
        }

        private string idUpper = ""; // lista (só é preenchido para subséries e (sud)documentos)
        public string IdUpper { get { return idUpper; } set { idUpper = value; } }

        #endregion


        #region Licencas de Obras: Properties and initializations

        // LicencaObra:
        private string tipoObra = string.Empty;
        public string TipoObra {
            get { return tipoObra; }
            set { tipoObra = value; }
        }
        private string pHTexto = string.Empty;
        public string PHTexto {
            get { return pHTexto;  }
            set { pHTexto = value; }
        }
        private string pHSimNao = string.Empty;
        public string PHSimNao
        {
            get { return pHSimNao; }
            set { pHSimNao = value; }
        }
        private string pPropriedadeHorizontal = string.Empty;
        public string PHCompleto
        {
            get { return pPropriedadeHorizontal; }
            set { pPropriedadeHorizontal = value; }
        }

        // LicencaObraAtestadoHabitabilidade:
        private string codigosAtestadoHabitabilidade = string.Empty;
        public string CodigosAtestadoHabitabilidade {
            get { return codigosAtestadoHabitabilidade; }
            set { codigosAtestadoHabitabilidade = value; }
        }

        // LicencaObraDataLicencaConstrucao:
        private string datas_LicencaObraDataLicencaConstrucao = string.Empty;
        public string Datas_LicencaObraDataLicencaConstrucao {
            get { return datas_LicencaObraDataLicencaConstrucao; }
            set { datas_LicencaObraDataLicencaConstrucao = value; }
        }

        // LicencaObraLocalizacaoObraActual:
        private string numPolicia_LicencaObraLocalizacaoObraActual = string.Empty;
        public string NumPolicia_LicencaObraLocalizacaoObraActual {
            get { return numPolicia_LicencaObraLocalizacaoObraActual; }
            set { numPolicia_LicencaObraLocalizacaoObraActual = value; }
        }

        private string termo_LicencaObraLocalizacaoObraActual = string.Empty;
        public string Termo_LicencaObraLocalizacaoObraActual {
            get { return termo_LicencaObraLocalizacaoObraActual; }
            set { termo_LicencaObraLocalizacaoObraActual = value; }
        }

        // LicencaObraLocalizacaoObraAntiga:
        private string numPolicia_LicencaObraLocalizacaoObraAntiga = string.Empty;
        public string NumPolicia_LicencaObraLocalizacaoObraAntiga {
            get { return numPolicia_LicencaObraLocalizacaoObraAntiga; }
            set { numPolicia_LicencaObraLocalizacaoObraAntiga = value; }
        }

        private string nomeLocal_LicencaObraLocalizacaoObraAntiga = string.Empty;
        public string NomeLocal_LicencaObraLocalizacaoObraAntiga {
            get { return nomeLocal_LicencaObraLocalizacaoObraAntiga; }
            set { nomeLocal_LicencaObraLocalizacaoObraAntiga = value; }
        }

        // LicencaObraRequerentes:
        private string nome_LicencaObraRequerentes = string.Empty;
        public string Nome_LicencaObraRequerentes {
            get { return nome_LicencaObraRequerentes; }
            set { nome_LicencaObraRequerentes = value; }
        }

        // LicencaObraTecnicoObra:
        private string termo_LicencaObraTecnicoObra = string.Empty;
        public string Termo_LicencaObraTecnicoObra {
            get { return termo_LicencaObraTecnicoObra; }
            set { termo_LicencaObraTecnicoObra = value; }
        }

        #endregion

        private string codigo_Codigo = string.Empty;
        public string Codigo_Codigo {
            get { return codigo_Codigo; }
            set { codigo_Codigo = value; }
        }

        #region Constructors

        public NivelDocumental()
        {

        }

        public NivelDocumental(long id)
        {
            ISession session = null;
            try
            {
                session = GISAUtils.SessionFactory.OpenSession();

                NivelEntity nivel = initNivel(session, id);
                initNivelDesignado(session, id);

                IList<RelacaoHierarquicaEntity> rhList = initRelacaoHierarquica(session, id);
                if (rhList != null)
                {
                    foreach (RelacaoHierarquicaEntity rh in rhList)
                        initTipoEntidadeProdutora(session, rh);
                    this.idUpper = string.Join(" ", rhList.Select(rh => rh.Upper.Id.ToString()).ToArray());
                }

                FRDBaseEntity frdb = initFRDBase(session, id);
                if (frdb != null)
                {
                    long frdbId = frdb.Id;
                    initSFRDContexto(session, frdbId);
                    initTipoTradicaoDocumental(session, frdbId);
                    initSFRDDatasProducao(session, frdbId);
                    initSFRDDocumentacaoAssociada(session, frdbId);
                    initSFRDConteudoEEstrutura(session, frdbId);
                    initTipoOrdenacao(session, frdbId);
                    initSFRDNotaGeral(session, frdbId);
                    initSFRDCondicaoDeAcesso(session, frdbId);
                    initControloAut(session, frdbId);
                    initTipos(session, frdbId, nivel, frdb);
                    initAutor(session, frdbId);
                    initSFRDUFCota(session, frdbId);
                    initSFRDAgrupador(session, frdbId);
                    initSFRDImagem(session, frdbId);

                    initLicencaObra(session, frdbId);
                    initLicencaObraAtestadoHabitabilidade(session, frdbId);
                    initLicencaObraDataLicencaConstrucao(session, frdbId);
                    initLicencaObraLocalizacaoObraActual(session, frdbId);
                    initLicencaObraLocalizacaoObraAntiga(session, frdbId);
                    initLicencaObraRequerentes(session, frdbId);
                    initLicencaObraTecnicoObra(session, frdbId);
                    initCodigo(session, frdbId);

                    // controlos de autoridade usados no campo estruturado também passam pesquisáveis a partir do campo de indexação
                    this.TermosDeIndexacao += " " + this.Termo_LicencaObraTecnicoObra;
                    this.TermosDeIndexacao += " " + this.Termo_LicencaObraLocalizacaoObraActual;
                }
            }
            catch (Exception) { throw; }
            finally
            {
                if (session != null)
                    session.Close();
            }
        }

        #endregion

        #region Initializations from database

        private NivelEntity initNivel(ISession session, long id)
        {            
            NivelEntity ne = session.Get<NivelEntity>(id);
            if (ne != null && ne.TipoNivel.Id == 3 && !ne.IsDeleted)
            {
                this.id = id.ToString();
                if (ne.Codigo != null)
                {
                    this.codigo = ne.Codigo;
                }                
            }
            else
            {
                throw new InvalidIdException("Invalid id: " + id);
            }
            return ne;
        }

        private FRDBaseEntity initFRDBase(ISession session, long nivelId)
        {
            
            FRDBaseEntity frdb = null;
            IQuery query = session.CreateQuery("from FRDBaseEntity f where f.Nivel.Id = " + nivelId + " AND f.IsDeleted = 0 ");
            query.SetTimeout(1000);
            foreach (FRDBaseEntity frdbase in query.List<FRDBaseEntity>())
            {
                frdb = frdbase;                
            }
            
            if (frdb != null)
            {
                // NotaDoAqruivista
                if (frdb.NotaDoArquivista != null)
                {
                    this.notaDoArquivista = frdb.NotaDoArquivista;
                }                

                // RegrasOuConvencoes
                if (frdb.RegrasOuConvencoes != null)
                {
                    this.regrasOuConvencoes = frdb.RegrasOuConvencoes;
                }                
            }
            else
            {
                log.Error("Id without frdbase tree: " + id);
            }

            return frdb;
        }

        private IList<RelacaoHierarquicaEntity> initRelacaoHierarquica(ISession session, long nivelId)        
        {
            IQuery rhq = session.CreateQuery("select c from RelacaoHierarquicaEntity as c where c.ID.Id = " + nivelId + " AND c.IsDeleted = 0 ");
            rhq.SetTimeout(1000);

            IList<RelacaoHierarquicaEntity> ret = null;

            RelacaoHierarquicaEntity rh = null;            
            if (rhq.List<RelacaoHierarquicaEntity>().Count > 0)
            {
                ret = rhq.List<RelacaoHierarquicaEntity>();
                rh = ret[0];
                if (rh != null)
                {
                    session.Evict(rh);
                    TipoNivelRelacionadoEntity tnr = session.Get<TipoNivelRelacionadoEntity>(rh.TipoNivelRelacionado.Id);
                    if (tnr != null)
                    {
                        if (tnr.Designacao != null)
                        {
                            this.designacaoTipoNivelRelacionado = tnr.Designacao;
                        }                        
                    }                   
                }
            }            

            return ret;
        }

        private void initSFRDContexto(ISession session, long frdbId)
        {            
            SFRDContextoEntity sfrdc = session.Get<SFRDContextoEntity>(frdbId);
            if (sfrdc != null && !sfrdc.IsDeleted)
            {
                // HistoriaAdministrativa
                if (sfrdc.HistoriaAdministrativa != null)
                {
                    this.historiaAdministrativa = sfrdc.HistoriaAdministrativa;
                }
               
                // HistoriaCustodial
                if (sfrdc.HistoriaCustodial != null)
                {
                    this.historiaCustodial = sfrdc.HistoriaCustodial;
                }

                // FonteImediataDeAquisicao
                if (sfrdc.FonteImediataDeAquisicao != null)
                {
                    this.fonteImediataDeAquisicao = sfrdc.FonteImediataDeAquisicao;
                }                
            }
        }

        private void initTipoTradicaoDocumental(ISession session, long frdbId)
        {            
            TipoTradicaoDocumentalEntity ttd = session.Get<TipoTradicaoDocumentalEntity>(frdbId);
            if (ttd != null && !ttd.IsDeleted)
            {
                if (ttd.Designacao != null)
                {
                    this.designacaoTipoTradicaoDocumental = ttd.Designacao;
                }                
            }
        }

        private void initSFRDDatasProducao(ISession session, long frdbId)
        {            
            // Datas de producao
            SFRDDatasProducaoEntity sfrddp = session.Get<SFRDDatasProducaoEntity>(frdbId);
            
            this.dataInicioProd = GISAUtils.DataInicioProdFormatada(sfrddp);
            this.dataFimProd = GISAUtils.DataFimProdFormatada(sfrddp);
            this.inicioAno = sfrddp.InicioAno;
            this.inicioMes = sfrddp.InicioMes;
            this.inicioDia = sfrddp.InicioDia;
            this.fimAno = sfrddp.FimAno;
            this.fimMes = sfrddp.FimMes;
            this.fimDia = sfrddp.FimDia;

            if (sfrddp != null && !sfrddp.IsDeleted)
            {
                this.inicioAtribuida = sfrddp.InicioAtribuida.ToString();
                this.fimAtribuida = sfrddp.FimAtribuida.ToString();  
            }            
        }

        private void initSFRDDocumentacaoAssociada(ISession session, long frdbId)
        {            
            SFRDDocumentacaoAssociadaEntity sfrdda = session.Get<SFRDDocumentacaoAssociadaEntity>(frdbId);
            if (sfrdda != null && !sfrdda.IsDeleted)
            {
                if (sfrdda.ExistenciaDeOriginais != null)
                {
                    this.ExistenciaDeOriginais = sfrdda.ExistenciaDeOriginais;
                }
                if (sfrdda.ExistenciaDeCopias != null)
                {
                    this.ExistenciaDeCopias = sfrdda.ExistenciaDeCopias;
                }
                if (sfrdda.UnidadesRelacionadas != null)
                {
                    this.UnidadesRelacionadas = sfrdda.UnidadesRelacionadas;
                }
                if (sfrdda.NotaDePublicacao != null)
                {
                    this.NotaDePublicacao = sfrdda.NotaDePublicacao;
                }
            }
        }

        private void initSFRDConteudoEEstrutura(ISession session, long frdbId)
        {            
            SFRDConteudoEEstruturaEntity sfrdcee = session.Get<SFRDConteudoEEstruturaEntity>(frdbId);
            if (sfrdcee != null && !sfrdcee.IsDeleted)
            {
                if (sfrdcee.ConteudoInformacional != null)
                {
                    this.ConteudoInformacional = sfrdcee.ConteudoInformacional;
                }
                if (sfrdcee.Incorporacao != null)
                {
                    this.Incorporacao = sfrdcee.Incorporacao;
                }
            }
        }

        private void initSFRDAgrupador(ISession session, long frdbId)
        {
            SFRDAgrupadorEntity agr = session.Get<SFRDAgrupadorEntity>(frdbId);
            if (agr != null && !agr.IsDeleted)
            {
                if (agr.Agrupador != null)
                {
                    this.Agrupador = agr.Agrupador;
                }
            }
        }

        private void initTipoOrdenacao(ISession session, long frdbId)
        {
            IQuery queryTO = session.CreateQuery("select c from SFRDOrdenacaoEntity as c where c.FRDBase.Id = " + frdbId + " AND c.IsDeleted = 0 ");
            queryTO.SetTimeout(1000);
            IList<SFRDOrdenacaoEntity> list = queryTO.List<SFRDOrdenacaoEntity>();
            foreach (SFRDOrdenacaoEntity sfrdo in list)
            {
                TipoOrdenacaoEntity to = sfrdo.TipoOrdenacao;
                if (to != null && to.Designacao != null)
                {
                    this.DesignacaoTipoOrdenacao = this.DesignacaoTipoOrdenacao + " " + to.Designacao;
                }
            }
        }

        private void initSFRDNotaGeral(ISession session, long frdbId)
        {            
            SFRDNotaGeralEntity sfrdng = session.Get<SFRDNotaGeralEntity>(frdbId);
            if (sfrdng != null && !sfrdng.IsDeleted && sfrdng.NotaGeral != null)
            {
                this.NotaGeral = sfrdng.NotaGeral;
            }
        }

        private void initSFRDCondicaoDeAcesso(ISession session, long frdbId)
        {            
            SFRDCondicaoDeAcessoEntity sfrdcda = session.Get<SFRDCondicaoDeAcessoEntity>(frdbId);
            if (sfrdcda != null && !sfrdcda.IsDeleted)
            {
                if (sfrdcda.EstatutoLegal != null)
                {
                    this.EstatutoLegal = sfrdcda.EstatutoLegal;
                }
                if (sfrdcda.CondicaoDeAcesso != null)
                {
                    this.CondicaoDeAcesso = sfrdcda.CondicaoDeAcesso;
                }
                if (sfrdcda.CondicaoDeReproducao != null)
                {
                    this.CondicaoDeReproducao = sfrdcda.CondicaoDeReproducao;
                }
                if (sfrdcda.AuxiliarDePesquisa != null)
                {
                    this.AuxiliarDePesquisa = sfrdcda.AuxiliarDePesquisa;
                }
            }
        }

        private void initAutor(ISession session, long frdbId)
        {
            IQuery queryCA = session.CreateQuery("select i from SFRDAutorEntity as i where i.FRDBase.Id = " + frdbId + " AND i.IsDeleted = 0 ");
            queryCA.SetTimeout(1000);

            foreach (SFRDAutorEntity autor in queryCA.Enumerable<SFRDAutorEntity>())
            {
                ControloAutEntity ca = autor.ControloAut;
                if (ca != null && !ca.IsDeleted)
                {
                    this.idsControlosAutoridade += ca.Id.ToString() + " ";
                    // Dicionario
                    IQuery queryCAD = session.CreateQuery("select c from ControloAutDicionarioEntity as c where c.ControloAut.Id = " + ca.Id + " AND c.IsDeleted = 0 ");
                    queryCAD.SetTimeout(1000);
                    foreach (ControloAutDicionarioEntity cad in queryCAD.Enumerable())
                    {
                        DicionarioEntity dic = cad.Dicionario;
                        if (dic != null && !dic.IsDeleted)
                        {
                            if (cad.ControloAut != null)
                            {
                                if (cad.ControloAut.TipoNoticiaAut != null && !cad.ControloAut.TipoNoticiaAut.IsDeleted)
                                    this.Autor = this.Autor + " " + dic.Termo;
                            }
                        }
                    }
                }
            }
        }

        private void initControloAut(ISession session, long frdbId)
        {
            IQuery queryCA = session.CreateQuery("select i from IndexFRDCAEntity as i where i.FRDBase.Id = " + frdbId + " AND i.IsDeleted = 0 ");
            queryCA.SetTimeout(1000);

            foreach (IndexFRDCAEntity ifrdca in queryCA.Enumerable<IndexFRDCAEntity>())
            {
                ControloAutEntity ca = ifrdca.ControloAut;
                if (ca != null && !ca.IsDeleted)
                {
                    this.idsControlosAutoridade += ca.Id.ToString() + " ";
                    if (ca.NotaExplicativa != null)
                    {
                        this.NotasExplicativas = this.NotasExplicativas + " " + ca.NotaExplicativa;
                    }
                    if (ca.ChaveColectividade != null)
                    {
                        this.ChavesColectividade = this.ChavesColectividade + " " + ca.ChaveColectividade;
                    }
                    if (ca.RegrasConvencoes != null)
                    {
                        this.RegrasConvencoes = this.RegrasConvencoes + " " + ca.RegrasConvencoes;
                    }
                    if (ca.Observacoes != null)
                    {
                        this.ObservacoesControloAuto = this.ObservacoesControloAuto + " " + ca.Observacoes;
                    }
                    if (ca.DescHistoria != null)
                    {
                        this.DescHistoricas = this.DescHistoricas + " " + ca.DescHistoria;
                    }
                    if (ca.DescZonaGeografica != null)
                    {
                        this.DescZonasGeograficas = this.DescZonasGeograficas + " " + ca.DescZonaGeografica;
                    }
                    if (ca.DescEstatutoLegal != null)
                    {
                        this.DescEstatutosLegais = this.DescEstatutosLegais + " " + ca.DescEstatutoLegal;
                    }
                    if (ca.DescOcupacoesActividades != null)
                    {
                        this.DescOcupacoesActividades = this.DescOcupacoesActividades + " " + ca.DescOcupacoesActividades;
                    }
                    if (ca.DescEnquadramentoLegal != null)
                    {
                        this.DescEnquadramentosLegais = this.DescEnquadramentosLegais + " " + ca.DescEnquadramentoLegal;
                    }
                    if (ca.DescEstruturaInterna != null)
                    {
                        this.DescEstruturasInternas = this.DescEstruturasInternas + " " + ca.DescEstruturaInterna;
                    }
                    if (ca.DescContextoGeral != null)
                    {
                        this.DescContextosGerais = this.DescContextosGerais + " " + ca.DescContextoGeral;
                    }
                    if (ca.DescOutraInformacaoRelevante != null)
                    {
                        this.DescOutraInformacaoRelevante = this.DescOutraInformacaoRelevante + " " + ca.DescOutraInformacaoRelevante;
                    }

                    // TipoNoticiaAut
                    if (ca.TipoNoticiaAut.Designacao != null)
                    {
                        this.DesignacoesTipoNoticiaAuto = this.DesignacoesTipoNoticiaAuto + " " + ca.TipoNoticiaAut.Designacao;
                    }

                    // Dicionario
                    IQuery queryCAD = session.CreateQuery("select c from ControloAutDicionarioEntity as c where c.ControloAut.Id = " + ca.Id + " AND c.IsDeleted = 0 ");
                    queryCAD.SetTimeout(1000);
                    foreach (ControloAutDicionarioEntity cad in queryCAD.Enumerable())
                    {
                        DicionarioEntity dic = cad.Dicionario;
                        if (dic != null && !dic.IsDeleted)
                        {
                            if (cad.ControloAut != null)
                            {
                                if (cad.ControloAut.TipoNoticiaAut != null && !cad.ControloAut.TipoNoticiaAut.IsDeleted)
                                {
                                    if (cad.ControloAut.TipoNoticiaAut.Id == 1 || cad.ControloAut.TipoNoticiaAut.Id == 2 || cad.ControloAut.TipoNoticiaAut.Id == 3)
                                    {
                                        if (dic.Termo != null)
                                        {
                                            this.TermosDeIndexacao = this.TermosDeIndexacao + " " + dic.Termo;
                                        }
                                    }
                                    else if (cad.ControloAut.TipoNoticiaAut.Id == 5)
                                    {
                                        if (dic.Termo != null)
                                        {
                                            this.TipologiaInformacional = this.TipologiaInformacional + " " + dic.Termo;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void initTipoEntidadeProdutora(ISession session, RelacaoHierarquicaEntity rh)
        {
            if (rh.Upper != null)
            {
                NivelEntity nUpper = rh.Upper;
                if (nUpper != null)
                {
                    IQuery ncaq = session.CreateQuery("select c from NivelControloAutEntity as c where c.ID.Id = " + nUpper.Id + " AND c.IsDeleted = 0 ");
                    ncaq.SetTimeout(1000);
                    NivelControloAutEntity nca = ncaq.UniqueResult<NivelControloAutEntity>();
                    if (nca != null && !nca.IsDeleted)
                    {
                        this.idsControlosAutoridade += nca.ControloAut.Id.ToString() + " ";
                        ControloAutEntidadeProdutoraEntity caep = session.Get<ControloAutEntidadeProdutoraEntity>(nca.ControloAut.Id);
                        if (caep != null && !caep.IsDeleted)
                        {
                            TipoEntidadeProdutoraEntity tep = caep.TipoEntidadeProdutora;
                            if (tep != null && !tep.IsDeleted && tep.Designacao != null)
                            {
                                this.DesignacoesTipoEntidadeProdutora = tep.Designacao;
                            }
                        }
                        IQuery cadq = session.CreateQuery("select c from ControloAutDicionarioEntity as c where c.ControloAut.Id = " + nca.ControloAut.Id + " AND c.IsDeleted = 0 ");
                        cadq.SetTimeout(1000);
                        foreach (ControloAutDicionarioEntity cad in cadq.Enumerable())
                        {
                            if (cad != null && cad.Dicionario != null && !cad.Dicionario.IsDeleted && cad.Dicionario.Termo != null)
                            {
                                this.EntidadeProdutora += " " + cad.Dicionario.Termo;
                            }
                        }

                    }
                }
            }
        }

        private void initTipos(ISession session, long frdbId, NivelEntity nivel, FRDBaseEntity frdb)
        {
            
            // TipoFormaSuporteAcond
            IQuery tfsaq = session.CreateQuery("select c from SFRDFormaSuporteAcondEntity as c where c.FRDBase.Id = " + frdbId + " AND c.IsDeleted = 0 ");
            tfsaq.SetTimeout(1000);
            foreach (SFRDFormaSuporteAcondEntity sfrdfsa in tfsaq.Enumerable())
            {
                TipoFormaSuporteAcondEntity tfsa = sfrdfsa.TipoFormaSuporteAcond;
                if (tfsa != null && tfsa.Designacao != null)
                {
                    this.DesignacoesTipoFormaSuporteAcond = this.DesignacoesTipoFormaSuporteAcond + " " + tfsa.Designacao;
                }
            }
            
            // TipoMaterialDeSuporte
            IQuery tmdsq = session.CreateQuery("select c from SFRDMaterialDeSuporteEntity as c where c.FRDBase.Id = " + frdbId + " AND c.IsDeleted = 0 ");
            tmdsq.SetTimeout(1000);
            foreach (SFRDMaterialDeSuporteEntity sfrdmds in tmdsq.Enumerable())
            {
                TipoMaterialDeSuporteEntity tmds = sfrdmds.TipoMaterialDeSuporte;
                if (tmds != null && tmds.Designacao != null)
                {
                    this.DesignacoesTipoMaterialDeSuporte = this.DesignacoesTipoMaterialDeSuporte + " " + tmds.Designacao;
                }
            }
            
            // TipoTecnicasDeRegisto
            IQuery ttdrq = session.CreateQuery("select c from SFRDTecnicasDeRegistoEntity as c where c.FRDBase.Id = " + frdbId + " AND c.IsDeleted = 0 ");
            ttdrq.SetTimeout(1000);
            foreach (SFRDTecnicasDeRegistoEntity sfrdtdr in ttdrq.Enumerable())
            {
                TipoTecnicasDeRegistoEntity ttdr = sfrdtdr.TipoTecnicasDeRegisto;
                if (ttdr != null && ttdr.Designacao != null)
                {
                    this.DesignacoesTipoTecnicasDeRegisto = this.DesignacoesTipoTecnicasDeRegisto + " " + ttdr.Designacao;
                }
            }

            // TipoEstadoDeConservacao
            IQuery tedcq = session.CreateQuery("select c from SFRDEstadoDeConservacaoEntity as c where c.FRDBase.Id = " + frdbId + " AND c.IsDeleted = 0 ");
            tedcq.SetTimeout(1000);
            foreach (SFRDEstadoDeConservacaoEntity sfrdedc in tedcq.Enumerable())
            {
                TipoEstadoDeConservacaoEntity tedc = sfrdedc.TipoEstadoDeConservacao;
                if (tedc != null && tedc.Designacao != null)
                {
                    this.DesignacoesTipoEstadoDeConservacao = this.DesignacoesTipoEstadoDeConservacao + " " + tedc.Designacao;
                }
            }

            // Iso639
            IQuery sfrdlq = session.CreateQuery("select c from SFRDLinguaEntity as c where c.FRDBase.Id = " + frdbId + " AND c.IsDeleted = 0 ");
            sfrdlq.SetTimeout(1000);
            foreach (SFRDLinguaEntity sfrdl in sfrdlq.Enumerable())
            {
                Iso639Entity iso639 = sfrdl.Iso639;
                if (iso639 != null && iso639.LanguageNameEnglish != null)
                {
                    this.LanguageNamesEnglish = this.LanguageNamesEnglish + " " + iso639.LanguageNameEnglish;
                }
            }

            // Iso 15924
            IQuery sfrdaq = session.CreateQuery("select c from SFRDAlfabetoEntity as c where c.FRDBase.Id = " + frdbId + " AND c.IsDeleted = 0 ");
            sfrdaq.SetTimeout(1000);
            foreach (SFRDAlfabetoEntity sfrda in sfrdaq.Enumerable())
            {
                Iso15924Entity iso15924 = sfrda.Iso15924;
                if (iso15924 != null && iso15924.ScriptNameEnglish != null)
                {
                    this.ScriptNamesEnglish = this.ScriptNamesEnglish + " " + iso15924.ScriptNameEnglish;
                }
            }
            
            // SFRDAvaliacao            
            SFRDAvaliacaoEntity sfrdav = session.Get<SFRDAvaliacaoEntity>(frdbId);
            
            if (sfrdav == null)
            {
                this.preservar = "PorAvaliar";
                this.publicar = "Nao";
            }
            else
            {
                if (sfrdav.Frequencia != null)
                {
                    this.Frequencia = sfrdav.Frequencia.ToString();
                }

                if (sfrdav.Preservar == null)
                {
                    this.preservar = "PorAvaliar";
                }
                else
                {
                    if (sfrdav.Preservar == true)
                    {
                        this.Preservar = "Preservar";
                    }
                    else
                    {
                        this.Preservar = "Eliminar";
                    }
                }
                if (sfrdav.Publicar)
                {
                    this.Publicar = "Sim";
                }
                else
                {
                    this.Publicar = "Nao";
                }

                if (sfrdav.Observacoes != null)
                {
                    this.ObservacoesSFRDAvaliacao = sfrdav.Observacoes;
                }

                // TipoSubDensidade
                TipoSubDensidadeEntity tsd = sfrdav.Subdensidade;
                if (tsd != null && tsd.Designacao != null)
                {
                    this.DesignacaoTipoSubDensidade = tsd.Designacao;
                }

                // TipoDensidade
                TipoDensidadeEntity td = sfrdav.Densidade;
                if (td != null && td.Designacao != null)
                {
                    this.DesignacaoTipoDensidade = td.Designacao;
                }

                // TipoPertinencia
                TipoPertinenciaEntity tp = sfrdav.Pertinencia;
                if (tp != null)
                {
                    if (tp.Designacao != null)
                    {
                        this.DesignacaoTipoPertinencia = tp.Designacao;
                    }
                    if (tp.Ponderacao != null)
                    {
                        this.Ponderacao = tp.Ponderacao;
                    }
                }

                // AutoEliminacao
                AutoEliminacaoEntity ae = sfrdav.AutoEliminacao;
                if (ae != null && ae.Designacao != null)
                {
                    this.DesignacaoAutoEliminacao = ae.Designacao;
                }
            }       
        }

        private void initNivelDesignado(ISession session, long id)
        {         
            NivelDesignadoEntity nd = session.Get<NivelDesignadoEntity>(id);
            if (nd != null && !nd.IsDeleted)
            {
                session.Evict(nd);
                if (nd.Designacao != null)
                {
                    this.DesignacaoNivelDesignado = nd.Designacao;
                }
            }
        }

        private void initSFRDUFCota(ISession session, long frdbId)
        {            
            IQuery ufs = session.CreateSQLQuery(
                "SELECT ct.Cota FROM SFRDUFCota ct " +
                "inner join FRDBase frd on frd.ID = ct.IDFRDBase and frd.isDeleted = 0 " +
                "inner join Nivel n on n.ID = frd.IDNivel and n.isDeleted = 0 " +
                "inner join SFRDUnidadeFisica uf on uf.IDNivel = n.ID and uf.isDeleted = 0 " +
                "WHERE ct.isDeleted = 0 and uf.IDFRDBase = " +
                frdbId
            );
            ufs.SetTimeout(1000);

            this.Cota = ufs.List<string>().ToList();
        }
        
        private void initSFRDImagem(ISession session, long frdbId)
        {
            IQuery sfrdiq = session.CreateQuery("select c from SFRDImagemEntity as c where c.FRDBase.Id = " + frdbId + " and (c.Tipo = 'Web' OR c.Tipo = 'Fedora') AND c.IsDeleted = 0 ");
            sfrdiq.SetTimeout(1000);
            this.NumImagens = sfrdiq.List().Count > 0 ? "sim" : "nao";

            IQuery ods = session.CreateSQLQuery(
                "SELECT img.IDFRDBase FROM SFRDImagem img " +
                "inner join SFRDImagemObjetoDigital imgOD on imgOD.idx = img.idx and imgOD.IDFRDBase = img.IDFRDBase and imgOD.isDeleted = 0 " +
                "inner join ObjetoDigital od on od.ID = imgOD.IDObjetoDigital and od.isDeleted = 0 and od.Publicado = 0 " +
                "WHERE img.isDeleted = 0 and img.IDFRDBase = " +
                frdbId
            );
            ods.SetTimeout(1000);

            this.NumODsNaoPublicados = ods.List<long>().ToList().Count > 0 ? "sim" : "nao";

            ods = session.CreateSQLQuery(
                "SELECT img.IDFRDBase FROM SFRDImagem img " +
                "inner join SFRDImagemObjetoDigital imgOD on imgOD.idx = img.idx and imgOD.IDFRDBase = img.IDFRDBase and imgOD.isDeleted = 0 " +
                "inner join ObjetoDigital od on od.ID = imgOD.IDObjetoDigital and od.isDeleted = 0 and od.Publicado = 1 " +
                "WHERE img.isDeleted = 0 and img.IDFRDBase = " +
                frdbId
            );
            ods.SetTimeout(1000);

            this.NumODsPublicados = ods.List<long>().ToList().Count > 0 ? "sim" : "nao";
        }

        #endregion

        #region Licencas de Obra: Initializations from database

        private void initLicencaObra(ISession session, long frdbId) {
            LicencaObraEntity lic = session.Get<LicencaObraEntity>(frdbId);
            if (lic != null && !lic.IsDeleted) {
                this.PHSimNao = lic.PropriedadeHorizontal ? "sim" : "nao";
                this.TipoObra = lic.TipoObra;
                this.PHTexto = lic.PHTexto;
                this.PHCompleto = this.PHSimNao + " " + lic.PHTexto;
            }
        }

        private void initLicencaObraAtestadoHabitabilidade(ISession session, long frdbId) {
            string query = " FROM LicencaObraAtestadoHabitabilidadeEntity  hab WHERE hab.FRDBase.Id = " + frdbId + " AND hab.IsDeleted = 0 ";
            IQuery atestadosHab = session.CreateQuery(query);
            atestadosHab.SetTimeout(1000);
            StringBuilder app_codigos = new StringBuilder();
            foreach (LicencaObraAtestadoHabitabilidadeEntity lic in atestadosHab.Enumerable()) {
                app_codigos.Append(lic.Codigo); app_codigos.Append(" ");
            }
            this.CodigosAtestadoHabitabilidade = app_codigos.ToString().Trim();
        }

        private void initLicencaObraDataLicencaConstrucao(ISession session, long frdbId) {
            string query = " FROM LicencaObraDataLicencaConstrucaoEntity  datahab WHERE datahab.FRDBase.Id = " + frdbId + " AND datahab.IsDeleted = 0 ";
            IQuery datasHab = session.CreateQuery(query);
            datasHab.SetTimeout(1000);
            StringBuilder allDatas = new StringBuilder();

            foreach (LicencaObraDataLicencaConstrucaoEntity dataHab in datasHab.Enumerable()) {
                int ano = 1, mes = 1, dia = 1;

                if (dataHab.Ano != null) int.TryParse(dataHab.Ano, out ano);
                if (ano < 1) ano = 1;

                if (dataHab.Mes != null) int.TryParse(dataHab.Mes, out mes);
                if (mes < 1) mes = 1;

                if (dataHab.Dia != null) int.TryParse(dataHab.Dia, out dia);
                if (dia < 1) dia = 1;

                string data = string.Format("{0:0000}{1:00}{2:00}", ano, mes, dia);
                allDatas.Append(data).Append(" ");
            }
            this.Datas_LicencaObraDataLicencaConstrucao = allDatas.ToString();
        }

        private void initLicencaObraLocalizacaoObraActual(ISession session, long frdbId) {
            string query = " FROM LicencaObraLocalizacaoObraActualEntity  locAct WHERE locAct.FRDBase.Id = " + frdbId + " AND locAct.IsDeleted = 0 ";
            IQuery locsObra = session.CreateQuery(query);
            locsObra.SetTimeout(1000);
            StringBuilder all_Nums_Policia = new StringBuilder();
            StringBuilder all_Dict_Termos = new StringBuilder();

            foreach (LicencaObraLocalizacaoObraActualEntity loc in locsObra.Enumerable()) {
                this.idsControlosAutoridade += loc.ControloAut.Id.ToString() + " ";
                all_Nums_Policia.Append(loc.NumPolicia).Append(" ");

                IQuery query_Dict = session.CreateQuery(" FROM ControloAutDicionarioEntity dict WHERE dict.ControloAut.Id = " + loc.ControloAut.Id + " AND dict.IsDeleted = 0 ");
                query_Dict.SetTimeout(1000);
                foreach (ControloAutDicionarioEntity dict in query_Dict.Enumerable())
                    all_Dict_Termos.Append(dict.Dicionario.Termo).Append(" ");
            }

            this.NumPolicia_LicencaObraLocalizacaoObraActual = all_Nums_Policia.ToString().Trim();
            this.Termo_LicencaObraLocalizacaoObraActual = all_Dict_Termos.ToString().Trim();
        }

        private void initLicencaObraLocalizacaoObraAntiga(ISession session, long frdbId) {
            string query = " FROM LicencaObraLocalizacaoObraAntigaEntity  locAnt WHERE locAnt.FRDBase.Id = " + frdbId + " AND locAnt.IsDeleted = 0 ";
            IQuery locsObra = session.CreateQuery(query);
            locsObra.SetTimeout(1000);
            StringBuilder app_Nums_Policia = new StringBuilder();
            StringBuilder app_Nomes_Local = new StringBuilder();

            foreach (LicencaObraLocalizacaoObraAntigaEntity loc in locsObra.Enumerable()) {
                app_Nums_Policia.Append(loc.NumPolicia).Append(" ");
                app_Nomes_Local.Append(loc.NomeLocal).Append(" ");
            }

            this.NumPolicia_LicencaObraLocalizacaoObraAntiga = app_Nums_Policia.ToString().Trim();
            this.NomeLocal_LicencaObraLocalizacaoObraAntiga = app_Nomes_Local.ToString().Trim();
        }


        private void initLicencaObraRequerentes(ISession session, long frdbId) {
            string query = " FROM LicencaObraRequerentesEntity  req WHERE req.FRDBase.Id = " + frdbId + " AND req.IsDeleted = 0 ";
            IQuery reqObra = session.CreateQuery(query);
            reqObra.SetTimeout(1000);
            StringBuilder app_Nome = new StringBuilder();

            foreach (LicencaObraRequerentesEntity req in reqObra.Enumerable()) {
                // req.Tipo nao e´ usado ('INICIAL' | 'AVRB')
                app_Nome.Append(req.Nome).Append(" ");
            }

            this.Nome_LicencaObraRequerentes = app_Nome.ToString().Trim();
        }

        private void initLicencaObraTecnicoObra(ISession session, long frdbId) {
            string query = " FROM LicencaObraTecnicoObraEntity  teco WHERE teco.FRDBase.Id = " + frdbId + " AND teco.IsDeleted = 0 ";
            IQuery tecosObra = session.CreateQuery(query);
            tecosObra.SetTimeout(1000);
            StringBuilder all_Dict_Termos = new StringBuilder();

            foreach (LicencaObraTecnicoObraEntity teco in tecosObra.Enumerable()) {
                this.idsControlosAutoridade += teco.ControloAut.Id.ToString() + " ";
                IQuery query_Dict = session.CreateQuery(" FROM ControloAutDicionarioEntity dict WHERE dict.ControloAut.Id = " + teco.ControloAut.Id + " AND dict.IsDeleted = 0 ");
                query_Dict.SetTimeout(1000);
                foreach (ControloAutDicionarioEntity dict in query_Dict.Enumerable())
                    all_Dict_Termos.Append(dict.Dicionario.Termo).Append(" ");
            }

            this.Termo_LicencaObraTecnicoObra = all_Dict_Termos.ToString().Trim();
        }

        #endregion

        private void initCodigo(ISession session, long frdbId) {
            string query = " FROM CodigoEntity  cod WHERE cod.FRDBase.Id = " + frdbId + " AND cod.IsDeleted = 0 ";
            IQuery codigos = session.CreateQuery(query);
            codigos.SetTimeout(1000);
            StringBuilder app_codigos = new StringBuilder();

            foreach (CodigoEntity cod in codigos.Enumerable()) {
                app_codigos.Append(cod.Codigo).Append(" ");
            }

            this.Codigo_Codigo = app_codigos.ToString().Trim();
        }

        public override string ToString(){
            StringBuilder ret = new StringBuilder();
            
            // Nivel
            ret.Append(this.id);ret.Append(" ");      
            ret.Append(this.codigo);ret.Append(" ");  

            // TipoNivelRelacionado
            ret.Append(this.designacaoTipoNivelRelacionado);ret.Append(" "); 
            
            // FRDBase
            ret.Append(this.notaDoArquivista);ret.Append(" ");    
            ret.Append(this.regrasOuConvencoes);ret.Append(" ");  

            // SFRDContexto
            ret.Append(this.historiaAdministrativa);ret.Append(" ");
            ret.Append(this.historiaCustodial);ret.Append(" ");           
            ret.Append(this.fonteImediataDeAquisicao);ret.Append(" ");    

            // TipoTradicaoDocumental
            ret.Append(this.designacaoTipoTradicaoDocumental);ret.Append(" "); // lista

            // SFRDDatasProducao
            ret.Append(this.inicioTexto);ret.Append(" ");
            ret.Append(this.inicioAno);ret.Append(" ");
            ret.Append(this.inicioMes);ret.Append(" ");
            ret.Append(this.inicioDia);ret.Append(" ");
            ret.Append(this.inicioAtribuida);ret.Append(" ");
            ret.Append(this.fimTexto);ret.Append(" ");
            ret.Append(this.fimAno);ret.Append(" ");
            ret.Append(this.fimMes);ret.Append(" ");
            ret.Append(this.fimDia);ret.Append(" ");
            ret.Append(this.fimAtribuida);ret.Append(" ");

            // SFRDDocumentacaoAssociada
            ret.Append(this.existenciaDeOriginais);ret.Append(" ");
            ret.Append(this.existenciaDeCopias);ret.Append(" ");
            ret.Append(this.unidadesRelacionadas);ret.Append(" ");
            ret.Append(this.notaDePublicacao);ret.Append(" ");

            //SFRDAgrupador
            ret.Append(this.agrupador); ret.Append(" ");

            // SFRDConteudoEEstrutura
            ret.Append(this.conteudoInformacional);ret.Append(" ");
            ret.Append(this.incorporacao);ret.Append(" ");

            // TipoOrdenacao
            ret.Append(this.designacaoTipoOrdenacao);ret.Append(" "); // lista

            // SFRDNotaGeral
            ret.Append(this.notaGeral);ret.Append(" ");

            //SFRDCondicaoDeAcesso
            ret.Append(this.estatutoLegal);ret.Append(" ");
            ret.Append(this.condicaoDeAcesso);ret.Append(" ");
            ret.Append(this.condicaoDeReproducao);ret.Append(" ");
            ret.Append(this.auxiliarDePesquisa);ret.Append(" ");

            // ControloAuto
            ret.Append(this.notasExplicativas);ret.Append(" "); // lista
            ret.Append(this.chavesColectividade);ret.Append(" "); // lista
            ret.Append(this.regrasConvencoes);ret.Append(" "); // lista
            ret.Append(this.observacoesControloAuto);ret.Append(" "); // lista
            ret.Append(this.descHistoricas);ret.Append(" "); // lista
            ret.Append(this.descZonasGeograficas);ret.Append(" "); // lista
            ret.Append(this.descEstatutosLegais);ret.Append(" "); // lista
            ret.Append(this.descOcupacoesActividades);ret.Append(" "); // lista
            ret.Append(this.descEnquadramentosLegais);ret.Append(" "); // lista
            ret.Append(this.descEstruturasInternas);ret.Append(" "); // lista
            ret.Append(this.descContextosGerais);ret.Append(" "); // lista
            ret.Append(this.descOutraInformacaoRelevante);ret.Append(" "); //lista

            // TipoNoticiaAuto
            ret.Append(this.designacoesTipoNoticiaAuto);ret.Append(" "); // lista

            // Dicionario
            ret.Append(this.termos);ret.Append(" "); // lista
            ret.Append(this.termosDeIndexacao);ret.Append(" "); // lista
            ret.Append(this.tipologiaInformacional);ret.Append(" "); // lista

            // Autor
            ret.Append(this.autor); ret.Append(" "); // lista

            // TipoEntidadeProdutor
            ret.Append(this.designacoesTipoEntidadeProdutora);ret.Append(" "); // lista
            ret.Append(this.entidadeProdutora); ret.Append(" ");

            //Ids de todos os controlos de autoridade directamente relacionados com o documento
            ret.Append(this.idsControlosAutoridade); ret.Append(" ");

            // TipoFormaSuporteAcond
            ret.Append(this.designacoesTipoFormaSuporteAcond);ret.Append(" "); // lista

            // TipoMaterialDeSuporte
            ret.Append(this.designacoesTipoMaterialDeSuporte);ret.Append(" "); // lista

            // TipoTecnicasDeRegisto
            ret.Append(this.designacoesTipoTecnicasDeRegisto);ret.Append(" "); // lista

            // TipoEstadoDeConservacao);ret.Append(" ");
            ret.Append(this.designacoesTipoEstadoDeConservacao);ret.Append(" "); // lista

            // Iso639
            ret.Append(this.languageNamesEnglish);ret.Append(" "); // lista

            // Iso15924
            ret.Append(this.scriptNamesEnglish);ret.Append(" "); // lista

            // SFRDAvaliacao    
            ret.Append(this.frequencia);ret.Append(" ");
            ret.Append(this.preservar);ret.Append(" ");            
            ret.Append(this.publicar);ret.Append(" ");
            ret.Append(this.observacoesSFRDAvaliacao);ret.Append(" ");
            ret.Append(this.idAutoEliminacao); ret.Append(" ");

            // TipoSubDensidade
            ret.Append(this.designacaoTipoSubDensidade);ret.Append(" ");

            // TipoDensidade
            ret.Append(this.designacaoTipoDensidade);ret.Append(" ");

            // TipoPertinencia
            ret.Append(this.designacaoTipoPertinencia);ret.Append(" ");
            ret.Append(this.ponderacao);ret.Append(" ");

            // AutoEliminacao
            ret.Append(this.designacaoAutoEliminacao);ret.Append(" ");
            
            // NivelDesignado
            ret.Append(this.designacaoNivelDesignado);ret.Append(" ");

            // SFRDUFCota
            this.cota.ForEach(c => { ret.Append(c); ret.Append(" "); });

            // SFRDImagem
            ret.Append(this.numImagens); ret.Append(" ");

            #region Licencas de obra (toString())
            // LicencaObra
            ret.Append(this.TipoObra); ret.Append(" ");
            ret.Append(this.PHSimNao); ret.Append(" ");
            ret.Append(this.PHTexto); ret.Append(" ");

            // LicencaObraAtestadoHabitabilidade
            ret.Append(this.CodigosAtestadoHabitabilidade); ret.Append(" ");

            // LicencaObraDataLicencaConstrucao
            //ret.Append(this.Datas_LicencaObraDataLicencaConstrucao).Append(" ");

            // LicencaObraLocalizacaoObraActual
            ret.Append(this.NumPolicia_LicencaObraLocalizacaoObraActual).Append(" ");
            ret.Append(this.Termo_LicencaObraLocalizacaoObraActual).Append(" ");

            // LicencaObraLocalizacaoObraAntiga
            ret.Append(this.NumPolicia_LicencaObraLocalizacaoObraAntiga).Append(" ");
            ret.Append(this.NomeLocal_LicencaObraLocalizacaoObraAntiga).Append(" ");

            // LicencaObraRequerentes
            ret.Append(this.Nome_LicencaObraRequerentes).Append(" ");

            // LicencaObraTecnicoObra
            ret.Append(this.Termo_LicencaObraTecnicoObra).Append(" ");

            #endregion

            // Codigo:
            ret.Append(this.Codigo_Codigo);

            // IdUpper
            ret.Append(this.idUpper);

            return ret.ToString();
        }
    }
}
