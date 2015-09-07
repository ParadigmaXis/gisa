using System;
using System.Data;
using System.Collections;
using System.Data.SqlClient;

namespace DBAbstractDataLayer.DataAccessRules.SqlClient
{
	/// <summary>
	/// Summary description for SqlClientGISATreeNodeRule.
	/// </summary>
	public class SqlClientGISATreeNodeRule: GISATreeNodeRule
	{
		public override System.Data.DataRow[] SelectRelacaoHierarquicaRow(System.Data.DataSet ds, long mNivelRowID, long mNivelUpperRowID)
		{
			return ds.Tables["RelacaoHierarquica"].Select(string.Format("ID={0} AND IDUpper={1}", mNivelRowID, mNivelUpperRowID), "", System.Data.DataViewRowState.CurrentRows | System.Data.DataViewRowState.Deleted);
		}
	}
}
