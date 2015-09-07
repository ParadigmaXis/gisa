//INSTANT C# NOTE: Formerly VB.NET project-level imports:
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;

#if DEBUG
using NUnit.Framework;
#endif
using System.Data.Common;
using System.Windows.Forms;
using System.Text;
using DBAbstractDataLayer.DataAccessRules;
using System.Reflection;

namespace GISA.Model
{
	public class GisaDataSetHelper
	{

		public static BooleanSwitch SqlTrace = new BooleanSwitch("GISAModelSqlTrace", "Controls SQL statement trace output.");

		public class HoldOpen : IDisposable
		{
			private IDbConnection mConn;
			private bool mWasOpen;

			public HoldOpen(IDbConnection Conn)
			{
				mWasOpen = (Conn.State == ConnectionState.Open);
				mConn = Conn;
				if (! mWasOpen)
					mConn.Open();
			}

			public void Dispose()
			{
				if (! mWasOpen)
					mConn.Close();
			}

			public IDbConnection Connection
			{
				get {return mConn;}
			}

            void IDisposable.Dispose()
            {
                Dispose();
            }
        }

		// Collects related rows based on an initial set of rows from a table.
		// Note that it doesn't find DataRowState.Deleted rows as these are not accessible through GetChildRows(...).
		public static DataRow[] CollectDependentRows(DataTable DataTable, DataRow[] DataRows)
		{
			ArrayList Result = new ArrayList();
			foreach (DataRow datarow in DataRows){
				foreach (DataRelation Relation in DataTable.ChildRelations)	{
					Result.AddRange(CollectDependentRows(Relation.ChildTable, datarow.GetChildRows(Relation)));
				}
				Result.Add(datarow);
			}

			DataRow[] ResultRows = null;
			ResultRows = new DataRow[Result.Count];
			Result.CopyTo(ResultRows, 0);

			return ResultRows;
		}

		public delegate void VisitIndexFRDCAHandler(GISADataset.IndexFRDCARow IndexFRDCA);
		public static void VisitIndexFRDCA(GISADataset.FRDBaseRow FRDBase, VisitIndexFRDCAHandler Callback)
		{
            GisaDataSetHelper.GetInstance().IndexFRDCA.Cast<GISADataset.IndexFRDCARow>().Where(r => r.RowState != DataRowState.Deleted && r.IDFRDBase == FRDBase.ID).ToList().ForEach(idx => Callback(idx));
		}

		public delegate void VisitControloAutDicionarioHandler(GISADataset.ControloAutDicionarioRow ControloAutDicionario);
		public static void VisitControloAutDicionario(GISADataset.ControloAutRow ControloAut, VisitControloAutDicionarioHandler Callback)
		{
			foreach (GISADataset.ControloAutDicionarioRow cad in (GISADataset.ControloAutDicionarioRow[])(GisaDataSetHelperRule.Current.selectControloAutDicionario(GisaDataSetHelper.GetInstance(), ControloAut.ID)))
				Callback(cad);
		}

		public static DataRow[] GetListViewTagRows(ListView lv)
		{
			int i = 0;
			DataRow[] idxs = null;

			idxs = new DataRow[lv.Items.Count];
			foreach (ListViewItem li in lv.Items)
			{
				idxs[i] = (DataRow)li.Tag;
				i = i + 1;
			}
			return idxs;
		}

		private static void VerifyContents(DataTable DataTable)
		{
			if (DataTable.Rows.Count == 0)
				Trace.WriteLine(DataTable.TableName + " is empty [FAIL].");
		}

		private static GISADataset mDataSet;
		public static GISADataset GetInstance()
		{
			if (mDataSet == null)
			{
				mDataSet = new GISADataset();

	#if DEBUG
				mDataSet.EnforceConstraints = false;
	#else
				mDataSet.EnforceConstraints = false;
	#endif

				//' This ensures that autonumber fields do not clash with database information
				foreach (DataTable t in mDataSet.Tables)
				{
					foreach (DataColumn c in t.Columns)
					{
						if (c.AutoIncrement)
						{
							c.AutoIncrementSeed = -1;
							c.AutoIncrementStep = -1;
						}
					}
				}

				// Carregar ficheiros xml embebidos no Gisa.Model contendo o meta model
				Assembly metaModelAssembly = Assembly.GetAssembly(typeof(MetaModelHelper));
				MetaModelHelper.MetaModel = new System.Xml.XmlDocument();
				MetaModelHelper.MetaModel.Load(metaModelAssembly.GetManifestResourceStream(metaModelAssembly.GetName().Name + ".MetaModel.GISADataset.xml"));
                MetaModelHelper.DataTypesDictionary = new System.Xml.XmlDocument();
                MetaModelHelper.DataTypesDictionary.Load(metaModelAssembly.GetManifestResourceStream(metaModelAssembly.GetName().Name + ".MetaModel.DataTypesDictionary.xml"));
                DBAbstractDataLayer.DataAccessRules.DALRule.MetaModel = new MetaModelHelper();

				IDbConnection conn = GisaDataSetHelper.GetConnection();
				try
				{
					conn.Open();
					GisaDataSetHelperRule.Current.LoadStaticDataTables(mDataSet, conn);
				}
				catch (System.SystemException ex)
				{
					// tratar as excepções de acesso à base de dados mantendo o dataset a null
					mDataSet = null;
					Trace.WriteLine(ex);
				}
				finally
				{
					conn.Close();
				}
			}
			return mDataSet;
		}

