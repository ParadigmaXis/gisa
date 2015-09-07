using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using GISA.Model;
using GISA.SharedResources;

namespace GISA
{
	public class PanelTrusteeDetailsUser : PanelTrusteeDetailsGroup
	{

	#region  Windows Form Designer generated code 

		public PanelTrusteeDetailsUser() : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call
            btnAdd.Click += btnAdd_Click;
            btnRemove.Click += btnRemove_Click;
            lvMembers.KeyUp += lvMembers_KeyUp;
            lvMembers.SelectedIndexChanged += lvMembers_SelectedIndexChanged;
            txtPassword.GotFocus += txtPassword_GotFocus;

			GetExtraResources();

            //Visible only if LDAP module active
            this.chkLDAP.Visible = 
                SessionHelper.AppConfiguration.GetCurrentAppconfiguration().LDAPServerName != null && 
                SessionHelper.AppConfiguration.GetCurrentAppconfiguration().LDAPServerSettings != null;
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
		protected internal System.Windows.Forms.TextBox txtFullName;
		protected internal System.Windows.Forms.Label Label1;
		protected internal System.Windows.Forms.CheckBox chkAuthority;
		internal System.Windows.Forms.Button btnRemove;
		internal System.Windows.Forms.Button btnAdd;
		protected internal System.Windows.Forms.TextBox txtPassword;
        private ComboBox cbTipoUser;
        protected internal Label lblTipoUser;
        protected internal CheckBox chkLDAP;
		protected internal System.Windows.Forms.Label lblPassword;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
            this.txtFullName = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.chkAuthority = new System.Windows.Forms.CheckBox();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.cbTipoUser = new System.Windows.Forms.ComboBox();
            this.lblTipoUser = new System.Windows.Forms.Label();
            this.chkLDAP = new System.Windows.Forms.CheckBox();
            this.grpMembers.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblDescription
            // 
            this.lblDescription.Location = new System.Drawing.Point(8, 88);
            // 
            // grpMembers
            // 
            this.grpMembers.Controls.Add(this.btnRemove);
            this.grpMembers.Controls.Add(this.btnAdd);
            this.grpMembers.Location = new System.Drawing.Point(8, 140);
            this.grpMembers.Size = new System.Drawing.Size(784, 452);
            this.grpMembers.TabIndex = 7;
            this.grpMembers.Text = "Grupos do utilizador";
            this.grpMembers.Controls.SetChildIndex(this.btnAdd, 0);
            this.grpMembers.Controls.SetChildIndex(this.btnRemove, 0);
            this.grpMembers.Controls.SetChildIndex(this.lvMembers, 0);
            // 
            // lvMembers
            // 
            this.lvMembers.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvMembers.Size = new System.Drawing.Size(744, 428);
            // 
            // txtGroupName
            // 
            this.txtGroupName.Location = new System.Drawing.Point(145, 9);
            this.txtGroupName.Size = new System.Drawing.Size(432, 20);
            // 
            // chkActive
            // 
            this.chkActive.Location = new System.Drawing.Point(593, 8);
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(145, 88);
            this.txtDescription.Size = new System.Drawing.Size(647, 20);
            this.txtDescription.TabIndex = 5;
            // 
            // txtFullName
            // 
            this.txtFullName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFullName.Location = new System.Drawing.Point(145, 35);
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.Size = new System.Drawing.Size(647, 20);
            this.txtFullName.TabIndex = 4;
            // 
            // Label1
            // 
            this.Label1.Location = new System.Drawing.Point(8, 37);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(88, 13);
            this.Label1.TabIndex = 12;
            this.Label1.Text = "Nome completo:";
            // 
            // chkAuthority
            // 
            this.chkAuthority.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkAuthority.Location = new System.Drawing.Point(657, 7);
            this.chkAuthority.Name = "chkAuthority";
            this.chkAuthority.Size = new System.Drawing.Size(59, 24);
            this.chkAuthority.TabIndex = 3;
            this.chkAuthority.Text = "Autor";
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRemove.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnRemove.Location = new System.Drawing.Point(755, 66);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(24, 24);
            this.btnRemove.TabIndex = 3;
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAdd.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAdd.Location = new System.Drawing.Point(755, 34);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(24, 24);
            this.btnAdd.TabIndex = 2;
            // 
            // txtPassword
            // 
            this.txtPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPassword.Location = new System.Drawing.Point(145, 114);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(647, 20);
            this.txtPassword.TabIndex = 6;
            // 
            // lblPassword
            // 
            this.lblPassword.Location = new System.Drawing.Point(8, 114);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(88, 13);
            this.lblPassword.TabIndex = 15;
            this.lblPassword.Text = "Palavra chave:";
            // 
            // cbTipoUser
            // 
            this.cbTipoUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTipoUser.FormattingEnabled = true;
            this.cbTipoUser.Items.AddRange(new object[] {
            "Acesso apenas a informação publicada",
            "Acesso a toda a informação"});
            this.cbTipoUser.Location = new System.Drawing.Point(145, 61);
            this.cbTipoUser.Name = "cbTipoUser";
            this.cbTipoUser.Size = new System.Drawing.Size(220, 21);
            this.cbTipoUser.TabIndex = 16;
            // 
            // lblTipoUser
            // 
            this.lblTipoUser.Location = new System.Drawing.Point(8, 64);
            this.lblTipoUser.Name = "lblTipoUser";
            this.lblTipoUser.Size = new System.Drawing.Size(131, 13);
            this.lblTipoUser.TabIndex = 17;
            this.lblTipoUser.Text = "Permissões por omissão:";
            // 
            // chkLDAP
            // 
            this.chkLDAP.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkLDAP.Location = new System.Drawing.Point(722, 9);
            this.chkLDAP.Name = "chkLDAP";
            this.chkLDAP.Size = new System.Drawing.Size(65, 20);
            this.chkLDAP.TabIndex = 18;
            this.chkLDAP.Text = "LDAP";
            // 
            // PanelTrusteeDetailsUser
            // 
            this.Controls.Add(this.chkLDAP);
            this.Controls.Add(this.lblTipoUser);
            this.Controls.Add(this.cbTipoUser);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.chkAuthority);
            this.Controls.Add(this.txtFullName);
            this.Controls.Add(this.Label1);
            this.Name = "PanelTrusteeDetailsUser";
            this.Controls.SetChildIndex(this.txtDescription, 0);
            this.Controls.SetChildIndex(this.lblName, 0);
            this.Controls.SetChildIndex(this.chkActive, 0);
            this.Controls.SetChildIndex(this.txtGroupName, 0);
            this.Controls.SetChildIndex(this.grpMembers, 0);
            this.Controls.SetChildIndex(this.lblDescription, 0);
            this.Controls.SetChildIndex(this.Label1, 0);
            this.Controls.SetChildIndex(this.txtFullName, 0);
            this.Controls.SetChildIndex(this.chkAuthority, 0);
            this.Controls.SetChildIndex(this.lblPassword, 0);
            this.Controls.SetChildIndex(this.txtPassword, 0);
            this.Controls.SetChildIndex(this.cbTipoUser, 0);
            this.Controls.SetChildIndex(this.lblTipoUser, 0);
            this.Controls.SetChildIndex(this.chkLDAP, 0);
            this.grpMembers.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

