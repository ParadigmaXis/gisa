using System;
using GISA.DBAccess.DBAbstractConnectionLayer;
using System.Data;
using System.Data.Common;

namespace GISA.DBAccess.DBAbstractDataLayer
{
	/// <summary>
	/// Summary description for SqlDataLayer.
	/// </summary>
	public sealed class SqlDataLayer: DBDataLayer
	{
		public SqlDataLayer()
		{
			
		}

		public void LoadDataPanelCAControlo(DbDataAdapter da, string ID) 
		{
			string QueryFilter = "IDControloAut=" + ID;
			string WhereQueryFilter = "WHERE " + QueryFilter;

			DataBaseLayer.ConnectionHolder ch = null;

			try 
			{
				ch = DBDataLayer.GetDBLayer().HoldConnection();
//				DBDataLayer.GetControloAutDataDeDescricaoDataAdapter(WhereQueryFilter, null, null).Fill(DBDataLayer.GetInstance().ControloAutDataDeDescricao);
//				DBDataLayer.GetTrusteeDataAdapter(null,null,null).Fill(DBDataLayer.GetInstance().Trustee);
//				DBDataLayer.GetTrusteeUserDataAdapter("WHERE IsAuthority=1",null,null).Fill(DBDataLayer.GetInstance().TrusteeUser);
			}
			finally
			{
				DataBaseLayer.DisposeConnection(ch);
			}
		}

		#region IDisposable Members
		public sealed override void Dispose() 
		{
//			if ((connection.State != ConnectionState.Closed) && (connection.State != ConnectionState.Broken)) 
//			{
//				this.connection.Close();
//			}
		}
		#endregion

	}
}
