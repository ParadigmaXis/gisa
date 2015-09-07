using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Diagnostics;
using Oracle.DataAccess.Client;

namespace ParadigmaXis.Data.AbstractCommandBuilder
{
	/// <summary>
	/// Summary description for OracleCustomCommandBuilder.
	/// </summary>
	public sealed class OracleCustomCommandBuilder: CustomCommandBuilder
	{
		DataTable dataTable;
		OracleConnection connection;
		OracleTransaction transaction;
		ArrayList columnOracleTypes;

		public OracleCustomCommandBuilder( DataTable dataTable, ArrayList columnOracleTypes)
		{
			if (dataTable.Columns.Count != columnOracleTypes.Count)
			{
				throw new ArgumentException("Number of OracleTypes does not match number of columns.");
			}
			this.dataTable = dataTable;
			this.columnOracleTypes = columnOracleTypes;
		}

		public OracleCustomCommandBuilder( DataTable dataTable, OracleConnection connection, ArrayList columnOracleTypes)
		{
			if (dataTable.Columns.Count != columnOracleTypes.Count)
			{
				throw new ArgumentException("Number of OracleTypes does not match number of columns.");
			}
			this.dataTable = dataTable;
			this.connection = connection;
			this.columnOracleTypes = columnOracleTypes;
		}