		public static void CreateDerivedColumn(GISADataset CurrentDataSet, string Target, string Source, string ColumnName, string RelationName)
		{
			if (CurrentDataSet == null)
				throw new ArgumentNullException("CurrentDataSet");

			if (! (CurrentDataSet.Tables.Contains(Target)))
				throw new ArgumentException("DataTable " + Target + " not found.");

			if (! (CurrentDataSet.Tables.Contains(Source)))
				throw new ArgumentException("DataTable " + Source + " not found.");

			if (! (CurrentDataSet.Tables[Source].Columns.Contains(ColumnName)))
				throw new ArgumentException("DataColumn " + Source + "." + ColumnName + " not found.");

			if (! (CurrentDataSet.Relations.Contains(RelationName)))
				throw new ArgumentException("DataRelation " + RelationName + " not found.");

			string TargetColumnName = Source + ColumnName;
			if (CurrentDataSet.Tables[Target].Columns.Contains(TargetColumnName))
				return;

			CurrentDataSet.Tables[Target].Columns.Add(new DataColumn(TargetColumnName, CurrentDataSet.Tables[Source].Columns[ColumnName].DataType, "Parent(" + RelationName + ")." + ColumnName));
		}

	#region DAL
	
		//esta variavel evita que a instrução addhandler seja executada sempre que o getconnection é chamado
		private static IDbConnection connection = null;
		public static IDbConnection GetConnection()
		{
			if (connection == null)
			{									
			    DBAbstractDataLayer.DataAccessRules.DALRule.ConnectionBuilder = new DBAbstractDataLayer.ConnectionBuilders.SqlClientBuilder();
			    DBAbstractDataLayer.DataAccessRules.DALRule.ConnectionBuilder.ConnectionStateChanged += new System.Data.StateChangeEventHandler(Connection_StateChange);
			    connection = DBAbstractDataLayer.DataAccessRules.DALRule.ConnectionBuilder.Connection;														
			}
			return connection;
		}

		public static IDbConnection GetTempConnection()
		{
			return DBAbstractDataLayer.DataAccessRules.DALRule.ConnectionBuilder.TempConnection;
		}


		public static IDbTransaction GetNewTransaction()
		{
			return GetNewTransaction(IsolationLevel.Serializable);
		}

//INSTANT C# NOTE: C# does not support optional parameters. Overloaded method(s) are created above.
//ORIGINAL LINE: Public Shared Function GetNewTransaction(Optional ByVal isolation As IsolationLevel = IsolationLevel.Serializable) As IDbTransaction
		public static IDbTransaction GetNewTransaction(IsolationLevel isolation)
		{
			return GetConnection().BeginTransaction(isolation);
		}

		public static IsolationLevel GetTransactionIsolationLevel()
		{
			return DBAbstractDataLayer.DataAccessRules.DALRule.ConnectionBuilder.TransactionIsolationLevel;
		}

		public delegate void ConnectionStateChangedEventHandler(bool isOpen);
		public static event ConnectionStateChangedEventHandler ConnectionStateChanged;

		private static bool wasClosed = true;
		public static void Connection_StateChange(object sender, System.Data.StateChangeEventArgs e) {
			bool isClosed = e.CurrentState == ConnectionState.Closed;
			if (isClosed ^ wasClosed)
			{
				if (ConnectionStateChanged != null)
					ConnectionStateChanged(!isClosed);
			}
			wasClosed = isClosed;
		}
	#endregion

		public static void ManageDatasetConstraints(bool @switch)
		{
            IDbConnection conn = GisaDataSetHelper.GetTempConnection();
            conn.Open();
            try
            {
#if DEBUG
                long start = DateTime.Now.Ticks;

                try
                {
                    GisaDataSetHelper.GetInstance().EnforceConstraints = @switch;
                }
                catch (ConstraintException ex)
                {
                    Trace.WriteLine("<EnforceContraints>");
                    Trace.WriteLine(ex.ToString());
                    GisaDataSetHelper.FixDataSet(GisaDataSetHelper.GetInstance(), conn);
                }
                catch (Exception e)
                {
                    Trace.WriteLine(e.ToString());
                    throw;
                }

                if (@switch)
                    Trace.WriteLine("EnforceContraints: " + new TimeSpan(DateTime.Now.Ticks - start).ToString());

#else
                if (GisaDataSetHelper.GetInstance().EnforceConstraints)
                {
                    //Trace.WriteLine("RELEASE MODE: EnforceConstraints = TRUE");
                    GisaDataSetHelper.GetInstance().EnforceConstraints = false;
                }
#endif
            }
            finally
            {
                conn.Close();
            }

        }

