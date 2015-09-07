using System;
using System.Data;
using System.Text;

namespace DBAbstractDataLayer.DataAccessRules
{
	/// <summary>
	/// Summary description for Syntax.
	/// </summary>
	public abstract class Syntax: DALRule
	{
		public enum DataDeletionStatus
		{
			Exists = 1,
			Deleted = 2,
			All = 3
		}

		// obter as colunas referentes a chaves primárias
		public static string BuildQuery (DataTable dt)
		{
			StringBuilder filter = new System.Text.StringBuilder();
			foreach (DataColumn col in dt.PrimaryKey)
			{				
			
				if (filter.Length != 0)
				{
					filter.Append(", ");
				}
				filter.Append(col.ColumnName);				
			}
			return filter.ToString();
		}
	}
}
