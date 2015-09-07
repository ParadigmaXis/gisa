using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;

#if DEBUG
using NUnit.Framework;
#endif
using System.Windows.Forms;
//ORIGINAL LINE: Imports DBAbstractDataLayer.DataAccessRules.NivelRule
//INSTANT C# NOTE: The following line has been modified since C# non-aliased 'using' statements only operate on namespaces:
using DBAbstractDataLayer.DataAccessRules;

namespace GISA.Model
{
	public class Nivel
	{

	#region Delete Nivel


		

		public static void DeleteInDataSet(DataRow DeletableRow, bool onlyRemove)
		{
			DeleteInDataSet(DeletableRow, onlyRemove, null);
		}

		public static void DeleteInDataSet(DataRow DeletableRow)
		{
			DeleteInDataSet(DeletableRow, false, null);
		}

		public static void DeleteInDataSet(DataRow DeletableRow, bool onlyRemove, DataSet gBackup)
		{
            // Parte-se do pressuposto que o nivel a apagar é folha
			ArrayList open = new ArrayList();
			ArrayList closed = new ArrayList();
			open.Add(DeletableRow);
			// open -> rows por expandir
			// closed -> rows já expandidas
			while (open.Count > 0)
			{
				DataRow current = (DataRow)(open[0]);
				if (! (closed.Contains(current)))
				{
					foreach (DataRelation rel in current.Table.DataSet.Relations)
					{
						if (rel.ParentTable == current.Table)
						{
							open.AddRange(current.GetChildRows(rel, DataRowVersion.Default));
						}
					}
					closed.Add(current);
					open.Remove(current);
				}
			}
			// Remover do dataset todas as linhas encontradas começando pelo fim 
			// de forma a manter a integridade referencial

			try
			{
				for (int i = closed.Count - 1; i >= 0; i--)
				{
					DataRow current = (DataRow)(closed[i]);
					Trace.WriteLine(string.Format("{0}.{1} is Delete'ing DataRow from DataTable {2}", new System.Diagnostics.StackFrame().GetMethod().DeclaringType.FullName, new System.Diagnostics.StackFrame().GetMethod().Name, current.Table.TableName));
					if (onlyRemove)
					{
						current.Table.Rows.Remove(current);
					}
					else
					{
						if (gBackup != null)
						{
							PersistencyHelper.BackupRow(ref gBackup, current);
						}
						current.Delete();
					}
				}
			}
			catch (Exception e)
			{
				Trace.WriteLine(e.ToString());
			}
		}

        public static PersistencyHelper.SaveResult CascadeDeleteNivel(GISADataset.NivelRow NivelRow)
		{
			//DeleteNivelInDataBase(NivelRow)
			PersistencyHelper.DeleteIDXPreSaveArguments args = new PersistencyHelper.DeleteIDXPreSaveArguments();
			args.ID = NivelRow.ID;
			DeleteInDataSet(NivelRow); // é possível que esta linha não seja já precisa uma vez que o cleandeleteddata seguinte irá limpar do DS de trabalho as linhas que já não existam
            PersistencyHelper.SaveResult saveSuccess = PersistencyHelper.save(new PersistencyHelper.preSaveDelegate(DeleteNivelXInDataBase), args);
			PersistencyHelper.cleanDeletedData();
            return saveSuccess;
		}

		public static void DeleteNivelXInDataBase(PersistencyHelper.PreSaveArguments args)
		{
			DBAbstractDataLayer.DataAccessRules.NivelRule.Current.DeleteNivelInDataBase(((PersistencyHelper.DeleteIDXPreSaveArguments)args).ID, args.tran);
		}

		public static void CascadeDeleteFRD(GISADataset.FRDBaseRow FRDRow)
		{
			PersistencyHelper.DeleteIDXPreSaveArguments args = new PersistencyHelper.DeleteIDXPreSaveArguments();
			args.ID = FRDRow.ID;
			DeleteInDataSet(FRDRow);
			PersistencyHelper.save(new PersistencyHelper.preSaveDelegate(DeleteFRDBaseXInDataBase), args);
			PersistencyHelper.cleanDeletedData();
		}

