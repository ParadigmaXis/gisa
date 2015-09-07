using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using GISA.Model;
using GISA.GUIHelper;
using DBAbstractDataLayer.DataAccessRules;
using GISA.SharedResources;

namespace GISA
{
	public class FormManageAutoresDescricao : System.Windows.Forms.Form
	{

	#region  Windows Form Designer generated code 

		public FormManageAutoresDescricao() : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call
            lstUsers.SelectedIndexChanged += lstUsers_SelectedIndexChanged;
            btnSair.Click += btnSair_Click;
            ToolBarUsers.ButtonClick += ToolBarUsers_ButtonClick;

			GetExtraResources();
			PopulateTrusteeUsers();
			if (lstUsers.Items.Count > 0)
			{
				lstUsers.Items[0].Selected = true;
			}
			else
			{
				PopulateTrusteeUsersDetails();
			}
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
		internal System.Windows.Forms.TextBox txtNomeCompleto;
		internal System.Windows.Forms.Button btnSair;
		internal System.Windows.Forms.Label lblUsername;
		internal System.Windows.Forms.Label lblNomeCompleto;
		internal System.Windows.Forms.GroupBox grpDetalhes;
		internal System.Windows.Forms.ToolBarButton ToolBarButtonCreateUser;
		internal System.Windows.Forms.ColumnHeader ColumnHeaderNomeCompleto;
		internal System.Windows.Forms.ListView lstUsers;
		internal System.Windows.Forms.ToolBar ToolBarUsers;
		internal System.Windows.Forms.TextBox txtNome;
		internal System.Windows.Forms.CheckBox chkAutoridade;
		internal System.Windows.Forms.ColumnHeader ColumnHeaderAutoridade;
		internal System.Windows.Forms.ToolBarButton ToolBarButtonDeleteUser;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			this.lstUsers = new System.Windows.Forms.ListView();
			this.ColumnHeaderNomeCompleto = new System.Windows.Forms.ColumnHeader();
			this.ColumnHeaderAutoridade = new System.Windows.Forms.ColumnHeader();
			this.txtNome = new System.Windows.Forms.TextBox();
			this.txtNomeCompleto = new System.Windows.Forms.TextBox();
			this.btnSair = new System.Windows.Forms.Button();
			this.lblUsername = new System.Windows.Forms.Label();
			this.lblNomeCompleto = new System.Windows.Forms.Label();
			this.chkAutoridade = new System.Windows.Forms.CheckBox();
			this.grpDetalhes = new System.Windows.Forms.GroupBox();
			this.ToolBarUsers = new System.Windows.Forms.ToolBar();
			this.ToolBarButtonCreateUser = new System.Windows.Forms.ToolBarButton();
			this.ToolBarButtonDeleteUser = new System.Windows.Forms.ToolBarButton();
			this.grpDetalhes.SuspendLayout();
			this.SuspendLayout();
			//
			//lstUsers
			//
			this.lstUsers.Anchor = (System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
			this.lstUsers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {this.ColumnHeaderNomeCompleto, this.ColumnHeaderAutoridade});
			this.lstUsers.FullRowSelect = true;
			this.lstUsers.HideSelection = false;
			this.lstUsers.Location = new System.Drawing.Point(4, 32);
			this.lstUsers.MultiSelect = false;
			this.lstUsers.Name = "lstUsers";
			this.lstUsers.Size = new System.Drawing.Size(464, 188);
			this.lstUsers.TabIndex = 1;
			this.lstUsers.View = System.Windows.Forms.View.Details;
			//
			//ColumnHeaderNomeCompleto
			//
			this.ColumnHeaderNomeCompleto.Text = "Nome completo";
			this.ColumnHeaderNomeCompleto.Width = 365;
			//
			//ColumnHeaderAutoridade
			//
			this.ColumnHeaderAutoridade.Text = "Autoridade ativa";
			this.ColumnHeaderAutoridade.Width = 95;
			//
			//txtNome
			//
			this.txtNome.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
			this.txtNome.Location = new System.Drawing.Point(100, 20);
			this.txtNome.Name = "txtNome";
			this.txtNome.Size = new System.Drawing.Size(152, 20);
			this.txtNome.TabIndex = 1;
			this.txtNome.Text = "";
			//
			//txtNomeCompleto
			//
			this.txtNomeCompleto.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
			this.txtNomeCompleto.Location = new System.Drawing.Point(100, 44);
			this.txtNomeCompleto.Name = "txtNomeCompleto";
			this.txtNomeCompleto.Size = new System.Drawing.Size(356, 20);
			this.txtNomeCompleto.TabIndex = 3;
			this.txtNomeCompleto.Text = "";
			//
			//btnSair
			//
			this.btnSair.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
			this.btnSair.Location = new System.Drawing.Point(364, 304);
			this.btnSair.Name = "btnSair";
			this.btnSair.TabIndex = 3;
			this.btnSair.Text = "Aceitar";
			//
			//lblUsername
			//
			this.lblUsername.Location = new System.Drawing.Point(8, 24);
			this.lblUsername.Name = "lblUsername";
			this.lblUsername.Size = new System.Drawing.Size(88, 16);
			this.lblUsername.TabIndex = 5;
			this.lblUsername.Text = "Nome:";
			//
			//lblNomeCompleto
			//
			this.lblNomeCompleto.Location = new System.Drawing.Point(8, 44);
			this.lblNomeCompleto.Name = "lblNomeCompleto";
			this.lblNomeCompleto.Size = new System.Drawing.Size(88, 16);
			this.lblNomeCompleto.TabIndex = 6;
			this.lblNomeCompleto.Text = "Nome completo:";
			//
			//chkAutoridade
			//
			this.chkAutoridade.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
			this.chkAutoridade.Location = new System.Drawing.Point(276, 20);
			this.chkAutoridade.Name = "chkAutoridade";
			this.chkAutoridade.Size = new System.Drawing.Size(112, 16);
			this.chkAutoridade.TabIndex = 2;
			this.chkAutoridade.Text = "Autoridade ativa";
			//
			//grpDetalhes
			//
			this.grpDetalhes.Anchor = (System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
			this.grpDetalhes.Controls.Add(this.chkAutoridade);
			this.grpDetalhes.Controls.Add(this.txtNome);
			this.grpDetalhes.Controls.Add(this.txtNomeCompleto);
			this.grpDetalhes.Controls.Add(this.lblUsername);
			this.grpDetalhes.Controls.Add(this.lblNomeCompleto);
			this.grpDetalhes.Location = new System.Drawing.Point(4, 224);
			this.grpDetalhes.Name = "grpDetalhes";
			this.grpDetalhes.Size = new System.Drawing.Size(464, 72);
			this.grpDetalhes.TabIndex = 2;
			this.grpDetalhes.TabStop = false;
			this.grpDetalhes.Text = "Detalhes";
			//
			//ToolBarUsers
			//
			this.ToolBarUsers.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
			this.ToolBarUsers.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {this.ToolBarButtonCreateUser, this.ToolBarButtonDeleteUser});
			this.ToolBarUsers.DropDownArrows = true;
			this.ToolBarUsers.Location = new System.Drawing.Point(0, 0);
			this.ToolBarUsers.Name = "ToolBarUsers";
			this.ToolBarUsers.ShowToolTips = true;
			this.ToolBarUsers.Size = new System.Drawing.Size(472, 28);
			this.ToolBarUsers.TabIndex = 120;
			//
			//ToolBarButtonCreateUser
			//
			this.ToolBarButtonCreateUser.ToolTipText = "Criar autor";
			//
			//ToolBarButtonDeleteUser
			//
			this.ToolBarButtonDeleteUser.ToolTipText = "Remover autor";
			//
			//FormManageAutoresDescricao
			//
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(472, 333);
			this.ControlBox = false;
			this.Controls.Add(this.ToolBarUsers);
			this.Controls.Add(this.grpDetalhes);
			this.Controls.Add(this.btnSair);
			this.Controls.Add(this.lstUsers);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormManageAutoresDescricao";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Autores das descrições";
			this.grpDetalhes.ResumeLayout(false);
			this.ResumeLayout(false);

