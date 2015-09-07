using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace DBAbstractDataLayer.DataAccessRules
{
	public abstract class NivelRule: DALRule
	{
		private static NivelRule current = null;
		public static void ClearCurrent() 
		{
			current = null;
		}
		public static NivelRule Current
		{
			get 
			{
				if (Object.ReferenceEquals(null, current)) 
				{
					current = (NivelRule) Create(typeof(NivelRule));
				}
				return current;
			}
		}
		//
		// TODO: abstract method signatures for Nivel data access rules
		//
		/// <summary>
		/// Semantics?
		/// </summary>
		/// <param name="dataSet">The <see cref="DataSet"/> affected by the method.</param>
		/// <param name="connection">The connection context that the method should use. Mandatory.</param>
		/// <param name="transaction">The transaction context where the method should run. Should be a transaction using <c>connection</c>. This parameter can be null.</param>			

        public abstract string GetCodigoCompletoNivel(long NivelRowID, IDbConnection conn);
        public abstract void LoadNivelDocumental(DataSet CurrentDataSet, long NivelRowID, IDbConnection conn);
		#region "Delete Nivel"
		public abstract void DeleteNivelInDataBase(long NivelID, IDbTransaction tran);
		public abstract void DeleteFRDBaseInDataBase(long FRDRowID, IDbTransaction tran);
		#endregion
		public abstract Hashtable EstimateChildCount(string NivelRowID, bool shouldShowSeries, IDbConnection conn);
		public abstract int getDirectChildCount(string nivelRowID, string extraFilter, IDbConnection conn);
		public abstract int getDirectChildCount(string nivelRowID, string extraFilter, IDbTransaction tran);
		public abstract int getParentCount(string nivelRowID, IDbTransaction tran);
		public abstract int getParentCount(string nivelRowID, IDbConnection conn);
		public abstract ArrayList GetCodigoOfNivel(long nivelRowID, IDbConnection conn);		
		public abstract bool existsRelacaoHierarquica(string IDUpper, string ID, IDbTransaction tran);
		public abstract bool existsNivel(string nRowID, IDbTransaction tran);
		public abstract bool isUniqueCodigo(string Codigo, long IDNivel, IDbTransaction tran);
		public abstract bool isUniqueCodigo(string Codigo, long IDNivel, IDbTransaction tran, bool testOnlyWithinNivel);
		public abstract bool isUniqueCodigo(string Codigo, long IDNivel, IDbTransaction tran, bool testOnlyWithinNivel, long IDNivelUpper);
        public abstract long GetIDCodigoRepetido(string Codigo, long IDNivel, IDbTransaction tran, bool testOnlyWithinNivel, long IDNivelUpper);
		public abstract int getUnidadesDescricaoCountForUnidadeFisica(long IDNivelUF, IDbConnection conn);

		#region " Carregamento de níveis e notícias de autoridade "
		public abstract long[] LoadEntidadesDetentoras(DataSet dataSet, IDbConnection connection);
		public abstract void LoadNivel(long nivelID, DataSet dataSet, IDbConnection connection);
		public abstract void LoadDesignacaoNivel(long nivelID, DataSet dataSet, IDbConnection connection);
		public abstract void LoadNivelByControloAut(long controloAutID, DataSet dataSet, IDbConnection connection);
		#endregion
		#region " Carregamento de níveis e notícias de autoridade descendentes "
		public abstract void LoadNivelChildren(long nivelID, DataSet dataSet, IDbConnection connection);
		public abstract void LoadNivelChildren(long nivelID, bool alsoLoadDesignacoes, DataSet dataSet, IDbConnection connection);
		public abstract void LoadControloAutChildren(long controloAutID, DataSet dataSet, IDbConnection connection) ;
		public abstract void LoadControloAutChildren(long controloAutID, bool alsoLoadNiveis, DataSet dataSet, IDbConnection connection);
		public abstract void LoadNivelControloAutChildrenByCA(long controloAutID, DataSet dataSet, IDbConnection connection);
		#endregion
		#region " Carregamento de níveis e notícias de autoridade ascendentes "
		public abstract void LoadNivelParents(long nivelID, DataSet dataSet, IDbConnection connection);
		public abstract void LoadNivelGrandparents(long nivelID, DataSet dataSet, IDbConnection connection);
		public abstract void LoadControloAutParents(long controloAutID, DataSet dataSet, IDbConnection connection);
		public abstract void LoadControloAutParents(long controloAutID, bool alsoLoadNiveis, DataSet dataSet, IDbConnection connection);
		public abstract void LoadNivelControloAutParentsByCA(long controloAutID, DataSet dataSet, IDbConnection connection);
		public abstract void LoadNivelControloAutParentsByNivel(long nivelID, DataSet dataSet, IDbConnection connection);
		#endregion
		#region " Obtenção de documentos e unidades físicas para efeitos de avaliação "
		public struct DocumentoAssociado
		{
			public long IDNivel;
			public long IDNivelUpper;
            public int IDTipoNivelRelacionado;
            public long IDFRD;
            public string Codigo;
            public string Designacao;
            public string DesignacaoUpper;
            public bool PermEscrever;
            public string InicioAno;
            public string InicioMes;
            public string InicioDia;
            public string FimAno;
            public string FimMes;
            public string FimDia;
            public string Preservar;
            public string IDAutoEliminacao;
            public string Expirado;
		}

		public struct UnidadeFisicaAssociada
		{
			public long IDNivel;
			public string Codigo;
            public string Designacao;
            public string InicioAno;
            public string InicioMes;
            public string InicioDia;
            public string FimAno;
            public string FimMes;
            public string FimDia;
            public bool IsNotDocRelated;
            public bool IsSerieRelated;
		}

        protected Dictionary<long, List<long>> UfsSeriesAssoc;
        protected Dictionary<long, List<long>> UfsDocsAssoc;
        protected OrderedDictionary UfsAssoc;
        protected OrderedDictionary DocsAssoc;

        public abstract void GetUfsEDocsAssociados(long nivelId, long userID, IDbConnection conn);
        public Dictionary<long, List<long>> GetUfsDocsAssoc() { return UfsDocsAssoc; }
        public Dictionary<long, List<long>> GetUfsSeriesAssoc() { return UfsSeriesAssoc; }
        public OrderedDictionary GetUfsAssoc() { return UfsAssoc; }
        public OrderedDictionary GetDocsAssoc() { return DocsAssoc; }
		#endregion
		#region " Publicação "
		public struct PublicacaoDocumentos
		{
			public PublicacaoDocumentos(long IDNivelDoc, bool Publicar)
			{
				this.IDNivelDoc = IDNivelDoc;
				this.Publicar = Publicar;
			}
			public long IDNivelDoc;
			public bool Publicar;
		}
		public abstract DataRow[] SelectFRDBase (DataSet dataSet, long nRowID, int TipoFRDBase);
		public abstract void ExecutePublishNivel(long NivelID, IDbTransaction tran);
        public abstract List<string> ExecutePublishSubDocumentos(List<PublicacaoDocumentos> DocsID, long IDTrustee, IDbTransaction tran);
		#endregion
        public abstract long GetNivelLastIDTipoNivelRelacionado(long frdID, IDbConnection conn);
		public abstract long GetNivelID(long controloAutID, IDbConnection conn);
		public abstract void FillNivelDesignado (DataSet CurrentDataSet, long CurrentNivelID, IDbConnection conn);
		public abstract void FillNivelControloAutRows (DataSet CurrentDataSet, long CurrentNivelID, IDbConnection conn);
		public abstract void FillTipoNivelRelacionadoCodigo(DataSet CurrentDataSet, IDbConnection connection);
		public abstract void FillTipoNivelRelacionadoCodigo(DataSet CurrentDataSet, IDbTransaction transaction);
		#region MasterPanelAdminGlobal
		public abstract long GetNivelEstruturalCount(IDbConnection conn);
		public abstract void LoadModelosAvaliacao(DataSet CurrentDataSet, IDbConnection connection);
		public abstract void ClearAvaliacaoTabelaSeries(DateTime data, IDbConnection connection);
		public abstract bool ManageModelosAvaliacao(bool Operacao, long IDModeloAvaliacao, string Designacao, short PrazoConservacao, bool Preservar, IDbConnection connection);
		public abstract bool ManageListasModelosAvaliacao(bool Operacao, long IDListaModeloAvaliacao, string Designacao, DateTime DataInicio, IDbConnection connection);
		#endregion
		#region MasterPanelSeries
		public struct ExpandNivelStruct
		{
			public long IDNivel;
			public string Desigancao;
			public long IDTipoNivelDesignado;
			public int GUIOrder;
		}
		public abstract ArrayList ExpandTreeView (long NivelID, long ExceptTipoNivel, IDbConnection conn);
		public abstract void LoadDataBeforeExpand (DataSet currentDataSet, long NivelID, long ExceptTipoNivel, IDbConnection conn);
		public abstract bool isNivelDeleted (long nivelID, IDbTransaction tran);
        public abstract void LoadNivelRelacoesHierarquicas(DataSet currentDataSet, List<long> IDNivel, IDbConnection conn);
		#endregion
		#region MasterPanelUnidadesFisicas
		public abstract void LoadUFsRelatedData(DataSet currentDataSet, IDbConnection conn);
		public abstract IDataReader GetUFReport (long NivelID, IDbConnection conn);
		#endregion
        #region MasterPanelFedora
        public abstract long GetDocNextGUIOrder(long DocCompostoNivelID, IDbTransaction tran);
        public abstract void LoadTipoDocumento(DataSet CurrentDataSet, long CurrentNivelID, IDbConnection conn);
        #endregion

        #region NivelEstruturalList
        public abstract void CalculateOrderedItemsEstrutural(ArrayList ordenacao, string filtroDesignacaoLike, IDbConnection conn);
        public abstract List<NivelDocumentalListItem> GetItemsEstrutural(DataSet currentDataSet, int pageNr, int itemsPerPage, IDbConnection conn);
        #endregion

        #region NivelGrupoArquivosList
        public abstract void CalculateOrderedItemsGA(IDbConnection conn);
        public abstract List<NivelDocumentalListItem> GetItemsGA(DataSet currentDataSet, int pageNr, int itemsPerPage, IDbConnection conn);
        #endregion

        #region " NivelDocumentalList "
        public class NivelDocumentalListItem {
            public long IDNivel;
            public string Designacao;
            public long IDTipoNivelRelacionado;
            public string Requisitado;
            public int GUIOrder;
            public string InicioAno;
            public string InicioMes;
            public string InicioDia;
            public bool InicioAtribuida = false;
            public string FimAno;
            public string FimMes;
            public string FimDia;
            public bool FimAtribuida = false;
            public string Agrupador = "";
        }

		public abstract void CalculateOrderedItems(ArrayList ordenacao, string filtroDesignacaoLike, string filtroCodigoParcialLike, string filtroIDLike, string filtroConteudoLike, long idMovimento, IDbConnection conn);
		public abstract ArrayList GetItems(DataSet currentDataSet, int pageNr, long exceptTipoNivel, int itemsPerPage, IDbConnection conn);
		public abstract void DeleteTemporaryResults(IDbConnection conn);
		#endregion

        #region " NivelDocumentalListNavigator "
        public abstract void CalculateOrderedItemsNav(DataSet currentDataSet, ArrayList ordenacao, long nivelID, string filtroDesignacaoLike, string filtroCodigoParcialLike, string filtroIDLike, string filtroConteudoLike, long? movimentoID, bool filtroExcluirRequisitados, bool showAllDocTopo, IDbConnection conn);
        public abstract ArrayList GetItemsNav(DataSet currentDataSet, int pageNr, int itemsPerPage, IDbConnection conn);
        #endregion

        #region " Controlo Localização "
        public struct MyNode 
		{
			public long IDNivel;
			public long IDNivelUpper;
			public string Designacao;
			public int Age;
			public long TipoNivelRelacionado;
			public string TipoNivel;
			public string AnoInicio;
			public string AnoFim;
		}

        public abstract void LoadNivelLocalizacao(DataSet currentDataSet, long NivelID, long TrusteeID, IDbConnection conn);
		public abstract ArrayList GetNivelLocalizacao(long NivelID, long TrusteeID, IDbConnection conn);
		public abstract ArrayList GetNivelChildren(long NivelID, long TrusteeID, long IDTipoNivelRelLimit, IDbConnection conn);
		#endregion

        #region " NivelDocumentalListNavigator "
        public abstract void LoadImagemIlustracao(long nID, DataSet currentDataSet, IDbConnection conn);
        #endregion

        public abstract bool HasSeries(long produtorID, IDbConnection conn);
    }
}
