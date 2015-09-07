using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using GISA.Model;
using GISA.GUIHelper;

using GISA.Controls;


namespace GISA
{
	public class MasterPanelTrustee : GISA.MasterPanel
	{

	#region  Windows Form Designer generated code 

		public MasterPanelTrustee() : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call
            ToolBar.ButtonClick += ToolBal_ButtonClick;
            base.StackChanged += MasterPanelTrustee_StackChanged;
            lstVwTrustees.BeforeNewSelection += lstVwTrustees_BeforeNewSelection;

			// Scan for updates on rows displayed in listview
			//AddHandler GisaDataSetHelper.GetInstance().Trustee.RowChanged, AddressOf RowChanged
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
		internal System.Windows.Forms.ToolBarButton ToolBarButtonAdd;
		internal System.Windows.Forms.ToolBarButton ToolBarButtonEdit;
		internal System.Windows.Forms.ToolBarButton ToolBarButtonDelete;
		protected internal PxListView lstVwTrustees;
		protected internal System.Windows.Forms.ColumnHeader ColumnHeaderName;
		protected internal System.Windows.Forms.ColumnHeader ColumnHeaderDescription;
		protected internal System.Windows.Forms.ColumnHeader ColumnHeaderIsActive;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			this.lstVwTrustees = new PxListView();
			this.ColumnHeaderName = new System.Windows.Forms.ColumnHeader();
			this.ColumnHeaderIsActive = new System.Windows.Forms.ColumnHeader();
			this.ColumnHeaderDescription = new System.Windows.Forms.ColumnHeader();
			this.ToolBarButtonAdd = new System.Windows.Forms.ToolBarButton();
			this.ToolBarButtonDelete = new System.Windows.Forms.ToolBarButton();
			this.ToolBarButtonEdit = new System.Windows.Forms.ToolBarButton();
			this.pnlToolbarPadding.SuspendLayout();
			this.SuspendLayout();
			//
			//lblFuncao
			//
			this.lblFuncao.Location = new System.Drawing.Point(0, 0);
			this.lblFuncao.Name = "lblFuncao";
			this.lblFuncao.Text = "Entidades autorizadas";
			//
			//ToolBar
			//
			this.ToolBar.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {this.ToolBarButtonAdd, this.ToolBarButtonEdit, this.ToolBarButtonDelete});
			this.ToolBar.ImageList = null;
			this.ToolBar.Name = "ToolBar";
			//
			//pnlToolbarPadding
			//
			this.pnlToolbarPadding.DockPadding.Left = 5;
			this.pnlToolbarPadding.DockPadding.Right = 5;
			this.pnlToolbarPadding.Location = new System.Drawing.Point(0, 24);
			this.pnlToolbarPadding.Name = "pnlToolbarPadding";
			//
			//lstVwTrustees
			//
			this.lstVwTrustees.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {this.ColumnHeaderName, this.ColumnHeaderIsActive, this.ColumnHeaderDescription});
			this.lstVwTrustees.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstVwTrustees.FullRowSelect = true;
			this.lstVwTrustees.HideSelection = false;
			this.lstVwTrustees.Location = new System.Drawing.Point(0, 52);
			this.lstVwTrustees.MultiSelect = false;
			this.lstVwTrustees.Name = "lstVwTrustees";
			this.lstVwTrustees.Size = new System.Drawing.Size(600, 228);
			this.lstVwTrustees.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.lstVwTrustees.TabIndex = 2;
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
			this.ColumnHeaderDescription.Width = 336;
			//
			//ToolBarButtonAdd
			//
			this.ToolBarButtonAdd.ImageIndex = 0;
			//
			//ToolBarButtonDelete
			//
			this.ToolBarButtonDelete.Enabled = false;
			this.ToolBarButtonDelete.ImageIndex = 2;
			//
			//ToolBarButtonEdit
			//
			this.ToolBarButtonEdit.Enabled = false;
			this.ToolBarButtonEdit.ImageIndex = 1;
			//
			//MasterPanelTrustee
			//
			this.Controls.Add(this.lstVwTrustees);
			this.Name = "MasterPanelTrustee";
			this.Controls.SetChildIndex(this.lblFuncao, 0);
			this.Controls.SetChildIndex(this.pnlToolbarPadding, 0);
			this.Controls.SetChildIndex(this.lstVwTrustees, 0);
			this.pnlToolbarPadding.ResumeLayout(false);
			this.ResumeLayout(false);

			//INSTANT C# NOTE: Converted event handlers:
			

		}

	#endregion

		private void ToolBal_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
		{
			if (e.Button == ToolBarButtonAdd)
				this.AddTrustee();
			else if (e.Button == ToolBarButtonEdit)
				this.EditTrustee();
			else if (e.Button == ToolBarButtonDelete)
				this.DeleteTrustee();
		}
        
		public override void UpdateToolBarButtons(ListViewItem item)
		{
			GISADataset.TrusteeRow selectedTrusteeRow = null;
			if (item != null && item.ListView != null)
				selectedTrusteeRow = (GISADataset.TrusteeRow)item.Tag;
			else if (item != null && item.ListView != null && lstVwTrustees.SelectedItems.Count > 0)
				selectedTrusteeRow = (GISADataset.TrusteeRow)(lstVwTrustees.SelectedItems[0].Tag);

			this.ToolBarButtonAdd.Enabled = AllowCreate;
			this.ToolBarButtonEdit.Enabled = selectedTrusteeRow != null && ! (selectedTrusteeRow.RowState == DataRowState.Detached) && AllowEdit;
			this.ToolBarButtonDelete.Enabled = selectedTrusteeRow != null && ! (selectedTrusteeRow.RowState == DataRowState.Detached) && AllowDelete;
		}

		protected virtual void AddTrustee()
		{
		}

		protected virtual void EditTrustee()
		{
		}

		protected virtual void DeleteTrustee()
		{
		}

		// este metodo deve ser sempre overriden
		protected virtual void UpdateTrustees(GISADataset.TrusteeRow tRow)
		{
		}

		protected virtual void UpdateTrustees()
		{
			UpdateTrustees(null);
		}

		private void MasterPanelTrustee_StackChanged(frmMain.StackOperation stackOperation, bool isSupport)
		{
			UpdateTrustees();
			UpdateContext();
		}

		private void lstVwTrustees_BeforeNewSelection(object sender, BeforeNewSelectionEventArgs e)
		{
			e.SelectionChange = UpdateContext(e.ItemToBeSelected);
            this.UpdateToolBarButtons(e.ItemToBeSelected);
		}

		protected virtual void UpdateListViewItem(ListViewItem li, GISADataset.TrusteeRow tRow)
		{
			li.Text = tRow.Name;
			while (li.SubItems.Count < li.ListView.Columns.Count)
				li.SubItems.Add(string.Empty);

			li.SubItems[ColumnHeaderIsActive.Index].Text = TranslationHelper.FormatBoolean(tRow.IsActive);
			
            if (tRow.IsDescriptionNull())
				li.SubItems[ColumnHeaderDescription.Index].Text = string.Empty;
			else
				li.SubItems[ColumnHeaderDescription.Index].Text = tRow.Description;

			li.Tag = tRow;
		}

		public override bool UpdateContext()
		{
			return UpdateContext(null);
		}

		public override bool UpdateContext(ListViewItem item)
		{
			ListViewItem selectedItem = null;
			bool successfulSave = false;

			if (item != null && item.ListView != null)
				selectedItem = item;
			else if (! (item != null && item.ListView == null) && lstVwTrustees.SelectedItems.Count > 0)
				selectedItem = lstVwTrustees.SelectedItems[0];

			if (selectedItem != null)
				successfulSave = CurrentContext.SetTrustee((GISADataset.TrusteeRow)selectedItem.Tag);
			else
				successfulSave = CurrentContext.SetTrustee(null);

			this.UpdateToolBarButtons(item);
			return successfulSave;
		}

		public static void DeleteTrusteeAndRelatedRows(GISADataset.TrusteeRow truRow)
		{
			GISADataset.TrusteeGroupRow[] grpRows = truRow.GetTrusteeGroupRows();
			GISADataset.TrusteeUserRow[] usrRows = truRow.GetTrusteeUserRows();
			GISADataset.UserGroupsRow[] ugRows = null;
            List<long> UserIDs = new List<long>();

            if (grpRows.Length > 0)
            {
                ugRows = grpRows[0].GetUserGroupsRows();
                foreach (GISADataset.UserGroupsRow ugRow in ugRows)
                    UserIDs.Add(ugRow.IDUser);
            }
            else if (usrRows.Length > 0)
                ugRows = usrRows[0].GetUserGroupsRows();

			GISADataset.TrusteePrivilegeRow[] tpRows = (GISADataset.TrusteePrivilegeRow[])(GisaDataSetHelper.GetInstance().TrusteePrivilege.Select(string.Format("IDTrustee={0}", truRow.ID)));

			foreach (GISADataset.TrusteePrivilegeRow tpRow in tpRows)
				tpRow.Delete();

            if (ugRows != null) 
                foreach (GISADataset.UserGroupsRow ugRow in ugRows)
                    ugRow.Delete();

			foreach (GISADataset.TrusteeGroupRow grpRow in grpRows)
				grpRow.Delete();

			foreach (GISADataset.TrusteeUserRow usrRow in usrRows)
			{
				foreach (GISADataset.TrusteeUserRow uRowAuth in usrRow.GetTrusteeUserRows())
					uRowAuth.SetIDTrusteeUserDefaultAuthorityNull();

				usrRow.Delete();
			}

			truRow.Delete();

			try
			{
                PersistencyHelper.save();
				PersistencyHelper.cleanDeletedData();
			}
			catch (Exception ex)
			{
				Trace.WriteLine(ex);
				throw;
			}
		}
	}
}