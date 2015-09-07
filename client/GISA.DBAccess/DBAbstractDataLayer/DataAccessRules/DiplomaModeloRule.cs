using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace DBAbstractDataLayer.DataAccessRules
{
	public abstract class DiplomaModeloRule: DALRule
	{
		private static DiplomaModeloRule current = null;
		public static void ClearCurrent() 
		{
			current = null;
		}
		public static DiplomaModeloRule Current
		{
			get 
			{
				if (Object.ReferenceEquals(null, current)) 
				{
					current = (DiplomaModeloRule) Create(typeof(DiplomaModeloRule));
				}
				return current;
			}
		}

		#region " FormPickDiplomaModelo "
		public abstract bool ExistsControloAutDicionario(long IDDicionario, long IDTipoControloAutForma, long IDTipoNoticiaAut, IDbTransaction tran);
		public abstract ArrayList GetDicionario(string catCode, string Termo, IDbTransaction tran);
		public abstract bool isTermoUsedByOthers(long CurrentIdCA, string catCode, string termo, bool actionDelete, long tnaID, IDbTransaction tran);
		public abstract bool isTermoUsedByOthers(long CurrentIdCA, string catCode, string termo, bool actionDelete, IDbTransaction tran);
		public abstract ICollection LoadNivelControloAut(DataSet currentDataSet, ArrayList IDs, IDbConnection conn);
		public abstract IDataReader GetNoticiasAutoridadeRelaccionadas (DataSet currentDataSet, ArrayList IDs, IDbConnection conn);
        public abstract List<CAAssociado> GetCANiveisAssociados(List<long> IDs, IDbConnection conn);
        public abstract List<long> GetIDLowers(long IDNivel, IDbConnection conn);
		#endregion

		protected string longCollectionToCommaDelimitedString(ICollection collection)
		{
			System.Text.StringBuilder strBuilder = new System.Text.StringBuilder();
			foreach (long val in collection) {
				if (strBuilder .Length == 0) {
					strBuilder .Append(val);
				} else {
					strBuilder.Append(", ");
					strBuilder.Append(val);
				}
			}
			return strBuilder.ToString();
		}
	}

    public class CAAssociado
    {
        public long IDNivel;
        public string TipoNivelDesignado;
        public string NivelDesignacao;
        public bool AllowDelete;
    }
}