		private static void DeleteFRDBaseXInDataBase(PersistencyHelper.PreSaveArguments args)
		{
			NivelRule.Current.DeleteFRDBaseInDataBase(((PersistencyHelper.DeleteIDXPreSaveArguments)args).ID, args.tran);
		}
	#endregion

		
		private static bool IsSubTipoNivel(GISADataset.TipoNivelRow TipoNivelRow)
		{
			return TipoNivelRow.ID == TipoNivelRelacionado.SSC | TipoNivelRow.ID == TipoNivelRelacionado.SSR | TipoNivelRow.ID == TipoNivelRelacionado.SD;
		}		

		public static bool IsDocumentTopLevel(GISADataset.NivelRow dr)
		{
			if (! dr.TipoNivelRow.IsDocument)
			{
				return false;
			}
			foreach (GISADataset.RelacaoHierarquicaRow nh in dr.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica())
			{
				if (nh.NivelRowByNivelRelacaoHierarquicaUpper.TipoNivelRow.IsDocument)
				{
					return false;
				}
			}
			return true;
		}


	#region  Obtenção de documentos e unidades físicas para efeitos de avaliação 

		public static string getFilterFromResults(ArrayList results)
		{
			System.Text.StringBuilder filter = new System.Text.StringBuilder("(");
			foreach (long nivelID in results)
			{
				if (filter.Length > 1)
				{
					filter.Append(", ");
				}
				filter.Append(nivelID);
			}
			if (filter.Length == 1)
			{
				filter.Append("-1");
			}
			filter.Append(")");
			return filter.ToString();
		}

	#endregion

        public static GISADataset.RelacaoHierarquicaRow[] GetChildren(GISADataset CurrentDataSet, GISADataset.NivelRow CurrentNivel)
		{

			//TODO: chamar método que faz os fill: fillChildren(CurrentDataSet, CurrentNivel.ID, conn, tran)

			GISADataset.RelacaoHierarquicaRow[] Children = CurrentNivel.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquicaUpper();
			return Children;
		}

		public static GISADataset.RelacaoHierarquicaRow[] GetSelf(GISADataset CurrentDataSet, GISADataset.NivelRow CurrentNivel)
		{
			//TODO: chamar método que faz os fill: fillSelf(CurrentDataSet, CurrentNivel.ID, conn, tran)

			return CurrentNivel.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica();
		}

		
		//TODO: FUNCAO MANTIDA PARA COMPATIBILIDADE DE CODIGO EXISTENTE, DEVERÁ DESAPARECER, SER UNIFICADA COM A ASSINATURA SEGUINTE, OU SER REFORMULADA ASSIM QUE POSSIVEL
		public static GISADataset.RelacaoHierarquicaRow[] GetParents(GISADataset CurrentDataSet, GISADataset.NivelRow CurrentNivel)
		{

			IDbConnection conn = GisaDataSetHelper.GetConnection();
			try
			{
				conn.Open();
				NivelRule.Current.LoadNivelGrandparents(CurrentNivel.ID, GisaDataSetHelper.GetInstance(), conn);
			}
			finally
			{
				conn.Close();
			}

			GISADataset.RelacaoHierarquicaRow[] Parents = null;
			ArrayList Result = new ArrayList();

			Parents = CurrentNivel.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica();
			foreach (GISADataset.RelacaoHierarquicaRow rhRow in Parents)
			{
				Result.AddRange(rhRow.NivelRowByNivelRelacaoHierarquicaUpper.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica());
			}
			return (GISADataset.RelacaoHierarquicaRow[])(Result.ToArray(typeof(GISADataset.RelacaoHierarquicaRow)));
		}

		public static GISADataset.RelacaoHierarquicaRow[] GetParentRelations(GISADataset.NivelRow CurrentNivel, GISADataset.NivelRow UpperNivel, IDbConnection connection)
		{
			try
			{
				NivelRule.Current.LoadNivelParents(UpperNivel.ID, GisaDataSetHelper.GetInstance(), connection);
			}
			catch (Exception ex)
			{
				Trace.WriteLine(ex);
				throw;
			}
			return UpperNivel.GetRelacaoHierarquicaRowsByNivelRelacaoHierarquica();
		}


