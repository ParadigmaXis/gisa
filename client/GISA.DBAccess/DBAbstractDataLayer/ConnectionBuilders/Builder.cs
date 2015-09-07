using System;
using System.Data;
using System.Collections;

namespace DBAbstractDataLayer.ConnectionBuilders
{
	public abstract class Builder 
	{
		protected abstract IDbConnection CreateConnection();

		public abstract IDbConnection Connection{get;}
		public abstract IDbConnection TempConnection{get;}

		#region Events
		public event StateChangeEventHandler ConnectionStateChanged;

		public void OnConnectionStateChanged(object sender, StateChangeEventArgs e)
		{
			if (ConnectionStateChanged != null) ConnectionStateChanged(sender, e);
		}
		#endregion

		private static string mServer = null;
		public static string Server 
		{
			get 
			{
				return mServer;
			}
			set 
			{
				mServer = value;
			}
		}

		public abstract IsolationLevel TransactionIsolationLevel{get;}

	}
}