			//INSTANT C# NOTE: Converted event handlers:
			

		}

	#endregion

		private void GetExtraResources()
		{
			ToolBarUsers.ImageList = SharedResourcesOld.CurrentSharedResources.UsrManipulacaoImageList;
			ToolBarButtonCreateUser.ImageIndex = 0;
			ToolBarButtonDeleteUser.ImageIndex = 2;
		}

		private void PopulateTrusteeUsers()
		{
			IDbConnection conn = GisaDataSetHelper.GetConnection();

			// Obter utilizadores que sejam autoridade        
			try
			{
				conn.Open();
				TrusteeRule.Current.LoadTrusteeUsers(GisaDataSetHelper.GetInstance(), conn);
			}
			catch (Exception ex)
			{
				Trace.WriteLine(ex);
				throw;
			}
			finally
			{
				conn.Close();
			}
			//TODO: PersistencyHelper.cleanDeletedRows()?

			// Apresentar utilizadores que sejam autoridade e não builtin
			foreach (GISADataset.TrusteeUserRow tuRow in GisaDataSetHelper.GetInstance().TrusteeUser.Select())
			{
				if (! tuRow.TrusteeRow.BuiltInTrustee)
				{
					AddUserToList(tuRow);
				}
			}
		}

		public ListViewItem AddUserToList(GISADataset.TrusteeUserRow tuRow)
		{
			string tuFullName = string.Empty;
			if (! (tuRow["FullName"] == DBNull.Value))
			{
				tuFullName = tuRow.FullName;
			}
			ListViewItem item = new ListViewItem(tuFullName);
			item.SubItems.Add(TranslationHelper.FormatBoolean(tuRow.IsAuthority));
			item.Tag = tuRow;
			lstUsers.Items.Add(item);
			return item;
		}

