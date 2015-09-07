using System;
using System.Data;

namespace DBAbstractDataLayer.DataAccessRules
{
	public abstract class GisaInstallerRule: DALRule
	{
		private static GisaInstallerRule current = null;
		public static void ClearCurrent() 
		{
			current = null;
		}
		public static GisaInstallerRule Current
		{
			get 
			{
				if (Object.ReferenceEquals(null, current)) 
				{
					current = (GisaInstallerRule) Create(typeof(GisaInstallerRule));
				}
				return current;
			}
		}

		#region " Database Attaching/Detaching "
		public abstract int CheckInstalledDB (string dbName, IDbConnection conn);
		public abstract void ExecuteAttachDatabase (string dbName, string filename, IDbConnection conn);
		public abstract void ExecuteDetachDatabase (string dbName, IDbConnection conn);
		#endregion

        #region " Database Backup "
        public abstract void ExecuteBackupDatabase(string filename, IDbConnection conn);
        #endregion
    }
}
