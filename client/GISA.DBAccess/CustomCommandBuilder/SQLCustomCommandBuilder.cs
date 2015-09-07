using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace GISA.Data.AbstractCommandBuilder
{	
	public sealed class SQLCustomCommandBuilder: CustomCommandBuilder
	{
		DataTable dataTable;
		SqlConnection connection;
		SqlTransaction transaction;
		ArrayList columnSQLTypes;

		public SQLCustomCommandBuilder( DataTable dataTable, ArrayList columnSQLTypes)
		{
			if (dataTable.Columns.Count != columnSQLTypes.Count)
			{
				throw new ArgumentException("Number of SqlDbTypes does not match number of columns.");
			}
			this.dataTable = dataTable;
			this.columnSQLTypes = columnSQLTypes;
		}

		public SQLCustomCommandBuilder( DataTable dataTable, SqlConnection connection, ArrayList columnSQLTypes)
		{
			if (dataTable.Columns.Count != columnSQLTypes.Count)
			{
				throw new ArgumentException("Number of SqlDbTypes does not match number of columns.");
			}
			this.dataTable = dataTable;
			this.connection = connection;
			this.columnSQLTypes = columnSQLTypes;
		}

		public SQLCustomCommandBuilder( DataTable dataTable, SqlConnection connection, ArrayList columnSQLTypes, SqlTransaction transaction)
		{
			if (dataTable.Columns.Count != columnSQLTypes.Count)
			{
				throw new ArgumentException("Number of SqlDbTypes does not match number of columns.");
			}
			this.dataTable = dataTable;
			this.connection = connection;
			this.columnSQLTypes = columnSQLTypes;
			this.transaction = transaction;
		}

		public sealed override IDbCommand SelectAllCommand
		{
			get 
			{
				string commandText = "SELECT " + ColumnsString + " FROM " + TableName;
				return GetTextCommand( commandText );
			}
		}

		public sealed override IDbCommand GetSelectWithFilterCommand( string filter )
		{
			string commandText = "SELECT " + ColumnsString + 
				" FROM " + TableName +
				"  " + filter; //" WHERE " + filter;
			return GetTextCommand( commandText );
		}

		public sealed override IDbCommand GetSelectWithOrderCommand( string order )
		{
			string commandText = "SELECT " + ColumnsString + 
				" FROM " + TableName +
				" ORDER BY " + order;
			return GetTextCommand( commandText );
		}

		public sealed override IDbCommand DeleteCommand
		{
			get
			{
				SqlCommand command = (SqlCommand) GetTextCommand( "" );
				StringBuilder whereString = new StringBuilder();
				foreach( DataColumn column in dataTable.PrimaryKey )
				{
					if( whereString.Length > 0 )
					{
						whereString.Append( " AND " );
					}
					whereString.Append( column.ColumnName )
						//.Append( " = @p" + column.Ordinal ); 						
						.Append(" = @").Append( column.ColumnName );
					command.Parameters.Add(CreateParam(column, (SqlDbType)columnSQLTypes[column.Ordinal], DataRowVersion.Original));
				}
				string commandText = "DELETE FROM " + TableName
					+ " WHERE " + whereString.ToString();
				command.CommandText = commandText;
				return command;
			}
		}

		/// <summary>
		/// Creates Insert command with support for Autoincrement (Identity) tables
		/// </summary>
		public sealed override IDbCommand InsertCommand
		{
			get
			{
                string outputClause = "OUTPUT ";
				SqlCommand command = (SqlCommand) GetTextCommand( "" );
				StringBuilder intoString = new StringBuilder();
				StringBuilder valuesString = new StringBuilder();
				ArrayList autoincrementColumns = AutoIncrementKeyColumns;
                var outputClauseColumns = new List<string>();
				SqlDbType columnType;
				foreach( DataColumn column in dataTable.Columns )
				{
					columnType = (SqlDbType)columnSQLTypes[column.Ordinal];
					// check for timestamp columns
					if (columnType == SqlDbType.Timestamp && column.ReadOnly)
						outputClauseColumns.Add("INSERTED." + column.ColumnName);

					// Not an autoincrement and not a readonly column (ie, timestamps)
					if( !autoincrementColumns.Contains(column)  && !column.ReadOnly)
					{
						if( intoString.Length > 0)
						{
							intoString.Append( ", " );
							valuesString.Append( ", " );
						} 
						intoString.Append( column.ColumnName );
						valuesString.Append("@").Append( column.ColumnName );
						command.Parameters.Add(CreateParam(column, columnType));
					}
				}

				// actualizar a datarow em memória com o valor atribuido pelo servidor para a chave
				if( autoincrementColumns.Count > 0 ) 
                    outputClauseColumns.Add("INSERTED." + (DataColumn)autoincrementColumns[0]);

                string cmd = "INSERT INTO " + TableName + "("
                    + intoString.ToString() + ") {0} VALUES (" + valuesString.ToString() + "); ";

                command.UpdatedRowSource = UpdateRowSource.Both;
                command.CommandText = string.Format(cmd, outputClauseColumns.Count == 0 ? "" : outputClause +  string.Join(",", outputClauseColumns.ToArray()));
				return command;
			}
		}

		/// <summary>
		/// Creates Update command with optimistic concurency support
		/// </summary>
		public sealed override IDbCommand UpdateCommand
		{
			get
			{
				SqlCommand command = (SqlCommand) GetTextCommand( "" );
				StringBuilder setString = new StringBuilder();
				StringBuilder whereString = new StringBuilder();
				DataColumn[] primaryKeyColumns = dataTable.PrimaryKey;
				ArrayList timestampParameters = new ArrayList();
				bool hasTimestampColumn = false;
				SqlDbType columnType;

				foreach( DataColumn column in dataTable.Columns )
				{
					columnType = (SqlDbType)columnSQLTypes[column.Ordinal];

					if (columnType == SqlDbType.Timestamp && column.ReadOnly)
					{ //timestamps
						hasTimestampColumn = true;
					}

					if( System.Array.IndexOf( primaryKeyColumns, column) != -1 )
					{
						// A primary key
						if( whereString.Length > 0 )
						{
							whereString.Append( " AND " );
						}
						whereString.Append( column.ColumnName )
							//.Append( "= @p" + column.Ordinal );
							.Append("=@ts_").Append(column.ColumnName);
						IDbDataParameter param = CreateParam(column, columnType);
						param.ParameterName= "@ts_"+column.ColumnName;
						timestampParameters.Add(param);
					} 
					else if(!column.ReadOnly) // do not put values on readonly columns (ie, timestamps)
					{
						if( setString.Length > 0 )
						{
							setString.Append( ", " );
						}
						setString.Append( column.ColumnName )
							//.Append( " = @p" + column.Ordinal );
							.Append("=@").Append(column.ColumnName);
						command.Parameters.Add( CreateParam( column, columnType ) );
					}
				}

				foreach( DataColumn column in dataTable.Columns )
				{
					if( System.Array.IndexOf( primaryKeyColumns, column) != -1 )
					{
						command.Parameters.Add( CreateParam( column, (SqlDbType)columnSQLTypes[column.Ordinal] ) );
					} 
				}

				string commandText = "UPDATE " + TableName + " SET "
					+ setString.ToString() + " WHERE " + whereString.ToString() + ";";

				if (hasTimestampColumn)
				{
					commandText += "SELECT * FROM " + TableName + " WHERE " + whereString.ToString() + ";";
					foreach (SqlParameter parameter in timestampParameters)
					{
						command.Parameters.Add(parameter);
					}
				}

				command.CommandText = commandText;
				return command;
			}
		}

		protected sealed override ArrayList AutoIncrementKeyColumns
		{
			get
			{
				ArrayList autoincrementKeys = new ArrayList();
				foreach( DataColumn primaryKeyColumn in dataTable.PrimaryKey )
				{
					if( primaryKeyColumn.AutoIncrement ) 
					{
						autoincrementKeys.Add( primaryKeyColumn );
					}
				}
				return autoincrementKeys;
			}
		}

		protected sealed override IDbDataParameter CreateParam( DataColumn column, object type, ParameterDirection direction)
		{
			return CreateParam(column, type, DataRowVersion.Current, direction);
		}

		protected sealed override IDbDataParameter CreateParam( DataColumn column, object type)
		{
			return CreateParam(column, type, DataRowVersion.Current, ParameterDirection.Input);
		}

		protected sealed override IDbDataParameter CreateParam(DataColumn column, object type, DataRowVersion version)
		{
			return CreateParam(column, type, version, ParameterDirection.Input);
		}

		protected sealed override IDbDataParameter CreateParam(DataColumn column, object type, DataRowVersion version, ParameterDirection direction)
		{
			SqlParameter sqlParam = new SqlParameter();
			string columnName = column.ColumnName;
			sqlParam.ParameterName = "@" + columnName;
			sqlParam.SourceColumn = columnName;
			sqlParam.SourceVersion = version;
			sqlParam.SqlDbType = (SqlDbType) type;
			return sqlParam;
		}

		protected sealed override IDbCommand GetTextCommand( string text )
		{
			SqlCommand command = new SqlCommand();
			command.CommandType = CommandType.Text;
			command.CommandText = text;
			command.Connection = connection;
			if(transaction != null)
				command.Transaction = transaction;
			return command;
		}

		protected sealed override string TableName 
		{
			get { return "[" + dataTable.TableName + "]"; }
		}

		protected sealed override string ColumnsString
		{
			get 
			{
				StringBuilder columnsString = new StringBuilder();
				foreach( DataColumn column in dataTable.Columns )
				{
					if( columnsString.Length > 0 ) 
					{
						columnsString.Append( ", " );
					}
					columnsString.Append( TableName );
					columnsString.Append( "." );
					columnsString.Append( column.ColumnName );
				}
				return columnsString.ToString();
			}
		}

		#region IDisposable Members
		public sealed override void Dispose() 
		{
			
		}
		#endregion

	}
}
