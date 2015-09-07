using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using GISA.Model;
using GISA.GUIHelper;
using DBAbstractDataLayer.DataAccessRules;


namespace GISA
{
	public class FormUserGroups : System.Windows.Forms.Form
	{

	#region  Windows Form Designer generated code 

		public FormUserGroups() : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call
            lstVwTrustees.SelectedIndexChanged += lstVwTrustees_SelectedIndexChanged;

			loadGroups();
			updateButtons();
		}

		//Form overrides dispose to clean up the component list.
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		//Required by the Windows Form Designer
		private System.ComponentModel.IContainer components = null;

		//NOTE: The following procedure is required by the Windows Form Designer
		//It can be modified using the Windows Form Designer.  
		//Do not modify it using the code editor.
		protected internal System.Windows.Forms.ListView lstVwTrustees;
		protected internal System.Windows.Forms.ColumnHeader ColumnHeaderName;
		protected internal System.Windows.Forms.ColumnHeader ColumnHeaderIsActive;
		protected internal System.Windows.Forms.ColumnHeader ColumnHeaderDescription;
		internal System.Windows.Forms.Button btnOk;
		internal System.Windows.Forms.Button btnCancel;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			this.lstVwTrustees = new System.Windows.Forms.ListView();
			this.ColumnHeaderName = new System.Windows.Forms.ColumnHeader();
			this.ColumnHeaderIsActive = new System.Windows.Forms.ColumnHeader();
			this.ColumnHeaderDescription = new System.Windows.Forms.ColumnHeader();
			this.btnOk = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			//
			//lstVwTrustees
			//
			this.lstVwTrustees.Anchor = (System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
			this.lstVwTrustees.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {this.ColumnHeaderName, this.ColumnHeaderIsActive, this.ColumnHeaderDescription});
			this.lstVwTrustees.FullRowSelect = true;
			this.lstVwTrustees.HideSelection = false;
			this.lstVwTrustees.Location = new System.Drawing.Point(4, 4);
			this.lstVwTrustees.Name = "lstVwTrustees";
			this.lstVwTrustees.Size = new System.Drawing.Size(400, 176);
			this.lstVwTrustees.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.lstVwTrustees.TabIndex = 3;
			this.lstVwTrustees.View = System.Windows.Forms.View.Details;
			//
			//ColumnHeaderName
			//
			this.ColumnHeaderName.Text = "Nome";
			this.ColumnHeaderName.Width = 120;
			//
			//ColumnHeaderIsActive
			//
			this.ColumnHeaderIsActive.Text = "Ativo";
			this.ColumnHeaderIsActive.Width = 56;
			//
			//ColumnHeaderDescription
			//
			this.ColumnHeaderDescription.Text = "Descrição";
			this.ColumnHeaderDescription.Width = 220;
			//
			//btnOk
			//
			this.btnOk.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
			this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOk.Location = new System.Drawing.Point(220, 188);
			this.btnOk.Name = "btnOk";
			this.btnOk.TabIndex = 4;
			this.btnOk.Text = "OK";
			//
			//btnCancel
			//
			this.btnCancel.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(308, 188);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.TabIndex = 5;
			this.btnCancel.Text = "Cancel";
			//
			//FormUserGroups
			//
			this.AcceptButton = this.btnOk;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(408, 221);
			this.ControlBox = false;
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.lstVwTrustees);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormUserGroups";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Grupos";
			this.ResumeLayout(false);

			//INSTANT C# NOTE: Converted event handlers:
			

		}

	#endregion

		private void loadGroups()
		{
			// carregar informação da base de dados
			IDbConnection conn = GisaDataSetHelper.GetConnection();
			try
			{
				conn.Open();
				TrusteeRule.Current.LoadGroupsData(GisaDataSetHelper.GetInstance(), conn);
			}
			finally
			{
				conn.Close();
			}

			lstVwTrustees.Items.Clear();
			ListViewItem item = null;
			foreach (GISADataset.TrusteeRow t in GisaDataSetHelper.GetInstance().Trustee)
			{
	#if TESTING
				if (t.CatCode == "GRP")
				{
					item = lstVwTrustees.Items.Add("");
					updateListViewItem(item, t);
					if (t.BuiltInTrustee)
					{
						item.ForeColor = System.Drawing.Color.Gray;
					}
				}
	#else
				if (t.CatCode == "GRP" && ! t.BuiltInTrustee)
				{
					item = lstVwTrustees.Items.Add("");
					updateListViewItem(item, t);
				}
	#endif
			}
			lstVwTrustees.Sort();
			if (lstVwTrustees.Items.Count > 0)
			{
				lstVwTrustees.EnsureVisible(0);
				lstVwTrustees.Items[0].Selected = true;
			}
		}

		protected virtual void updateListViewItem(ListViewItem li, GISADataset.TrusteeRow tRow)
		{
			li.Text = tRow.Name;
			while (li.SubItems.Count < li.ListView.Columns.Count)
			{
				li.SubItems.Add("");
			}
			li.SubItems[ColumnHeaderIsActive.Index].Text = TranslationHelper.FormatBoolean(tRow.IsActive);
			if (tRow.IsDescriptionNull())
			{
				li.SubItems[ColumnHeaderDescription.Index].Text = "";
			}
			else
			{
				li.SubItems[ColumnHeaderDescription.Index].Text = tRow.Description;
			}
			li.Tag = tRow;
		}

		public GISADataset.TrusteeRow[] SelectedRows
		{
			get
			{
				ArrayList rows = new ArrayList();
				foreach (ListViewItem item in lstVwTrustees.Items)
				{
					rows.Add((GISADataset.TrusteeRow)item.Tag);
				}
				return (GISADataset.TrusteeRow[])(rows.ToArray(typeof(GISADataset.TrusteeRow)));
			}
		}

		private void lstVwTrustees_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			updateButtons();
		}

		private void updateButtons()
		{
			if (lstVwTrustees.SelectedItems.Count > 0)
			{
				btnOk.Enabled = true;
			}
			else
			{
				btnOk.Enabled = false;
			}
		}
	}

} //end of root namespace