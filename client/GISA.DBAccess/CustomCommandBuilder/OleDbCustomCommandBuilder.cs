using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Data.OleDb;

namespace ParadigmaXis.Data.AbstractCommandBuilder
{	
	public sealed class OleDbCustomCommandBuilder: CustomCommandBuilder
	{
		DataTable dataTable;
		OleDbConnection connection;
		OleDbTransaction transaction;
		ArrayList columnOleDbTypes;
		
		public OleDbCustomCommandBuilder( DataTable dataTable, ArrayList columnOleDbTypes)
		{
			if (dataTable.Columns.Count != columnOleDbTypes.Count)
			{
				throw new ArgumentException("Number of OleDbTypes does not match number of columns.");
			}
			this.dataTable = dataTable;
			this.columnOleDbTypes = columnOleDbTypes;
		}

		public OleDbCustomCommandBuilder( DataTable dataTable, OleDbConnection connection, ArrayList columnOleDbTypes)
		{
			if (dataTable.Columns.Count != columnOleDbTypes.Count){
				throw new ArgumentException("Number of OleDbTypes does not match number of columns.");
			}
			this.dataTable = dataTable;
			this.connection = connection;
			this.columnOleDbTypes = columnOleDbTypes;
		}

		public OleDbCustomCommandBuilder( DataTable dataTable, OleDbConnection connection, ArrayList columnOleDbTypes, OleDbTransaction transaction)
		{
			if (dataTable.Columns.Count != columnOleDbTypes.Count){
				throw new ArgumentException("Number of OleDbTypes does not match number of columns.");
			}
			this.dataTable = dataTable;
			this.connection = connection;
			this.columnOleDbTypes = columnOleDbTypes;
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
				OleDbCommand command = (OleDbCommand) GetTextCommand( "" );
				StringBuilder whereString = new StringBuilder();
				foreach( DataColumn column in dataTable.PrimaryKey )
				{
					if( whereString.Length > 0 )
					{
						whereString.Append( " AND " );
					}
					whereString.Append( column.ColumnName )
						.Append( " = ?" ); //.Append( column.ColumnName );
					command.Parameters.Add(CreateParam(column, (OleDbType)columnOleDbTypes[column.Ordinal], DataRowVersion.Original));
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
				OleDbCommand command = (OleDbCommand) GetTextCommand( "" );
				StringBuilder intoString = new StringBuilder();
				StringBuilder valuesString = new StringBuilder();
				StringBuilder timestampFilter = new StringBuilder();
				ArrayList autoincrementColumns = AutoIncrementKeyColumns;
				ArrayList timestampParameters = new ArrayList();
				bool hasTimestampColumn = false;
				OleDbType columnType;
				foreach( DataColumn column in dataTable.Columns )
				{
					columnType = (OleDbType)columnOleDbTypes[column.Ordinal];
					// check for timestamp columns
					if (columnType == OleDbType.Binary && column.ReadOnly){ //timestamps
						hasTimestampColumn = true;
					}

					// Not an autoincrement and not a readonly column (ie, timestamps)
					if( !autoincrementColumns.Contains(column)  && !column.ReadOnly)
					{
						if( intoString.Length > 0)
						{
							intoString.Append( ", " );
							valuesString.Append( ", " );
						} 
						intoString.Append( column.ColumnName );
						valuesString.Append( "?" );
						command.Parameters.Add(CreateParam(column, columnType));
					}

					if (Array.IndexOf(column.Table.PrimaryKey, column) != -1){
						if(timestampFilter.Length > 0){
							timestampFilter.Append(" AND ");
						}
						timestampFilter.Append(column.ColumnName);
						timestampFilter.Append("=?");
						timestampParameters.Add(CreateParam(column, columnType));
					}
				}

				string commandText = "INSERT INTO " + TableName + "("
					+ intoString.ToString() + ") VALUES (" + valuesString.ToString() + "); ";

				// use SCOPE_IDENTITY when identity columns exist, otherwise use the values for the existing primarykey
				if( autoincrementColumns.Count > 0 ) 
				{
					// only works for tables with no more than one autoincrement column
					commandText += "SELECT * FROM " + TableName + " WHERE " + ( (DataColumn) autoincrementColumns[0]).ColumnName +  " = SCOPE_IDENTITY();";
				}else if (hasTimestampColumn){
					commandText += "SELECT * FROM " + TableName + " WHERE " + timestampFilter + ";";
					foreach (OleDbParameter parameter in timestampParameters)
					{
						command.Parameters.Add(parameter);
					}
				}
				command.CommandText = commandText;
				command.UpdatedRowSource = UpdateRowSource.FirstReturnedRecord;
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
				OleDbCommand command = (OleDbCommand) GetTextCommand( "" );
				StringBuilder setString = new StringBuilder();
				StringBuilder whereString = new StringBuilder();
				DataColumn[] primaryKeyColumns = dataTable.PrimaryKey;
				ArrayList timestampParameters = new ArrayList();
				bool hasTimestampColumn = false;
				OleDbType columnType;

				foreach( DataColumn column in dataTable.Columns )
				{
					columnType = (OleDbType)columnOleDbTypes[column.Ordinal];

					if (columnType == OleDbType.Binary && column.ReadOnly)
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
							.Append( "= ?" );
						timestampParameters.Add(CreateParam(column, columnType));
					} 
					else if(!column.ReadOnly) // do not put values on readonly columns (ie, timestamps)
					{
						if( setString.Length > 0 )
						{
							setString.Append( ", " );
						}
						setString.Append( column.ColumnName )
							.Append( " = ?" );
						command.Parameters.Add( CreateParam( column, columnType ) );
					}
				}

				foreach( DataColumn column in dataTable.Columns )
				{
					if( System.Array.IndexOf( primaryKeyColumns, column) != -1 )
					{
						command.Parameters.Add( CreateParam( column, (OleDbType)columnOleDbTypes[column.Ordinal] ) );
					} 
				}

				string commandText = "UPDATE " + TableName + " SET "
					+ setString.ToString() + " WHERE " + whereString.ToString() + ";";

				if (hasTimestampColumn)
				{
					commandText += "SELECT * FROM " + TableName + " WHERE " + whereString.ToString() + ";";
					foreach (OleDbParameter parameter in timestampParameters)
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
			OleDbParameter sqlParam = new OleDbParameter();
			string columnName = column.ColumnName;
			sqlParam.ParameterName = "@" + columnName;
			sqlParam.SourceColumn = columnName;
			sqlParam.SourceVersion = version;
			sqlParam.OleDbType = (OleDbType) type;
			return sqlParam;
		}

		protected sealed override IDbCommand GetTextCommand( string text )
		{
			OleDbCommand command = new OleDbCommand();
			command.CommandType = CommandType.Text;
			command.CommandText = text;
			command.Connection = connection;
			if(transaction != null){
				command.Transaction = transaction;
			}
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