		public OracleCustomCommandBuilder( DataTable dataTable, OracleConnection connection, ArrayList columnOracleTypes, OracleTransaction transaction)
		{
			if (dataTable.Columns.Count != columnOracleTypes.Count)
			{
				throw new ArgumentException("Number of OracleTypes does not match number of columns.");
			}
			this.dataTable = dataTable;
			this.connection = connection;
			this.columnOracleTypes = columnOracleTypes;
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
				OracleCommand command = (OracleCommand) GetTextCommand( "" );
				StringBuilder whereString = new StringBuilder();
				foreach( DataColumn column in dataTable.PrimaryKey )
				{
					if( whereString.Length > 0 )
					{
						whereString.Append( " AND " );
					}
					whereString.Append( column.ColumnName )
						//.Append( " = @p" + column.Ordinal ); 						
						.Append(" = :").Append( column.ColumnName );
					command.Parameters.Add(CreateParam(column, (OracleDbType)columnOracleTypes[column.Ordinal], DataRowVersion.Original));
				}
				string commandText = 
					"BEGIN " +
					"LOCK TABLE " + dataTable.TableName + " IN ROW SHARE MODE NOWAIT; " +
					"DELETE FROM " + TableName
					+ " WHERE " + whereString.ToString() + "; END;";
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
				OracleCommand command = (OracleCommand) GetTextCommand( "" );
				StringBuilder intoString = new StringBuilder();
				StringBuilder valuesString = new StringBuilder();
				StringBuilder timestampFilter = new StringBuilder();
				ArrayList autoincrementColumns = AutoIncrementKeyColumns;
				ArrayList timestampParameters = new ArrayList();
				bool hasTimestampColumn = false;
				OracleDbType columnType;
				foreach( DataColumn column in dataTable.Columns )
				{
					columnType = (OracleDbType)columnOracleTypes[column.Ordinal];
					// check for timestamp columns
					if (columnType == OracleDbType.Raw && column.ReadOnly)
					{ //timestamps
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
						//valuesString.Append( "@p" + column.Ordinal );
						valuesString.Append(":").Append( column.ColumnName );
						command.Parameters.Add(CreateParam(column, columnType));
					}

					if (Array.IndexOf(column.Table.PrimaryKey, column) != -1)
					{
						if(timestampFilter.Length > 0)
						{
							timestampFilter.Append(" AND ");
						}
						timestampFilter.Append(column.ColumnName);
						//timestampFilter.Append("=@p"+column.Ordinal );
						timestampFilter.Append("=:ts_").Append(column.ColumnName);
						IDbDataParameter param = CreateParam(column, columnType);
						param.ParameterName= ":ts_"+column.ColumnName;
						timestampParameters.Add(param);
					}
				}

				string commandText;
				if( autoincrementColumns.Count > 0 ) 
				{
					string colID = ( (DataColumn) autoincrementColumns[0]).ColumnName;
					string seqName;
					seqName = TableName + "_" + colID + "_SEQ";
					 
					commandText = "DECLARE " +
						"currID NUMBER(19,0);  " +
						"BEGIN " + 
						"LOCK TABLE " + TableName + " IN ROW SHARE MODE NOWAIT; " + 
						//"INSERT INTO " + TableName + "(" + intoString.ToString() + ") VALUES (" + valuesString.ToString() + "); " +
						"INSERT INTO " + TableName + "(" + colID + ", " + intoString.ToString() + ") VALUES (" + seqName + ".NEXTVAL, " + valuesString.ToString() + "); " +
						"SELECT " + seqName + ".CURRVAL INTO currID FROM DUAL; " + 
						"SELECT " + colID + " INTO :" + colID + " FROM " + TableName + " WHERE " + colID + " = currID; " +
						"SELECT Versao INTO :Versao FROM " + TableName + " WHERE " + colID + " = currID; END;";
					
					command.Parameters.Add(CreateParam((DataColumn) autoincrementColumns[0], columnOracleTypes[dataTable.Columns[colID].Ordinal] , ParameterDirection.Output));
					command.Parameters.Add(CreateParam(dataTable.Columns["Versao"], columnOracleTypes[dataTable.Columns["Versao"].Ordinal], ParameterDirection.Output));
				}
				else if (hasTimestampColumn)
				{
					commandText = "BEGIN " + 
						"LOCK TABLE " + TableName + " IN ROW SHARE MODE; " +
						"INSERT INTO " + TableName + "(" + intoString.ToString() + ") VALUES (" + valuesString.ToString() + "); " +
						"SELECT Versao INTO :Versao FROM " + TableName + " WHERE " + timestampFilter + "; END;";
					//Trace.WriteLine("SELECT Versao INTO :Versao FROM " + TableName + " WHERE " + timestampFilter + "; END;");
					command.Parameters.Add(CreateParam(dataTable.Columns["Versao"], columnOracleTypes[dataTable.Columns["Versao"].Ordinal], ParameterDirection.Output));
					foreach (OracleParameter parameter in timestampParameters)
					{
						command.Parameters.Add(parameter);
					}					
				}
				else
				{
					commandText = 					
						"INSERT INTO " + TableName + "("
						+ intoString.ToString() + ") VALUES (" + valuesString.ToString() + "); ";
				}


				command.CommandText = commandText;
				command.UpdatedRowSource = UpdateRowSource.OutputParameters;
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
				OracleCommand command = (OracleCommand) GetTextCommand( "" );
				StringBuilder setString = new StringBuilder();
				StringBuilder whereString = new StringBuilder();
				StringBuilder whereStringTS = new StringBuilder();
				DataColumn[] primaryKeyColumns = dataTable.PrimaryKey;
				ArrayList timestampParameters = new ArrayList();
				bool hasTimestampColumn = false;
				OracleDbType columnType;

				foreach( DataColumn column in dataTable.Columns )
				{
					columnType = (OracleDbType)columnOracleTypes[column.Ordinal];

					if (columnType == OracleDbType.Raw && column.ReadOnly)
					{ //timestamps
						hasTimestampColumn = true;
					}

					if( System.Array.IndexOf( primaryKeyColumns, column) != -1 )
					{
						// A primary key
						if( whereStringTS.Length > 0 )
						{
							whereStringTS.Append( " AND " );
							whereString.Append( " AND " );
						}
						whereString.Append( column.ColumnName ).Append("=:").Append(column.ColumnName);
						whereStringTS.Append( column.ColumnName )
							//.Append( "= @p" + column.Ordinal );
							.Append("=:ts_").Append(column.ColumnName);
						IDbDataParameter param = CreateParam(column, columnType);
						param.ParameterName= ":ts_"+column.ColumnName;
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
							.Append("=:").Append(column.ColumnName);
						command.Parameters.Add( CreateParam( column, columnType ) );
					}
				}

				foreach( DataColumn column in dataTable.Columns )
				{
					if( System.Array.IndexOf( primaryKeyColumns, column) != -1 )
					{
						command.Parameters.Add( CreateParam( column, (OracleDbType)columnOracleTypes[column.Ordinal] ) );
					} 
				}

				string commandText = "UPDATE " + TableName + " SET "
					+ setString.ToString() + " WHERE " + whereString.ToString() + "; ";

				if (hasTimestampColumn)
				{
					commandText = "BEGIN " + 
						"LOCK TABLE " + dataTable.TableName + " IN ROW SHARE MODE NOWAIT; " +
						commandText +
						"SELECT Versao INTO :Versao FROM " + TableName + " WHERE " + whereStringTS.ToString() + "; END;";
					command.Parameters.Add(CreateParam(dataTable.Columns["Versao"], columnOracleTypes[dataTable.Columns["Versao"].Ordinal], ParameterDirection.Output));
					foreach (OracleParameter parameter in timestampParameters)
					{
						command.Parameters.Add(parameter);
					}
				}
				else
				{
					commandText = "BEGIN " +
						"LOCK TABLE " + dataTable.TableName + " IN ROW SHARE MODE NOWAIT; " +
						commandText +
						"END;";
				}

				command.UpdatedRowSource = UpdateRowSource.OutputParameters;
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
			OracleParameter oracleParam = new OracleParameter();
			string columnName = column.ColumnName;
			oracleParam.ParameterName = ":" + columnName;
			oracleParam.SourceColumn = columnName;
			oracleParam.SourceVersion = version;
			oracleParam.OracleDbType = (OracleDbType) type;
			oracleParam.Direction = direction;
			if (column.DataType == typeof(System.Int64) && (OracleDbType) type == OracleDbType.Int64)
			{
				oracleParam.Precision = 19;
				oracleParam.Scale = 0;
			} 
			else if (column.DataType == typeof(System.Byte[]) && (OracleDbType) type == OracleDbType.Raw)
			{
				oracleParam.Size = 8;
				oracleParam.DbType = DbType.Binary;
			}
			else if (column.DataType == typeof(System.String) && ((OracleDbType) type == OracleDbType.NVarchar2 || (OracleDbType) type == OracleDbType.Varchar2 || (OracleDbType) type == OracleDbType.NClob))
			{
				oracleParam.Size = column.MaxLength;
			}
//			oracleParam.Precision = 
//			oracleParam.Scale =
//			oracleParam.Size =
			return oracleParam;
		}

		protected sealed override IDbCommand GetTextCommand( string text )
		{
			OracleCommand command = new OracleCommand();
			command.CommandType = CommandType.Text;
			command.CommandText = text;
			command.Connection = connection;
//			if(transaction != null)
//			{
//				command.Transaction = transaction;
//			}
			return command;
		}

		protected sealed override string TableName 
		{
			get { return dataTable.TableName; }
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
