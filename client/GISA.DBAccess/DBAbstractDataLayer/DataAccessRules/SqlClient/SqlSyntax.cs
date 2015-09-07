using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using GISA.Data.AbstractCommandBuilder;

namespace DBAbstractDataLayer.DataAccessRules.SqlClient
{
    
    
	public class SqlSyntax: Syntax
	{
        public static Dictionary<string, System.Text.StringBuilder> columnNames = new Dictionary<string,System.Text.StringBuilder>();

        public static SqlCommand CreateSelectCommandWithNoDeletedRowsParam(SqlTransaction tran)
        {
            SqlCommand command = new SqlCommand("", tran.Connection, tran);
            command.Parameters.AddWithValue("@isDeleted", 0);
            return command;
        }

        public static SqlCommand CreateSelectCommandWithNoDeletedRowsParam(SqlConnection conn)
        {
            SqlCommand command = new SqlCommand("", conn);
            command.Parameters.AddWithValue("@isDeleted", 0);
            return command;
        }

		public static string CreateSelectCommandText(DataTable table) 
		{
			return CreateSelectCommandText(table, "", Syntax.DataDeletionStatus.Exists);
		}

		public static string CreateSelectCommandText(DataTable table, string Suffix) 
		{
			return CreateSelectCommandText(table, Suffix, Syntax.DataDeletionStatus.Exists);
		}

		public static string CreateSelectCommandText(DataTable table, string Suffix, DBAbstractDataLayer.DataAccessRules.Syntax.DataDeletionStatus deletionStatus)
		{			
			string command = string.Empty;
			if (deletionStatus != Syntax.DataDeletionStatus.All)
			{
                var selectQuery = new System.Text.StringBuilder();
                var tmpApp = new System.Text.StringBuilder();
                selectQuery.AppendFormat("SELECT {2} FROM {0} {1} ", table.TableName, Suffix, getAllColumnNames(table));

                if (columnNames.ContainsKey(table.TableName))
                    tmpApp = columnNames[table.TableName];
                else
                    columnNames[table.TableName] = tmpApp;
                
                selectQuery.Append(tmpApp.ToString());

                selectQuery.AppendFormat(" {0} {1}.isDeleted=@isDeleted ", 
                    Suffix.Contains("WHERE ") ? "AND": "WHERE",
                    table.TableName);

                return selectQuery.ToString();
			}
            else
			{
                SQLCustomCommandBuilder cb = new SQLCustomCommandBuilder(table, DALRule.MetaModel.getColumnTypes(table.TableName, "TransactSQL"));
				return ((SqlCommand)(cb.GetSelectWithFilterCommand(Suffix))).CommandText;
			}
		}

		public static SqlCommand CreateUpdateCommand(DataTable table) 
		{
			System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter();
			SQLCustomCommandBuilder cb = new SQLCustomCommandBuilder(table, DALRule.MetaModel.getColumnTypes(table.TableName, "TransactSQL"));
			da.UpdateCommand = (SqlCommand) cb.UpdateCommand;
			return da.UpdateCommand;
		}

		public static SqlCommand CreateDeleteCommand(DataTable table) 
		{
			System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter();
			SQLCustomCommandBuilder cb = new SQLCustomCommandBuilder(table, DALRule.MetaModel.getColumnTypes(table.TableName, "TransactSQL"));
			da.DeleteCommand = (SqlCommand) cb.DeleteCommand;
			return da.DeleteCommand;
		}

		public static SqlCommand CreateInsertCommand(DataTable table) 
		{
			System.Data.SqlClient.SqlDataAdapter da = new System.Data.SqlClient.SqlDataAdapter();
			SQLCustomCommandBuilder cb = new SQLCustomCommandBuilder(table, DALRule.MetaModel.getColumnTypes(table.TableName, "TransactSQL"));
			da.InsertCommand = (SqlCommand) cb.InsertCommand;
			return da.InsertCommand;            
		}

		internal static string getAllColumnNames(DataTable table)
		{
            var cols = table.Columns.Cast<DataColumn>().Select(c => table.TableName + "." + c.ColumnName).ToArray();
            return string.Join(",", cols);
		}

	}
}
