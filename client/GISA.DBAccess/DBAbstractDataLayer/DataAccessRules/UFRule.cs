using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;

namespace DBAbstractDataLayer.DataAccessRules
{
	public abstract class UFRule: DALRule
	{
		private static UFRule current = null;
		public static void ClearCurrent() 
		{
			current = null;
		}
		public static UFRule Current
		{
			get 
			{
				if (Object.ReferenceEquals(null, current)) 
				{
					current = (UFRule) Create(typeof(UFRule));
				}
				return current;
			}
		}

        public struct UFsAssociadas
        {
            public long ID;
            public string Codigo;
            public string Designacao;
            public string DPInicioAno;
            public string DPInicioMes;
            public string DPInicioDia;
            public bool DPInicioAtribuida;
            public string DPFimAno;
            public string DPFimMes;
            public string DPFimDia;
            public bool DPFimAtribuida;
            public string Cota;
            public string CotaDocumento;
            public string TipoAcondicionamento;
            public string TipoMedida;
            public decimal Largura;
            public decimal Altura;
            public decimal Profundidade;
            public bool Eliminado;
            public string AutosAssociados;
        }

		#region " PanelUFConteudoEstrutura "
		public abstract void LoadUFConteudoEstruturaData (DataSet currentDataSet, long CurrentFRDBaseID, IDbConnection conn);
		#endregion

		#region " PanelUFControloDescricao "
		public abstract void LoadUFControloDescricaoData (DataSet currentDataSet, DataSet newDataSet, long CurrentFRDBaseID, IDbConnection conn);
        public abstract List<string> GetNiveisDocAssociados(long CurrentUFID, IDbConnection conn);
		#endregion

		#region " PanelUFIdentificacao2 "
		public abstract void LoadUFIdentificacao2Data (DataSet currentDataSet, long CurrentFRDBaseID, IDbConnection conn);
		public abstract void LoadTipoAcondicionamento (DataSet currentDataSet, IDbConnection conn);
		#endregion

		#region " UnidadeFisicaList "
        public abstract void CalculateOrderedItems(long TipoNivelRelacionadoUF, string FiltroDesignacaoLike, string FiltroCodigoLike, string FiltroCotaLike, string FiltroCodigoBarrasLike, string FiltroConteudoLike, string FiltroAndParentNivel, bool onlyNotAssociadas, bool excluirEliminados, ArrayList ordenacao, IDbConnection conn);
		public abstract ArrayList GetItems(DataSet currentDataSet, int pageNr, int itemsPerPage, IDbConnection conn);
		public abstract void DeleteTemporaryResults(IDbConnection conn);
		#endregion

		#region " MasterPanelUnidadesFisicas "
		public abstract decimal IsCodigoUFBeingUsed (long idNivel, decimal ano, IDbTransaction tran);
		public abstract bool isNivelRowDeleted (long nivelID, IDbTransaction tran);
		public abstract bool isRelacaoHierarquicaDeleted (long nivelID, long nivelIDUpper, IDbTransaction tran);
		public abstract void InsertOrUpdateUF (DataRow ufRow, IDbTransaction tran);
		public abstract void ReloadNivelUFCodigo (DataSet currentDataSet, long idNivel, decimal ano, IDbTransaction tran);
        public abstract void LoadUF(DataSet currentDataSet, long idNivel, IDbConnection conn);
		#endregion

		#region " PanelOIDimensoesSuporte "
		public abstract void LoadUFData (DataSet currentDataSet, long nivelID, IDbConnection conn);
        public abstract decimal LoadDescricaoFisicaAndGetSomatorioLargura(DataSet currentDataSet, long[] ufIDs, IDbConnection conn);
		#endregion

		public abstract ArrayList GetEntidadeDetentoraForNivel(long nivelID, IDbConnection conn);
        public abstract string LoadUFAutosAssociados(long nivelID, IDbConnection conn);

        public class UnidadeFisicaInfo
        {
            public long ID;
            public string Codigo;
            public string Designacao;
            public string Tipo;
            public decimal Altura;
            public decimal Largura;
            public decimal Profundidade;
            public string Medida;
            public string Cota;
            public string FimAno;
            public string FimMes;
            public string FimDia;
            public string InicioAno;
            public string InicioMes;
            public string InicioDia;
            public bool Eliminado;
        }
	}	
}