	#endregion

		private void GetExtraResources()
		{
			btnAdd.Image = SharedResourcesOld.CurrentSharedResources.Adicionar;
			btnRemove.Image = SharedResourcesOld.CurrentSharedResources.Apagar;

			if (! DesignMode)
				base.ParentChanged += PanelTrusteeUserDetails_ParentChanged;
		}

		// runs only once. sets tooltip as soon as it's parent appears
		private void PanelTrusteeUserDetails_ParentChanged(object sender, System.EventArgs e)
		{
			MultiPanel.CurrentToolTip.SetToolTip(btnAdd, SharedResourcesOld.CurrentSharedResources.AdicionarString);
			MultiPanel.CurrentToolTip.SetToolTip(btnRemove, SharedResourcesOld.CurrentSharedResources.ApagarString);
			base.ParentChanged -= PanelTrusteeUserDetails_ParentChanged;
		}

		protected override void DisplayMembership(GISADataset.UserGroupsRow ug)
		{
	#if ! TESTING
			// ignorar built-in trustees
			if (ug.TrusteeGroupRow.TrusteeRow.BuiltInTrustee)
			{
				return;
			}
	#endif

			ListViewItem tempWith1 = lvMembers.Items.Add(ug.TrusteeGroupRow.TrusteeRow.Name);
			if (! ug.TrusteeGroupRow.TrusteeRow.IsDescriptionNull())
				tempWith1.SubItems.Add(ug.TrusteeGroupRow.TrusteeRow.Description);
			else
				tempWith1.SubItems.Add(string.Empty);

			if (ug.TrusteeGroupRow.TrusteeRow.BuiltInTrustee)
			{
				tempWith1.ForeColor = System.Drawing.Color.Gray;
				tempWith1.Tag = ug;
			}
			else
				tempWith1.Tag = ug;
		}

