using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace DBAbstractDataLayer.ConnectionBuilders
{
	// TODO: passar pra builder, tudo que for comum entre os vários builders, passar para a classe abstracta
	public sealed class SqlClientBuilder: Builder
	{
        private const string NormalConnectionTemplate = @"Integrated Security=SSPI;Persist Security Info=True;Connect Timeout=60;Initial Catalog={0};Data Source={1}; ";

		private static SqlConnection connection = null;
		protected sealed override IDbConnection CreateConnection()
		{
            string connectionString = string.Format(NormalConnectionTemplate, "GISA", Server);
            SqlConnection conn = new SqlConnection(connectionString);
            Trace.WriteLine(string.Format("Connecting {0}@{1}", conn.Database, conn.DataSource));
            return conn;
		}

		private void connection_StateChange(object sender, StateChangeEventArgs e)
		{
			OnConnectionStateChanged(sender, e);
		}

		public sealed override IDbConnection Connection 
		{ 
			get 
			{
				if (connection == null) 
				{
					connection = (SqlConnection) CreateConnection();
					connection.StateChange += new StateChangeEventHandler(connection_StateChange);
				}

				return connection; 
			} 
		}

		public sealed override IDbConnection TempConnection 
		{ 
			get 
			{
				return CreateConnection(); 
			} 
		}

		public override IsolationLevel TransactionIsolationLevel
		{
			get
			{
				return IsolationLevel.Serializable;
			}
		}

	}
}