		private abstract class WidthFirstVisitor
		{
			public void Run(GISADataset CurrentDataSet, GISADataset.NivelRow CurrentNivel, INivelChainVisitor CurrentVisitor)
			{
				Queue PendingNivel = new Queue();
				Stack ContextNivel = new Stack();

				PendingNivel.Enqueue(CurrentNivel);

				CurrentVisitor.InitVisit();

				while (PendingNivel.Count > 0)
				{
					GISADataset.NivelRow CursorNivel = (GISADataset.NivelRow)(PendingNivel.Dequeue());
					if (CursorNivel == null)
					{
						ContextNivel.Pop();
					}
					else
					{
						ContextNivel.Push(CursorNivel);

						GISADataset.NivelRow[] ContextNivelEx = null;
						ContextNivelEx = new GISADataset.NivelRow[ContextNivel.Count];
						ContextNivel.CopyTo(ContextNivelEx, 0);
						CurrentVisitor.Visit(CurrentDataSet, ContextNivelEx);

						foreach (GISADataset.RelacaoHierarquicaRow rhRow in GetNextNivelRows(CurrentDataSet, CursorNivel))
						{
							PendingNivel.Enqueue(rhRow.NivelRowByNivelRelacaoHierarquica);
						}
						PendingNivel.Enqueue(null);
					}
				}

				CurrentVisitor.DoneVisit();
			}
			protected abstract GISADataset.RelacaoHierarquicaRow[] GetNextNivelRows(GISADataset CurrentDataSet, GISADataset.NivelRow CurrentNivel);
		}

		private class WidthFirstVisitorParents : WidthFirstVisitor
		{
			protected override GISADataset.RelacaoHierarquicaRow[] GetNextNivelRows(GISADataset CurrentDataSet, GISADataset.NivelRow CurrentNivel)
			{
				return Nivel.GetParents(CurrentDataSet, CurrentNivel);
			}
		}

		private class WidthFirstVisitorChildren : WidthFirstVisitor
		{
			protected override GISADataset.RelacaoHierarquicaRow[] GetNextNivelRows(GISADataset CurrentDataSet, GISADataset.NivelRow CurrentNivel)
			{
				return Nivel.GetChildren(CurrentDataSet, CurrentNivel);
			}
		}

		public static void VisitParentChains(GISADataset CurrentDataSet, GISADataset.NivelRow CurrentNivel, INivelChainVisitor CurrentVisitor)
		{
			// TODO Visit the graph upwards, Width-First.
			new WidthFirstVisitorParents().Run(CurrentDataSet, CurrentNivel, CurrentVisitor);
		}

		public static void VisitChildChains(GISADataset CurrentDataSet, GISADataset.NivelRow CurrentNivel, INivelChainVisitor CurrentVisitor)
		{
			// TODO Visit the graph downwards, Width-First.
			new WidthFirstVisitorChildren().Run(CurrentDataSet, CurrentNivel, CurrentVisitor);
		}

		public static bool isNivelOrganico(GISADataset.NivelRow nRow)
		{
			if (nRow.CatCode.Trim().Equals("CA"))
			{
				return true;
			}
			else if (nRow.CatCode.Trim().Equals("NVL"))
			{
				return false;
			}
			throw new ArgumentException("details not found in Nivel.Table.DataSet", "nRow");
		}

