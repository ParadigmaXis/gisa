using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using GISA.Model;

namespace GISA
{
	public class ControloAutDragDrop : GenericDragDrop
	{
        Control parent = null;

		public delegate void AddControloAutEventHandler(object Sender, ListViewItem ListViewItem);
		public event AddControloAutEventHandler AddControloAut;

		protected ListView ListView { get {return (ListView)Control; } }

		private TipoNoticiaAut[] TipoNoticiaAutAllowed;
		internal protected GISADataset.FRDBaseRow FRDBaseRow;
        internal protected GISADataset.ControloAutRow ControloAutRow;

		public GISADataset.FRDBaseRow FRDBase
		{
			get {return this.FRDBaseRow;}
			set {this.FRDBaseRow = value;}
		}

		public GISADataset.ControloAutRow ControloAut
		{
			get {return this.ControloAutRow;}
            set {this.ControloAutRow = value;}
		}

		public ControloAutDragDrop(ListView ListView, TipoNoticiaAut[] TipoNoticiaAutAllowed, GISADataset.FRDBaseRow FRDBaseRow) : base(ListView, typeof(GISADataset.ControloAutRow), typeof(GISADataset.ControloAutRow[]))
		{
			this.TipoNoticiaAutAllowed = TipoNoticiaAutAllowed;
			this.FRDBaseRow = FRDBaseRow;
			this.ControloAutRow = null;
		}

		public ControloAutDragDrop(ListView ListView, TipoNoticiaAut[] TipoNoticiaAutAllowed, GISADataset.ControloAutRow ControloAutRow) : base(ListView, typeof(GISADataset.ControloAutRow), typeof(GISADataset.ControloAutRow[]))
		{
			this.TipoNoticiaAutAllowed = TipoNoticiaAutAllowed;
			this.FRDBaseRow = null;
			this.ControloAutRow = ControloAutRow;
		}

        public ControloAutDragDrop(ListView ListView, TipoNoticiaAut[] TipoNoticiaAutAllowed, GISADataset.ControloAutRow ControloAutRow, Control parent)
            : base(ListView, typeof(GISADataset.ControloAutRow), typeof(GISADataset.ControloAutRow[]))
        {
            this.TipoNoticiaAutAllowed = TipoNoticiaAutAllowed;
            this.FRDBaseRow = null;
            this.ControloAutRow = ControloAutRow;
            this.parent = parent;
        }

		protected override void VerifyContents(object Value, ref bool Cancel)
		{
			int tipoID = 0;
			foreach (TipoNoticiaAut tipoNoticia in TipoNoticiaAutAllowed)
			{
				tipoID = System.Convert.ToInt32(System.Enum.Format(typeof(TipoNoticiaAut), tipoNoticia, "D"));
				if (((GISADataset.ControloAutRow)Value). IDTipoNoticiaAut == tipoID)
				{
					Cancel = false;
					return;
				}
			}
			Cancel = true;
			return;
		}

        internal protected virtual DataRow NewRow(GISADataset.ControloAutRow caRow)
        {
            var newRow = GisaDataSetHelper.GetInstance().IndexFRDCA.NewIndexFRDCARow();
		    newRow.FRDBaseRow = FRDBaseRow;
            newRow.ControloAutRow = caRow;
            newRow["Selector"] = DBNull.Value;
            GisaDataSetHelper.GetInstance().IndexFRDCA.AddIndexFRDCARow(newRow);
            return newRow;
        }

        internal protected virtual DataRow GetRow()
        {
            return GisaDataSetHelper.GetInstance().IndexFRDCA.Cast<GISADataset.IndexFRDCARow>().Single(r => r.RowState != DataRowState.Deleted && r.IDControloAut == ControloAutRow.ID && r.IDFRDBase == FRDBaseRow.ID);
        }

