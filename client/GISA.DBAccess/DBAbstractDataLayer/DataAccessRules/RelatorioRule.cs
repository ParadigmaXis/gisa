using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Collections.Generic;

namespace DBAbstractDataLayer.DataAccessRules
{
	public abstract class RelatorioRule: DALRule
	{
		private static RelatorioRule current = null;
		public static void ClearCurrent() 
		{
			current = null;
		}
		public static RelatorioRule Current
		{
			get 
			{
				if (Object.ReferenceEquals(null, current)) 
				{
					current = (RelatorioRule) Create(typeof(RelatorioRule));
				}
				return current;
			}
		}

        protected enum TipoRel
        {
            InventariosCatalogosPesqDetalhada = 0,
            UnidadesFisicas = 1,
            EntidadesProdutoras = 2,
            Conteudos = 3,
            Tipologias = 4
        }

        protected abstract List<ReportParameter> BuildParamList(TipoRel tRel, bool incCamposEstr);

        protected static string mSelectClause;
        protected static string mJoinClause;
        protected static string mWhereClause;
        public static void BuildReportQuery(List<ReportParameter> parameters, List<string> separadores)
        {
            List<string> fields = new List<string>();
            List<string> joins = new List<string>();
            List<string> wheres = new List<string>();

            foreach (ReportParameter parameter in parameters)
            {
                foreach (string field in parameter.DBField)
                    InsertIfNotExist(fields, field);
                foreach (string field in parameter.JoinClause)
                    InsertIfNotExist(joins, field);
                foreach (string field in parameter.WhereClause)
                    InsertIfNotExist(wheres, field);
            }

            mSelectClause = BuildClause(fields, separadores[0]);
            mJoinClause = BuildClause(joins, separadores[1]);
            mWhereClause = BuildClause(wheres, separadores[2]);
        }

        private static void InsertIfNotExist(List<string> lista, string elemento)
        {
            if (!lista.Contains(elemento))
                lista.Add(elemento);
        }

        private static string BuildClause(List<string> lista, string separador)
        {
            StringBuilder clause = new StringBuilder();
            foreach (string elemento in lista)
            {
                if (clause.Length > 0)
                    clause.Append(separador);

                clause.Append(elemento);
            }
            return clause.ToString();
        }

		#region AutoEliminacao
		public abstract IDataReader ReportAutoEliminacao(long IDTrustee, long IDAutoEliminacao, IDbConnection conn);
        public abstract IDataReader ReportAutoEliminacaoPortaria(long IDTrustee, long IDAutoEliminacao, IDbConnection conn);
		#endregion

		#region ControloAut
        public List<ReportParameter> BuildParamListControloAut()
        {
            return BuildParamList(TipoRel.EntidadesProdutoras, false);
        }
		public abstract void InitializeControloAut(ArrayList parameters, IDbConnection conn);
		public abstract void FinalizeControloAut(IDbConnection conn);
        public abstract IDataReader ReportControloAut(List<ReportParameter> fields, IDbConnection conn);
		#endregion

		#region ControlAutoEliminacao
		public abstract void LoadAutosEliminacao(DataSet currentDataSet, IDbConnection conn);
        public abstract List<string> GetAutoEliminacaoAssociations(long IDAutoEliminacao, IDbConnection conn);
		#endregion

        #region ControlLocalConsulta
        public abstract void LoadLocaisConsulta(DataSet currentDataSet, IDbConnection conn);
        public abstract List<string> GetLocalConsultaAssociations(long IDAutoEliminacao, IDbConnection conn);
        //public abstract long GetAutoEliminacao(string designacao, IDbTransaction tran);
        #endregion

		#region FormAutoEliminacaoPicker
		public abstract ArrayList LoadDataFormAutoEliminacaoPicker(bool excludeEmptyAutos, IDbConnection conn);
		public abstract void LoadAutoEliminacao(DataSet currentDataSet, long aeID, IDbConnection conn);
		public abstract void LoadAutosEliminacao(DataSet currentDataSet, ArrayList aeIDs, IDbConnection conn);
		#endregion

		#region Inventario
        public List<ReportParameter> BuildParamListInventCat(bool incCamposEstr)
        {
            return BuildParamList(TipoRel.InventariosCatalogosPesqDetalhada, incCamposEstr);
        }
        public abstract void InitializeInventario(long ? IDTopoNivel, IDbConnection conn);
		public abstract void FinalizeInventario(IDbConnection conn);
        public abstract IDataReader ReportInventario(long IDTopoNivel, long IDTrustee, bool isCatalogo, bool isDetalhado, bool isTopDownExpansion, List<ReportParameter> fields, IDbConnection conn);
        public abstract ArrayList GetProdutores();
        public abstract Hashtable GetCodCompletos();
		#endregion

		#region UnidadeFisica
        public List<ReportParameter> BuildParamListUF()
        {
            return BuildParamList(TipoRel.UnidadesFisicas, false);
        }
		public abstract void InitializeListaUnidadesFisicas(ArrayList parameters, IDbConnection conn);
		public abstract void FinalizeListaUnidadesFisicas(IDbConnection conn);
        public abstract IDataReader ReportUnidadesFisicas(long IDTrustee, List<ReportParameter> fields, IDbConnection conn);
		public abstract void LoadGeneratePdfUFAgData (DataSet currentDataSet, long NivelRowID, IDbConnection conn);
		public abstract void LoadGeneratePdfUFnAgData (DataSet currentDataSet, long TipoNivelRelacionadoUF, IDbConnection conn);

        public abstract IDataReader ReportResPesquisaResumidoUnidadesFisicas(IDbConnection conn);
		#endregion

