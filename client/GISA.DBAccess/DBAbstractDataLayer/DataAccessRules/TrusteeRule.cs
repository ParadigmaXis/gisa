using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace DBAbstractDataLayer.DataAccessRules
{
	/// <summary>
	/// Summary description for TrusteeRule.
	/// </summary>
	public abstract class TrusteeRule: DALRule
		
	{
		private static TrusteeRule current = null;
		public static void ClearCurrent() 
		{
			current = null;
		}
		public static TrusteeRule Current
		{
			get 
			{
				if (Object.ReferenceEquals(null, current)) 
				{
					current = (TrusteeRule) Create(typeof(TrusteeRule));
				}
				return current;
			}
		}		

		#region " Trustee "
		public enum IndexErrorMessages 
		{
            CleaningRowsError = 4,
			InvalidUser = 7,
			InactiveUser = 8,
			UserWithoutPermissions = 9,
		}

		public abstract bool isValidNewTrustee(string name, System.Data.IDbTransaction tran);
		public abstract IndexErrorMessages validateUser(string username, string password, System.Data.IDbConnection conn);
        public abstract bool hasRegistos(long TrusteeID, System.Data.IDbConnection conn);
		public abstract bool hasUsers(long TrusteeID, System.Data.IDbConnection conn);
        public abstract void deleteDeletedData(System.Data.IDbConnection conn);
		#endregion

		#region " FormManageAutoresDescricao "
		public abstract void LoadTrusteeUsers (DataSet currentDataSet, IDbConnection conn);
		public abstract bool IsUserInUse (long tuRowID, IDbConnection conn);
		#endregion

		#region " FormSwitchAuthor "
		public abstract void LoadAuthorsData(DataSet currentDataSet, IDbConnection conn);
		public abstract void saveTrustee(DataSet currentDataSet, DataRow[] rows, IDbConnection conn);
		#endregion

		#region " GisaPrincipal "
		public abstract void LoadGroups (DataSet currentDataSet, long tuRowID, IDbConnection conn);
		public abstract void LoadTrusteePrivilegeData (DataTable mTrusteePrivileges, string tsRowBuiltInName, string mRowBuiltInName, long tuRowID, System.Data.IDbConnection conn, System.Data.IDbTransaction tran);
		#endregion

		#region " FormUserGroups "
		public abstract void LoadGroupsData(DataSet currentDataSet, IDbConnection conn);
		#endregion

		#region " frmMain "
		public abstract DataRow[] LoadCurrentOperatorData (DataSet currentDataSet, string username, IDbConnection conn);
		#endregion

		#region " MasterPanelTrusteeGroup "
		public abstract void LoadTrusteesGrpForUpdate (DataSet currentDataSet, IDbConnection conn);
		#endregion

		#region " MasterPanelTrusteeUser "
		public abstract void LoadTrusteesUsr (DataSet currentDataSet, IDbConnection conn);
		#endregion

		#region " PanelTrusteeDetailsGroup "
		public abstract void LoadMembership(DataSet currentDataSet, long IDTrustee, IDbConnection conn);
        //public abstract void SetUserPermissions(long userID, IDbConnection conn);
        //public abstract void SetUserPermissions(long userID, List<long> groupIDs, IDbConnection conn);
		#endregion

		#region " PanelTrusteePermissions "
		public abstract void LoadPanelTrusteePermissionsData (DataSet currentDataSet, long CurrentTrusteeRowID, IDbConnection conn);
        public struct Nivel
        {
            public long ID;
            public string Designacao;
            public Dictionary<string, byte> Permissoes;
        }
		#endregion

		#region " PanelTrusteeNivelPermissions " 
        public enum DocsFilter
        {
            Proprio = 0,
            TodosDocumentais = 1,
            Todos = 2
        }
		public abstract void LoadNivelUserPermissions (DataSet currentDataSet, long trusteeRowID, IDbConnection conn);
        public abstract void CalculateOrderedItems(long IDNivel, long IDTrustee, long IDLoginTrustee, string FiltroDesignacaoLike, long IDTipoNivelRelacionado, int Filtro, IDbConnection conn);
		public abstract ArrayList GetItems(DataSet currentDataSet, int pageNr, int itemsPerPage, long IDTrustee, IDbConnection conn);
		public abstract void DeleteTemporaryResults(IDbConnection conn);
		#endregion

        #region PanelTrusteeObjetoDigitalPermissions
        public abstract void CalculateODOrderedItems(long IDNivel, long IDTrustee, long IDLoginTrustee, ArrayList ordenacao, IDbConnection conn);
        public abstract int CountODPages(int itemsPerPage, IDbConnection conn);
        public abstract ArrayList GetODItems(DataSet currentDataSet, int pageNr, int itemsPerPage, long IDTrustee, IDbConnection conn);
        public abstract void DeleteODTemporaryResults(IDbConnection conn);
        #endregion

        #region " FormPickUser "
        public class User
        {
            public long userID;
            public string userName;
            public string userType;
            public string userInternalExternal;
        }
        public abstract List<User> LoadUsers(DataSet currentDataSet, IDbConnection conn);
        #endregion
    }
}
