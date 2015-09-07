using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using GISA.Model;
using DBAbstractDataLayer.DataAccessRules;

namespace GISA
{
	public class PanelTrusteeDetailsGroup : GISA.GISAPanel
	{

	#region  Windows Form Designer generated code 

		public PanelTrusteeDetailsGroup() : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call

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
		protected internal System.Windows.Forms.Label lblDescription;
		protected internal System.Windows.Forms.GroupBox grpMembers;
		protected internal System.Windows.Forms.ListView lvMembers;
		protected internal System.Windows.Forms.TextBox txtGroupName;
		protected internal System.Windows.Forms.CheckBox chkActive;
		protected internal System.Windows.Forms.Label lblName;
		protected internal System.Windows.Forms.TextBox txtDescription;
		internal System.Windows.Forms.ColumnHeader ColumnHeaderName;
		internal System.Windows.Forms.ColumnHeader ColumnHeaderDescription;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
            this.lblDescription = new System.Windows.Forms.Label();
            this.grpMembers = new System.Windows.Forms.GroupBox();
            this.lvMembers = new System.Windows.Forms.ListView();
            this.ColumnHeaderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnHeaderDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.txtGroupName = new System.Windows.Forms.TextBox();
            this.chkActive = new System.Windows.Forms.CheckBox();
            this.lblName = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.grpMembers.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblDescription
            // 
            this.lblDescription.Location = new System.Drawing.Point(8, 37);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(64, 16);
            this.lblDescription.TabIndex = 11;
            this.lblDescription.Text = "Descrição:";
            // 
            // grpMembers
            // 
            this.grpMembers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpMembers.Controls.Add(this.lvMembers);
            this.grpMembers.Location = new System.Drawing.Point(8, 58);
            this.grpMembers.Name = "grpMembers";
            this.grpMembers.Size = new System.Drawing.Size(789, 539);
            this.grpMembers.TabIndex = 4;
            this.grpMembers.TabStop = false;
            this.grpMembers.Text = "Membros do grupo";
            // 
            // lvMembers
            // 
            this.lvMembers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvMembers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnHeaderName,
            this.ColumnHeaderDescription});
            this.lvMembers.FullRowSelect = true;
            this.lvMembers.Location = new System.Drawing.Point(8, 16);
            this.lvMembers.Name = "lvMembers";
            this.lvMembers.Size = new System.Drawing.Size(773, 515);
            this.lvMembers.TabIndex = 1;
            this.lvMembers.UseCompatibleStateImageBehavior = false;
            this.lvMembers.View = System.Windows.Forms.View.Details;
            // 
            // ColumnHeaderName
            // 
            this.ColumnHeaderName.Text = "Nome";
            this.ColumnHeaderName.Width = 115;
            // 
            // ColumnHeaderDescription
            // 
            this.ColumnHeaderDescription.Text = "Descrição";
            this.ColumnHeaderDescription.Width = 285;
            // 
            // txtGroupName
            // 
            this.txtGroupName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGroupName.Enabled = false;
            this.txtGroupName.Location = new System.Drawing.Point(72, 9);
            this.txtGroupName.Name = "txtGroupName";
            this.txtGroupName.Size = new System.Drawing.Size(614, 20);
            this.txtGroupName.TabIndex = 1;
            // 
            // chkActive
            // 
            this.chkActive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkActive.Location = new System.Drawing.Point(733, 7);
            this.chkActive.Name = "chkActive";
            this.chkActive.Size = new System.Drawing.Size(64, 24);
            this.chkActive.TabIndex = 2;
            this.chkActive.Text = "Ativo";
            // 
            // lblName
            // 
            this.lblName.Location = new System.Drawing.Point(8, 11);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(64, 16);
            this.lblName.TabIndex = 6;
            this.lblName.Text = "Nome:";
            // 
            // txtDescription
            // 
            this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescription.Location = new System.Drawing.Point(72, 35);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(725, 20);
            this.txtDescription.TabIndex = 3;
            // 
            // PanelTrusteeDetailsGroup
            // 
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.grpMembers);
            this.Controls.Add(this.txtGroupName);
            this.Controls.Add(this.chkActive);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.txtDescription);
            this.Name = "PanelTrusteeDetailsGroup";
            this.grpMembers.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

	#endregion

		protected void EnumerateMembership(GISADataset.TrusteeRow CurrentTrusteeRow)
		{
			lvMembers.Items.Clear();
			if (CurrentTrusteeRow.RowState == DataRowState.Detached)
			{
				MessageBox.Show("Não é possível editar o utilizador selecionado uma vez que foi apagado.", "Edição de Utilizador", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				MultiPanel.Recontextualize();
				return;
			}
			foreach (GISADataset.UserGroupsRow ug in GisaDataSetHelper.GetInstance().UserGroups.Select(string.Format("IDUser={0} OR IDGroup={0}", CurrentTrusteeRow.ID)))
				DisplayMembership(ug);
		}

		protected virtual void DisplayMembership(GISADataset.UserGroupsRow ug)
		{
			ListViewItem tempWith1 = lvMembers.Items.Add(ug.TrusteeUserRow.TrusteeRow.Name);
			if (! ug.TrusteeUserRow.TrusteeRow.IsDescriptionNull())
				tempWith1.SubItems.Add(ug.TrusteeUserRow.TrusteeRow.Description);
			else
				tempWith1.SubItems.Add("");

			tempWith1.Tag = ug;
		}

		protected GISADataset.TrusteeRow CurrentTrusteeRow;
		public override void LoadData(DataRow CurrentDataRow, IDbConnection conn)
		{
			IsLoaded = false;

			if (CurrentDataRow == null)
				return;

			CurrentTrusteeRow = (GISADataset.TrusteeRow)CurrentDataRow;

			// carregar informação da base de dados
			TrusteeRule.Current.LoadMembership(GisaDataSetHelper.GetInstance(), CurrentTrusteeRow.ID, conn);

			IsLoaded = true;
		}

		public override void ModelToView()
		{
			IsPopulated = false;
			txtGroupName.Text = CurrentTrusteeRow.Name;
			chkActive.Checked = CurrentTrusteeRow.IsActive;
			if (! CurrentTrusteeRow.IsDescriptionNull() )
				txtDescription.Text = CurrentTrusteeRow.Description;
			else
				txtDescription.Text = "";

			txtDescription.Focus();
			CurrentTrusteeRow.IsActive = chkActive.Checked;

			EnumerateMembership(CurrentTrusteeRow);
			IsPopulated = true;
		}

		public override void ViewToModel()
		{
			if (CurrentTrusteeRow == null || CurrentTrusteeRow.RowState == DataRowState.Detached || ! IsLoaded)
				return;

			CurrentTrusteeRow.Description = txtDescription.Text;
			CurrentTrusteeRow.IsActive = chkActive.Checked;
		}

		public override void Deactivate()
		{
            GUIHelper.GUIHelper.clearField(txtGroupName);
            GUIHelper.GUIHelper.clearField(chkActive);
            GUIHelper.GUIHelper.clearField(txtDescription);
            GUIHelper.GUIHelper.clearField(lvMembers);
			CurrentTrusteeRow = null;
		}
	}
}