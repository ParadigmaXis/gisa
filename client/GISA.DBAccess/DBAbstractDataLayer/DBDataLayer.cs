#if DEBUG
using NUnit.Framework;
#endif
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Windows.Forms;
using System.Text;
using ParadigmaXis.Data.AbstractCommandBuilder;
using GISA.DBAccess.DBAbstractConnectionLayer;
using System.Reflection;
using System.Diagnostics;
using System;
using System.ComponentModel;
using System.Xml;

namespace GISA.DBAccess.DBAbstractDataLayer
{

	public enum DataBaseLayers { SqlDataLayer }

	public abstract class DBDataLayer: IDisposable
	{
		public static BooleanSwitch SqlTrace = new BooleanSwitch("GISAModelSqlTrace", "Controls SQL statement trace output.");

#region " DBLayer & Connections "
		private static string mWorkingDBMS;
		public static string workingDBMS 
		{
			get 
			{
				if (mWorkingDBMS == null) 
				{
					mWorkingDBMS = "SQLServer";
				}
				return mWorkingDBMS;
			}
		}

		private const string LocalServer = "(local)\\GISA";

		// Informação de licença necessária a esta assembly e que lhe é passada pela assembly Gisa.exe
		private static string mLicenseServer = null;
		public static string LicenseServer 
		{
			get 
			{
				return mLicenseServer;
			}
			set 
			{
				mLicenseServer = value;
			}
		}

		private static DataBaseLayer dbLayer = null;
		public static DataBaseLayer GetDBLayer()
		{
			if (dbLayer == null) 
			{
#if (Debug || RegistryConnection)
				dbLayer = GetRegistryDBLayer();
#else 
				if (LicenseServer == null) 
				{
					dbLayer = GetLocalDBLayer();
				} 
				else 
				{
					dbLayer = GetLicenseDBLayer();
				}
#endif
			}
			return dbLayer;
		}

		private static DataBaseLayer GetLocalDBLayer()
		{
			return makeDbLayer(LocalServer, null);
		}

		private static DataBaseLayer GetLicenseDBLayer()
		{
			return makeDbLayer(LicenseServer, null);
		}

