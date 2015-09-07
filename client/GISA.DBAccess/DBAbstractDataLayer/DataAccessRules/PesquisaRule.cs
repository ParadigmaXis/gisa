using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DBAbstractDataLayer.DataAccessRules
{
	public abstract class PesquisaRule: DALRule
	{
		private static PesquisaRule current = null;
		public static void ClearCurrent() 
		{
			current = null;
		}
		public static PesquisaRule Current
		{
			get 
			{
				if (Object.ReferenceEquals(null, current)) 
				{
					current = (PesquisaRule) Create(typeof(PesquisaRule));
				}
				return current;
			}
		}

		internal bool codigosCalculados = false;
		internal List<long> CacheSearchResult = new List<long>();
		public StringBuilder orderByQuery;
		public StringBuilder innerQuery;
		
        internal ArrayList lastOrdenacao = new ArrayList();
		internal bool MesmaOrdenacao (ArrayList novaOrdenacao)
		{
			if (lastOrdenacao.Count != novaOrdenacao.Count)
				return false;

			for (int i = 0; i < lastOrdenacao.Count; i++)
			{
				if (!novaOrdenacao[i].ToString().Equals(lastOrdenacao[i].ToString()))
					return false;
			}

			return true;
		}

        


		#region " Listas Paginadas "
		public abstract string CollateOptionCIAI {get;}	
		public abstract string CollateOptionCIAIEscape {get;}	
		#endregion

		public abstract string sanitizeSearchTerm(string str, bool addWildcardsToExtremities);
		public abstract string buildLikeStatement(string str1, string str2);
		public string sanitizeSearchTerm(string str)
		{
			return sanitizeSearchTerm(str, false);
		}
        public abstract string sanitizeSearchTerm_WithoutWidcards(string str);


		#region " SlavePanelPesquisa "
        public abstract void LoadSelectedData (DataSet currentDataSet, long IDNivel, long IDTipoFRDBase, IDbConnection conn);
		public abstract void LoadImagemVolume (DataSet currentDataSet, long frdID, IDbConnection conn);


        public abstract List<UFRule.UFsAssociadas> LoadDetalhesUF(DataSet currentDataSet, string id, IDbConnection conn);
		public abstract void LoadFRDBaseData (DataSet currentDataSet, string id, IDbConnection conn);
		public abstract void LoadRHParentsSelectedResult (DataSet currentDataSet, long NivelID, IDbConnection conn);
        
        public struct TermosIndexacao {
            public string Termo;
            public string Outras_Formas;
            public int IndexFRDCA_Selector;
            public long IDTipoNoticiaAut;
        }
        public abstract List<TermosIndexacao> GetTermosIndexacao(long NivelID, IDbConnection conn);
        public abstract List<string> LoadDocumentoCotas(string IDFRDbase, IDbConnection conn);

        public abstract long CountSubDocumentos(long IDNivel, IDbConnection conn);
        public abstract List<NivelRule.NivelDocumentalListItem> GetSubDocumentos(long IDNivel, IDbConnection conn);
		#endregion

		#region " PesquisaList "
        public abstract void CalculateOrderedItems(ArrayList ordenacao, List<string> IDs, Int64? IDNivelEstrutura, long userID, bool SoDocExpirados, bool _newSearch, out long nrResults, IDbConnection conn);
		public abstract int CountPages(int itemsPerPage, IDbConnection conn);
		public abstract int GetPageForID(long IDFRDBase, int pageLimit, IDbConnection conn);
		public abstract ArrayList GetItems(DataSet currentDataSet, int pageNr, int itemsPerPage, IDbConnection conn);
		public abstract void DeleteTemporaryResults(IDbConnection conn);
        public abstract ArrayList GetPesquisaResultsDetails(string idsTableName, bool calculaCodigos, IDbConnection conn);
		#endregion

		#region " SlavePanelPesquisaUF "
		public struct DocAssociado 
		{
			public long IDNivel;
			public int GUIOrder;
			public string Codigo;
			public string RelDesignacao;
			public string NivelDesignacao;
            public bool Requisitado;
		}
        public abstract List<DocAssociado> LoadEstruturaDocsData(DataSet currentDataSet, string id, IDbConnection conn);
		public abstract void LoadFRDBaseUFData (DataSet currentDataSet, long FRDBaseRowIDNivel, IDbConnection conn);
		public abstract bool isNivelDeleted (DataSet currentDataSet, long nivelID, IDbConnection conn);
		#endregion

		#region " PesquisaListUF "
        public abstract void CalculateOrderedItemsUF(ArrayList ordenacao, List<string> ids, string operador, int anoEdicaoInicio, int mesEdicaoInicio, int diaEdicaoInicio, int anoEdicaoFim, int mesEdicaoFim, int diaEdicaoFim, long IDNivel, int assoc, bool _newSearch, out long nrResults, IDbConnection conn);
		public abstract ArrayList GetItemsUF(DataSet currentDataSet, int pageNr, int itemsPerPage, long TipoNivelUF, IDbConnection conn);
		public abstract void DeleteTemporaryResultsUF(IDbConnection conn);
		#endregion

        public class NivelDocumental
        {
            public long IDNivel;
            public long IDFRDBase;
            public string CodigoCompleto;
            public long IDTipoNivelRelacionado;
            public string TipoNivelRelacionado;
            public string Designacao;
            public string InicioAno;
            public string InicioMes;
            public string InicioDia;
            public bool InicioAtribuida;
            public string FimAno;
            public string FimMes;
            public string FimDia;
            public bool FimAtribuida;
            public bool Eliminado;
            public bool Requisitado;
            public string Agrupador;

            // Campo estruturado Licença de obra
            public string RequerentesIniciais;
            public string RequerentesAverbamentos;
            public string LocObraNumPoliciaAct;
            public string LocObraDesignacaoAct;
            public string LocObraNumPoliciaAnt;
            public string LocObraDesignacaoAnt;
            public string TecnicoObra;
            public string AtestHabit;
            public string DataLicConstr;
            public bool PropriedadeHorizontal;
            public string TipoObra;
        }
    }
}