	#region Foreign key resolution
		private static void FixTable(DataTable DataTable, IDbConnection conn)
		{
			if (DataTable.HasErrors)
			{
				Trace.WriteLine(string.Format("Fixing table {0}...", DataTable.TableName));

				foreach (DataRow r in DataTable.GetErrors())
				{
					if (r.RowError.Length > 0)
					{
						Trace.WriteLine("Fixing row error: " + r.RowError);
					}
					else
					{
						Trace.WriteLine("Fixing row error: (no error description)");
					}
					GisaDataSetHelperRule.Current.FixRow(GisaDataSetHelper.GetInstance(), r, conn);
				}
			}
		}

		public static void FixDataSet(DataSet DataSet, IDbConnection conn)
		{
			int IterationLimit = 10;
			Trace.WriteLine("Fixing rows...");
			while (DataSet.HasErrors)
			{
				foreach (DataTable t in DataSet.Tables)
				{
					FixTable(t, conn);
				}
				if (! DataSet.EnforceConstraints)
				{
					Trace.WriteLine("Enabling constraints...");
					try
					{
						DataSet.EnforceConstraints = true;
					}
					catch (ConstraintException Ex)
					{
						IterationLimit = IterationLimit - 1;
						if (IterationLimit <= 0)
						{
							throw Ex;
						}
						Trace.WriteLine("Fixing more rows...");
					}
				}
			}
		}

		public static void DumpRowErrors()
		{
			DataSet dataSet = GetInstance();
			DataRow row = null;
			foreach (DataTable table in dataSet.Tables)
			{
				if (table.HasErrors)
				{
					Trace.WriteLine(" :: Errors found for table \"" + table.TableName + "\" :: ");
				}
				foreach (DataRow rowWithinLoop in table.GetErrors())
				{
				row = rowWithinLoop;
					StringBuilder colErrors = null;
					foreach (DataColumn col in rowWithinLoop.GetColumnsInError())
					{
						if (colErrors.Length != 0)
						{
							colErrors.Append(", ");
						}
						colErrors.Append(col.ColumnName);
					}
					Trace.WriteLine(string.Format("Found error in row envolving column(s) {0}: {1}", colErrors, rowWithinLoop.RowError));
				}
			}
		}

	#endregion
		
		public enum TipoControloAutForma: int
		{
			FormaAutorizada = 1
		}

		//This method recieves an array of DataRows and checks for modified rows
		public static bool hasModifiedRow(DataRow[] rows)
		{
			foreach (DataRow row in rows)
			{
				if (row.RowState != DataRowState.Unchanged)
					return true;
			}
			return false;
		}

		public static string GetDBNullableText(DataRow row, string field)
		{
			if (row[field] == DBNull.Value)
				return "";
			else
				return (string)(row[field]);
		}

		public static string GetDBNullableText(ref IDataReader dataReader, int index)
		{
			if (! (dataReader.IsDBNull(index)))
				return dataReader.GetValue(index).ToString();
			else
				return "";
		}

		public static bool GetDBNullableBoolean(ref IDataReader dataReader, int index)
		{
            if (! (dataReader.IsDBNull(index)))
				return System.Convert.ToBoolean(dataReader.GetValue(index));
			else
				return false;
		}

		public static bool UsingNiveisOrganicos()
		{
			return ((GISADataset.GlobalConfigRow)(GisaDataSetHelper.GetInstance().GlobalConfig.Select()[0])).NiveisOrganicos;
		}

		public static bool UsingGestaoIntegrada()
		{
			return ((GISADataset.GlobalConfigRow)(GisaDataSetHelper.GetInstance().GlobalConfig.Select()[0])).GestaoIntegrada;
		}

        public static string EscapeLikeValue(string value)
        {
            StringBuilder sb = new StringBuilder(value.Length);
            for (int i = 0; i < value.Length; i++)
            {
                char c = value[i];
                switch (c)
                {
                    case ']':
                    case '[':
                    case '%':
                        if (i>0 && i<(value.Length-1))
                            sb.Append("[").Append(c).Append("]");
                        else
                            sb.Append(c);
                        break;
                    case '*':
                        sb.Append("[").Append(c).Append("]");
                        break;
                    case '\'':
                        sb.Append("''");
                        break;
                    default:
                        sb.Append(c);
                        break;
                }
            }
            return sb.ToString();
        }
	}