		protected override void AcceptContents(object Value)
		{
			this.ControloAutRow = (GISADataset.ControloAutRow)Value;

			if (FRDBaseRow != null)
			{
				NotPresentInIndexResults indexPresence = NotPresentInIndex(ControloAutRow);
				switch (indexPresence)
				{
					case NotPresentInIndexResults.NotPresentInIndex:
					{
                        TempIndexFRDCA = NewRow(ControloAutRow);
                                                
						GISADataset.ControloAutDicionarioRow[] cadRows = null;
						cadRows = (GISADataset.ControloAutDicionarioRow[])(GisaDataSetHelper.GetInstance().ControloAutDicionario.Select(string.Format("IDControloAut={0} AND IDTipoControloAutForma = {1:d}", ControloAutRow.ID, TipoControloAutForma.FormaAutorizada)));
						if (cadRows.Length == 0)
						{
							GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
							try
							{
								DBAbstractDataLayer.DataAccessRules.ControloAutRule.Current.LoadFormaAutorizada(ControloAutRow.ID, GisaDataSetHelper.GetInstance(), ho.Connection);
							}
							finally
							{
								ho.Dispose();
							}
							cadRows = (GISADataset.ControloAutDicionarioRow[])(GisaDataSetHelper.GetInstance().ControloAutDicionario.Select(string.Format("IDControloAut={0} AND IDTipoControloAutForma = {1:d}", ControloAutRow.ID, TipoControloAutForma.FormaAutorizada)));
						}
						if (cadRows.Length > 0)
						{
							DisplayFormaAutorizada(cadRows[0]);
						}
						break;
					}
					case NotPresentInIndexResults.PresentInIndex:
					{
						MessageBox.Show("Não é possível a existência de items repetidos.", "Adição", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						break;
					}
					case NotPresentInIndexResults.PresentInIndexDeleted:
					{
						GISADataset.ControloAutDicionarioRow[] cadRows = null;
						TempIndexFRDCA = GetRow();
						cadRows = (GISADataset.ControloAutDicionarioRow[])(GisaDataSetHelper.GetInstance().ControloAutDicionario.Select(string.Format("IDControloAut={0} AND IDTipoControloAutForma = {1:d}", ControloAutRow.ID, TipoControloAutForma.FormaAutorizada)));
						if (cadRows.Length == 0)
						{
							GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
							try
							{
								DBAbstractDataLayer.DataAccessRules.ControloAutRule.Current.LoadFormaAutorizada(ControloAutRow.ID, GisaDataSetHelper.GetInstance(), ho.Connection);
							}
							finally
							{
								ho.Dispose();
							}
							cadRows = (GISADataset.ControloAutDicionarioRow[])(GisaDataSetHelper.GetInstance().ControloAutDicionario.Select(string.Format("IDControloAut={0} AND IDTipoControloAutForma = {1:d}", ControloAutRow.ID, TipoControloAutForma.FormaAutorizada)));
						}
						if (cadRows.Length > 0)
						{
							DisplayFormaAutorizada(cadRows[0]);
						}
						break;
					}
				}
			}
			else if (this.ControloAutRow != null)
			{
				// TODO: FIXME legibilidade
				// Determinar se Value já se encontra em ControloAutRel
				try
				{
					// make sure we do not relate an item to itself
					if (ControloAutRow.ID == this.ControloAutRow.ID)
					{
						return;
					}

					// If the dropped item was not already added to the selected
					// item (or vice versa)
					if (GisaDataSetHelper.GetInstance().ControloAutRel.Select(GetRelConstraint(ControloAutRow, this.ControloAutRow) + " OR " + GetRelConstraint(this.ControloAutRow, ControloAutRow)).Length == 0)
					{

						// adicionar nova relação
                        FormControloAutRel frm = new FormControloAutRel(ControloAutRow, parent);

						switch (frm.ShowDialog())
						{
							case DialogResult.OK:
								TempIndexFRDCA = GisaDataSetHelper.GetInstance().ControloAutRel.NewControloAutRelRow();
                                GISADataset.ControloAutRelRow tempWith1 = (GISADataset.ControloAutRelRow)TempIndexFRDCA;
								tempWith1.IDControloAut = this.ControloAutRow.ID;
								tempWith1.IDControloAutAlias = ControloAutRow.ID;
								tempWith1["IDTipoRel"] = ((GISADataset.TipoControloAutRelRow)(((DataRowView)frm.relacaoCA.cbTipoControloAutRel.SelectedItem).Row)).ID;
								tempWith1.Descricao = frm.relacaoCA.txtDescricao.Text;
								GisaDataSetHelper.GetInstance().ControloAutRel.AddControloAutRelRow((GISADataset.ControloAutRelRow)TempIndexFRDCA);
								break;
							case DialogResult.Cancel:
								return;
						}

						GisaDataSetHelper. VisitControloAutDicionario(ControloAutRow, DisplayFormaAutorizada);
					}
				}
				catch (Exception ex)
				{
					Trace.WriteLine(ex);
				}
			}
		}

		private string GetRelConstraint(GISADataset.ControloAutRow a, GISADataset.ControloAutRow b)
		{

			return "(IDControloAut = " + a.ID.ToString() + " AND IDControloAutAlias = " + b.ID.ToString() + ")";
		}

		private string FormatName
		{
			get
			{
				return typeof(GISADataset.ControloAutRow).FullName;
			}
		}

		private string FormatNameArray
		{
			get
			{
				return typeof(GISADataset.ControloAutRow[]).FullName;
			}
		}

		private object TempIndexFRDCA;
		protected virtual void DisplayFormaAutorizada(GISADataset.ControloAutDicionarioRow ControloAutDicionario)
		{
			Debug.Assert(TempIndexFRDCA != null);

			if (ControloAutDicionario.IDTipoControloAutForma != Convert.ToInt64(TipoControloAutForma.FormaAutorizada))
			{

				Debug.Assert(false, "CAD should always be Autorizado");

				GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
				try
				{
					DBAbstractDataLayer.DataAccessRules.ControloAutRule.Current.LoadFormaAutorizada(ControloAutDicionario.ControloAutRow.ID, GisaDataSetHelper.GetInstance(), ho.Connection);
				}
				finally
				{
					ho.Dispose();
				}

				ControloAutDicionario = GISA.Model.ControloAutHelper.getFormaAutorizada(ControloAutDicionario.ControloAutRow);
			}

			ListViewItem li = ListView.Items.Add(ControloAutDicionario.DicionarioRow.Termo);
			li.Tag = TempIndexFRDCA;
			RaiseEventAddControloAut(li);

		}

		protected void RaiseEventAddControloAut(ListViewItem ListViewItem)
		{
			if (AddControloAut != null)
				AddControloAut(this, ListViewItem);
		}

        internal protected enum NotPresentInIndexResults : int
		{
			PresentInIndex = 1,
			NotPresentInIndex = 2,
			PresentInIndexDeleted = 3
		}

		internal protected virtual NotPresentInIndexResults NotPresentInIndex(GISADataset.ControloAutRow ControloAutRow)
		{
			if (GisaDataSetHelper.GetInstance().IndexFRDCA. Select("IDFRDBase=" + FRDBaseRow.ID.ToString() + " AND IDControloAut=" + ControloAutRow.ID.ToString()).Length == 0)
			{
				DataRow[] deletedRows = GisaDataSetHelper.GetInstance().IndexFRDCA.Select("", "", DataViewRowState.Deleted);

				if (deletedRows.Length > 0)
				{
					foreach (DataRow deletedRow in deletedRows)
					{
						if (((long)(deletedRow["IDFRDBase", DataRowVersion.Original])) == FRDBaseRow.ID && ((long)(deletedRow["IDControloAut", DataRowVersion.Original])) == ControloAutRow.ID)
						{
							deletedRow.RejectChanges();
							return NotPresentInIndexResults.PresentInIndexDeleted;
						}
					}
				}
				return NotPresentInIndexResults.NotPresentInIndex;
			}
			else
				return NotPresentInIndexResults.PresentInIndex;
		}
	}
}