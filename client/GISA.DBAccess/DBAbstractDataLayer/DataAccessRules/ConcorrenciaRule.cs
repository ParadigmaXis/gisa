using System;
using System.Data;
using System.Collections;
using System.Text;

namespace DBAbstractDataLayer.DataAccessRules
{	
	public abstract class ConcorrenciaRule: DALRule
	{
		private static ConcorrenciaRule current = null;
		public static void ClearCurrent() 
		{
			current = null;
		}
		public static ConcorrenciaRule Current
		{
			get 
			{
				if (Object.ReferenceEquals(null, current)) 
				{
					current = (ConcorrenciaRule) Create(typeof(ConcorrenciaRule));
				}
				return current;
			}
		}

		// estrutura utilizada para que o metodo getChildRowsFromDB consiga devolver nao so as linhas filhas como também quais as colunas que as relacionam ao pai em causa
		public struct ChildRelationRows
		{
			public ChildRelationRows(ArrayList tables, ArrayList relationColumns)
			{
				this.tables = tables;
				this.relationColumns = relationColumns;
			}
			public ArrayList tables;
			public ArrayList relationColumns;
		}

		public abstract void fillRowsToOtherDataset(DataTable table, ArrayList rows, DataSet data, IDbTransaction tran);
		public abstract string buildFilter(DataTable dt, DataRow row);
		public abstract string buildFilterPK(DataTable dt, DataRow row);
		public abstract string buildFilter(DataTable dt, DataRow row, bool fillDataSetPorpose);
		public abstract string buildFilterPK(DataTable dt, DataRow row, bool fillDataSetPorpose);
		public abstract ChildRelationRows getChildRowsFromDB(DataSet data, DataTable table, ArrayList parentRows, IDbTransaction tran);
		public abstract void FillTableInGetLinhasConcorrentes(DataSet ds, DataTable table, string query, DBAbstractDataLayer.DataAccessRules.Syntax.DataDeletionStatus status, IDbTransaction tran);
		public abstract string getQueryForRows(DataRow[] rows, params DataRowState[] rowStates);
	}
}
