using System;
using System.Data;
using System.Data.SqlClient;

namespace DBAbstractDataLayer.DataAccessRules.SqlClient
{
	public class SqlClientTipoNivelRule: TipoNivelRule
	{		
		public override IDataReader GetPossibleSubItems(long nivelRowID, IDbConnection conn) 
		{
			SqlCommand command = new SqlCommand("sp_getPossibleSubTypesOf", (SqlConnection) conn);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add("@nivelID", SqlDbType.BigInt);
			command.Parameters[0].Value = nivelRowID;

			return command.ExecuteReader();
		}

		public override DataRow[] SelectTipoNivel(DataSet ds)
		{			
			return ds.Tables["TipoNivel"].Select("GUIOrder=1");
		}

		public override DataRow[] SelectTipoNivelRelacionado(DataSet ds, long TipoNivelRelacionado)
		{
			return ds.Tables["TipoNivelRelacionado"].Select(string.Format("ID={0}", TipoNivelRelacionado));
		}


	}
}
