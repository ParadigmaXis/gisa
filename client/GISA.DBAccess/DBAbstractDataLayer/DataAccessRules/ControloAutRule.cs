using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace DBAbstractDataLayer.DataAccessRules
{	
	public abstract class ControloAutRule: DALRule
	{
		private static ControloAutRule current = null;
		public static void ClearCurrent() 
		{
			current = null;
		}
		public static ControloAutRule Current
		{
			get 
			{
				if (Object.ReferenceEquals(null, current)) 
				{
					current = (ControloAutRule) Create(typeof(ControloAutRule));
				}
				return current;
			}
		}

		#region " ControloAut "
		public abstract void LoadFormaAutorizada(long caRowID, DataSet ds, IDbConnection conn);
		public abstract int ExistsRel(long ID, long IDUpper, long IDTipoRel, bool isCarRow, IDbTransaction tran);
        public abstract void LoadControloAutFromNivel(DataSet currentDataSet, long IDNivel, IDbConnection conn);
        public abstract void LoadNivelFromControloAut(DataSet currentDataSet, long IDControloAut, IDbConnection conn);
		#endregion

		#region " PanelCAControlo "
		public abstract void LoadDataPanelCAControlo(DataSet currentDataSet, long CurrentControloAutID, IDbConnection conn);
        public abstract List<string> GetNiveisDocAssociados(long CurrentControloAutID, IDbConnection conn);
		#endregion		

		#region " PanelCADescricao "
		public abstract void LoadDataPanelCADescricao(DataSet currentDataSet, long CurrentControloAutID, IDbConnection conn);
		#endregion

		#region " PanelCAIdentificacao "
		public abstract void FillControloAutEntidadeProdutora(DataSet currentDataSet, long CurrentControloAutID, IDbConnection conn);
		public abstract void LoadThesaurus(DataSet currentDataSet, long CurrentControloAutID, IDbConnection conn);
		public abstract List<long> LoadTermos(DataSet currentDataSet, long CurrentControloAutID, IDbConnection conn);
		public abstract void SetUpIsReachable(DataSet currentDataSet, long CurrentControloAutID, IDbTransaction tran);
		public abstract void ComputeIsReachable(IDbTransaction tran);
		public abstract bool IsReachable(long ID, IDbTransaction tran);
		public abstract void TearDownIsReachable(IDbTransaction tran);
		public abstract bool isCarInDataBase(DataRow carRow, IDbTransaction tran);
		public abstract bool isControloAutInDataBase (long [] CaIDs, IDbTransaction tran);
//		public abstract string getCodigoRef (DataSet currentDataSet, long CurrentControloAutID, IDbConnection conn);
		public abstract ArrayList GetTermos (long CurrentControloAutID , IDbConnection conn);
		#endregion

		#region " PanelCARelacoes "
		public abstract void LoadControloAutRel(DataSet currentDataSet, long IDControloAut, long IDControloAutAlias, long IDTipoRel, IDbConnection conn);
		public abstract void LoadRelacaoHierarquica(DataSet currentDataSet, long ID, long IDUpper, long IDTipoNivelRelacionado, IDbConnection conn);
		#endregion

		#region " FRDCA "
		public abstract void LoadControloAut(DataSet currentDataSet, long controloAutID, IDbConnection conn);
		public abstract void LoadControloAutData(DataSet currentDataSet, long controloAutID, IDbConnection conn);
		#endregion

		#region " MasterPanelControloAut "
		public abstract void LoadDicionarioAndControloAutDicionario (DataSet currentDataSet, long caRowID, IDbConnection conn);
		public abstract bool isCADeleted (string IDControloAut, IDbTransaction tran);
		public abstract bool isCADDeleted (long IDControloAut, long IDDicionario, long IDTipoControloAutForma, IDbTransaction tran);
		#endregion

		#region " FormCreateControloAut "
		public abstract bool ExistsControloAutDicionario(long IDDicionario, long IDTipoControloAutForma, long IDTipoNoticiaAut, IDbConnection conn);
		#endregion

		#region " ControloAutList "
		public abstract void CalculateOrderedItems(string autorizado, string FiltroTermoLike, long[] FiltroNoticiaAut, IDbConnection conn);
		public abstract int CountPages(int itemsPerPage, IDbConnection conn);
		public abstract int GetPageForID(long [] cad, int pageLimit, IDbConnection conn);
		public abstract ArrayList GetItems (DataSet currentDataSet, int pageNr, int itemsPerPage, long[] FiltroNoticiaAut, IDbConnection conn);
		public abstract void DeleteTemporaryResults(IDbConnection conn);
		#endregion

		#region " FormNivelEstrutural "
		public abstract void LoadFormaAutorizada(DataSet currentDataSet, long caRowID, string TipoControloAutForma, IDbConnection conn);
		#endregion

		#region ListTermos
		public abstract ArrayList LoadTermosData(DataSet currentDataSet, bool filterAutorizados, long excludeAutorizadosTipoNoticiaAut, ArrayList filterOthers, long caID, IDbConnection conn);
/*		public abstract ArrayList LoadAllTermosData(DataSet currentDataSet, IDbConnection conn);
		public abstract ArrayList LoadAllTermosNonAutorizadosData(DataSet currentDataSet, IDbConnection conn);
		public abstract ArrayList LoadSomeTermosData(DataSet currentDataSet, ArrayList exceptions, IDbConnection conn);
		*/
		#endregion
	}
}
