using System;
using System.Data;
using System.Collections;

namespace DBAbstractDataLayer.DataAccessRules
{
	public abstract class GISATreeNodeRule: DALRule
	{
		private static GISATreeNodeRule current = null;
		public static void ClearCurrent() 
		{
			current = null;
		}
		public static GISATreeNodeRule Current
		{
			get 
			{
				if (Object.ReferenceEquals(null, current)) 
				{
					current = (GISATreeNodeRule) Create(typeof(GISATreeNodeRule));
				}
				return current;
			}
		}

		public abstract System.Data.DataRow[] SelectRelacaoHierarquicaRow (System.Data.DataSet ds, long mNivelRowID, long mNivelUpperRowID);		
	}
}
