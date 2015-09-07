using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using System.Security.Principal;
using DBAbstractDataLayer.DataAccessRules;

using GISA.Model;

namespace GISA.Model
{
	public interface IGisaPrincipal : IPrincipal
	{

		bool CanPerform(TipoFunctionGroup a, TipoFunction b, TipoOperation c);
	}

	public class GisaPrincipal : GenericPrincipal, IGisaPrincipal
	{
		// Assume-se que anteriormente à criação do GisaPrincipal terá já de 
		// existir carregada a licença da aplicação
		public GisaPrincipal(GISADataset.TrusteeUserRow tuRow) : base(new GenericIdentity(tuRow.TrusteeRow.Name), new string[]{})
		{
			IDbConnection conn = GisaDataSetHelper.GetConnection();
			try
			{
				conn.Open();
				// Carregar os grupos a que este utilizador pertence
				TrusteeRule.Current.LoadGroups(GisaDataSetHelper.GetInstance(), tuRow.ID, conn);

				// Estabelecer qual é o utilizador actual. Ao faze-lo são automaticamente calculadas as suas permissões
				TrusteeUserOperator = tuRow;
				if (! TrusteeUserOperator.IsIDTrusteeUserDefaultAuthorityNull())
				{
					TrusteeUserAuthor = TrusteeUserOperator.TrusteeUserRowParent;
				}
			}
			finally
			{
				conn.Close();
			}
		}

		public bool CanPerform(TipoFunctionGroup TheTipoFunctionGroup, TipoFunction TheTipoFunction, TipoOperation TheTipoOperation)
		{
			try
			{
				DataRow[] dr = GisaDataSetHelper.GetInstance().TrusteePrivilege.Select(string.Format("IDTrustee={0} AND IDTipoFunctionGroup={1} AND IdxTipoFunction={2} AND IDTipoOperation={3}", this.TrusteeUserOperator.ID, TheTipoFunctionGroup, TheTipoFunction, TheTipoOperation), "");
				return dr.Length > 0 && ((GISADataset.TrusteePrivilegeRow)(dr.GetValue(0))).IsGrant;
			}
			catch (Exception ex)
			{
				Trace.WriteLine("IGisaPrincipal.CanPerform");
				Trace.WriteLine(ex);
				throw ex;
			}
		}


		public delegate void TrusteeUserOperatorChangedEventHandler();
		public event TrusteeUserOperatorChangedEventHandler TrusteeUserOperatorChanged;
		public delegate void TrusteeUserAuthorChangedEventHandler();
		public event TrusteeUserAuthorChangedEventHandler TrusteeUserAuthorChanged;

	#region  Propiedades que definem quais as permissões 
		private GISADataset.TrusteeUserRow mTrusteeOperator = null;
		public GISADataset.TrusteeUserRow TrusteeUserOperator
		{
			get
			{
				return mTrusteeOperator;
			}
			set
			{
				mTrusteeOperator = value;
				RecalculatePrivileges(SessionHelper.AppConfiguration.GetCurrentAppconfiguration().TipoServer, SessionHelper.AppConfiguration.GetCurrentAppconfiguration().Modules);
				if (TrusteeUserOperatorChanged != null)
					TrusteeUserOperatorChanged();
				ReportAction("GisaPrincipal.TrusteeUserOperator");
			}
		}

		private GISADataset.TrusteeUserRow mTrusteeUserAuthor = null;
		public GISADataset.TrusteeUserRow TrusteeUserAuthor
		{
			get
			{
				return mTrusteeUserAuthor;
			}
			set
			{
				mTrusteeUserAuthor = value;
				if (TrusteeUserAuthorChanged != null)
					TrusteeUserAuthorChanged();
				ReportAction("GisaPrincipal.TrusteeUserAuthor");
			}
		}

		private GISADataset.TrusteePrivilegeDataTable mTrusteePrivileges = null;
		public GISADataset.TrusteePrivilegeDataTable TrusteePrivileges
		{
			get
			{
				return mTrusteePrivileges;
			}
		}
	#endregion

		private static void ReportAction(string label)
		{
			Trace.Write(string.Format("{0} was changed. Location: {1}", label, new StackFrame(2, true).ToString()));
		}

	#region  Carregamento de dados 
		public void RecalculatePrivileges()
		{
			RecalculatePrivileges(null);
		}

		public void RecalculatePrivileges(IDbTransaction Trans)
		{
			RecalculatePrivileges(SessionHelper.AppConfiguration.GetCurrentAppconfiguration().TipoServer, SessionHelper.AppConfiguration.GetCurrentAppconfiguration().Modules, Trans);
		}

		public void RecalculatePrivileges(GISADataset.TipoServerRow tsRow, List<GISADataset.ModulesRow> mRows)
		{
			RecalculatePrivileges(tsRow, mRows, null);
		}

		public void RecalculatePrivileges(GISADataset.TipoServerRow tsRow, List<GISADataset.ModulesRow> modulesList, IDbTransaction Trans)
		{
			mTrusteePrivileges = (GISADataset.TrusteePrivilegeDataTable)(GisaDataSetHelper.GetInstance().TrusteePrivilege.Clone());

            string modules = string.Empty;
            foreach (GISADataset.ModulesRow mRow in modulesList)
            {
                modules += mRow.ID + ",";
            }
            modules = modules.TrimEnd(',');

			// This resolves user privileges and his groups.
			// Does not handle groups of groups. These are not supported by DB
			IDbConnection conn = GisaDataSetHelper.GetConnection();
			if (Trans == null && ! (conn.State == ConnectionState.Open))
			{
				try
				{
					conn.Open();
					TrusteeRule.Current.LoadTrusteePrivilegeData(mTrusteePrivileges, tsRow.BuiltInName, modules, TrusteeUserOperator.ID, conn, Trans);
				}
				catch (Exception ex)
				{
					Trace.WriteLine(ex);
				}
				finally
				{
					conn.Close();
				}
			}
			else if (Trans == null && conn.State == ConnectionState.Open)
			{
				try
				{
					TrusteeRule.Current.LoadTrusteePrivilegeData(mTrusteePrivileges, tsRow.BuiltInName, modules, TrusteeUserOperator.ID, conn, Trans);
				}
				catch (Exception ex)
				{
					Trace.WriteLine(ex);
				}
			}
			else
			{
				TrusteeRule.Current.LoadTrusteePrivilegeData(mTrusteePrivileges, tsRow.BuiltInName, modules, TrusteeUserOperator.ID, conn, Trans);
			}

			// Keep our copy of this data and merge it with the dataset
			GisaDataSetHelper.GetInstance().Merge(mTrusteePrivileges);

		}
		
	#endregion

	}
} //end of root namespace