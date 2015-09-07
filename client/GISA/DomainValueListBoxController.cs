using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using GISA.Model;

namespace GISA
{
	public class DomainValueListBoxController
	{

		private DataTable DomainValues;
		private DataView Selection;
		private string SelectionColumn;
		private ListBox ListBox;
		private bool ExclusiveSelection;


        public DomainValueListBoxController(GISADataset.FRDBaseRow FRDBaseRow, DataTable DomainValues, DataTable SelectionTable, string SelectionColumn, ListBox ListBox)
            : this(FRDBaseRow, DomainValues, SelectionTable, SelectionColumn, ListBox, false)
		{
		}

        public DomainValueListBoxController(GISADataset.FRDBaseRow FRDBaseRow, DataTable DomainValues, DataTable SelectionTable, string SelectionColumn, ListBox ListBox, bool ExclusiveSelection)
            : base()

		{
			this.DomainValues = DomainValues;
			this.Selection = new DataView(SelectionTable, "IDFRDBase=" + FRDBaseRow.ID.ToString(), "", DataViewRowState.CurrentRows);
			this.SelectionColumn = SelectionColumn;
			this.ListBox = ListBox;
			this.ExclusiveSelection = ExclusiveSelection;

			((CheckedListBox)ListBox).ItemCheck +=  ListBox_Itemcheck;
		}

		private void ListBox_Itemcheck(object sender, ItemCheckEventArgs e)
		{

			if (ExclusiveSelection && e.NewValue == CheckState.Checked)
			{
                CheckedListBox tempWith1 = (CheckedListBox)ListBox;
				foreach (int idx in tempWith1.CheckedIndices)
				{
					tempWith1.SetItemChecked(idx, false);
				}
			}
		}

		public void ModelToView()
		{
			// TODO: There is a BUG in CheckedListBox. It was not designed for
			// DataBinding and therefore does not work under this condition.
			// The workaround is to insert the CheckedListBox population
			// manually.
			//ListBox.DataSource = DomainValues
			ListBox.DisplayMember = "Designacao";

			ListBox.Items.Clear();
			DataRowView lastDr = null;
			foreach (DataRowView dr in new DataView(DomainValues, "", "Designacao", DataViewRowState.CurrentRows))
			{
				if ("Outra".Equals(dr["Designacao"]) || "Outro".Equals(dr["Designacao"]))
				{
					lastDr = dr;
				}
				else
				{
					ListBox.Items.Add(dr);
				}
			}
			if (lastDr != null)
			{
				ListBox.Items.Add(lastDr);
			}

			int i = 0;
			DataRowView row2 = null;
			foreach (DataRowView row in Selection)
			{
				for (i = 0; i < ListBox.Items.Count; i++)
				{
					row2 = (DataRowView)(ListBox.Items[i]);
					if (ListBox is CheckedListBox)
					{
                        CheckedListBox tempWith1 = (CheckedListBox)ListBox;
						if (row[SelectionColumn].Equals(row2["ID"]))
						{
							tempWith1.SetItemChecked(i, true);
						}
					}
					else
					{
						if (row[SelectionColumn].Equals(row2["ID"]))
						{
							ListBox.SetSelected(i, true);
						}
					}
				}
			}
		}

		private DataRow FindRowBySelection(DataRowView row2)
		{
			DataRow nr = null;
			foreach (DataRowView row in Selection)
			{
				if (row[SelectionColumn].Equals(row2["ID"]))
				{
					nr = row.Row;
				}
			}
			return nr;
		}

		// TUDO: FIXME
		// Bug?
		// ListBox.Items contents changed make Enumerator invalid.
		public void ViewToModel(GISADataset.FRDBaseRow FRDBaseRow)
		{
			foreach (DataRowView row2 in ListBox.Items)
			{
				DataRow nr = FindRowBySelection(row2);
				if (((CheckedListBox)ListBox).CheckedItems. Contains(row2))
				{

					if (nr == null)
					{
						nr = Selection.Table.NewRow();
						nr["IDFRDBase"] = FRDBaseRow.ID;
						nr[SelectionColumn] = row2["ID"];
						Selection.Table.Rows.Add(nr);
					}
				}
				else
				{
					if (nr != null)
					{
						nr.Delete();
					}
				}
			}
		}

		public void Deactivate()
		{
			int i = 0;
			for (i = 0; i < ListBox.Items.Count; i++)
			{
				if (ListBox is CheckedListBox)
				{
					((CheckedListBox)ListBox).SetItemChecked(i, false);
				}
			}
		}
	}
} //end of root namespace