		// password improvavel, valor para representar "password nao modificada"
		private string mOriginalPasswd = "ºººººº";
		public override void LoadData(DataRow CurrentDataRow, IDbConnection conn)
		{
			IsLoaded = false;
			base.LoadData(CurrentDataRow, conn);

			if (CurrentDataRow == null)
				return;

			if (! (CurrentTrusteeRow.GetTrusteeUserRows()[0].IsFullNameNull()))
				txtFullName.Text = CurrentTrusteeRow.GetTrusteeUserRows()[0].FullName;
			else
				txtFullName.Text = string.Empty;

			txtPassword.Text = mOriginalPasswd;
			chkAuthority.Checked = CurrentTrusteeRow.GetTrusteeUserRows()[0].IsAuthority;
			UpdateButtonState();
			IsLoaded = true;
		}

        public override void ModelToView()
        {
            if (CurrentTrusteeRow == null || CurrentTrusteeRow.RowState == DataRowState.Detached || ! IsLoaded)
				return;

            base.ModelToView();
            if (CurrentTrusteeRow.GetTrusteeUserRows().Length > 0)
            {
                GISADataset.TrusteeUserRow turow = CurrentTrusteeRow.GetTrusteeUserRows()[0];
                chkLDAP.Checked = turow.IsLDAPUser;

                DataRow[] ugAcessoCompleto =
                    GisaDataSetHelper.GetInstance().UserGroups.Select(string.Format("IDUser={0} AND IDGroup={1}", CurrentTrusteeRow.ID, PermissoesHelper.GrpAcessoCompleto.ID));

                DataRow[] ugAcessoPublicados =
                    GisaDataSetHelper.GetInstance().UserGroups.Select(string.Format("IDUser={0} AND IDGroup={1}", CurrentTrusteeRow.ID, PermissoesHelper.GrpAcessoPublicados.ID));

                Debug.Assert(!(ugAcessoCompleto.Length == 1 && ugAcessoPublicados.Length == 1), "O utilizador não pode pertencer aos grupos ACESSO_COMPLETO e ACESSO_PUBLICADOS ao mesmo tempo.");
                Debug.Assert(!(ugAcessoCompleto.Length == 0 && ugAcessoPublicados.Length == 0), "O utilizador tem de pertencer ao grupo ACESSO_COMPLETO ou ao grupo ACESSO_PUBLICADOS.");

                if (ugAcessoCompleto.Length > 0)
                    mTipoUser = TipoUser.AcessoTudo;
                else if (ugAcessoPublicados.Length > 0)
                    mTipoUser = TipoUser.AcessoInfoPub;

                cbTipoUser.SelectedIndex = (int)Enum.Parse(typeof(TipoUser), mTipoUser.ToString());

                this.cbTipoUser.SelectedIndexChanged += new System.EventHandler(this.cbTipoUser_SelectedIndexChanged);                
            }
        }

