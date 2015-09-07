using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Data;
using System.Data.SqlClient;

namespace DBAbstractDataLayer.DataAccessRules
{
	public abstract class GisaDataSetHelperRule: DALRule
	{
		private static GisaDataSetHelperRule current = null;
		public static void ClearCurrent() 
		{
			current = null;
		}
		public static GisaDataSetHelperRule Current
		{
			get 
			{
				if (Object.ReferenceEquals(null, current)) 
				{
					current = (GisaDataSetHelperRule) Create(typeof(GisaDataSetHelperRule));
				}
				return current;
			}
		}

		public abstract DataRow[] selectIndexFRDCA (DataSet ds, long FRDBaseID);
		public abstract DataRow[] selectControloAutDicionario (DataSet ds, long ControloAutID);
		public abstract void LoadStaticDataTables(DataSet ds, IDbConnection conn);
		public abstract int GetRowCount(string TableName, IDbConnection Conn);
		public abstract int GetRowCount(string TableName, string Suffix, IDbConnection Conn);
		#region "Foreign key resolution"
		public abstract string GetJoinExpression(DataRelation DataRelation, DataRow ChildDataRow);
		public abstract void FixRow(DataSet ds, DataRow dr, IDbConnection conn);
		#endregion

        public static void ImportIDs(long[] IDs, IDbTransaction tran)
        {
            SqlCommand command = new SqlCommand(string.Empty, (SqlConnection)tran.Connection, (SqlTransaction)tran);
            ImportIDs(IDs, command);
        }

        public static void ImportIDs(long[] IDs, IDbConnection conn)
        {
            SqlCommand command = new SqlCommand(string.Empty, (SqlConnection)conn);
            ImportIDs(IDs, command);
        }

        private static void ImportIDs(long[] IDs, SqlCommand command)
        {
            long start = DateTime.Now.Ticks;

            DataTable t = new DataTable();
            DataColumn c = new DataColumn("ID", typeof(long));
            t.Columns.Add(c);
            DataColumn c2 = new DataColumn("seq_nr", typeof(long));
            c2.AutoIncrementSeed = 1;
            c2.AutoIncrementStep = 1;
            c2.AutoIncrement = true;
            t.Columns.Add(c2);

            foreach (long id in IDs)
            {
                DataRow dr = t.NewRow();
                dr[0] = id;
                t.Rows.Add(dr);
            }

            command.CommandText = "IF OBJECT_ID(N'tempdb..#temp', N'U') IS NOT NULL " +
                                    "DROP TABLE #temp " +
                                    "CREATE TABLE #temp(ID BIGINT, seq_nr BIGINT); CREATE INDEX ix ON #temp (ID);";
            command.ExecuteNonQuery();

            SqlBulkCopy copy = new SqlBulkCopy(command.Connection, SqlBulkCopyOptions.UseInternalTransaction, null);
            copy.DestinationTableName = "#temp";
            copy.WriteToServer(t);

            t.Dispose();
            Debug.WriteLine("<<ImportIDs>> " + new TimeSpan(DateTime.Now.Ticks - start).ToString());
        }

        public static void ImportDesignacoes(string[] designacoes, IDbConnection conn)
        {
            SqlCommand command = new SqlCommand(string.Empty, (SqlConnection)conn);
            ImportDesignacoes(designacoes, command);
        }

        //public static void ImportDesignacoes(string[] designacoes, IDbTransaction tran)
        //{
        //    SqlCommand command = new SqlCommand(string.Empty, (SqlConnection)tran.Connection, (SqlTransaction)tran);
        //    ImportDesignacoes(designacoes, command);
        //}

        public static void ImportDesignacoes(string[] designacoes, SqlCommand command)
        {
            long start = DateTime.Now.Ticks;

            DataTable t = new DataTable();
            DataColumn c = new DataColumn("Designacao", typeof(string));
            t.Columns.Add(c);

            foreach (string designacao in designacoes)
            {
                DataRow dr = t.NewRow();
                dr[0] = designacao;
                t.Rows.Add(dr);
            }

            command.CommandText = "IF OBJECT_ID(N'tempdb..#temp', N'U') IS NOT NULL " +
                                    "DROP TABLE #temp " + 
                                    "CREATE TABLE #temp(Designacao NVARCHAR(768));";
            command.ExecuteNonQuery();

            SqlBulkCopy copy = new SqlBulkCopy(command.Connection, SqlBulkCopyOptions.UseInternalTransaction, null);
            copy.DestinationTableName = "#temp";
            copy.WriteToServer(t);

            //command.CommandText = "INSERT INTO #temp SELECT Designacoes.Designacao FROM @SearchDesignacoes as Designacoes;";
            //SqlParameter paramIds = command.Parameters.AddWithValue("@SearchDesignacoes", t);
            //paramIds.SqlDbType = SqlDbType.Structured;
            //paramIds.TypeName = "SearchDesignacoes";
            //command.ExecuteNonQuery();

            t.Dispose();
            Debug.WriteLine("<<ImportDesignacaoess>> " + new TimeSpan(DateTime.Now.Ticks - start).ToString());
        }
	}
}