	#if DEBUG
//<TestFixture()> Public Class TestGisaDataSetHelper
//    <Test()> Public Sub Connection()
//        Assertion.AssertNotNull(GisaDataSetHelper.GetConnection())
//        Assertion.AssertSame(GisaDataSetHelper.GetConnection(), GisaDataSetHelper.GetConnection())
//        GisaDataSetHelper.GetConnection().Open()
//        GisaDataSetHelper.GetConnection().Close()
//    End Sub
//    <Test()> Public Sub SingletonInstance()
//        Assertion.AssertNotNull(GisaDataSetHelper.GetInstance())
//        Assertion.AssertSame(GisaDataSetHelper.GetInstance(), GisaDataSetHelper.GetInstance())
//    End Sub
//    <Test()> Public Sub GetRowCount()
//        Dim ds As GISADataset = New GISADataset
//        ds.Merge(GisaDataSetHelper.GetInstance())
//        Assertion.AssertEquals(ConnectionState.Closed, GisaDataSetHelper.GetConnection().State)
//        Assertion.Assert(GisaDataSetHelper.GetRowCount("TipoNivel") > 0)
//        Assertion.AssertEquals(ConnectionState.Closed, GisaDataSetHelper.GetConnection().State)

//        GisaDataSetHelper.GetConnection().Open()
//        Assertion.Assert(GisaDataSetHelper.GetRowCount(ds.TipoNivel) > 0)
//        Assertion.AssertEquals(ConnectionState.Open, GisaDataSetHelper.GetConnection().State)
//        GisaDataSetHelper.GetConnection().Close()
//    End Sub
//    <Test()> Public Sub TableNivel()
//        Dim ds As DataSet = New DataSet
//        Dim da As DbDataAdapter = GisaDataSetHelper.GetNivelDataAdapter()
//        Assertion.AssertNotNull(ds)
//        da.Fill(ds, "Nivel")
//        Assertion.Assert(ds.Tables.Contains("Nivel"))
//        Assertion.Assert(ds.Tables("Nivel").Rows.Count >= 0)

//        ' INSERT
//        Dim r As DataRow = ds.Tables("Nivel").NewRow()
//        r.Item("ID") = -1
//        r.Item("IDTipoNivel") = 1
//        r.Item("Codigo") = "Teste"
//        ds.Tables("Nivel").Rows.Add(r)
//        Dim rr As DataRow
//        For Each rr In ds.Tables("Nivel").Rows
//            Assertion.Assert(Not rr.IsNull("ID"))
//            Assertion.AssertNotNull(rr.Item("ID"))
//        Next
//        da.Update(ds.Tables("Nivel"))

//        ' The autonumber key was retrieved
//        Assertion.Assert(Not r.Item("ID").Equals(-1))

//        Dim ds2 As DataSet = New DataSet
//        Assertion.AssertNotNull(ds2)
//        da.Fill(ds2, "Nivel")
//        Assertion.Assert(ds2.Tables.Contains("Nivel"))
//        Assertion.AssertEquals(ds.Tables("Nivel").Rows.Count, ds2.Tables("Nivel").Rows.Count)

//        ' UPDATE

//        r.Item("Codigo") = "Teste2"
//        da.Update(ds.Tables("Nivel"))

//        ds2 = New DataSet
//        da.Fill(ds2, "Nivel")
//        Assertion.AssertEquals("Teste2", ds2.Tables("Nivel").Select("ID=" + r.Item("ID").ToString())(0).Item("Codigo"))

//        ' DELETE
//        Assertion.Assert(ds2.Tables("Nivel").Select("ID=" + r.Item("ID").ToString()).Length = 1)
//        r.Delete()
//        da.Update(ds.Tables("Nivel"))

//        ds2 = New DataSet
//        Assertion.AssertNotNull(ds2)
//        da.Fill(ds2, "Nivel")
//        Assertion.Assert(ds2.Tables.Contains("Nivel"))
//        Assertion.AssertEquals(ds.Tables("Nivel").Rows.Count, ds2.Tables("Nivel").Rows.Count)
//    End Sub

//    <Test()> Public Sub SelectStatementSuffix()
//        Dim ds1 As DataSet = New DataSet
//        Dim ds2 As DataSet = New DataSet
//        Dim da1 As DbDataAdapter = GisaDataSetHelper.GetTipoNivelDataAdapter("WHERE ID IS NOT NULL")
//        Dim da2 As DbDataAdapter = GisaDataSetHelper.GetTipoNivelDataAdapter("WHERE ID IS NULL")
//        da1.Fill(ds1, "TipoNivel")
//        da2.Fill(ds2, "TipoNivel")
//        Assertion.Assert(ds1.Tables("TipoNivel").Rows.Count <> ds2.Tables("TipoNivel").Rows.Count)
//    End Sub
//End Class
	#endif

} //end of root namespace