		private static string GetDesignacaoInDataSet(GISADataset.NivelRow Nivel)
		{
			bool nivelOrganico = isNivelOrganico(Nivel);
			GisaDataSetHelper.HoldOpen ho = null;
			if (! nivelOrganico)
			{
				if (Nivel.GetNivelDesignadoRows().Length == 0)
				{
					ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
					try
					{
						NivelRule.Current.FillNivelDesignado(GisaDataSetHelper.GetInstance(), Nivel.ID, ho.Connection);
					}
					catch (Exception ex)
					{
						Trace.WriteLine(ex);
						throw;
					}
					finally
					{
						ho.Dispose();
					}
				}
				Debug.Assert(Nivel.GetNivelDesignadoRows().Length > 0);
				return Nivel.GetNivelDesignadoRows()[0].Designacao;
			}
			if (nivelOrganico)
			{
				if (Nivel.GetNivelControloAutRows().Length == 0 || 
					Nivel.GetNivelControloAutRows()[0].ControloAutRow.GetControloAutDicionarioRows().Length == 0)
				{
					ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
					try
					{
						NivelRule.Current.FillNivelControloAutRows(GisaDataSetHelper.GetInstance(), Nivel.ID, ho.Connection);
					}
					catch (Exception ex)
					{
						Trace.WriteLine(ex);
						throw;
					}
					finally
					{
						ho.Dispose();
					}
				}
				Debug.Assert(Nivel.GetNivelControloAutRows().Length > 0);
				Debug.Assert(Nivel.GetNivelControloAutRows()[0].ControloAutRow.GetControloAutDicionarioRows().Length > 0);
				foreach (GISADataset.ControloAutDicionarioRow cad in Nivel.GetNivelControloAutRows()[0].ControloAutRow.GetControloAutDicionarioRows())
				{
					if (cad.IDTipoControloAutForma == (long)TipoControloAutForma.FormaAutorizada)
					{
						return cad.DicionarioRow.Termo;
					}
				}
			}
#if (DEBUG)
			Debug.WriteLine("Nivel ID=", Nivel.ID.ToString());
			Debug.WriteLine("Nivel IDTipoNivel=", Nivel.IDTipoNivel.ToString());
			throw new ArgumentException("details not found in Nivel.Table.DataSet", "Nivel");
#else
			{
			Console.WriteLine("details not found in Nivel.Table.DataSet / Tipo=" + Nivel.IDTipoNivel.ToString());
			return "";
			}
#endif
		}

		public static string GetDesignacao(GISADataset.NivelRow Nivel)
		{
			return GetDesignacaoInDataSet(Nivel);
		}

		public static string buildPath(ArrayList rhRows)
		{
			GISADataset.RelacaoHierarquicaRow rhPreviousRow = null;
			string separador = null;
			System.Text.StringBuilder fullCodigo = new System.Text.StringBuilder();

			foreach (GISADataset.RelacaoHierarquicaRow rhRow in rhRows)
			{
				separador = "/";
				// a primeira condição prevê a excepção de estarmos a construir o caminho completo de niveis documentais. 
				// nesse caso, o separador pode ser um "-" se o segundo nivel documental for uma subsérie
				if ((rhPreviousRow == null && rhRow.TipoNivelRelacionadoRow.ID == TipoNivelRelacionado.SSR) || rhPreviousRow != null && ((rhPreviousRow.IDTipoNivelRelacionado == TipoNivelRelacionado.D || rhPreviousRow.IDTipoNivelRelacionado == TipoNivelRelacionado.SD) && rhRow.IDTipoNivelRelacionado == TipoNivelRelacionado.SD || (rhPreviousRow.IDTipoNivelRelacionado == TipoNivelRelacionado.SR || rhPreviousRow.IDTipoNivelRelacionado == TipoNivelRelacionado.SSR) && rhRow.IDTipoNivelRelacionado == TipoNivelRelacionado.SSR || (rhPreviousRow.IDTipoNivelRelacionado == TipoNivelRelacionado.SC || rhPreviousRow.IDTipoNivelRelacionado == TipoNivelRelacionado.SSC) && rhRow.IDTipoNivelRelacionado == TipoNivelRelacionado.SSC || (rhPreviousRow.IDTipoNivelRelacionado == TipoNivelRelacionado.A || rhPreviousRow.IDTipoNivelRelacionado == TipoNivelRelacionado.SA) && rhRow.IDTipoNivelRelacionado == TipoNivelRelacionado.SA))
				{

					separador = "-";
				}

				if (fullCodigo.Length == 0)
				{
					if (rhRow.NivelRowByNivelRelacaoHierarquicaUpper.IDTipoNivel == TipoNivel.ESTRUTURAL && rhRow.NivelRowByNivelRelacaoHierarquica.IDTipoNivel == TipoNivel.DOCUMENTAL)
					{
						fullCodigo.AppendFormat("{0}", rhRow.NivelRowByNivelRelacaoHierarquica.Codigo);
					}
					else
					{
						fullCodigo.AppendFormat("{0}{1}{2}", rhRow.NivelRowByNivelRelacaoHierarquicaUpper.Codigo, separador, rhRow.NivelRowByNivelRelacaoHierarquica.Codigo);
					}

				}
				else
				{
					fullCodigo.AppendFormat("{0}{1}", separador, rhRow.NivelRowByNivelRelacaoHierarquica.Codigo);
				}

				rhPreviousRow = rhRow;
			}
			return fullCodigo.ToString();
		}

