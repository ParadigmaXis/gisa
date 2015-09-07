using System;
using System.Data;

namespace DBAbstractDataLayer.DataAccessRules
{
	public abstract class PersistencyHelperRule: DALRule
	{
		private static PersistencyHelperRule current = null;
		public static void ClearCurrent() 
		{
			current = null;
		}
		public static PersistencyHelperRule Current
		{
			get 
			{
				if (Object.ReferenceEquals(null, current)) 
				{
					current = (PersistencyHelperRule) Create(typeof(PersistencyHelperRule));
				}
				return current;
			}
		}

		public abstract void saveRows(DataTable dt, DataRow[] dr, IDbTransaction tran);
        public abstract byte[] CleanDatasetDeletedData(DataSet ds, DataTable dt, byte[] ts, IDbConnection conn);
	}
}
