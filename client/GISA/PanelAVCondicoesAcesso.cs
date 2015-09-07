using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using GISA.Model;
using DBAbstractDataLayer.DataAccessRules;
using GISA.SharedResources;

namespace GISA
{
	public class PanelAVCondicoesAcesso : GISA.GISAPanel
	{

	#region  Windows Form Designer generated code 

		public PanelAVCondicoesAcesso() : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call
            btnAddLingua.Click += btnAddLingua_Click;
            btnRemoveLingua.Click += btnRemoveLingua_Click;
            btnAddAlfabeto.Click += btnAddAlfabeto_Click;
            btnRemoveAlfabeto.Click += btnRemoveAlfabeto_Click;

			ResizeMiddle tempWith1 = new ResizeMiddle(this.grpIdioma, this.grpLingua, this.grpAlfabeto);

			GetExtraResources();

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
		internal System.Windows.Forms.GroupBox grpCondicoesAcesso;
		internal System.Windows.Forms.TextBox txtCondicoesAcesso;
		internal System.Windows.Forms.GroupBox grpCondicoesReproducao;
		internal System.Windows.Forms.TextBox txtCondicoesReproducao;
		internal System.Windows.Forms.GroupBox grpIdioma;
		internal GISA.Controls.PxListView lstVwLanguage;
		internal System.Windows.Forms.GroupBox grpLingua;
		internal System.Windows.Forms.GroupBox grpAlfabeto;
		internal System.Windows.Forms.Button btnRemoveAlfabeto;
		internal System.Windows.Forms.Button btnAddAlfabeto;
		internal System.Windows.Forms.Button btnRemoveLingua;
		internal System.Windows.Forms.Button btnAddLingua;
		internal GISA.Controls.PxListView lstVwAlfabeto;
		internal System.Windows.Forms.ColumnHeader colAlfabeto;
		internal System.Windows.Forms.ColumnHeader colLingua;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
            this.grpCondicoesAcesso = new System.Windows.Forms.GroupBox();
            this.txtCondicoesAcesso = new System.Windows.Forms.TextBox();
            this.grpCondicoesReproducao = new System.Windows.Forms.GroupBox();
            this.txtCondicoesReproducao = new System.Windows.Forms.TextBox();
            this.lstVwAlfabeto = new GISA.Controls.PxListView();
            this.colAlfabeto = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lstVwLanguage = new GISA.Controls.PxListView();
            this.colLingua = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.grpIdioma = new System.Windows.Forms.GroupBox();
            this.grpAlfabeto = new System.Windows.Forms.GroupBox();
            this.btnRemoveAlfabeto = new System.Windows.Forms.Button();
            this.btnAddAlfabeto = new System.Windows.Forms.Button();
            this.grpLingua = new System.Windows.Forms.GroupBox();
            this.btnRemoveLingua = new System.Windows.Forms.Button();
            this.btnAddLingua = new System.Windows.Forms.Button();
            this.grpCondicoesAcesso.SuspendLayout();
            this.grpCondicoesReproducao.SuspendLayout();
            this.grpIdioma.SuspendLayout();
            this.grpAlfabeto.SuspendLayout();
            this.grpLingua.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpCondicoesAcesso
            // 
            this.grpCondicoesAcesso.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpCondicoesAcesso.Controls.Add(this.txtCondicoesAcesso);
            this.grpCondicoesAcesso.Location = new System.Drawing.Point(3, 3);
            this.grpCondicoesAcesso.Name = "grpCondicoesAcesso";
            this.grpCondicoesAcesso.Size = new System.Drawing.Size(793, 366);
            this.grpCondicoesAcesso.TabIndex = 5;
            this.grpCondicoesAcesso.TabStop = false;
            this.grpCondicoesAcesso.Text = "4.1. Condições de acesso";
            // 
            // txtCondicoesAcesso
            // 
            this.txtCondicoesAcesso.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCondicoesAcesso.Location = new System.Drawing.Point(8, 14);
            this.txtCondicoesAcesso.Multiline = true;
            this.txtCondicoesAcesso.Name = "txtCondicoesAcesso";
            this.txtCondicoesAcesso.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtCondicoesAcesso.Size = new System.Drawing.Size(777, 344);
            this.txtCondicoesAcesso.TabIndex = 0;
            // 
            // grpCondicoesReproducao
            // 
            this.grpCondicoesReproducao.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpCondicoesReproducao.Controls.Add(this.txtCondicoesReproducao);
            this.grpCondicoesReproducao.Location = new System.Drawing.Point(3, 377);
            this.grpCondicoesReproducao.Name = "grpCondicoesReproducao";
            this.grpCondicoesReproducao.Size = new System.Drawing.Size(793, 104);
            this.grpCondicoesReproducao.TabIndex = 6;
            this.grpCondicoesReproducao.TabStop = false;
            this.grpCondicoesReproducao.Text = "4.2. Condições de reprodução";
            // 
            // txtCondicoesReproducao
            // 
            this.txtCondicoesReproducao.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCondicoesReproducao.Location = new System.Drawing.Point(8, 14);
            this.txtCondicoesReproducao.Multiline = true;
            this.txtCondicoesReproducao.Name = "txtCondicoesReproducao";
            this.txtCondicoesReproducao.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtCondicoesReproducao.Size = new System.Drawing.Size(777, 82);
            this.txtCondicoesReproducao.TabIndex = 0;
            // 
            // lstVwAlfabeto
            // 
            this.lstVwAlfabeto.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstVwAlfabeto.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colAlfabeto});
            this.lstVwAlfabeto.CustomizedSorting = false;
            this.lstVwAlfabeto.FullRowSelect = true;
            this.lstVwAlfabeto.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lstVwAlfabeto.HideSelection = false;
            this.lstVwAlfabeto.Location = new System.Drawing.Point(8, 16);
            this.lstVwAlfabeto.MultiSelect = false;
            this.lstVwAlfabeto.Name = "lstVwAlfabeto";
            this.lstVwAlfabeto.ReturnSubItemIndex = false;
            this.lstVwAlfabeto.Size = new System.Drawing.Size(318, 56);
            this.lstVwAlfabeto.TabIndex = 1;
            this.lstVwAlfabeto.UseCompatibleStateImageBehavior = false;
            this.lstVwAlfabeto.View = System.Windows.Forms.View.Details;
            this.lstVwAlfabeto.SelectedIndexChanged += new System.EventHandler(this.lstVwAlfabeto_SelectedIndexChanged);
            // 
            // colAlfabeto
            // 
            this.colAlfabeto.Width = 121;
            // 
            // lstVwLanguage
            // 
            this.lstVwLanguage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstVwLanguage.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colLingua});
            this.lstVwLanguage.CustomizedSorting = false;
            this.lstVwLanguage.FullRowSelect = true;
            this.lstVwLanguage.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lstVwLanguage.HideSelection = false;
            this.lstVwLanguage.Location = new System.Drawing.Point(8, 16);
            this.lstVwLanguage.MultiSelect = false;
            this.lstVwLanguage.Name = "lstVwLanguage";
            this.lstVwLanguage.ReturnSubItemIndex = false;
            this.lstVwLanguage.Size = new System.Drawing.Size(363, 56);
            this.lstVwLanguage.TabIndex = 10;
            this.lstVwLanguage.UseCompatibleStateImageBehavior = false;
            this.lstVwLanguage.View = System.Windows.Forms.View.Details;
            this.lstVwLanguage.SelectedIndexChanged += new System.EventHandler(this.lstVwLanguage_SelectedIndexChanged);
            // 
            // colLingua
            // 
            this.colLingua.Width = 121;
            // 
            // grpIdioma
            // 
            this.grpIdioma.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpIdioma.Controls.Add(this.grpAlfabeto);
            this.grpIdioma.Controls.Add(this.grpLingua);
            this.grpIdioma.Location = new System.Drawing.Point(3, 489);
            this.grpIdioma.Name = "grpIdioma";
            this.grpIdioma.Size = new System.Drawing.Size(793, 108);
            this.grpIdioma.TabIndex = 7;
            this.grpIdioma.TabStop = false;
            this.grpIdioma.Text = "4.3. Língua e alfabeto";
            // 
            // grpAlfabeto
            // 
            this.grpAlfabeto.Controls.Add(this.btnRemoveAlfabeto);
            this.grpAlfabeto.Controls.Add(this.btnAddAlfabeto);
            this.grpAlfabeto.Controls.Add(this.lstVwAlfabeto);
            this.grpAlfabeto.Location = new System.Drawing.Point(422, 16);
            this.grpAlfabeto.Name = "grpAlfabeto";
            this.grpAlfabeto.Size = new System.Drawing.Size(363, 80);
            this.grpAlfabeto.TabIndex = 12;
            this.grpAlfabeto.TabStop = false;
            this.grpAlfabeto.Text = "Alfabeto";
            // 
            // btnRemoveAlfabeto
            // 
            this.btnRemoveAlfabeto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveAlfabeto.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRemoveAlfabeto.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnRemoveAlfabeto.Location = new System.Drawing.Point(333, 48);
            this.btnRemoveAlfabeto.Name = "btnRemoveAlfabeto";
            this.btnRemoveAlfabeto.Size = new System.Drawing.Size(24, 24);
            this.btnRemoveAlfabeto.TabIndex = 5;
            // 
            // btnAddAlfabeto
            // 
            this.btnAddAlfabeto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddAlfabeto.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAddAlfabeto.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAddAlfabeto.Location = new System.Drawing.Point(333, 16);
            this.btnAddAlfabeto.Name = "btnAddAlfabeto";
            this.btnAddAlfabeto.Size = new System.Drawing.Size(24, 24);
            this.btnAddAlfabeto.TabIndex = 4;
            // 
            // grpLingua
            // 
            this.grpLingua.Controls.Add(this.btnRemoveLingua);
            this.grpLingua.Controls.Add(this.btnAddLingua);
            this.grpLingua.Controls.Add(this.lstVwLanguage);
            this.grpLingua.Location = new System.Drawing.Point(8, 16);
            this.grpLingua.Name = "grpLingua";
            this.grpLingua.Size = new System.Drawing.Size(408, 80);
            this.grpLingua.TabIndex = 11;
            this.grpLingua.TabStop = false;
            this.grpLingua.Text = "Língua";
            // 
            // btnRemoveLingua
            // 
            this.btnRemoveLingua.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveLingua.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRemoveLingua.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnRemoveLingua.Location = new System.Drawing.Point(378, 48);
            this.btnRemoveLingua.Name = "btnRemoveLingua";
            this.btnRemoveLingua.Size = new System.Drawing.Size(24, 24);
            this.btnRemoveLingua.TabIndex = 12;
            // 
            // btnAddLingua
            // 
            this.btnAddLingua.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddLingua.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAddLingua.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAddLingua.Location = new System.Drawing.Point(378, 16);
            this.btnAddLingua.Name = "btnAddLingua";
            this.btnAddLingua.Size = new System.Drawing.Size(24, 24);
            this.btnAddLingua.TabIndex = 11;
            // 
            // PanelAVCondicoesAcesso
            // 
            this.Controls.Add(this.grpIdioma);
            this.Controls.Add(this.grpCondicoesReproducao);
            this.Controls.Add(this.grpCondicoesAcesso);
            this.Name = "PanelAVCondicoesAcesso";
            this.grpCondicoesAcesso.ResumeLayout(false);
            this.grpCondicoesAcesso.PerformLayout();
            this.grpCondicoesReproducao.ResumeLayout(false);
            this.grpCondicoesReproducao.PerformLayout();
            this.grpIdioma.ResumeLayout(false);
            this.grpAlfabeto.ResumeLayout(false);
            this.grpLingua.ResumeLayout(false);
            this.ResumeLayout(false);

		}

	#endregion

		private void GetExtraResources()
		{
			btnAddAlfabeto.Image = SharedResourcesOld.CurrentSharedResources.Adicionar;
			btnAddLingua.Image = SharedResourcesOld.CurrentSharedResources.Adicionar;
			btnRemoveAlfabeto.Image = SharedResourcesOld.CurrentSharedResources.Apagar;
			btnRemoveLingua.Image = SharedResourcesOld.CurrentSharedResources.Apagar;

			base.ParentChanged += PanelAVCondicoesAcesso_ParentChanged;
		}

		private void PanelAVCondicoesAcesso_ParentChanged(object sender, System.EventArgs e)
		{
			MultiPanel.CurrentToolTip.SetToolTip(btnAddAlfabeto, SharedResourcesOld.CurrentSharedResources.AdicionarString);
			MultiPanel.CurrentToolTip.SetToolTip(btnAddLingua, SharedResourcesOld.CurrentSharedResources.AdicionarString);
			MultiPanel.CurrentToolTip.SetToolTip(btnRemoveAlfabeto, SharedResourcesOld.CurrentSharedResources.ApagarString);
			MultiPanel.CurrentToolTip.SetToolTip(btnRemoveLingua, SharedResourcesOld.CurrentSharedResources.ApagarString);

			base.ParentChanged -= PanelAVCondicoesAcesso_ParentChanged;
		}

		private void LoadAlfabetoAndLingua()
		{
			this.lstVwAlfabeto.BeginUpdate();
			this.lstVwAlfabeto.Items.Clear();

			if (CurrentSFRDCondicaoDeAcesso.GetSFRDAlfabetoRows().Length > 0)
			{
				foreach (GISADataset.SFRDAlfabetoRow alfRow in CurrentSFRDCondicaoDeAcesso.GetSFRDAlfabetoRows())
				{
					ListViewItem lstVw = new ListViewItem();
					lstVw.SubItems[colAlfabeto.Index].Text = alfRow.Iso15924Row.ScriptNameEnglish.Trim();
					lstVw.Tag = alfRow;
					this.lstVwAlfabeto.Items.Add(lstVw);
				}
			}
			this.lstVwAlfabeto.EndUpdate();
			this.lstVwAlfabeto.Enabled = true;

			this.lstVwLanguage.BeginUpdate();
			this.lstVwLanguage.Items.Clear();
			if (CurrentSFRDCondicaoDeAcesso.GetSFRDLinguaRows().Length > 0)
			{
				foreach (GISADataset.SFRDLinguaRow langRow in CurrentSFRDCondicaoDeAcesso.GetSFRDLinguaRows())
				{
					ListViewItem lstVw = new ListViewItem();
					lstVw.Tag = langRow;
					lstVw.SubItems[colLingua.Index].Text = langRow.Iso639Row.LanguageNameEnglish.Trim();
					this.lstVwLanguage.Items.Add(lstVw);
				}
			}
			this.lstVwLanguage.EndUpdate();
		}

		private GISADataset.SFRDCondicaoDeAcessoRow CurrentSFRDCondicaoDeAcesso;
		public override void LoadData(DataRow CurrentDataRow, IDbConnection conn)
		{
			IsLoaded = false;
			byte[] Versao = null;
			if (CurrentDataRow == null)
				return;

			GISADataset.FRDBaseRow CurrentFRDBase = null;
			CurrentFRDBase = (GISADataset.FRDBaseRow)CurrentDataRow;

			try
			{
				FRDRule.Current.LoadPanelAVCondicoesAcessoData(GisaDataSetHelper.GetInstance(), CurrentFRDBase.ID, conn);
				CurrentSFRDCondicaoDeAcesso = (GISADataset.SFRDCondicaoDeAcessoRow)(GisaDataSetHelper.GetInstance().SFRDCondicaoDeAcesso.Select("IDFRDBase=" + CurrentFRDBase.ID.ToString())[0]);
			}
			catch (IndexOutOfRangeException)
			{
				CurrentSFRDCondicaoDeAcesso = GisaDataSetHelper.GetInstance().SFRDCondicaoDeAcesso.AddSFRDCondicaoDeAcessoRow(CurrentFRDBase, "", "", "", "", Versao, 0);
			}

            IsLoaded = true;
		}

		public override void ModelToView()
		{
			IsPopulated = false;

			if (CurrentSFRDCondicaoDeAcesso.IsCondicaoDeAcessoNull())
				txtCondicoesAcesso.Text = "";
			else
				txtCondicoesAcesso.Text = CurrentSFRDCondicaoDeAcesso.CondicaoDeAcesso;

			if (CurrentSFRDCondicaoDeAcesso.IsCondicaoDeReproducaoNull())
				txtCondicoesReproducao.Text = "";
			else
				txtCondicoesReproducao.Text = CurrentSFRDCondicaoDeAcesso.CondicaoDeReproducao;

			LoadAlfabetoAndLingua();

            UpdateButtons();

			IsPopulated = true;
		}

		public override void ViewToModel()
		{
			if (CurrentSFRDCondicaoDeAcesso == null || ! IsLoaded)
				return;

			CurrentSFRDCondicaoDeAcesso.CondicaoDeAcesso = txtCondicoesAcesso.Text;
			CurrentSFRDCondicaoDeAcesso.CondicaoDeReproducao = txtCondicoesReproducao.Text;
		}

		public override void Deactivate()
		{
            GUIHelper.GUIHelper.clearField(txtCondicoesAcesso);
            GUIHelper.GUIHelper.clearField(txtCondicoesReproducao);
		}
		
		private void btnAddLingua_Click(object sender, System.EventArgs e)
		{
			if (CurrentSFRDCondicaoDeAcesso.RowState == DataRowState.Detached)
			{
				MessageBox.Show("O Nivel selecionado como contexto foi " + System.Environment.NewLine + "apagada por outro utilizador e por esse motivo não pode ser editada.", "Seleção de Nivel", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			FormPickISOs frmPick = new FormPickISOs(false);
			frmPick.Title = "Linguagens";
			frmPick.reloadList();

			switch (frmPick.ShowDialog())
			{
				case DialogResult.OK:
					foreach (ListViewItem li in frmPick.SelectedItems)
					{
						GISADataset.Iso639Row langRow = null;
						GISADataset.SFRDLinguaRow sLRow = null;
						langRow = (GISADataset.Iso639Row)li.Tag;
						string query = string.Format("IDIso639={0} AND IDFRDBase={1}", langRow.ID, CurrentSFRDCondicaoDeAcesso.IDFRDBase);
                        
						if ((GisaDataSetHelper.GetInstance().SFRDLingua.Select(query)).Length == 0)
						{
							sLRow = GisaDataSetHelper.GetInstance().SFRDLingua.NewSFRDLinguaRow();
							sLRow.IDIso639 = langRow.ID;
							sLRow.SFRDCondicaoDeAcessoRow = CurrentSFRDCondicaoDeAcesso;
							sLRow.isDeleted = 0;
                            
							GisaDataSetHelper.GetInstance().SFRDLingua.AddSFRDLinguaRow(sLRow);
                            
							if (sLRow.RowState == DataRowState.Detached)
								MessageBox.Show("Não foi possível estabelecer a relação uma vez que a Linguagem " + System.Environment.NewLine + "que pretende associar foi apagada por outro utilizador.", "Erro ao estabelecer relação", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
						}
						else
							MessageBox.Show("A relação que pretende adicionar já existe.", "Erro ao estabelecer relação", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
					}
					LoadAlfabetoAndLingua();
					break;
				case DialogResult.Cancel:
				break;
			}
		}

		private void btnRemoveLingua_Click(object sender, System.EventArgs e)
		{
			if (lstVwLanguage.SelectedItems.Count > 0)
                GUIHelper.GUIHelper.deleteSelectedLstVwItems(lstVwLanguage);
		}

		private void btnAddAlfabeto_Click(object sender, System.EventArgs e)
		{
			if (CurrentSFRDCondicaoDeAcesso.RowState == DataRowState.Detached)
			{
				MessageBox.Show("O Nivel selecionado como contexto foi " + System.Environment.NewLine + "apagada por outro utilizador e por esse motivo não pode ser editada.", "Seleção de Nivel", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			FormPickISOs frmPick = new FormPickISOs(true);
			frmPick.Title = "Alfabetos";
			frmPick.reloadList();

			switch (frmPick.ShowDialog())
			{
				case DialogResult.OK:
					foreach (ListViewItem li in frmPick.SelectedItems)
					{
						GISADataset.Iso15924Row alphaRow = null;
						GISADataset.SFRDAlfabetoRow sARow = null;
						alphaRow = (GISADataset.Iso15924Row)li.Tag;
						string query = string.Format("IDIso15924={0} AND IDFRDBase={1}", alphaRow.ID, CurrentSFRDCondicaoDeAcesso.IDFRDBase);

						if ((GisaDataSetHelper.GetInstance().SFRDAlfabeto.Select(query)).Length == 0)
						{
							sARow = GisaDataSetHelper.GetInstance().SFRDAlfabeto.NewSFRDAlfabetoRow();
							sARow.IDIso15924 = alphaRow.ID;
							sARow.SFRDCondicaoDeAcessoRow = CurrentSFRDCondicaoDeAcesso;
							sARow.isDeleted = 0;

							GisaDataSetHelper.GetInstance().SFRDAlfabeto.AddSFRDAlfabetoRow(sARow);

							if (sARow.RowState == DataRowState.Detached)
								MessageBox.Show("Não foi possível estabelecer a relação uma vez que a Linguagem " + System.Environment.NewLine + "que pretende associar foi apagada por outro utilizador.", "Erro ao estabelecer relação", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
						}
						else
							MessageBox.Show("A relação que pretende adicionar já existe.", "Erro ao estabelecer relação", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
					}
					LoadAlfabetoAndLingua();
					break;
				case DialogResult.Cancel:
				break;
			}
		}

		private void btnRemoveAlfabeto_Click(object sender, System.EventArgs e)
		{
			if (lstVwAlfabeto.SelectedItems.Count > 0)
                GUIHelper.GUIHelper.deleteSelectedLstVwItems(lstVwAlfabeto);
		}

        private void lstVwLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateButtons();
        }

        private void lstVwAlfabeto_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateButtons();
        }

        private void UpdateButtons()
        {
            btnRemoveLingua.Enabled = lstVwLanguage.SelectedItems.Count > 0;
            btnRemoveAlfabeto.Enabled = lstVwAlfabeto.SelectedItems.Count > 0;
        }
	}
}