        public static bool HasTipologiaChanged(GISADataset.FRDBaseRow frdRow, out string newTip)
        {
            newTip = string.Empty;
            var newTipRow = frdRow.GetIndexFRDCARows().SingleOrDefault(r => r.RowState == DataRowState.Added && r["Selector"] != DBNull.Value && r.Selector == -1);
            if (newTipRow != null)
            {
                newTip = newTipRow.ControloAutRow.GetControloAutDicionarioRows().Single(r => r.IDTipoControloAutForma == (long)TipoControloAutForma.FormaAutorizada).DicionarioRow.Termo;
                return true;
            }
            

            var remTipRow = GisaDataSetHelper.GetInstance().IndexFRDCA.Cast<GISADataset.IndexFRDCARow>().SingleOrDefault(r => r.RowState == DataRowState.Deleted && System.Convert.ToInt64(r["IDFRDBase", DataRowVersion.Original]) == frdRow.ID && r["Selector", DataRowVersion.Original] != DBNull.Value && System.Convert.ToInt64(r["Selector", DataRowVersion.Original]) == -1);
            if (remTipRow != null) { newTip = null; return true; }

            return false;
        }

        public static List<string> HasIndexacaoChanged(GISADataset.FRDBaseRow frdRow)
        {
            var newIdxRows = frdRow.GetIndexFRDCARows().Where(r => r.RowState == DataRowState.Added && (r.ControloAutRow.IDTipoNoticiaAut == (long)TipoNoticiaAut.Ideografico || r.ControloAutRow.IDTipoNoticiaAut == (long)TipoNoticiaAut.Onomastico || r.ControloAutRow.IDTipoNoticiaAut == (long)TipoNoticiaAut.ToponimicoGeografico));
            if (newIdxRows.Count() > 0)
                return newIdxRows.Where(r => r.ControloAutRow.IDTipoNoticiaAut == (long)TipoNoticiaAut.Ideografico || r.ControloAutRow.IDTipoNoticiaAut == (long)TipoNoticiaAut.Onomastico || r.ControloAutRow.IDTipoNoticiaAut == (long)TipoNoticiaAut.ToponimicoGeografico).Select(r => r.ControloAutRow.GetControloAutDicionarioRows().Single(cad => cad.IDTipoControloAutForma == (long)TipoControloAutForma.FormaAutorizada).DicionarioRow.Termo).ToList();

            var remIdxRows = GisaDataSetHelper.GetInstance().IndexFRDCA.Cast<GISADataset.IndexFRDCARow>().Where(r => r.RowState == DataRowState.Deleted && (long)r["IDFRDBase", DataRowVersion.Original] == frdRow.ID);
            foreach (var idx in remIdxRows)
            {
                var res = new List<string>();
                var caRow = GisaDataSetHelper.GetInstance().ControloAut.Cast<GISADataset.ControloAutRow>().Single(r => r.ID == (long)idx["IDControloAut", DataRowVersion.Original]);
                if (caRow.IDTipoNoticiaAut == (long)TipoNoticiaAut.Ideografico || caRow.IDTipoNoticiaAut == (long)TipoNoticiaAut.Onomastico || caRow.IDTipoNoticiaAut == (long)TipoNoticiaAut.ToponimicoGeografico)
                    res.Add(caRow.GetControloAutDicionarioRows().Single(r => r.IDTipoControloAutForma == 1).DicionarioRow.Termo);
                return res;
            }
            return null;
        }
	}

	public interface INivelChainVisitor
	{
		void InitVisit();
		void Visit(GISADataset CurrentDataSet, GISADataset.NivelRow[] ContextNivel);
		void DoneVisit();
	}

	#if DEBUG
	[TestFixture()]
	public class TestNivel
	{
		private GISADataset ds;
		private GISADataset.NivelRow r1;
		private GISADataset.NivelRow r2;
		private GISADataset.RelacaoHierarquicaRow r3;

