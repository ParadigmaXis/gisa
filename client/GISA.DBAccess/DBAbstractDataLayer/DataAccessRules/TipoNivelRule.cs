using System;

namespace DBAbstractDataLayer.DataAccessRules
{
	public abstract class TipoNivelRule: DALRule
	{
		private static TipoNivelRule current = null;
		public static void ClearCurrent() 
		{
			current = null;
		}
		public static TipoNivelRule Current
		{
			get 
			{
				if (Object.ReferenceEquals(null, current)) 
				{
					current = (TipoNivelRule) Create(typeof(TipoNivelRule));
				}
				return current;
			}
		}

		public abstract System.Data.IDataReader GetPossibleSubItems(long nivelRowID, System.Data.IDbConnection conn);
		public abstract System.Data.DataRow[] SelectTipoNivel(System.Data.DataSet ds);
		public abstract System.Data.DataRow[] SelectTipoNivelRelacionado(System.Data.DataSet ds, long TipoNivelRelacionado);
	}
}