		private static DataBaseLayer GetRegistryDBLayer()
		{
			string DataSource;
			Microsoft.Win32.RegistryKey key;
			key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("Software\\ParadigmaXis\\GISA", false);
			if (!(key == null)) 
			{
				DataSource = ((string)(key.GetValue("SqlServer", LocalServer)));
			} 
			else 
			{
				DataSource = LocalServer;
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
			return makeDbLayer(DataSource, Catalog);
#else
			if (!(key == null)) 
			{
				key.Close();
			}
			return makeDbLayer(DataSource, null);
#endif			
		}

		private static DataBaseLayer makeDbLayer(string server, string database)
		{
			if (workingDBMS == "SQLServer") 
			{
				dbLayer = new SqlLayer(server, database, "sa", "password");
			} 
			else if (workingDBMS == "Other") 
			{
				dbLayer = new OleDbLayer(server, database, "sa", "password");
			} 
			else 
			{
				Debug.Assert(false, "SGBD desconhecido");
			}
			dbLayer.ConnectionStateChanged += new System.Data.StateChangeEventHandler(Connection_StateChange);
			return dbLayer;
		}

		public static IDbConnection GetConnection()
		{
			return GetDBLayer().Connection;
		}

		public static DataBaseLayer getTemporaryDbLayer()
		{
			return GetDBLayer().ForkConnection();
		}
		public static event ConnectionStateChangedEventHandler ConnectionStateChanged;

		public delegate void ConnectionStateChangedEventHandler(object sender, ConnectionStateChangedEventArgs e);

		private static bool wasClosed = true;
		public static void Connection_StateChange(object sender, System.Data.StateChangeEventArgs e)
		{
			bool isClosed = e.CurrentState == ConnectionState.Closed;
			if (isClosed ^ wasClosed) 
			{
				if (ConnectionStateChanged != null) 
				{
					ConnectionStateChanged(sender, new ConnectionStateChangedEventArgs(!isClosed));
				}
			}
			wasClosed = isClosed;
		}
#endregion

#region " DataAdapter "
		
		public enum DataDeletionStatus
		{
			Exists = 1,
			Deleted = 2,
			All = 3
		}

		public static DbDataAdapter GetDataAdapter(string TableName, string Suffix, IDbConnection Conn, IDbTransaction Trans, DataDeletionStatus deletionStatus)
		{
			if (Conn == null) 
			{
				Conn = GetConnection();
			}
			string command = string.Empty;
			DataTable table = GetInstance().Tables[TableName];
			string tableAlias = "__gisatable__";
			if (Suffix.Length > 0 && !(deletionStatus == DataDeletionStatus.All)) 
			{
				StringBuilder selectQuery = new StringBuilder();
				selectQuery.AppendFormat("SELECT {3} FROM {0} INNER JOIN (SELECT {3} FROM {0} {2}) {1} ON ", TableName, tableAlias, Suffix, getAllPrimaryKeys(table,null));
				foreach (DataColumn column in table.PrimaryKey) 
				{
					if (column.Ordinal > 0) 
					{
						selectQuery.Append(" AND");
					}
					selectQuery.AppendFormat(" {0}.{2}={1}.{2}", TableName, tableAlias, column.ColumnName);
				}
				if (deletionStatus == DataDeletionStatus.Exists) 
				{
					selectQuery.AppendFormat(" WHERE {0}.isDeleted=0", tableAlias);
				} 
				else if (deletionStatus == DataDeletionStatus.Deleted) 
				{
					selectQuery.AppendFormat(" WHERE {0}.isDeleted=1", tableAlias);
				}
				command = selectQuery.ToString();
			} 
			else if (Suffix.Length == 0 && !(deletionStatus == DataDeletionStatus.All)) 
			{
				StringBuilder selectQuery = new StringBuilder();
				selectQuery.AppendFormat("SELECT {0} FROM {1}", getAllPrimaryKeys(table,null), TableName);
				if (deletionStatus == DataDeletionStatus.Exists) 
				{
					selectQuery.AppendFormat(" WHERE {0}.isDeleted=0", TableName);
				} 
				else if (deletionStatus == DataDeletionStatus.Deleted) 
				{
					selectQuery.AppendFormat(" WHERE {0}.isDeleted=1", TableName);
				}
				command = selectQuery.ToString();
			}


			if (workingDBMS == "SQLServer") 
			{
				SqlDataAdapter da = new SqlDataAdapter();
				SQLCustomCommandBuilder cb = new SQLCustomCommandBuilder(GetInstance().Tables[TableName], ((SqlConnection)(Conn)), MetaModelHelper.getColumnTypes(TableName, "TransactSQL"), ((SqlTransaction)(Trans)));
				if (Suffix.Length > 0 && deletionStatus == DataDeletionStatus.All) 
				{
					da.SelectCommand = ((SqlCommand)(cb.GetSelectWithFilterCommand(Suffix)));
				} 
				else if (Suffix.Length > 0 && !(deletionStatus == DataDeletionStatus.All) && command.Length > 0) 
				{
					da.SelectCommand = new SqlCommand(command);
					da.SelectCommand.Connection = ((SqlConnection)(Conn));
					da.SelectCommand.CommandType = CommandType.Text;
					da.SelectCommand.Transaction = ((SqlTransaction)(Trans));
				} 
				else if (Suffix.Length == 0 && command.Length == 0) 
				{
					da.SelectCommand = ((SqlCommand)(cb.SelectAllCommand));
					da.SelectCommand.CommandText += "WHERE isDeleted = 0";
				} 
				else if (Suffix.Length == 0 && command.Length > 0) 
				{
					da.SelectCommand = new SqlCommand(command);
					da.SelectCommand.Connection = ((SqlConnection)(Conn));
					da.SelectCommand.CommandType = CommandType.Text;
					da.SelectCommand.Transaction = ((SqlTransaction)(Trans));
				}
				da.UpdateCommand = ((SqlCommand)(cb.UpdateCommand));
				da.DeleteCommand = ((SqlCommand)(cb.DeleteCommand));
				da.InsertCommand = ((SqlCommand)(cb.InsertCommand));
				Trace.WriteLineIf(SqlTrace.Enabled, da.SelectCommand.CommandText);
				return da;
			} 
			else if (workingDBMS == "Others") 
			{
				OleDbDataAdapter da = new OleDbDataAdapter();
				OleDbCustomCommandBuilder cb = new OleDbCustomCommandBuilder(GetInstance().Tables[TableName], ((OleDbConnection)(Conn)), MetaModelHelper.getColumnTypes(TableName, "oleDB"), ((OleDbTransaction)(Trans)));
				if (Suffix.Length > 0 && deletionStatus == DataDeletionStatus.All) 
				{
					da.SelectCommand = ((OleDbCommand)(cb.GetSelectWithFilterCommand(Suffix)));
				} 
				else if (Suffix.Length > 0 && !(deletionStatus == DataDeletionStatus.All) && command.Length > 0) 
				{
					da.SelectCommand = new OleDbCommand(command);
					da.SelectCommand.Connection = ((OleDbConnection)(Conn));
					da.SelectCommand.CommandType = CommandType.Text;
					da.SelectCommand.Transaction = ((OleDbTransaction)(Trans));
				} 
				else if (Suffix.Length == 0 && command.Length == 0) 
				{
					da.SelectCommand = ((OleDbCommand)(cb.SelectAllCommand));
					da.SelectCommand.CommandText += "WHERE isDeleted = 0";
				} 
				else if (Suffix.Length == 0 && command.Length > 0) 
				{
					da.SelectCommand = new OleDbCommand(command);
					da.SelectCommand.Connection = ((OleDbConnection)(Conn));
					da.SelectCommand.CommandType = CommandType.Text;
					da.SelectCommand.Transaction = ((OleDbTransaction)(Trans));
				}
				da.UpdateCommand = ((OleDbCommand)(cb.UpdateCommand));
				da.DeleteCommand = ((OleDbCommand)(cb.DeleteCommand));
				da.InsertCommand = ((OleDbCommand)(cb.InsertCommand));
				Trace.WriteLineIf(SqlTrace.Enabled, da.SelectCommand.CommandText);
				return da;
			} 
			else 
			{
				Debug.Assert(false, "(DataAdapter) SGBD desconhecido");
				return null;
			}
		}

		internal static string getAllPrimaryKeys(DataTable table, string aliasName)
		{
			StringBuilder columnsString = new StringBuilder();
			foreach (DataColumn column in table.Columns) 
			{
				if (columnsString.Length > 0) 
				{
					columnsString.Append(", ");
				}
				if (aliasName == null) 
				{
					columnsString.Append(table.TableName);
				} 
				else 
				{
					columnsString.Append(aliasName);
				}
				columnsString.Append(".");
				columnsString.Append(column.ColumnName);
			}
			return columnsString.ToString();
		}
#endregion		

#region "Generated by SQLServer Transact-SQL script"
		public static DbDataAdapter GetAccessControlElementDataAdapter(string Suffix, IDbConnection Conn, IDbTransaction Trans)
		{
			return GetDataAdapter("AccessControlElement", Suffix, Conn, Trans, DataDeletionStatus.Exists);
		}

		public static DbDataAdapter GetAutoEliminacaoDataAdapter(string Suffix, IDbConnection Conn, IDbTransaction Trans)
		{
			return GetDataAdapter("AutoEliminacao", Suffix, Conn, Trans, DataDeletionStatus.Exists);
		}

		public static DbDataAdapter GetControloAutDataAdapter(string Suffix, IDbConnection Conn, IDbTransaction Trans)
		{
			return GetDataAdapter("ControloAut", Suffix, Conn, Trans, DataDeletionStatus.Exists);
		}

		public static DbDataAdapter GetControloAutDatasExistenciaDataAdapter(string Suffix, IDbConnection Conn, IDbTransaction Trans)
		{
			return GetDataAdapter("ControloAutDatasExistencia", Suffix, Conn, Trans, DataDeletionStatus.Exists);
		}

		public static DbDataAdapter GetControloAutDataDeDescricaoDataAdapter(string Suffix, IDbConnection Conn, IDbTransaction Trans)
		{
			return GetDataAdapter("ControloAutDataDeDescricao", Suffix, Conn, Trans, DataDeletionStatus.Exists);
		}

		public static DbDataAdapter GetControloAutDicionarioDataAdapter(string Suffix, IDbConnection Conn, IDbTransaction Trans)
		{
			return GetDataAdapter("ControloAutDicionario", Suffix, Conn, Trans, DataDeletionStatus.Exists);
		}

		public static DbDataAdapter GetControloAutEntidadeProdutoraDataAdapter(string Suffix, IDbConnection Conn, IDbTransaction Trans)
		{
			return GetDataAdapter("ControloAutEntidadeProdutora", Suffix, Conn, Trans, DataDeletionStatus.Exists);
		}

		public static DbDataAdapter GetControloAutRelDataAdapter(string Suffix, IDbConnection Conn, IDbTransaction Trans)
		{
			return GetDataAdapter("ControloAutRel", Suffix, Conn, Trans, DataDeletionStatus.Exists);
		}

		public static DbDataAdapter GetDicionarioDataAdapter(string Suffix, IDbConnection Conn, IDbTransaction Trans)
		{
			return GetDataAdapter("Dicionario", Suffix, Conn, Trans, DataDeletionStatus.Exists);
		}

		public static DbDataAdapter GetFRDBaseDataAdapter(string Suffix, IDbConnection Conn, IDbTransaction Trans)
		{
			return GetDataAdapter("FRDBase", Suffix, Conn, Trans, DataDeletionStatus.Exists);
		}

		public static DbDataAdapter GetFRDBaseDataDeDescricaoDataAdapter(string Suffix, IDbConnection Conn, IDbTransaction Trans)
		{
			return GetDataAdapter("FRDBaseDataDeDescricao", Suffix, Conn, Trans, DataDeletionStatus.Exists);
		}

		public static DbDataAdapter GetFunctionOperationDataAdapter(string Suffix, IDbConnection Conn, IDbTransaction Trans)
		{
			return GetDataAdapter("FunctionOperation", Suffix, Conn, Trans, DataDeletionStatus.Exists);
		}

		public static DbDataAdapter GetIndexFRDCADataAdapter(string Suffix, IDbConnection Conn, IDbTransaction Trans)
		{
			return GetDataAdapter("IndexFRDCA", Suffix, Conn, Trans, DataDeletionStatus.Exists);
		}

		public static DbDataAdapter GetIndexFRDImagemCADataAdapter(string Suffix, IDbConnection Conn, IDbTransaction Trans)
		{
			return GetDataAdapter("IndexFRDImagemCA", Suffix, Conn, Trans, DataDeletionStatus.Exists);
		}

		public static DbDataAdapter GetIso15924DataAdapter(string Suffix, IDbConnection Conn, IDbTransaction Trans)
		{
			return GetDataAdapter("Iso15924", Suffix, Conn, Trans, DataDeletionStatus.Exists);
		}

		public static DbDataAdapter GetIso3166DataAdapter(string Suffix, IDbConnection Conn, IDbTransaction Trans)
		{
			return GetDataAdapter("Iso3166", Suffix, Conn, Trans, DataDeletionStatus.Exists);
		}

		public static DbDataAdapter GetIso639DataAdapter(string Suffix, IDbConnection Conn, IDbTransaction Trans)
		{
			return GetDataAdapter("Iso639", Suffix, Conn, Trans, DataDeletionStatus.Exists);
		}

		public static DbDataAdapter GetNivelDataAdapter(string Suffix, IDbConnection Conn, IDbTransaction Trans)
		{
			return GetDataAdapter("Nivel", Suffix, Conn, Trans, DataDeletionStatus.Exists);
		}

		public static DbDataAdapter GetNivelControloAutDataAdapter(string Suffix, IDbConnection Conn, IDbTransaction Trans)
		{
			return GetDataAdapter("NivelControloAut", Suffix, Conn, Trans, DataDeletionStatus.Exists);
		}

		public static DbDataAdapter GetNivelDesignadoDataAdapter(string Suffix, IDbConnection Conn, IDbTransaction Trans)
		{
			return GetDataAdapter("NivelDesignado", Suffix, Conn, Trans, DataDeletionStatus.Exists);
		}

		public static DbDataAdapter GetNivelUnidadeFisicaDataAdapter(string Suffix, IDbConnection Conn, IDbTransaction Trans)
		{
			return GetDataAdapter("NivelUnidadeFisica", Suffix, Conn, Trans, DataDeletionStatus.Exists);
		}

		public static DbDataAdapter GetNivelUnidadeFisicaCodigoDataAdapter(string Suffix, IDbConnection Conn, IDbTransaction Trans)
		{
			return GetDataAdapter("NivelUnidadeFisicaCodigo", Suffix, Conn, Trans, DataDeletionStatus.Exists);
		}

		public static DbDataAdapter GetProductFunctionDataAdapter(string Suffix, IDbConnection Conn, IDbTransaction Trans)
		{
			return GetDataAdapter("ProductFunction", Suffix, Conn, Trans, DataDeletionStatus.Exists);
		}

		public static DbDataAdapter GetRelacaoHierarquicaDataAdapter(string Suffix, IDbConnection Conn, IDbTransaction Trans)
		{
			return GetDataAdapter("RelacaoHierarquica", Suffix, Conn, Trans, DataDeletionStatus.Exists);
		}

		public static DbDataAdapter GetRelacaoTipoNivelRelacionadoDataAdapter(string Suffix, IDbConnection Conn, IDbTransaction Trans)
		{
			return GetDataAdapter("RelacaoTipoNivelRelacionado", Suffix, Conn, Trans, DataDeletionStatus.Exists);
		}

		public static DbDataAdapter GetSecurableObjectDataAdapter(string Suffix, IDbConnection Conn, IDbTransaction Trans)
		{
			return GetDataAdapter("SecurableObject", Suffix, Conn, Trans, DataDeletionStatus.Exists);
		}

		public static DbDataAdapter GetSecurableObjectNivelDataAdapter(string Suffix, IDbConnection Conn, IDbTransaction Trans)
		{
			return GetDataAdapter("SecurableObjectNivel", Suffix, Conn, Trans, DataDeletionStatus.Exists);
		}

		public static DbDataAdapter GetSFRDAvaliacaoDataAdapter(string Suffix, IDbConnection Conn, IDbTransaction Trans)
		{
			return GetDataAdapter("SFRDAvaliacao", Suffix, Conn, Trans, DataDeletionStatus.Exists);
		}

		public static DbDataAdapter GetSFRDAvaliacaoRelDataAdapter(string Suffix, IDbConnection Conn, IDbTransaction Trans)
		{
			return GetDataAdapter("SFRDAvaliacaoRel", Suffix, Conn, Trans, DataDeletionStatus.Exists);
		}

		public static DbDataAdapter GetSFRDCondicaoDeAcessoDataAdapter(string Suffix, IDbConnection Conn, IDbTransaction Trans)
		{
			return GetDataAdapter("SFRDCondicaoDeAcesso", Suffix, Conn, Trans, DataDeletionStatus.Exists);
		}

		public static DbDataAdapter GetSFRDConteudoEEstruturaDataAdapter(string Suffix, IDbConnection Conn, IDbTransaction Trans)
		{
			return GetDataAdapter("SFRDConteudoEEstrutura", Suffix, Conn, Trans, DataDeletionStatus.Exists);
		}

		public static DbDataAdapter GetSFRDContextoDataAdapter(string Suffix, IDbConnection Conn, IDbTransaction Trans)
		{
			return GetDataAdapter("SFRDContexto", Suffix, Conn, Trans, DataDeletionStatus.Exists);
		}

		public static DbDataAdapter GetSFRDCotaDataAdapter(string Suffix, IDbConnection Conn, IDbTransaction Trans)
		{
			return GetDataAdapter("SFRDCota", Suffix, Conn, Trans, DataDeletionStatus.Exists);
		}

		public static DbDataAdapter GetSFRDDatasProducaoDataAdapter(string Suffix, IDbConnection Conn, IDbTransaction Trans)
		{
			return GetDataAdapter("SFRDDatasProducao", Suffix, Conn, Trans, DataDeletionStatus.Exists);
		}

		public static DbDataAdapter GetSFRDUFDescricaoFisicaDataAdapter(string Suffix, IDbConnection Conn, IDbTransaction Trans)
		{
			return GetDataAdapter("SFRDUFDescricaoFisica", Suffix, Conn, Trans, DataDeletionStatus.Exists);
		}

		public static DbDataAdapter GetSFRDOIDescricaoFisicaDataAdapter(string Suffix, IDbConnection Conn, IDbTransaction Trans)
		{
			return GetDataAdapter("SFRDOIDescricaoFisica", Suffix, Conn, Trans, DataDeletionStatus.Exists);
		}

		public static DbDataAdapter GetSFRDDocumentacaoAssociadaDataAdapter(string Suffix, IDbConnection Conn, IDbTransaction Trans)
		{
			return GetDataAdapter("SFRDDocumentacaoAssociada", Suffix, Conn, Trans, DataDeletionStatus.Exists);
		}

		public static DbDataAdapter GetSFRDEstadoDeConservacaoDataAdapter(string Suffix, IDbConnection Conn, IDbTransaction Trans)
		{
			return GetDataAdapter("SFRDEstadoDeConservacao", Suffix, Conn, Trans, DataDeletionStatus.Exists);
		}

		public static DbDataAdapter GetSFRDFormaSuporteAcondDataAdapter(string Suffix, IDbConnection Conn, IDbTransaction Trans)
		{
			return GetDataAdapter("SFRDFormaSuporteAcond", Suffix, Conn, Trans, DataDeletionStatus.Exists);
		}

		public static DbDataAdapter GetSFRDImagemDataAdapter(string Suffix, IDbConnection Conn, IDbTransaction Trans)
		{
			return GetDataAdapter("SFRDImagem", Suffix, Conn, Trans, DataDeletionStatus.Exists);
		}

		public static DbDataAdapter GetSFRDImagemVolumeDataAdapter(string Suffix, IDbConnection Conn, IDbTransaction Trans)
		{
			return GetDataAdapter("SFRDImagemVolume", Suffix, Conn, Trans, DataDeletionStatus.Exists);
		}

		public static DbDataAdapter GetSFRDMaterialDeSuporteDataAdapter(string Suffix, IDbConnection Conn, IDbTransaction Trans)
		{
			return GetDataAdapter("SFRDMaterialDeSuporte", Suffix, Conn, Trans, DataDeletionStatus.Exists);
		}

		public static DbDataAdapter GetSFRDNotaGeralDataAdapter(string Suffix, IDbConnection Conn, IDbTransaction Trans)
		{
			return GetDataAdapter("SFRDNotaGeral", Suffix, Conn, Trans, DataDeletionStatus.Exists);
		}

		public static DbDataAdapter GetSFRDOrdenacaoDataAdapter(string Suffix, IDbConnection Conn, IDbTransaction Trans)
		{
			return GetDataAdapter("SFRDOrdenacao", Suffix, Conn, Trans, DataDeletionStatus.Exists);
		}

		public static DbDataAdapter GetSFRDTecnicasDeRegistoDataAdapter(string Suffix, IDbConnection Conn, IDbTransaction Trans)
		{
			return GetDataAdapter("SFRDTecnicasDeRegisto", Suffix, Conn, Trans, DataDeletionStatus.Exists);
		}

		public static DbDataAdapter GetSFRDTradicaoDocumentalDataAdapter(string Suffix, IDbConnection Conn, IDbTransaction Trans)
		{
			return GetDataAdapter("SFRDTradicaoDocumental", Suffix, Conn, Trans, DataDeletionStatus.Exists);
		}

		public static DbDataAdapter GetSFRDUnidadeFisicaDataAdapter(string Suffix, IDbConnection Conn, IDbTransaction Trans)
		{
			return GetDataAdapter("SFRDUnidadeFisica", Suffix, Conn, Trans, DataDeletionStatus.Exists);
		}

		public static DbDataAdapter GetTipoControloAutFormaDataAdapter(string Suffix, IDbConnection Conn, IDbTransaction Trans)
		{
			return GetDataAdapter("TipoControloAutForma", Suffix, Conn, Trans, DataDeletionStatus.Exists);
		}

		public static DbDataAdapter GetTipoControloAutRelDataAdapter(string Suffix, IDbConnection Conn, IDbTransaction Trans)
		{
			return GetDataAdapter("TipoControloAutRel", Suffix, Conn, Trans, DataDeletionStatus.Exists);
		}

		public static DbDataAdapter GetTipoDensidadeDataAdapter(string Suffix, IDbConnection Conn, IDbTransaction Trans)
		{
			return GetDataAdapter("TipoDensidade", Suffix, Conn, Trans, DataDeletionStatus.Exists);
		}

		public static DbDataAdapter GetTipoEntidadeProdutoraDataAdapter(string Suffix, IDbConnection Conn, IDbTransaction Trans)
		{
			return GetDataAdapter("TipoEntidadeProdutora", Suffix, Conn, Trans, DataDeletionStatus.Exists);
		}

		public static DbDataAdapter GetTipoEstadoDeConservacaoDataAdapter(string Suffix, IDbConnection Conn, IDbTransaction Trans)
		{
			return GetDataAdapter("TipoEstadoDeConservacao", Suffix, Conn, Trans, DataDeletionStatus.Exists);
		}

		public static DbDataAdapter GetTipoFormaSuporteAcondDataAdapter(string Suffix, IDbConnection Conn, IDbTransaction Trans)
		{
			return GetDataAdapter("TipoFormaSuporteAcond", Suffix, Conn, Trans, DataDeletionStatus.Exists);
		}

		public static DbDataAdapter GetTipoFRDBaseDataAdapter(string Suffix, IDbConnection Conn, IDbTransaction Trans)
		{
			return GetDataAdapter("TipoFRDBase", Suffix, Conn, Trans, DataDeletionStatus.Exists);
		}

		public static DbDataAdapter GetTipoFunctionDataAdapter(string Suffix, IDbConnection Conn, IDbTransaction Trans)
		{
			return GetDataAdapter("TipoFunction", Suffix, Conn, Trans, DataDeletionStatus.Exists);
		}

		public static DbDataAdapter GetTipoFunctionGroupDataAdapter(string Suffix, IDbConnection Conn, IDbTransaction Trans)
		{
			return GetDataAdapter("TipoFunctionGroup", Suffix, Conn, Trans, DataDeletionStatus.Exists);
		}

		public static DbDataAdapter GetTipoMaterialDeSuporteDataAdapter(string Suffix, IDbConnection Conn, IDbTransaction Trans)
		{
			return GetDataAdapter("TipoMaterialDeSuporte", Suffix, Conn, Trans, DataDeletionStatus.Exists);
		}

		public static DbDataAdapter GetTipoMedidaDataAdapter(string Suffix, IDbConnection Conn, IDbTransaction Trans)
		{
			return GetDataAdapter("TipoMedida", Suffix, Conn, Trans, DataDeletionStatus.Exists);
		}

		public static DbDataAdapter GetTipoNivelDataAdapter(string Suffix, IDbConnection Conn, IDbTransaction Trans)
		{
			return GetDataAdapter("TipoNivel", Suffix, Conn, Trans, DataDeletionStatus.Exists);
		}

		public static DbDataAdapter GetTipoNivelRelacionadoDataAdapter(string Suffix, IDbConnection Conn, IDbTransaction Trans)
		{
			return GetDataAdapter("TipoNivelRelacionado", Suffix, Conn, Trans, DataDeletionStatus.Exists);
		}

		public static DbDataAdapter GetTipoNoticiaAutDataAdapter(string Suffix, IDbConnection Conn, IDbTransaction Trans)
		{
			return GetDataAdapter("TipoNoticiaAut", Suffix, Conn, Trans, DataDeletionStatus.Exists);
		}

		public static DbDataAdapter GetTipoNoticiaATipoControloAFormaDataAdapter(string Suffix, IDbConnection Conn, IDbTransaction Trans)
		{
			return GetDataAdapter("TipoNoticiaATipoControloAForma", Suffix, Conn, Trans, DataDeletionStatus.Exists);
		}

		public static DbDataAdapter GetTipoOperationDataAdapter(string Suffix, IDbConnection Conn, IDbTransaction Trans)
		{
			return GetDataAdapter("TipoOperation", Suffix, Conn, Trans, DataDeletionStatus.Exists);
		}

		public static DbDataAdapter GetTipoOrdenacaoDataAdapter(string Suffix, IDbConnection Conn, IDbTransaction Trans)
		{
			return GetDataAdapter("TipoOrdenacao", Suffix, Conn, Trans, DataDeletionStatus.Exists);
		}

		public static DbDataAdapter GetTipoPertinenciaDataAdapter(string Suffix, IDbConnection Conn, IDbTransaction Trans)
		{
			return GetDataAdapter("TipoPertinencia", Suffix, Conn, Trans, DataDeletionStatus.Exists);
		}

		public static DbDataAdapter GetTipoServerDataAdapter(string Suffix, IDbConnection Conn, IDbTransaction Trans)
		{
			return GetDataAdapter("TipoServer", Suffix, Conn, Trans, DataDeletionStatus.Exists);
		}

		public static DbDataAdapter GetTipoClientDataAdapter(string Suffix, IDbConnection Conn, IDbTransaction Trans)
		{
			return GetDataAdapter("TipoClient", Suffix, Conn, Trans, DataDeletionStatus.Exists);
		}

		public static DbDataAdapter GetTipoQuantidadeDataAdapter(string Suffix, IDbConnection Conn, IDbTransaction Trans)
		{
			return GetDataAdapter("TipoQuantidade", Suffix, Conn, Trans, DataDeletionStatus.Exists);
		}

		public static DbDataAdapter GetTipoSubDensidadeDataAdapter(string Suffix, IDbConnection Conn, IDbTransaction Trans)
		{
			return GetDataAdapter("TipoSubDensidade", Suffix, Conn, Trans, DataDeletionStatus.Exists);
		}

		public static DbDataAdapter GetTipoTecnicasDeRegistoDataAdapter(string Suffix, IDbConnection Conn, IDbTransaction Trans)
		{
			return GetDataAdapter("TipoTecnicasDeRegisto", Suffix, Conn, Trans, DataDeletionStatus.Exists);
		}

		public static DbDataAdapter GetTipoTradicaoDocumentalDataAdapter(string Suffix, IDbConnection Conn, IDbTransaction Trans)
		{
			return GetDataAdapter("TipoTradicaoDocumental", Suffix, Conn, Trans, DataDeletionStatus.Exists);
		}

		public static DbDataAdapter GetTrusteeDataAdapter(string Suffix, IDbConnection Conn, IDbTransaction Trans)
		{
			return GetDataAdapter("Trustee", Suffix, Conn, Trans, DataDeletionStatus.Exists);
		}

		public static DbDataAdapter GetTrusteeGroupDataAdapter(string Suffix, IDbConnection Conn, IDbTransaction Trans)
		{
			return GetDataAdapter("TrusteeGroup", Suffix, Conn, Trans, DataDeletionStatus.Exists);
		}

		public static DbDataAdapter GetTrusteePrivilegeDataAdapter(string Suffix, IDbConnection Conn, IDbTransaction Trans)
		{
			return GetDataAdapter("TrusteePrivilege", Suffix, Conn, Trans, DataDeletionStatus.Exists);
		}

		public static DbDataAdapter GetTrusteeUserDataAdapter(string Suffix, IDbConnection Conn, IDbTransaction Trans)
		{
			return GetDataAdapter("TrusteeUser", Suffix, Conn, Trans, DataDeletionStatus.Exists);
		}

		public static DbDataAdapter GetUserGroupsDataAdapter(string Suffix, IDbConnection Conn, IDbTransaction Trans)
		{
			return GetDataAdapter("UserGroups", Suffix, Conn, Trans, DataDeletionStatus.Exists);
		}

		public static DbDataAdapter GetGlobalConfigDataAdapter(string Suffix, IDbConnection Conn, IDbTransaction Trans)
		{
			return GetDataAdapter("GlobalConfig", Suffix, Conn, Trans, DataDeletionStatus.Exists);
		}
#endregion

		public static void LoadStaticDataTables(GISADataset CurrentDataSet)
		{
			DataBaseLayer dbLayer = GetDBLayer();
			DataBaseLayer.ConnectionHolder ch = dbLayer.HoldConnection();
			try 
			{
				// Normas de países, línguas, caligrafia...
				GetIso15924DataAdapter(null, null, null).Fill(CurrentDataSet.Iso15924);
				GetIso3166DataAdapter(null, null, null).Fill(CurrentDataSet.Iso3166);
				GetIso639DataAdapter(null, null, null).Fill(CurrentDataSet.Iso639);

				// Configuracoes
				GetGlobalConfigDataAdapter(null, null, null).Fill(CurrentDataSet.GlobalConfig);

				// Conjunto de Privilégios da Aplicação
				// Identificam acesso a Módulos da aplicação ou funcionalidades dentro destes, não a dados.
				GetTipoFunctionGroupDataAdapter(null, null, null).Fill(CurrentDataSet.TipoFunctionGroup);
				GetTipoFunctionDataAdapter(null, null, null).Fill(CurrentDataSet.TipoFunction);
				GetTipoOperationDataAdapter(null, null, null).Fill(CurrentDataSet.TipoOperation);
				GetFunctionOperationDataAdapter(null, null, null).Fill(CurrentDataSet.FunctionOperation);

				// Tipos de producto existentes e funcionalidades proprias de cada um
				GetTipoServerDataAdapter(null, null, null).Fill(CurrentDataSet.TipoServer);
				GetTipoClientDataAdapter(null, null, null).Fill(CurrentDataSet.TipoClient);
				GetProductFunctionDataAdapter(null, null, null).Fill(CurrentDataSet.ProductFunction);

				//Enumerados utilizados em foreign keys
				GetTipoControloAutFormaDataAdapter(null, null, null).Fill(CurrentDataSet.TipoControloAutForma);
				GetTipoControloAutRelDataAdapter(null, null, null).Fill(CurrentDataSet.TipoControloAutRel);
				GetTipoDensidadeDataAdapter(null, null, null).Fill(CurrentDataSet.TipoDensidade);
				GetTipoEntidadeProdutoraDataAdapter(null, null, null).Fill(CurrentDataSet.TipoEntidadeProdutora);
				GetTipoEstadoDeConservacaoDataAdapter(null, null, null).Fill(CurrentDataSet.TipoEstadoDeConservacao);
				GetTipoFormaSuporteAcondDataAdapter(null, null, null).Fill(CurrentDataSet.TipoFormaSuporteAcond);
				GetTipoFRDBaseDataAdapter(null, null, null).Fill(CurrentDataSet.TipoFRDBase);
				GetTipoMaterialDeSuporteDataAdapter(null, null, null).Fill(CurrentDataSet.TipoMaterialDeSuporte);
				GetTipoMedidaDataAdapter(null, null, null).Fill(CurrentDataSet.TipoMedida);
				GetTipoNivelDataAdapter(null, null, null).Fill(CurrentDataSet.TipoNivel);
				GetTipoNivelRelacionadoDataAdapter(null, null, null).Fill(CurrentDataSet.TipoNivelRelacionado);
				GetRelacaoTipoNivelRelacionadoDataAdapter(null, null, null).Fill(CurrentDataSet.RelacaoTipoNivelRelacionado);
				GetTipoNoticiaAutDataAdapter(null, null, null).Fill(CurrentDataSet.TipoNoticiaAut);
				GetTipoNoticiaATipoControloAFormaDataAdapter(null, null, null).Fill(CurrentDataSet.TipoNoticiaATipoControloAForma);
				GetTipoOrdenacaoDataAdapter(null, null, null).Fill(CurrentDataSet.TipoOrdenacao);
				GetTipoPertinenciaDataAdapter(null, null, null).Fill(CurrentDataSet.TipoPertinencia);
				GetTipoQuantidadeDataAdapter(null, null, null).Fill(CurrentDataSet.TipoQuantidade);
				GetTipoSubDensidadeDataAdapter(null, null, null).Fill(CurrentDataSet.TipoSubDensidade);
				GetTipoTecnicasDeRegistoDataAdapter(null, null, null).Fill(CurrentDataSet.TipoTecnicasDeRegisto);
				GetTipoTradicaoDocumentalDataAdapter(null, null, null).Fill(CurrentDataSet.TipoTradicaoDocumental);

				// Autos de eliminação
				GetAutoEliminacaoDataAdapter(null, null, null).Fill(CurrentDataSet.AutoEliminacao);
			} 
			finally 
			{
				DataBaseLayer.DisposeConnection(ch);
			}
		}

#region IDisposable Members
		
		public abstract void Dispose();
		
#endregion
	}

	public class ConnectionStateChangedEventArgs: System.EventArgs 
	{
		private bool wasClosed = true;

		public ConnectionStateChangedEventArgs(bool wClosed) 
		{
			wasClosed = wClosed;
		}

		public bool WasClosed 
		{
			get 
			{
				return wasClosed;
			}
			set 
			{
				wasClosed = value;
			}
		}
	}

	[TestFixture]
	public class Test {
		[Test]
		public void Create() {
			DAL.ConnectionBuilder = new SqlClientBuilder();

			Assert.AreSame(typeof(SqlClientNivelDAL), NivelDal.CurrentNivelDAL);
			Assert.AreEqual(1, NivelDal.CurrentNivelDAL.Count);

			DAL.ConnectionBuilder = new OleDbBuilder();
			Assert.AreSame(typeof(OleDbNivelDAL), NivelDal.CurrentNivelDAL);
			Assert.AreEqual(2, NivelDal.CurrentNivelDAL.Count);
		}
	}
}