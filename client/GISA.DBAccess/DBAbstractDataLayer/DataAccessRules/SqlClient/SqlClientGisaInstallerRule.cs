using System;
using System.Data;
using System.Data.SqlClient;

namespace DBAbstractDataLayer.DataAccessRules.SqlClient
{
	public class SqlClientGisaInstallerRule: GisaInstallerRule
	{
		#region " Database Attaching/Detaching "
		public override int CheckInstalledDB(string dbName, System.Data.IDbConnection conn)
		{
			SqlCommand command = new SqlCommand(string.Format("SELECT COUNT(*) FROM master.dbo.sysdatabases WHERE name LIKE '{0}'", dbName), (SqlConnection) conn);
			return System.Convert.ToInt32(command.ExecuteScalar());
		}

		public override void ExecuteAttachDatabase(string dbName, string filename, IDbConnection conn)
		{
			SqlCommand command = new SqlCommand("sp_attach_db", (SqlConnection) conn);
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add("@dbname", SqlDbType.NVarChar);
			command.Parameters[0].Value = dbName;
			command.Parameters.Add("@filename1", SqlDbType.NVarChar);
			command.Parameters[1].Value = filename;

			for (int i = 2; i<17; i++) 
			{
				command.Parameters.Add("@filename" + i, SqlDbType.NVarChar);
				command.Parameters[i].Value=System.DBNull.Value.ToString();
			}
			command.ExecuteNonQuery();			
		}

		public override void ExecuteDetachDatabase(string dbName, IDbConnection conn)
		{
			// matar todos os processos no sqlServer que utilizem a BD GISA
			string cmdText= string.Format(
				"DECLARE @sql VARCHAR(500) " + 
				"SET @sql = '' " + 
				"SELECT @sql = @sql + ' KILL ' + CAST(procs.spid AS VARCHAR(10)) + ' ' " + 
				"FROM master.dbo.sysprocesses procs " + 
				"WHERE DB_NAME(procs.dbid) like 'GISA' " + 
				"AND procs.spid > 50 AND procs.spid <> @@SPID " + 
				"EXEC(@sql)", dbName);

			SqlCommand command = new SqlCommand(cmdText, (SqlConnection) conn);
			command.ExecuteNonQuery();

			// detach da BD
			command.CommandText = "sp_detach_db";
			command.CommandType = CommandType.StoredProcedure;
			command.Parameters.Add("@dbname", SqlDbType.NVarChar);
			command.Parameters[0].Value = dbName;
			command.Parameters.Add("@skipchecks", SqlDbType.NVarChar);
			command.Parameters[1].Value = System.DBNull.Value.ToString();
			
			command.ExecuteNonQuery();        
		}
		#endregion

        #region " Database Backup "
        public override void ExecuteBackupDatabase(string filename, IDbConnection conn)
        {
            try
            {
                SqlCommand command = new SqlCommand(string.Empty, (SqlConnection)conn);
                command.CommandText = string.Format(@"
BACKUP DATABASE GISA 
TO DISK = '{0}'
WITH 
    FORMAT"
                    , filename);
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
	}
}
