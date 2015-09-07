using System;
using System.Data;
using Oracle.DataAccess.Client;
using System.Diagnostics;

namespace DBAbstractDataLayer.ConnectionBuilders
{
	/// <summary>
	/// Summary description for OracleClientBuilder.
	/// </summary>
	public sealed class OracleClientBuilder: Builder 
	{
		private const string LocalServer = "GISA";
		private const string DefaultSchema = "GISA";
		private const string NormalConnectionTemplate = "Data Source=(DESCRIPTION="
		+ "(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST={0})(PORT=1521)))"
		+ "(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME={1})));"
		+ "User Id={2};Password={3};";

//		private const string NormalConnectionTemplate = "User Id={1};Password={2};Data Source={0}";

		private static OracleConnection connection = null;
		protected sealed override IDbConnection CreateConnection()
		{
			//if (Object.ReferenceEquals(connection, null)){
#if (DEBUG || RegistryConnection)
			return GetRegistryDB();
#else 
				if (LicenseServer == null) 
				{
					return GetLocalDB();
				} 
				else 
				{
					return GetLicenseDB();
				}
#endif
			//}
			//return connection;
		}

		private  OracleConnection GetLocalDB()
		{
			return instanceConnection(LocalServer);
		}

		private  OracleConnection GetLicenseDB()
		{
			return instanceConnection(LicenseServer);
		}

		private  OracleConnection GetRegistryDB()
		{
			string DataSource;
			string Schema;
			Microsoft.Win32.RegistryKey key;
			key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("Software\\ParadigmaXis\\GISA", false);
			if (!(key == null)) 
			{
				DataSource = ((string)(key.GetValue("Oracle", LocalServer)));
				Schema = ((string)(key.GetValue("Schema", LocalServer)));
			} 
			else 
			{
				DataSource = LocalServer;
				Schema = DefaultSchema;
			}
#if (AllowAlternativeDB)
			string Catalog;
			if (!(key == null)) 
			{
				Catalog = ((string)(key.GetValue("Database", "GISA")));
				key.Close();
			} 
			else 
			{
				Catalog = "GISA";
			}
			return instanceConnection(DataSource, Catalog, Schema);
#else
			if (!(key == null)) 
			{
				key.Close();
			}
			return instanceConnection(DataSource);
#endif			
		}
		
		private  OracleConnection instanceConnection(string server)
		{
			return instanceConnection(server, "GISA");
		}

		private  OracleConnection instanceConnection(string server, string database)
		{
			return instanceConnection(server, database, DefaultSchema);			
		}

		private  OracleConnection instanceConnection(string server, string database, string schema) 
		{
			string connectionString = string.Format(NormalConnectionTemplate, server, database, schema, password);
			OracleConnection conn = new OracleConnection(connectionString);			
			Trace.WriteLine(string.Format("Connecting {0}", conn.DataSource));
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
					connection = (OracleConnection) CreateConnection();
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
				return IsolationLevel.ReadCommitted;
			}
		}

	}
}
