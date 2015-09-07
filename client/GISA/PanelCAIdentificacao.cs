using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;

using GISA.Model;
using DBAbstractDataLayer.DataAccessRules;
using GISA.SharedResources;

using GISA.Controls.ControloAut;

namespace GISA
{
	public class PanelCAIdentificacao : GISA.GISAPanel
	{
        private GISADataset.ControloAutRow CurrentControloAut;

	#region  Windows Form Designer generated code 

		public PanelCAIdentificacao() : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

			ResizeMiddle tempWith1 = new ResizeMiddle(this, this.grpCodigoCA, this.grpTipoEntidadeProdutora);

			//Add any initialization after the InitializeComponent() call
            btnAdd.Click += btnAdd_Click;
            btnEdit.Click += btnEdit_Click;
            btnRemove.Click += btnRemove_Click;
            ControlTermosIndexacao1.AfterSelect += new TreeViewEventHandler(ControlTermosIndexacao1_AfterSelect);
            ControlTermosIndexacao1.KeyUp += new KeyEventHandler(ControlTermosIndexacao1_KeyUp);

			GetExtraResources();

			btnAdd.Enabled = false;
			btnEdit.Enabled = false;
			btnRemove.Enabled = false;
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
		internal System.Windows.Forms.GroupBox grpTermoIndexacao;
        internal ControlTermosIndexacao ControlTermosIndexacao1;
		internal System.Windows.Forms.ComboBox cbTipoEntidadeProdutora;
		internal System.Windows.Forms.GroupBox grpTipoEntidadeProdutora;
		internal System.Windows.Forms.GroupBox grpIdentificadorUnico;
		internal System.Windows.Forms.TextBox txtIdentificadorUnico;
		internal System.Windows.Forms.Button btnRemove;
		internal System.Windows.Forms.Button btnAdd;
		internal System.Windows.Forms.GroupBox grpCodigoCA;
		internal System.Windows.Forms.TextBox txtCodRef;
        internal GroupBox grpTipoDocumento;
        internal ComboBox cbTipoDocumento;
		internal System.Windows.Forms.Button btnEdit;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
            this.grpTermoIndexacao = new System.Windows.Forms.GroupBox();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.ControlTermosIndexacao1 = new GISA.Controls.ControloAut.ControlTermosIndexacao();
            this.grpCodigoCA = new System.Windows.Forms.GroupBox();
            this.txtCodRef = new System.Windows.Forms.TextBox();
            this.grpTipoDocumento = new System.Windows.Forms.GroupBox();
            this.cbTipoDocumento = new System.Windows.Forms.ComboBox();
            this.grpTipoEntidadeProdutora = new System.Windows.Forms.GroupBox();
            this.cbTipoEntidadeProdutora = new System.Windows.Forms.ComboBox();
            this.grpIdentificadorUnico = new System.Windows.Forms.GroupBox();
            this.txtIdentificadorUnico = new System.Windows.Forms.TextBox();
            this.grpTermoIndexacao.SuspendLayout();
            this.grpCodigoCA.SuspendLayout();
            this.grpTipoDocumento.SuspendLayout();
            this.grpTipoEntidadeProdutora.SuspendLayout();
            this.grpIdentificadorUnico.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpTermoIndexacao
            // 
            this.grpTermoIndexacao.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpTermoIndexacao.Controls.Add(this.btnEdit);
            this.grpTermoIndexacao.Controls.Add(this.btnRemove);
            this.grpTermoIndexacao.Controls.Add(this.btnAdd);
            this.grpTermoIndexacao.Controls.Add(this.ControlTermosIndexacao1);
            this.grpTermoIndexacao.Location = new System.Drawing.Point(8, 3);
            this.grpTermoIndexacao.Name = "grpTermoIndexacao";
            this.grpTermoIndexacao.Size = new System.Drawing.Size(788, 542);
            this.grpTermoIndexacao.TabIndex = 2;
            this.grpTermoIndexacao.TabStop = false;
            this.grpTermoIndexacao.Text = "1.2. - 1.5. Termo de indexação";
            // 
            // btnEdit
            // 
            this.btnEdit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnEdit.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnEdit.Location = new System.Drawing.Point(758, 98);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(24, 24);
            this.btnEdit.TabIndex = 4;
            this.btnEdit.Visible = false;
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRemove.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnRemove.Location = new System.Drawing.Point(758, 66);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(24, 24);
            this.btnRemove.TabIndex = 3;
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAdd.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAdd.Location = new System.Drawing.Point(758, 34);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(24, 24);
            this.btnAdd.TabIndex = 2;
            // 
            // ControlTermosIndexacao1
            // 
            this.ControlTermosIndexacao1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ControlTermosIndexacao1.Location = new System.Drawing.Point(6, 16);
            this.ControlTermosIndexacao1.Name = "ControlTermosIndexacao1";
            this.ControlTermosIndexacao1.NavigationMode = false;
            this.ControlTermosIndexacao1.Size = new System.Drawing.Size(748, 518);
            this.ControlTermosIndexacao1.TabIndex = 1;
            // 
            // grpCodigoCA
            // 
            this.grpCodigoCA.Controls.Add(this.txtCodRef);
            this.grpCodigoCA.Location = new System.Drawing.Point(8, 8);
            this.grpCodigoCA.Name = "grpCodigoCA";
            this.grpCodigoCA.Size = new System.Drawing.Size(350, 48);
            this.grpCodigoCA.TabIndex = 2;
            this.grpCodigoCA.TabStop = false;
            this.grpCodigoCA.Text = "Código parcial";
            this.grpCodigoCA.Visible = false;
            // 
            // txtCodRef
            // 
            this.txtCodRef.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCodRef.Enabled = false;
            this.txtCodRef.Location = new System.Drawing.Point(8, 16);
            this.txtCodRef.Name = "txtCodRef";
            this.txtCodRef.Size = new System.Drawing.Size(334, 20);
            this.txtCodRef.TabIndex = 0;
            // 
            // grpTipoDocumento
            // 
            this.grpTipoDocumento.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpTipoDocumento.Controls.Add(this.cbTipoDocumento);
            this.grpTipoDocumento.Location = new System.Drawing.Point(8, 16);
            this.grpTipoDocumento.Name = "grpTipoDocumento";
            this.grpTipoDocumento.Size = new System.Drawing.Size(788, 48);
            this.grpTipoDocumento.TabIndex = 2;
            this.grpTipoDocumento.TabStop = false;
            this.grpTipoDocumento.Text = "1.*. Estruturação de documentos segundo";
            this.grpTipoDocumento.Visible = false;
            // 
            // cbTipoDocumento
            // 
            this.cbTipoDocumento.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbTipoDocumento.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTipoDocumento.Location = new System.Drawing.Point(8, 16);
            this.cbTipoDocumento.Name = "cbTipoDocumento";
            this.cbTipoDocumento.Size = new System.Drawing.Size(774, 21);
            this.cbTipoDocumento.TabIndex = 0;
            // 
            // grpTipoEntidadeProdutora
            // 
            this.grpTipoEntidadeProdutora.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.grpTipoEntidadeProdutora.Controls.Add(this.cbTipoEntidadeProdutora);
            this.grpTipoEntidadeProdutora.Location = new System.Drawing.Point(444, 8);
            this.grpTipoEntidadeProdutora.Name = "grpTipoEntidadeProdutora";
            this.grpTipoEntidadeProdutora.Size = new System.Drawing.Size(360, 48);
            this.grpTipoEntidadeProdutora.TabIndex = 1;
            this.grpTipoEntidadeProdutora.TabStop = false;
            this.grpTipoEntidadeProdutora.Text = "1.1. Tipo de entidade";
            this.grpTipoEntidadeProdutora.Visible = false;
            // 
            // cbTipoEntidadeProdutora
            // 
            this.cbTipoEntidadeProdutora.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbTipoEntidadeProdutora.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTipoEntidadeProdutora.Location = new System.Drawing.Point(8, 16);
            this.cbTipoEntidadeProdutora.Name = "cbTipoEntidadeProdutora";
            this.cbTipoEntidadeProdutora.Size = new System.Drawing.Size(344, 21);
            this.cbTipoEntidadeProdutora.TabIndex = 0;
            // 
            // grpIdentificadorUnico
            // 
            this.grpIdentificadorUnico.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpIdentificadorUnico.Controls.Add(this.txtIdentificadorUnico);
            this.grpIdentificadorUnico.Location = new System.Drawing.Point(8, 545);
            this.grpIdentificadorUnico.Name = "grpIdentificadorUnico";
            this.grpIdentificadorUnico.Size = new System.Drawing.Size(788, 48);
            this.grpIdentificadorUnico.TabIndex = 3;
            this.grpIdentificadorUnico.TabStop = false;
            this.grpIdentificadorUnico.Text = "1.6. Identificador único";
            this.grpIdentificadorUnico.Visible = false;
            // 
            // txtIdentificadorUnico
            // 
            this.txtIdentificadorUnico.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtIdentificadorUnico.Location = new System.Drawing.Point(8, 16);
            this.txtIdentificadorUnico.Name = "txtIdentificadorUnico";
            this.txtIdentificadorUnico.Size = new System.Drawing.Size(772, 20);
            this.txtIdentificadorUnico.TabIndex = 4;
            // 
            // PanelCAIdentificacao
            // 
            this.Controls.Add(this.grpCodigoCA);
            this.Controls.Add(this.grpIdentificadorUnico);
            this.Controls.Add(this.grpTipoEntidadeProdutora);
            this.Controls.Add(this.grpTermoIndexacao);
            this.Controls.Add(this.grpTipoDocumento);
            this.Name = "PanelCAIdentificacao";
            this.grpTermoIndexacao.ResumeLayout(false);
            this.grpCodigoCA.ResumeLayout(false);
            this.grpCodigoCA.PerformLayout();
            this.grpTipoDocumento.ResumeLayout(false);
            this.grpTipoEntidadeProdutora.ResumeLayout(false);
            this.grpIdentificadorUnico.ResumeLayout(false);
            this.grpIdentificadorUnico.PerformLayout();
            this.ResumeLayout(false);

		}