        private enum TipoUser
        {
            AcessoInfoPub = 0,
            AcessoTudo = 1
        }

        private TipoUser mTipoUser = TipoUser.AcessoInfoPub;
        public override void ViewToModel()
		{
			IsPopulated = false;
			if (CurrentTrusteeRow == null || CurrentTrusteeRow.RowState == DataRowState.Detached || ! IsLoaded)
				return;

			base.ViewToModel();
            if (CurrentTrusteeRow.GetTrusteeUserRows().Length > 0)
            {
                // LDAP
                GISADataset.TrusteeUserRow turow = CurrentTrusteeRow.GetTrusteeUserRows()[0];
                turow.IsLDAPUser = chkLDAP.Checked;

                CurrentTrusteeRow.GetTrusteeUserRows()[0].FullName = txtFullName.Text;
                if (!(txtPassword.Text.Equals(mOriginalPasswd)) && !(txtPassword.Text.Length == 0))
                    CurrentTrusteeRow.GetTrusteeUserRows()[0].Password = CryptographyHelper.GetMD5(txtPassword.Text);
                CurrentTrusteeRow.GetTrusteeUserRows()[0].IsAuthority = chkAuthority.Checked;
            }
			CurrentTrusteeRow.IsActive = chkActive.Checked;            

			IsPopulated = true;
		}

