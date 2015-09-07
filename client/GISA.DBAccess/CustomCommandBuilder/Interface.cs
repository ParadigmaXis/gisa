using System;
using System.Data;
using System.Data.Common;
using System.Collections;

namespace GISA.Data.AbstractCommandBuilder 
{	

	public abstract class CustomCommandBuilder: IDisposable 
	{	
		public abstract IDbCommand SelectAllCommand  { get; }

		public abstract IDbCommand GetSelectWithFilterCommand( string filter );

		public abstract IDbCommand GetSelectWithOrderCommand( string order );

		public abstract IDbCommand DeleteCommand { get; }

		public abstract IDbCommand InsertCommand { get; }

		public abstract IDbCommand UpdateCommand { get; }

		protected abstract ArrayList AutoIncrementKeyColumns { get; }

		protected abstract IDbDataParameter CreateParam( DataColumn column, object type, ParameterDirection direction);

		protected abstract IDbDataParameter CreateParam( DataColumn column, object type);

		protected abstract IDbDataParameter CreateParam(DataColumn column, object type, DataRowVersion version);

		protected abstract IDbDataParameter CreateParam(DataColumn column, object type, DataRowVersion version, ParameterDirection direction);

		protected abstract IDbCommand GetTextCommand( string text );

		protected abstract string TableName { get; }

		protected abstract string ColumnsString { get; }

		#region IDisposable Members

		public abstract void Dispose();

		#endregion
	}
}
	