		[SetUp()]
		public void SetUp()
		{
			ds = new GISADataset();
			ds.Merge(GisaDataSetHelper.GetInstance());

			r1 = ds.Nivel.NewNivelRow();
			r1.IDTipoNivel = 1;
			r1.Codigo = "Teste";

			r2 = ds.Nivel.NewNivelRow();
			r2.IDTipoNivel = 2;
			r2.Codigo = "Teste";

			r3 = ds.RelacaoHierarquica.NewRelacaoHierarquicaRow();
			r3.ID = r2.ID;
			r3.IDUpper = r1.ID;

			ds.Nivel.Rows.Add(r1);
			ds.Nivel.Rows.Add(r2);
			ds.RelacaoHierarquica.Rows.Add(r3);

			GISADataset.NivelDesignadoRow r4 = ds.NivelDesignado.NewNivelDesignadoRow();
			r4.ID = r1.ID;
			r4.Designacao = "Designacao";
			ds.NivelDesignado.AddNivelDesignadoRow(r4);
		}
		[TearDown()]
		public void TearDown()
		{
			ds = null;
			r1 = null;
			r2 = null;
			r3 = null;
		}
		[Test()]
		public void GetChildNivel()
		{
			Assert.IsTrue(ds.Nivel.Rows.Count >= 2);
			Assert.IsTrue(ds.RelacaoHierarquica.Rows.Count >= 1);

			DataRow[] Children = Nivel.GetChildren(ds, r1);
			Assert.AreEqual(1, Children.Length);
			Assert.AreSame(r2, Children[0]);
		}
		[Test()]
		public void GetParentNivel()
		{
			Assert.IsTrue(ds.Nivel.Rows.Count >= 2);
			Assert.IsTrue(ds.RelacaoHierarquica.Rows.Count >= 1);

			DataRow[] Parents = Nivel.GetParents(ds, r2);
			Assert.AreEqual(1, Parents.Length);
			Assert.AreSame(r1, Parents[0]);
		}


		private class MockVisitor : INivelChainVisitor
		{
			private object r1;
				private object r2;
			public int Count;
			public MockVisitor(object r1, object r2)
			{
				this.r1 = r1;
				this.r2 = r2;
				this.Count = 0;
			}
			public void InitVisit()
			{
				Assert.AreEqual(0, Count);
				Count = 1;
			}
			public void Visit(GISADataset CurrentDataSet, GISADataset.NivelRow[] ContextNivel)
			{
				Assert.IsTrue(ContextNivel.Length > 0);

				if (ContextNivel.Length == 1)
				{
					Assert.AreEqual(1, Count);
					Assert.AreSame(r1, ContextNivel[0]);
				}
				else if (ContextNivel.Length == 2)
				{
					Assert.AreEqual(2, Count);
					Assert.AreSame(r2, ContextNivel[0]);
					Assert.AreSame(r1, ContextNivel[1]);
				}
				else
				{
					Assert.Fail();
				}
				Count = Count + 1;
			}
			public void DoneVisit()
			{
				Assert.AreEqual(3, Count);
			}
		}
		[Test()]
		public void VisitParentChains()
		{
			Assert.IsTrue(ds.Nivel.Rows.Count >= 2);
			Assert.IsTrue(ds.RelacaoHierarquica.Rows.Count >= 1);

			Nivel.VisitParentChains(ds, r2, new MockVisitor(r2, r1));
		}
		[Test()]
		public void VisitChildrenChains()
		{
			Assert.IsTrue(ds.Nivel.Rows.Count >= 2);
			Assert.IsTrue(ds.RelacaoHierarquica.Rows.Count >= 1);

			Nivel.VisitChildChains(ds, r1, new MockVisitor(r1, r2));
		}
		[Test()]
		public void GetDesignacao()
		{
			Assert.IsTrue(ds.Nivel.Rows.Count >= 2);
			string Designacao = Nivel.GetDesignacao((GISADataset.NivelRow)(ds.Nivel.Rows[0]));
			Assert.IsTrue(Designacao.Length > 0);
		}
	}
	#endif

} //end of root namespace