	#endregion

		private void GetExtraResources()
		{
			btnAdd.Image = SharedResourcesOld.CurrentSharedResources.Adicionar;
			btnRemove.Image = SharedResourcesOld.CurrentSharedResources.Apagar;
			btnEdit.Image = SharedResourcesOld.CurrentSharedResources.Editar;

			base.ParentChanged += PanelCAIdentificacao_ParentChanged;
		}

		// runs only once. sets tooltip as soon as it's parent appears
		private void PanelCAIdentificacao_ParentChanged(object sender, System.EventArgs e)
		{
			MultiPanel.CurrentToolTip.SetToolTip(btnAdd, SharedResourcesOld.CurrentSharedResources.AdicionarString);
			MultiPanel.CurrentToolTip.SetToolTip(btnRemove, SharedResourcesOld.CurrentSharedResources.ApagarString);
			MultiPanel.CurrentToolTip.SetToolTip(btnEdit, SharedResourcesOld.CurrentSharedResources.EditarString);

			base.ParentChanged -= PanelCAIdentificacao_ParentChanged;
		}

        private void ControlTermosIndexacao1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == Convert.ToInt32(Keys.Delete))
                DeleteSelectedTermo();
        }

        private void ControlTermosIndexacao1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            UpdateButtonState(e.Node);
        }

		private bool mVisibleEPExtensions = false;
		[System.ComponentModel.DefaultValue(false), System.ComponentModel.Browsable(false)]
		public bool VisibleEPExtensions
		{
			get
			{
                return mVisibleEPExtensions;
			}
			set
			{
                mVisibleEPExtensions = value;
				if (value)
				{
					grpTipoEntidadeProdutora.Visible = true;
					grpCodigoCA.Visible = true;
                    grpIdentificadorUnico.Text = "1.6. Identificador único";
					grpIdentificadorUnico.Visible = true;
					grpTermoIndexacao.Top = grpTipoEntidadeProdutora.Top + grpTipoEntidadeProdutora.Height;
					grpTermoIndexacao.Height = grpIdentificadorUnico.Top - grpTermoIndexacao.Top;
				}
				else
				{
					//TODO: FIXME this should never happen
					throw new ArgumentOutOfRangeException();
				}
			}
		}

        private bool mVisibleTipExtensions = false;
        [System.ComponentModel.DefaultValue(false), System.ComponentModel.Browsable(false)]
        public bool VisibleTipExtensions
        {
            get {
                return mVisibleTipExtensions; }
            set {
                mVisibleTipExtensions = value;
                if (value)
                {
                    grpTipoDocumento.Visible = true;
                    grpTipoDocumento.BringToFront();
                    grpTermoIndexacao.Top = grpTipoDocumento.Top + grpTipoDocumento.Height;
                    grpTermoIndexacao.Height = grpIdentificadorUnico.Top - grpTermoIndexacao.Top;
                }
                else
                {
                    //TODO: FIXME this should never happen
                    throw new ArgumentOutOfRangeException();
                }
            }
        }

        private bool mVisibleOnomasticoExtensions = false;
        [System.ComponentModel.DefaultValue(false), System.ComponentModel.Browsable(false)]
        public bool VisibleOnomasticoExtensions
        {
            get
            {
                return mVisibleOnomasticoExtensions;
            }
            set
            {
                mVisibleOnomasticoExtensions = value;
                if (value)
                {
                    grpIdentificadorUnico.Text = "Identificador único";
                    grpIdentificadorUnico.Visible = true;
                }
                else
                {
                    //TODO: FIXME this should never happen
                    throw new ArgumentOutOfRangeException();
                }
            }
        }

		public override void LoadData(DataRow CurrentDataRow, IDbConnection conn)
		{
			IsLoaded = false;
			CurrentControloAut = (GISADataset.ControloAutRow)CurrentDataRow;

			// carregar os nós para os vários tipos de noticias de autoridade
			if (CurrentControloAut.IDTipoNoticiaAut == Convert.ToInt64(TipoNoticiaAut.EntidadeProdutora))
			{
				cbTipoEntidadeProdutora.DataSource = GisaDataSetHelper.GetInstance().TipoEntidadeProdutora.Select();
				cbTipoEntidadeProdutora.DisplayMember = "Designacao";

				ControloAutRule.Current.FillControloAutEntidadeProdutora(GisaDataSetHelper.GetInstance(), CurrentControloAut.ID, conn);

			}
			else if (CurrentControloAut.IDTipoNoticiaAut == (long)TipoNoticiaAut.Ideografico 
                || CurrentControloAut.IDTipoNoticiaAut == (long)TipoNoticiaAut.Onomastico 
                || CurrentControloAut.IDTipoNoticiaAut == (long)TipoNoticiaAut.ToponimicoGeografico 
                || CurrentControloAut.IDTipoNoticiaAut == (long)TipoNoticiaAut.TipologiaInformacional)
			{
                grpIdentificadorUnico.Visible = CurrentControloAut.IDTipoNoticiaAut == (long)TipoNoticiaAut.Onomastico;

                if (CurrentControloAut.IDTipoNoticiaAut == Convert.ToInt64(TipoNoticiaAut.TipologiaInformacional))
                {
                    cbTipoDocumento.Items.Clear();
                    var tipos = GisaDataSetHelper.GetInstance().TipoTipologias.Cast<GISADataset.TipoTipologiasRow>().ToList();
                    var newEmptyRow = GisaDataSetHelper.GetInstance().TipoTipologias.NewTipoTipologiasRow();
                    newEmptyRow.Designacao = string.Empty;
                    tipos.Insert(0, newEmptyRow);
                    cbTipoDocumento.Items.AddRange(tipos.ToArray());
                    cbTipoDocumento.DisplayMember = "Designacao";
                }
			}

            ControlTermosIndexacao1.LoadData(CurrentControloAut, conn);

			IsLoaded = true;
		}

		public override void ModelToView()
		{
			IsPopulated = false;

			// adicionar os nós para os vários tipos de noticias de autoridade
			if (CurrentControloAut.IDTipoNoticiaAut == Convert.ToInt64(TipoNoticiaAut.EntidadeProdutora))
			{
				cbTipoEntidadeProdutora.DataSource = GisaDataSetHelper.GetInstance().TipoEntidadeProdutora.Select();
				cbTipoEntidadeProdutora.DisplayMember = "Designacao";

				GISADataset.ControloAutEntidadeProdutoraRow[] caep = (GISADataset.ControloAutEntidadeProdutoraRow[])(GisaDataSetHelper.GetInstance().ControloAutEntidadeProdutora.Select("IDControloAut=" + CurrentControloAut.ID.ToString()));
				if (caep.Length == 1)
				{
					cbTipoEntidadeProdutora.SelectedItem = caep[0].TipoEntidadeProdutoraRow;
					cbTipoEntidadeProdutora.Tag = caep[0];
				}
				else
				{
					cbTipoEntidadeProdutora.SelectedItem = GisaDataSetHelper.GetInstance().TipoEntidadeProdutora.Select("ID=1"); // escolher por omissão "Colectividade"
					cbTipoEntidadeProdutora.Tag = GisaDataSetHelper.GetInstance().ControloAutEntidadeProdutora.AddControloAutEntidadeProdutoraRow(CurrentControloAut, (GISADataset.TipoEntidadeProdutoraRow)cbTipoEntidadeProdutora.SelectedItem, new byte[]{}, 0);
				}

				if (CurrentControloAut.IsChaveColectividadeNull())
					txtIdentificadorUnico.Text = string.Empty;
				else
					txtIdentificadorUnico.Text = CurrentControloAut.ChaveColectividade;

				foreach (GISADataset.NivelControloAutRow CorNivelControloAut in CurrentControloAut.GetNivelControloAutRows())
				{
					if (CorNivelControloAut.NivelRow == null)
						txtCodRef.Text = string.Empty;
					else
						txtCodRef.Text = CorNivelControloAut.NivelRow.Codigo;
				}
			}
			else if (CurrentControloAut.IDTipoNoticiaAut == Convert.ToInt64(TipoNoticiaAut.Ideografico) || CurrentControloAut.IDTipoNoticiaAut == Convert.ToInt64(TipoNoticiaAut.Onomastico) || CurrentControloAut.IDTipoNoticiaAut == Convert.ToInt64(TipoNoticiaAut.ToponimicoGeografico))
			{
                // populate NIF
                if (CurrentControloAut.IDTipoNoticiaAut == Convert.ToInt64(TipoNoticiaAut.Onomastico))
                {
                    if (CurrentControloAut.IsChaveColectividadeNull())
                        txtIdentificadorUnico.Text = string.Empty;
                    else
                        txtIdentificadorUnico.Text = CurrentControloAut.ChaveColectividade;
                }
			}
			else if (CurrentControloAut.IDTipoNoticiaAut == Convert.ToInt64(TipoNoticiaAut.TipologiaInformacional))
                cbTipoDocumento.SelectedItem = CurrentControloAut.TipoTipologiasRow;

			IsPopulated = true;
		}

		public override void ViewToModel()
		{
			if (CurrentControloAut == null || CurrentControloAut.RowState == DataRowState.Detached || ! IsLoaded)
				return;

			CurrentControloAut.ChaveColectividade = txtIdentificadorUnico.Text;

			if (CurrentControloAut.IDTipoNoticiaAut == Convert.ToInt64(TipoNoticiaAut.EntidadeProdutora))
			{
				GISADataset.ControloAutEntidadeProdutoraRow caep = null;
				caep = (GISADataset.ControloAutEntidadeProdutoraRow)cbTipoEntidadeProdutora.Tag;
				GISADataset.TipoEntidadeProdutoraRow sel = (GISADataset.TipoEntidadeProdutoraRow)cbTipoEntidadeProdutora.SelectedItem;
				if (sel.ID < 0)
				{
					caep.Delete();
					cbTipoEntidadeProdutora.Tag = null;
				}
				else
				{
					if (caep.IDTipoEntidadeProdutora != sel.ID)
						caep.IDTipoEntidadeProdutora = sel.ID;
				}
			}
            else if (CurrentControloAut.IDTipoNoticiaAut == Convert.ToInt64(TipoNoticiaAut.TipologiaInformacional)) {
                GISADataset.TipoTipologiasRow tipoTiplogia = (GISADataset.TipoTipologiasRow)cbTipoDocumento.SelectedItem;
                // Excluir a TipoTipologia vazia presente na combo (com ID < 0):
                if (tipoTiplogia != null && tipoTiplogia.ID > 0)
                    CurrentControloAut.TipoTipologiasRow = tipoTiplogia;
                else
                    CurrentControloAut.TipoTipologiasRow = null;
            }
            else if (CurrentControloAut.IDTipoNoticiaAut == Convert.ToInt64(TipoNoticiaAut.Onomastico))
                CurrentControloAut.ChaveColectividade = txtIdentificadorUnico.Text;
		}

		public override void Deactivate()
		{
            GUIHelper.GUIHelper.clearField(txtIdentificadorUnico);
            GUIHelper.GUIHelper.clearField(txtCodRef);
            cbTipoDocumento.Items.Clear();
            ControlTermosIndexacao1.Deactivate();
			OnHidePanel();
		}

        private void UpdateButtonState(TreeNode node)
		{
			TreeNode branchNode = ControlTermosIndexacao1.GetBranchNode(node);

			if (branchNode.Tag == null)
			{
				btnAdd.Enabled = false;
				btnRemove.Enabled = false;
				btnEdit.Enabled = false;
			}
			else if (branchNode.Tag is GISADataset.TipoControloAutFormaRow)
			{
				GISADataset.TipoControloAutFormaRow branchNodeRow = null;
				branchNodeRow = (GISADataset.TipoControloAutFormaRow)branchNode.Tag;

				if (branchNodeRow.ID == Convert.ToInt64(TipoControloAutForma.FormaAutorizada) && branchNode.Nodes.Count > 0)
				{
					// pode existir apenas um e so um termo como forma autorizada
					btnAdd.Enabled = false;
					btnRemove.Enabled = false;
					btnEdit.Enabled = false;
				}
				else
				{
					btnAdd.Enabled = true;
					// so podemos deixar remover se o nó selecionado for uma folha
					btnRemove.Enabled = node != branchNode;
                    btnEdit.Enabled = node != branchNode;

				}
			}
			else if (branchNode.Tag is GISADataset.TipoControloAutRelRow)
			{
				GISADataset.TipoControloAutRelRow tcarRow = (GISADataset.TipoControloAutRelRow)branchNode.Tag;
				btnAdd.Enabled = true;
                btnRemove.Enabled = node != branchNode;
                btnEdit.Enabled = node != branchNode;
			}
		}

		private int DeleteSelectedTermo()
		{
			// quando é eliminada uma relacao de ControloAutDicionarioRow deviamos/podiamos 
			// verificar se o termo ainda é usado em algum sitio e elimina-lo caso não seja
			// referido em lado nenhum. No entanto, para maior eficiencia, executa-se esse 
			// processo apenas quando se carrega o formulário de manipulação de termos.

			TreeNode node = ControlTermosIndexacao1.SelectedNode;
			// make sure the selected node is not a branch
			if (node.Parent != null)
			{
				if (node.Tag is GISADataset.ControloAutDicionarioRow || node.Tag is GISADataset.ControloAutRelRow)
				{
					PersistencyHelper.PreConcArguments args = null;

					if (node.Tag is GISADataset.ControloAutDicionarioRow)
					{
						PersistencyHelper.DeleteTermoXPreConcArguments argsCad = new PersistencyHelper.DeleteTermoXPreConcArguments();
						argsCad.drowID = ((GISADataset.ControloAutDicionarioRow)node.Tag).DicionarioRow.ID;
						argsCad.caRowID = ((GISADataset.ControloAutDicionarioRow)node.Tag).ControloAutRow.ID;
						args = argsCad;
					}

					var diagResult = ControlTermosIndexacao1.DeleteNode(node);
                    if (diagResult == (int)DialogResult.Cancel)
                        return diagResult;

					UpdateButtonState(node);

					if (node.Tag is GISADataset.ControloAutDicionarioRow)
					{
                        PersistencyHelper.SaveResult successfulSave = PersistencyHelper.save(deleteTermoX, args);

                        if (successfulSave == PersistencyHelper.SaveResult.successful)
                            UpdateCA(CurrentControloAut);
					}
					else if (node.Tag is GISADataset.ControloAutRelRow)
					{
						PersistencyHelper.SaveResult successfulSave = PersistencyHelper.save();

                        if (successfulSave == PersistencyHelper.SaveResult.successful)
                        {
                            UpdateCA(CurrentControloAut);
                            GISADataset.ControloAutRelRow carRow = (GISADataset.ControloAutRelRow)node.Tag;
                            if (CurrentControloAut.ID == carRow.IDControloAut)
                                UpdateCA(carRow.ControloAutRowByControloAutControloAutRel);
                            else
                                UpdateCA(carRow.ControloAutRowByControloAutControloAutRelAlias);
                        }
					}
                    PersistencyHelper.cleanDeletedData(new List<TableDepthOrdered.TableCloudType>(new TableDepthOrdered.TableCloudType[] { PersistencyHelper.determinaNuvem("Nivel"), PersistencyHelper.determinaNuvem("ControloAut") }));

					// indicar que deverá ser criado um registo referente à alteração do item selecionado
                    ((frmMain)TopLevelControl).CurrentContext.RaiseRegisterModificationEvent(CurrentControloAut);

                    ControlTermosIndexacao1.RepopulateThesaurus(true);
				}
			}
			return 0;
		}

		private void AddNewTermo()
		{
			if (CurrentControloAut.RowState == DataRowState.Detached)
			{
				MessageBox.Show("Não é possível associar um novo termo porque a notícia de " + System.Environment.NewLine + "autoridade selecionada foi apagada por outro utilizador.", "Associação de Termos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

            TreeNode branchNode = ControlTermosIndexacao1.GetBranchNode(ControlTermosIndexacao1.SelectedNode);

			if (branchNode.Tag is GISADataset.TipoControloAutFormaRow)
			{
				FormTermo form = null;
				try
				{
					((frmMain)(this.TopLevelControl)).EnterWaitMode();
					form = new FormTermo(CurrentControloAut.ID);
				}
				finally
				{
					((frmMain)(this.TopLevelControl)).LeaveWaitMode();
				}
				switch (form.ShowDialog(this))
				{
					case DialogResult.OK:
						GISADataset.DicionarioRow dicionarioRow = null;
						if (GisaDataSetHelper.GetInstance().Dicionario.Select(string.Format("Termo = '{0}'", form.ListTermos.ValidAuthorizedForm.Replace("'", "''"))).Length > 0 && form.ListTermos.ValidAuthorizedForm != null)
						{
							dicionarioRow = (GISADataset.DicionarioRow)(GisaDataSetHelper.GetInstance().Dicionario.Select(string.Format("Termo = '{0}'", form.ListTermos.ValidAuthorizedForm.Replace("'", "''")))[0]);
						}
						else if (form.ListTermos.ValidAuthorizedForm != null && form.ListTermos.ValidAuthorizedForm.Length > 0)
						{
							dicionarioRow = GisaDataSetHelper.GetInstance().Dicionario.NewDicionarioRow();
							dicionarioRow.Termo = form.ListTermos.ValidAuthorizedForm;
							dicionarioRow.CatCode = "CA";
							dicionarioRow.Versao = new byte[]{};
						}

						if (dicionarioRow == null)
						{
							MessageBox.Show("O termo escolhido é inválido." + System.Environment.NewLine + "Se tentou criar um novo termo é possível que este já exista.", "Seleção de termo", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
						}
						else
						{
							// se o estado da row for detached trata-se de um novo termo 
							// criado. nesse caso é necessário adiciona-lo ao dataset.
							if (dicionarioRow.RowState == DataRowState.Detached)
								GisaDataSetHelper.GetInstance().Dicionario.AddDicionarioRow(dicionarioRow);
							else
							{
								// se o termo já existir não se toma nenhuma acção
                                if (ControlTermosIndexacao1.NodeAlreadyExists(dicionarioRow))
								{
									MessageBox.Show("O termo escolhido já existe na notícia de autoridade e não será por isso adicionado.", "Seleção de termo", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
									return;
								}
							}

							// indicar que deverá ser criado um registo referente à alteração do item selecionado
                            ((frmMain)TopLevelControl).CurrentContext.RaiseRegisterModificationEvent(CurrentControloAut);

							// adicionar a nova relação ao modelo de dados e à interface
							GISADataset.ControloAutDicionarioRow cadr = GisaDataSetHelper.GetInstance().ControloAutDicionario.AddControloAutDicionarioRow(CurrentControloAut, dicionarioRow, (GISADataset.TipoControloAutFormaRow)branchNode.Tag, new byte[] { }, 0);
							ControlTermosIndexacao1.RegisterNode(branchNode, cadr);
						}
						break;
					case DialogResult.Cancel:
						// do nothing
					break;
				}
			}
			else if (branchNode.Tag is GISADataset.TipoControloAutRelRow)
				AddNewTermoThesaurus();
		}

		private static void deleteTermoX(PersistencyHelper.PreConcArguments args)
		{
			PersistencyHelper.DeleteTermoXPreConcArguments dtxPca = null;
			dtxPca = (PersistencyHelper.DeleteTermoXPreConcArguments)args;

			long caRowID = dtxPca.caRowID;
			GISADataset.DicionarioRow drow = (GISADataset.DicionarioRow)(GisaDataSetHelper.GetInstance().Dicionario.Select("ID=" + dtxPca.drowID.ToString())[0]);

			if (! (DBAbstractDataLayer.DataAccessRules.DiplomaModeloRule.Current.isTermoUsedByOthers(caRowID, drow.CatCode, drow.Termo, true, dtxPca.tran)))
			{
				System.Data.DataSet tempgisaBackup1 = dtxPca.gisaBackup;
				PersistencyHelper.BackupRow(ref tempgisaBackup1, drow);
					dtxPca.gisaBackup = tempgisaBackup1;
				drow.Delete();
			}
		}

	 
        

		private void SaveNewTermoThesaurus()
		{
			PersistencyHelper.newControloAutRelPreConcArguments args = new PersistencyHelper.newControloAutRelPreConcArguments();
			GISADataset.ControloAutRelRow[] newCarRows = (GISADataset.ControloAutRelRow[])(GisaDataSetHelper.GetInstance().ControloAutRel.Select("", "", DataViewRowState.Added));

			GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
			try
			{
				if (newCarRows.Length > 0)
				{
					args.currentCAID = CurrentControloAut.ID;
					args.car = newCarRows[0];
                    GISADataset.ControloAutRow relatedCA = null;
					if (CurrentControloAut.ID == newCarRows[0].IDControloAut)
                        relatedCA = newCarRows[0].ControloAutRowByControloAutControloAutRelAlias;
					else
                        relatedCA = newCarRows[0].ControloAutRowByControloAutControloAutRel;

                    args.relatedCAID = relatedCA.ID;
					PersistencyHelper.save(validateNewCAR, args);
                    PersistencyHelper.cleanDeletedData(new List<TableDepthOrdered.TableCloudType>(new TableDepthOrdered.TableCloudType[] { PersistencyHelper.determinaNuvem("Nivel"), PersistencyHelper.determinaNuvem("ControloAut") }));
					if (args.addSuccessful)
					{
                        UpdateCA(CurrentControloAut);
                        UpdateCA(relatedCA);
					}
					else
					{
						MessageBox.Show(args.message, "Adição de termo genérico/específico", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					}
				}

				if (CurrentControloAut.RowState == DataRowState.Detached)
				{
					MultiPanel.Recontextualize();
					return;
				}

                ControlTermosIndexacao1.RepopulateThesaurus(newCarRows.SingleOrDefault());
			}
			finally
			{
				ho.Dispose();
			}
		}

		private void validateNewCAR(PersistencyHelper.PreConcArguments args)
		{
			PersistencyHelper.newControloAutRelPreConcArguments newCarPca = null;
			newCarPca = (PersistencyHelper.newControloAutRelPreConcArguments)args;

			ControloAutRule.Current.SetUpIsReachable(GisaDataSetHelper.GetInstance(), CurrentControloAut.ID, newCarPca.tran);
			ControloAutRule.Current.ComputeIsReachable(newCarPca.tran);

			if (! (ControloAutRule.Current.IsReachable(newCarPca.relatedCAID, newCarPca.tran)))
			{
				//verificar na BD se a relação ja existe (bloquear linha car no caso de existir)
				if (ControloAutRule.Current.isCarInDataBase(newCarPca.car, newCarPca.tran))
				{
					newCarPca.car.RejectChanges();
					newCarPca.message = "Não é possível adicionar o mesmo termo mais do que uma vez.";
					newCarPca.addSuccessful = false;
				}
				else
				{
					//se nao:
					//'verificar se os controlos de autoridade ja existem
					long[] CaIDs = {newCarPca.car.IDControloAut, newCarPca.car.IDControloAutAlias};
					if (ControloAutRule.Current.isControloAutInDataBase(CaIDs, newCarPca.tran))
					{
						//'se existirem os 2
						newCarPca.addSuccessful = true; //permitir q a seja adicionado o elemento na árvore
					}
					else
					{
						//'se um dos 2 controlos de autoridade nao existirem
						newCarPca.addSuccessful = false;
						newCarPca.car.RejectChanges();
						newCarPca.message = "Um dos controlos de autoridade foi apagado por outro utilizador " + System.Environment.NewLine + "o que por esse motivo não é possível concluir a operação.";
					}
				}
			}
			else
			{
				newCarPca.addSuccessful = false;
				newCarPca.car.RejectChanges();
				newCarPca.message = "O termo escolhido está já relacionado, directa ou indirectamente, com " + System.Environment.NewLine + "esta notícia de autoridade.";
			}
			ControloAutRule.Current.TearDownIsReachable(newCarPca.tran);
		}

		private void AddNewTermoThesaurus()
		{
			byte[] Versao = null;
			FormPickControloAut frmPickControloAut = new FormPickControloAut(CurrentControloAut);
            frmPickControloAut.Text = "Controlo de autoridade - " + ControlTermosIndexacao1.GetBranchNode(ControlTermosIndexacao1.SelectedNode).Text;

            switch ((TipoNoticiaAut)CurrentControloAut.IDTipoNoticiaAut)
			{
				case TipoNoticiaAut.Ideografico:
				case TipoNoticiaAut.Onomastico:
				case TipoNoticiaAut.ToponimicoGeografico:
					frmPickControloAut.caList.AllowedNoticiaAut(TipoNoticiaAut.Ideografico, TipoNoticiaAut.Onomastico, TipoNoticiaAut.ToponimicoGeografico);
					break;
				case TipoNoticiaAut.TipologiaInformacional:
					frmPickControloAut.caList.AllowedNoticiaAut(TipoNoticiaAut.TipologiaInformacional);
					break;
				default:
					throw new InvalidOperationException(string.Format("ControloAut of type \"{0}\" don't have relations with other ControloAut.", CurrentControloAut.TipoNoticiaAutRow.Designacao));
			}
			((frmMain)TopLevelControl).EnterWaitMode();
			try
			{
				frmPickControloAut.caList.txtFiltroDesignacao.Clear();
				frmPickControloAut.caList.ReloadList();
			}
			finally
			{
				((frmMain)TopLevelControl).LeaveWaitMode();
			}

			switch (frmPickControloAut.ShowDialog())
			{
				case DialogResult.OK:
					((frmMain)TopLevelControl).EnterWaitMode();
					try
					{
						foreach (ListViewItem li in frmPickControloAut.caList.SelectedItems)
						{
							Debug.Assert(li.Tag is GISADataset.ControloAutDicionarioRow);

							GISADataset.ControloAutRow ca = ((GISADataset.ControloAutDicionarioRow)li.Tag).ControloAutRow;
                            GISADataset.TipoControloAutRelRow tcarRow = (GISADataset.TipoControloAutRelRow)(ControlTermosIndexacao1.GetBranchNode(ControlTermosIndexacao1.SelectedNode).Tag);

							GISADataset.ControloAutRelRow newRel = null;
							newRel = null;

							if (tcarRow.ID == (long)TipoControloAutRel.TermoGenerico)
							{
                                if (tcarRow.Designacao.Equals(ControlTermosIndexacao1.SelectedNode.Text) || (ControlTermosIndexacao1.SelectedNode.Parent != null && tcarRow.Designacao.Equals(ControlTermosIndexacao1.SelectedNode.Parent.Text)))
								{
                                    // Relação directa (termo genérico)
									if (GisaDataSetHelper.GetInstance().ControloAutRel.Select(string.Format("IDControloAut={0} AND IDControloAutAlias={1} AND IDTipoRel={2}", CurrentControloAut.ID, ca.ID, ((GISADataset.TipoControloAutRelRow)(ControlTermosIndexacao1.GetBranchNode(ControlTermosIndexacao1.SelectedNode).Tag)).ID)).Length == 0)
                                        newRel = GisaDataSetHelper.GetInstance().ControloAutRel.AddControloAutRelRow(CurrentControloAut, ca, (GISADataset.TipoControloAutRelRow)(ControlTermosIndexacao1.GetBranchNode(ControlTermosIndexacao1.SelectedNode).Tag), "", "", null, null, null, null, null, Versao, 0);
									else
										MessageBox.Show("O termo escolhido já está relacionado, directa ou indirectamente, com " + System.Environment.NewLine + "esta notícia de autoridade.", "Adição de termo genérico/específico", MessageBoxButtons.OK, MessageBoxIcon.Warning);
								}
								else
								{
                                    // Relação inversa (termo específico)
									if (GisaDataSetHelper.GetInstance().ControloAutRel.Select(string.Format("((IDControloAut={0} AND IDControloAutAlias={1}) OR (IDControloAut={1} AND IDControloAutAlias={0})) AND IDTipoRel={2}", ca.ID, CurrentControloAut.ID, ((GISADataset.TipoControloAutRelRow)(ControlTermosIndexacao1.GetBranchNode(ControlTermosIndexacao1.SelectedNode).Tag)).ID)).Length == 0)
                                        newRel = GisaDataSetHelper.GetInstance().ControloAutRel.AddControloAutRelRow(ca, CurrentControloAut, (GISADataset.TipoControloAutRelRow)(ControlTermosIndexacao1.GetBranchNode(ControlTermosIndexacao1.SelectedNode).Tag), "", "", null, null, null, null, null, Versao, 0);
									else
										MessageBox.Show("O termo escolhido está já relacionado, directa ou indirectamente, com " + System.Environment.NewLine + "esta notícia de autoridade.", "Adição de termo genérico/específico", MessageBoxButtons.OK, MessageBoxIcon.Warning);
								}
							}
							else
							{
								// Relação não dirigida
								if (GisaDataSetHelper.GetInstance().ControloAutRel.Select(string.Format("((IDControloAut={0} AND IDControloAutAlias={1}) OR (IDControloAut={1} AND IDControloAutAlias={0})) AND IDTipoRel={2}", CurrentControloAut.ID, ca.ID, ((GISADataset.TipoControloAutRelRow)(ControlTermosIndexacao1.GetBranchNode(ControlTermosIndexacao1.SelectedNode).Tag)).ID)).Length == 0)
									newRel = GisaDataSetHelper.GetInstance().ControloAutRel.AddControloAutRelRow(CurrentControloAut, ca, (GISADataset.TipoControloAutRelRow)(ControlTermosIndexacao1.GetBranchNode(ControlTermosIndexacao1.SelectedNode).Tag), "", "", null, null, null, null, null, Versao, 0);
								else
									MessageBox.Show("O termo escolhido está já relacionado, directa ou indirectamente, com " + System.Environment.NewLine + "esta notícia de autoridade.", "Adição de termo genérico/específico", MessageBoxButtons.OK, MessageBoxIcon.Warning);
							}

                            // indicar que deverá ser criado um registo referente à alteração do item selecionado
							if (newRel != null)
                                ((frmMain)TopLevelControl).CurrentContext.RaiseRegisterModificationEvent(CurrentControloAut);
						}

						SaveNewTermoThesaurus();

					}
					finally
					{
						((frmMain)TopLevelControl).LeaveWaitMode();
					}
					break;
				case DialogResult.Cancel:

				break;
				default:
					throw new InvalidOperationException("Invalid DialogResult.");
			}
		}

		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			AddNewTermo();
		}

        private void btnEdit_Click(object sender, System.EventArgs e)
        {
            if (DeleteSelectedTermo() == (int)DialogResult.Cancel) return;
            
            AddNewTermo();
        }

		private void btnRemove_Click(object sender, System.EventArgs e)
		{
			DeleteSelectedTermo();
		}

        private void UpdateCA(GISADataset.ControloAutRow caRow)
        {
            GisaDataSetHelper.HoldOpen ho = new GisaDataSetHelper.HoldOpen(GisaDataSetHelper.GetConnection());
            try
            {
                List<string> IDNiveis = ControloAutRule.Current.GetNiveisDocAssociados(CurrentControloAut.ID, ho.Connection);
                GISA.Search.Updater.updateNivelDocumental(IDNiveis);
                switch (caRow.IDTipoNoticiaAut)
                {
                    case (long)TipoNoticiaAut.EntidadeProdutora:
                        GISA.Search.Updater.updateProdutor(caRow.ID);
                        GISA.Search.Updater.updateNivelDocumentalComProdutores(caRow.GetNivelControloAutRows()[0].ID);
                        break;
                    case (long)TipoNoticiaAut.Onomastico:
                    case (long)TipoNoticiaAut.Ideografico:
                    case (long)TipoNoticiaAut.ToponimicoGeografico:
                        GISA.Search.Updater.updateAssunto(caRow.ID);
                        break;
                    case (long)TipoNoticiaAut.TipologiaInformacional:
                        GISA.Search.Updater.updateTipologia(caRow.ID);
                        break;
                }
            }
            catch (GISA.Search.UpdateServerException e)
            { 
                Trace.WriteLine(e.ToString()); 
                throw; 
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
                throw;
            }
            finally
            {
                ho.Dispose();
            }
        }
	}
}