		private void lstUsers_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			PopulateTrusteeUsersDetails();
		}

		private void PopulateTrusteeUsersDetails()
		{
			GISADataset.TrusteeUserRow tuRow = null;
			if (lstUsers.SelectedItems.Count > 0)
			{
				tuRow = (GISADataset.TrusteeUserRow)(lstUsers.SelectedItems[0].Tag);
			}
			clearDetailsBindings();
			BindDetails(tuRow);
			UpdateControlsState(tuRow);
		}

		private void UpdateControlsState(GISADataset.TrusteeUserRow tuRow)
		{
			IDbConnection conn = GisaDataSetHelper.GetConnection();
			try
			{
				conn.Open();
				// se o utilizar estiver já a ser utilizado em alguma 
				// descrição é necessário impedir que seja editado ou removido
				if (tuRow == null || TrusteeRule.Current.IsUserInUse(tuRow.ID, conn))
				{
					txtNome.Enabled = false;
					txtNomeCompleto.Enabled = false;
					ToolBarButtonDeleteUser.Enabled = false;
				}
				else
				{
					txtNome.Enabled = true;
					txtNomeCompleto.Enabled = true;
					ToolBarButtonDeleteUser.Enabled = true;
				}
			}
			finally
			{
				conn.Close();
			}
		}
		
		private void clearDetailsBindings()
		{
			txtNome.DataBindings.Clear();
			txtNome.Clear();
			txtNomeCompleto.DataBindings.Clear();
			txtNomeCompleto.Clear();
			chkAutoridade.DataBindings.Clear();
			chkAutoridade.Checked = false;
		}

		private void BindDetails(GISADataset.TrusteeUserRow tuRow)
		{
			if (tuRow == null)
			{
				return;
			}

			if (tuRow.TrusteeRow["Name"] == DBNull.Value)
			{
				tuRow.TrusteeRow.Name = string.Empty;
			}

			if (tuRow["FullName"] == DBNull.Value)
			{
				tuRow.FullName = string.Empty;
			}

			txtNome.DataBindings.Add("Text", tuRow.TrusteeRow, "Name");
			txtNomeCompleto.DataBindings.Add("Text", tuRow, "FullName");
			chkAutoridade.DataBindings.Add("Checked", tuRow, "IsAuthority");
		}

		private void btnSair_Click(object sender, System.EventArgs e)
		{
            // TODO: que fazer com o código?
			//GisaDataSetHelper.GetTrusteeDataAdapter().Update(GisaDataSetHelper.GetInstance().Trustee.Select("", "", DataViewRowState.Added Or DataViewRowState.ModifiedCurrent))
			//GisaDataSetHelper.GetTrusteeUserDataAdapter().Update(GisaDataSetHelper.GetInstance().TrusteeUser.Select("", "", DataViewRowState.Added Or DataViewRowState.ModifiedCurrent))
			//GisaDataSetHelper.GetUserGroupsDataAdapter().Update(GisaDataSetHelper.GetInstance().UserGroups.Select("", "", DataViewRowState.Added))
			PersistencyHelper.save();
			PersistencyHelper.cleanDeletedData();
			this.Close();
		}

		private void ToolBarUsers_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			if (e.Button == ToolBarButtonCreateUser)
			{
				GISADataset.TrusteeUserRow tuRow = null;
				tuRow = CreateNewUser();
				ListViewItem item = null;
				item = AddUserToList(tuRow);
				item.Selected = true;
				txtNome.Focus();
				txtNome.SelectAll();
			}
			else if (e.Button == ToolBarButtonDeleteUser)
			{
				ListViewItem item = null;
				GISADataset.TrusteeUserRow truRow = null;
				item = lstUsers.SelectedItems[0];
				truRow = (GISADataset.TrusteeUserRow)item.Tag;

				clearDetailsBindings();
				MasterPanelTrustee.DeleteTrusteeAndRelatedRows(truRow.TrusteeRow);
				item.Remove();
			}
		}

		public GISADataset.TrusteeUserRow CreateNewUser()
		{
			//adicionar um utilizador sem grupo nem permissões para utilização da aplicação
			//, com builtinuser = false, com isauhtority = true e com isactive = false
			GISADataset.TrusteeRow tRow = null;
			GISADataset.TrusteeUserRow tuRow = null;
			
			byte[] Versao = null;
			try
			{
				tRow = GisaDataSetHelper.GetInstance().Trustee.AddTrusteeRow("autor", "", "USR", false, false, true, true, Versao, 0);
				tuRow = GisaDataSetHelper.GetInstance().TrusteeUser.NewTrusteeUserRow();
				tuRow.TrusteeRow = tRow;
				tuRow.Password = "";
				tuRow.FullName = "Novo autor";
				tuRow.IsAuthority = true;
				tuRow["IDTrusteeUserDefaultAuthority"] = DBNull.Value;
				GisaDataSetHelper.GetInstance().TrusteeUser.AddTrusteeUserRow(tuRow);

                // TODO: apagar?
				//GisaDataSetHelper.GetTrusteeDataAdapter.Update(New DataRow() {tRow})
				//GisaDataSetHelper.GetTrusteeUserDataAdapter.Update(New DataRow() {tuRow})

				PersistencyHelper.save();
				PersistencyHelper.cleanDeletedData();
			}
			finally
			{
                // TODO: apagar?
				//ho.Dispose()
			}

			return tuRow;
		}
	}

} //end of root namespace