		public override void Deactivate()
		{
            GUIHelper.GUIHelper.clearField(chkAuthority);
            GUIHelper.GUIHelper.clearField(txtFullName);
            GUIHelper.GUIHelper.clearField(txtPassword);
			base.Deactivate();
		}

		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			FormUserGroups form = new FormUserGroups();
			byte[] Versao = null;
			if (form.ShowDialog() == DialogResult.OK)
			{
                this.Cursor = Cursors.WaitCursor;
				GISADataset.TrusteeRow tRow = null;
				List<long> tRowIDs = new List<long>();
				foreach (ListViewItem item in form.lstVwTrustees.SelectedItems)
				{
					tRow = (GISADataset.TrusteeRow)item.Tag;
					// verificar se o utilizador foi anteriormente adicionado ao grupo
					if (GisaDataSetHelper.GetInstance().UserGroups.Select(string.Format("IDUser={0} AND IDGroup={1}", (CurrentTrusteeRow.GetTrusteeUserRows()[0]).ID.ToString(), (tRow.GetTrusteeGroupRows()[0]).ID.ToString())).Length == 0)
					{
                        tRowIDs.Add(tRow.ID);
                        GisaDataSetHelper.GetInstance().UserGroups.AddUserGroupsRow(CurrentTrusteeRow.GetTrusteeUserRows()[0], tRow.GetTrusteeGroupRows()[0], Versao, 0);
					}
				}

                PersistencyHelper.save();
				PersistencyHelper.cleanDeletedData();
				EnumerateMembership(CurrentTrusteeRow);
                this.Cursor = Cursors.Default;
			}
		}

		private void btnRemove_Click(object sender, System.EventArgs e)
		{
			DeleteItems();
		}

		private void lvMembers_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyValue == Convert.ToInt32(Keys.Delete))
				DeleteItems();
		}

		private void DeleteItems()
		{
            this.Cursor = Cursors.WaitCursor;

			if (((DataRow)(lvMembers.SelectedItems[0].Tag)).RowState == DataRowState.Detached)
			{
				ListViewItem item = lvMembers.SelectedItems[0];
				lvMembers.SelectedItems.Clear();
				lvMembers.Items.Remove(item);
			}
			else
			{
                GUIHelper.GUIHelper.deleteSelectedLstVwItems(lvMembers);
                PersistencyHelper.save();
				PersistencyHelper.cleanDeletedData();
			}
            MultiPanel.Recontextualize();

            this.Cursor = Cursors.Default;
		}

		private void lvMembers_SelectedIndexChanged(object sender, EventArgs e)
		{
			UpdateButtonState();
		}

		private void UpdateButtonState()
		{
			if (lvMembers.SelectedItems.Count > 0)
			{
				// verificar se algum dos items selecionados é apagavel 
				foreach (ListViewItem item in lvMembers.SelectedItems)
				{
					if (item.Tag != null)
					{
						btnRemove.Enabled = true;
						return;
					}
				}
				btnRemove.Enabled = false;
			}
			else
				btnRemove.Enabled = false;
		}

		private void txtPassword_GotFocus(object sender, System.EventArgs e)
		{
			if (txtPassword.Text.Equals(mOriginalPasswd))
				txtPassword.Clear();
		}

        private void cbTipoUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            if ((TipoUser)Enum.Parse(typeof(TipoUser), cbTipoUser.SelectedIndex.ToString()) == TipoUser.AcessoTudo && mTipoUser != TipoUser.AcessoTudo)
            {
                mTipoUser = TipoUser.AcessoTudo;
                GISADataset.UserGroupsRow[] ugAcessoPublicados =
                    (GISADataset.UserGroupsRow[])GisaDataSetHelper.GetInstance().UserGroups.Select(string.Format("IDUser={0} AND IDGroup={1}", CurrentTrusteeRow.ID, PermissoesHelper.GrpAcessoPublicados.ID));

                Debug.Assert(ugAcessoPublicados.Length > 0);
                ugAcessoPublicados[0].Delete();

                GISADataset.UserGroupsRow ugAcessoCompleto = GisaDataSetHelper.GetInstance().UserGroups.NewUserGroupsRow();
                ugAcessoCompleto.IDUser = CurrentTrusteeRow.ID;
                ugAcessoCompleto.IDGroup = PermissoesHelper.GrpAcessoCompleto.ID;
                GisaDataSetHelper.GetInstance().UserGroups.AddUserGroupsRow(ugAcessoCompleto);
            }
            else if ((TipoUser)Enum.Parse(typeof(TipoUser), cbTipoUser.SelectedIndex.ToString()) == TipoUser.AcessoInfoPub && mTipoUser != TipoUser.AcessoInfoPub)                 
            {
                mTipoUser = TipoUser.AcessoInfoPub;
                GISADataset.UserGroupsRow[] ugAcessoCompleto =
                    (GISADataset.UserGroupsRow[])GisaDataSetHelper.GetInstance().UserGroups.Select(string.Format("IDUser={0} AND IDGroup={1}", CurrentTrusteeRow.ID, PermissoesHelper.GrpAcessoCompleto.ID));

                Debug.Assert(ugAcessoCompleto.Length > 0);
                ugAcessoCompleto[0].Delete();

                GISADataset.UserGroupsRow ugAcessoPublicados = GisaDataSetHelper.GetInstance().UserGroups.NewUserGroupsRow();
                ugAcessoPublicados.IDUser = CurrentTrusteeRow.ID;
                ugAcessoPublicados.IDGroup = PermissoesHelper.GrpAcessoPublicados.ID;
                GisaDataSetHelper.GetInstance().UserGroups.AddUserGroupsRow(ugAcessoPublicados);
            }

            GUIHelper.GUIHelper.deleteSelectedLstVwItems(lvMembers);
            PersistencyHelper.save();
            PersistencyHelper.cleanDeletedData();

            GUIHelper.GUIHelper.clearField(lvMembers);
            base.EnumerateMembership(CurrentTrusteeRow);
            this.Cursor = Cursors.Default;
        }
	}
}