		#region ResultadosPesquisa
		public abstract IDataReader ReportResPesquisa(IDbConnection conn);
        public abstract void InitializeReportResPesquisa(IDbConnection conn);
        public abstract void FinalizeReportResPesquisa(IDbConnection conn);

        public abstract void InitializeReportResPesquisaDet(IDbConnection conn);
        public abstract void FinalizeReportResPesquisaDet(IDbConnection conn);
        public abstract IDataReader ReportResPesquisaDetalhado(List<ReportParameter> fields, long IDTrustee, IDbConnection conn);
		#endregion
	}

    public abstract class ReportParameter
    {
        public enum ReturnType
        {
            TextOnly = 0,
            List = 1,
            Details = 2
        }

        private string[] dbField;
        private string[] joinClause;
        private string[] whereClause;
        private ReturnType rType;
        protected ReportParameter(string[] dbField, string[] joinClause, string[] whereClause, ReturnType rType)
        {
            this.dbField = dbField;
            this.joinClause = joinClause;
            this.whereClause = whereClause;
            this.rType = rType;
        }

        public string[] DBField { get { return this.dbField; } }
        public string[] JoinClause { get { return this.joinClause; } }
        public string[] WhereClause { get { return this.whereClause; } }
        public ReturnType RetType { get { return this.rType; } }
    }

    public class ReportParameterRelInvCatPesqDet : ReportParameter
    {
        public enum CamposRelInvCatPesqDet
        {
            IDNivel = 0,
            Agrupador = 1,
            Dimensao = 48,
            CotaDocumento = 2,
            UFsAssociadas = 3,
            Autores = 4,
            HistAdministrativaBiografica = 5,
            FonteImediataAquisicaoTransferencia = 6,
            HistoriaArquivistica = 7,
            TipologiaInformacional = 8,
            Diplomas = 9,
            Modelos = 10,
            ConteudoInformacional = 11,
            LO_RequerentesIniciais = 39,
            LO_RequerentesAverbamentos = 40,
            LO_DesignacaoNumPoliciaAct = 41,
            LO_DesignacaoNumPoliciaAntigo = 42,
            LO_TipoObra = 43,
            LO_PropHorizontal = 44,
            LO_TecnicoObra = 45,
            LO_AtestHabit = 46,
            LO_DataLicConst = 47,
            DiplomaLegal = 12,
            RefTab = 13,
            DestinoFinal = 14,
            Prazo = 15,
            Publicado = 16,
            ObservacoesEnquadramentoLegal = 17,
            Incorporacoes = 18,
            TradicaoDocumental = 19,
            Ordenacao = 20,
            ObjectosDigitais = 21,
            CondicoesAcesso = 22,
            CondicoesReproducao = 23,
            Lingua = 24,
            Alfabeto = 25,
            FormaSuporteAcondicionamento = 26,
            MaterialSuporte = 27,
            TecnicaRegisto = 28,
            EstadoConservacao = 29,
            InstrumentosPesquisa = 30,
            ExistenciaLocalizacaoOriginais = 31,
            ExistenciaLocalizacaoCopias = 32,
            UnidadesDescricaoAssociadas = 33,
            NotaPublicacao = 34,
            Notas = 35,
            NotaArquivista = 36,
            RegrasConvenções = 37,
            Indexacao = 38
        }

        private CamposRelInvCatPesqDet campo;
        public ReportParameterRelInvCatPesqDet(CamposRelInvCatPesqDet campo, string[] dbField, string[] joinClause, string[] whereClause, ReturnType rType)
            : base(dbField, joinClause, whereClause, rType)
        {
            this.campo = campo;
        }

        public CamposRelInvCatPesqDet Campo { get { return this.campo; } }
    }

    public class ReportParameterRelPesqUF : ReportParameter
    {
        public enum CamposRelPesqUF
        {
            GuiaIncorporacao = 0,
            CotaCodigoBarras = 1,
            DatasProducao = 2,
            TipoDimensoes = 3,
            UltimaAlteracao = 4,
            ConteudoInformacional = 5,
            UnidadesInformacionaisAssociadas = 6,
            Eliminada = 7
        }

        private CamposRelPesqUF campo;
        public ReportParameterRelPesqUF(CamposRelPesqUF campo, string[] dbField, string[] joinClause, string[] whereClause, ReturnType rType)
            : base(dbField, joinClause, whereClause, rType)
        {
            this.campo = campo;
        }

        public CamposRelPesqUF Campo { get { return this.campo; } }
    }

    public class ReportParameterRelEPs : ReportParameter
    {
        public enum CamposRelEPs
        {
            TipoEntidadeProdutora = 0,
            FormaParalela = 1,
            FormaNormalizada = 2,
            OutrasFormas = 3,
            IdentificadorUnico = 4,
            DatasExistencia = 5,
            Historia = 6,
            ZonaGeografica = 7,
            EstatutoLegal = 8,
            FuncoesOcupacoesActividades = 9,
            EnquadramentoLegal = 10,
            EstruturaInterna = 11,
            ContextoGeral = 12,
            OutrasInformacoesRelevantes = 13,
            Relacoes = 14,
            RegrasConvencoes = 15,
            Validado = 16,
            Completo = 17,
            LinguaAlfabeto = 18,
            FontesObservacoes = 19            
        }

        private CamposRelEPs campo;
        public ReportParameterRelEPs(CamposRelEPs campo, string[] dbField, string[] joinClause, string[] whereClause, ReturnType rType)
            : base(dbField, joinClause, whereClause, rType)
        {
            this.campo = campo;
        }

        public CamposRelEPs Campo { get { return this.campo; } }
    }
}
