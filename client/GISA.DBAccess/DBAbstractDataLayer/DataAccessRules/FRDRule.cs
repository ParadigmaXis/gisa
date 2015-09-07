using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DBAbstractDataLayer.DataAccessRules
{
	public abstract class FRDRule: DALRule
	{
		private static FRDRule current = null;
		public static void ClearCurrent() 
		{
			current = null;
		}
		public static FRDRule Current
		{
			get 
			{
				if (Object.ReferenceEquals(null, current)) 
				{
					current = (FRDRule) Create(typeof(FRDRule));
				}
				return current;
			}
		}

		#region " PanelAmbitoConteudo "

		public abstract void LoadConteudoEEstrutura(System.Data.DataSet currentDataSet, long CurrentFRDBaseID, System.Data.IDbConnection conn);
        public abstract void LoadDadosLicencasDeObras(System.Data.DataSet currentDataSet, long CurrentFRDBaseID, System.Data.IDbConnection conn);
        public abstract bool possuiDadosLicencaDeObras(System.Data.DataSet currentDataSet, long CurrentFRDBaseID, System.Data.IDbConnection conn);
        public abstract bool isDocumentoProcessoObra(long CurrentFRDBaseID, System.Data.IDbConnection conn);

		#endregion

		#region " PanelCondicoesAcesso "
		public abstract void LoadCondicoesAcessoData (DataSet currentDataSet, long CurrentFRDBaseID, System.Data.IDbConnection conn);
		#endregion

		#region " PanelContexto "
		public abstract void LoadContextoData(DataSet currentDataSet, long CurrentFRDBaseID, IDbConnection conn);
		public abstract ArrayList LoadProdutores(DataSet currentDataSet, long CurrentFRDBaseID, long NivelRowID, ArrayList caList, IDbConnection conn);
		#endregion

		#region " PanelDocumentacaoAssociada "
		public abstract void LoadDocumentacaoAssociadaData (DataSet currentDataSet, long CurrentFRDBaseID, IDbConnection conn);
		#endregion

		#region " PanelIdentificacao "
		public abstract void LoadIdentificacaoData (DataSet currentDataSet, long CurrentFRDBaseID, IDbConnection conn);
        public abstract string UFsWithSameCota(long CurrentFRDBaseID, string cota, IDbConnection conn);
		#endregion

		#region " PanelIncorporacoes "
		public abstract void LoadIncorporacoesData (DataSet currentDataSet, long CurrentFRDBaseID, IDbConnection conn);
		#endregion

		#region " PanelIndexacao "
		public abstract void LoadIndexacaoData (DataSet currentDataSet, long CurrentFRDBaseID, IDbConnection conn);
		#endregion

		#region " PanelIndiceDocumento "
        public abstract void LoadIndiceDocumentoData(DataSet currentDataSet, long CurrentFRDBaseID, IDbConnection conn);
        public abstract List<string> FedoraIDs(IDbConnection conn);
		#endregion

		#region " PanelNotas "
		public abstract void LoadNotasData (DataSet currentDataSet, long CurrentFRDBaseID, IDbConnection conn);
		#endregion

		#region " PanelOIControloDescricao "
		public abstract void LoadOIControloDescricaoData (DataSet currentDataSet, DataSet newDataSet, long CurrentFRDBaseID, IDbConnection conn);
		#endregion

		#region " PanelOIDimensoesSuporte "
        public abstract List<UFRule.UFsAssociadas> LoadOIDimensoesSuporteData(DataSet currentDataSet, long CurrentFRDBaseID, IDbConnection conn);
        public abstract void LoadUFRelacionada(DataSet currentDataSet, long CurrentFRDBaseID, long IDNivelUF, IDbConnection conn);
		public abstract int CountUFDimensoesAcumuladas(long IDTipoAcondicionamento, IDbTransaction tran);
		#endregion

		#region " PanelUFUnidadesDescricao "
		public abstract ArrayList LoadUFUnidadesDescricaoData(DataSet currentDataSet, long CurrentNivelID, IDbConnection conn);
		public abstract Hashtable LoadUFUnidadesDescricaoDetalhe(DataSet currentDataSet, long CurrentNivelID, long userID, IDbConnection conn);
		public abstract ArrayList FilterUFUnidadesDescricao(string Des, long TNRid, long CurrentNivelID, IDbConnection conn);
		public abstract void LoadFRD(DataSet currentDataSet, long CurrentNivelID, IDbConnection conn);
		#endregion

		#region " PanelOrganizacaoOrdenacao "
		public abstract void LoadOrganizacaoOrdenacaoData (DataSet currentDataSet, long CurrentFRDBaseID, IDbConnection conn);
		#endregion		

		#region " FRDOIRecolha "
		public abstract void ReloadPubNivelActualData (DataSet currentDataSet, long NivelEstrututalDocumentalID, IDbConnection conn);
		public abstract void LoadFRDOIRecolhaData (DataSet currentDataSet, long NivelEstrututalDocumentalID, string TipoFRDBase, IDbConnection conn);
		#endregion

		#region " FRDUnidadeFisica "
		public abstract void LoadFRDUnidadeFisicaData (DataSet currentDataSet, long NivelUnidadeFisicaID, string TipoFRDBase, IDbConnection conn);
		#endregion

		#region " PanelAvaliacaoDocumentosUnidadesFisicas "
        public abstract void LoadCurrentFRDAvaliacao(DataSet currentDataSet, long CurrentFRDBaseID, IDbConnection conn);
        public abstract void LoadPanelAvaliacaoDocumentosUnidadesFisicasData(DataSet currentDataSet, long CurrentFRDBaseID, long CurrentFRDBaseNivelRowID, long CurrentIDTipoFRDBase, long FRDUFIDTipoFRDBase, long grpAcPubID, IDbConnection conn);
		#endregion

		#region " PanelAVCondicoesAcesso "
		public abstract void LoadPanelAVCondicoesAcessoData(DataSet currentDataSet, long CurrentFRDBaseID, IDbConnection conn);
		#endregion

		#region " PanelCARelacoes "
		public abstract void LoadRetrieveSelectionData(DataSet currentDataSet, long cadRowIDControloAut, IDbConnection conn);
		#endregion

		#region " PanelAvaliacaoSeleccaoEliminacao "
		public abstract void LoadPanelAvaliacaoSeleccaoEliminacaoData(DataSet currentDataSet, long CurrentFRDBaseID, long CurrentNivelID, long grpAcPubID, IDbConnection conn);
		public abstract void ExecuteAvaliaDocumentosTabela (long frdID, long modeloAvaliacaoID, bool avaliacaoTabela, bool preservar, short prazoConservacao, IDbTransaction tran);
		#endregion

		#region " MasterPanelSeries "
		public abstract void LoadNivelAvaliacaoData(DataSet currentDataSet, long nivelID, IDbTransaction tran);
		public abstract void LoadSFRDAvaliacaoData(DataSet currentDataSet, long nivelID, IDbConnection conn);
		#endregion
	}
}