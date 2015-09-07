namespace GISA
{
    partial class SlavePanelMovimentos
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        internal GISA.PanelMensagem PanelMensagem1;
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SlavePanelMovimentos));
            this.ToolBarButtonAuxList = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButtonFiltro = new System.Windows.Forms.ToolBarButton();
            this.ImageList1 = new System.Windows.Forms.ImageList(this.components);
            this.grpDesc = new System.Windows.Forms.GroupBox();
            this.lstVwNiveisAssoc = new System.Windows.Forms.ListView();
            this.chID = new System.Windows.Forms.ColumnHeader();
            this.chDesignacao = new System.Windows.Forms.ColumnHeader();
            this.chCodigo = new System.Windows.Forms.ColumnHeader();
            this.chNivelDesc = new System.Windows.Forms.ColumnHeader();
            this.chDatasProd = new System.Windows.Forms.ColumnHeader();
            this.btnRemove = new System.Windows.Forms.Button();
            this.grpFiltro = new System.Windows.Forms.GroupBox();
            this.btnAplicar = new System.Windows.Forms.Button();
            this.txtFiltroDesignacao = new System.Windows.Forms.TextBox();
            this.lblFiltroDesignacao = new System.Windows.Forms.Label();
            this.PanelMensagem1 = new GISA.PanelMensagem();
            this.pnlDesc = new System.Windows.Forms.Panel();
            this.grpNotas = new System.Windows.Forms.GroupBox();
            this.txtNotas = new System.Windows.Forms.TextBox();
            this.pnlToolbarPadding.SuspendLayout();
            this.grpDesc.SuspendLayout();
            this.grpFiltro.SuspendLayout();
            this.pnlDesc.SuspendLayout();
            this.grpNotas.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblFuncao
            // 
            this.lblFuncao.Location = new System.Drawing.Point(0, 0);
            // 
            // ToolBar
            // 
            this.ToolBar.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.ToolBarButtonAuxList,
            this.ToolBarButtonFiltro});
            this.ToolBar.ImageList = this.ImageList1;
            // 
            // pnlToolbarPadding
            // 
            this.pnlToolbarPadding.Location = new System.Drawing.Point(0, 24);
            // 
            // ToolBarButtonAuxList
            // 
            this.ToolBarButtonAuxList.ImageIndex = 1;
            this.ToolBarButtonAuxList.Name = "ToolBarButtonAuxList";
            this.ToolBarButtonAuxList.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.ToolBarButtonAuxList.ToolTipText = "Apresentar/esconder painel de apoio";
            // 
            // ToolBarButtonFiltro
            // 
            this.ToolBarButtonFiltro.ImageIndex = 3;
            this.ToolBarButtonFiltro.Name = "ToolBarButtonFiltro";
            this.ToolBarButtonFiltro.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            this.ToolBarButtonFiltro.ToolTipText = "Apresentar/esconder campos de filtro";
            // 
            // ImageList1
            // 
            this.ImageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageList1.ImageStream")));
            this.ImageList1.TransparentColor = System.Drawing.Color.Fuchsia;
            this.ImageList1.Images.SetKeyName(0, "");
            this.ImageList1.Images.SetKeyName(1, "");
            this.ImageList1.Images.SetKeyName(2, "");
            this.ImageList1.Images.SetKeyName(3, "");
            // 
            // grpDesc
            // 
            this.grpDesc.AutoSize = true;
            this.grpDesc.Controls.Add(this.lstVwNiveisAssoc);
            this.grpDesc.Controls.Add(this.btnRemove);
            this.grpDesc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpDesc.Location = new System.Drawing.Point(0, 0);
            this.grpDesc.Name = "grpDesc";
            this.grpDesc.Size = new System.Drawing.Size(600, 129);
            this.grpDesc.TabIndex = 67;
            this.grpDesc.TabStop = false;
            this.grpDesc.Text = "Unidades de informação associadas";
            // 
            // lstVwNiveisAssoc
            // 
            this.lstVwNiveisAssoc.AllowDrop = true;
            this.lstVwNiveisAssoc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstVwNiveisAssoc.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chID,
            this.chDesignacao,
            this.chCodigo,
            this.chNivelDesc,
            this.chDatasProd});
            this.lstVwNiveisAssoc.FullRowSelect = true;
            this.lstVwNiveisAssoc.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstVwNiveisAssoc.HideSelection = false;
            this.lstVwNiveisAssoc.Location = new System.Drawing.Point(8, 20);
            this.lstVwNiveisAssoc.Name = "lstVwNiveisAssoc";
            this.lstVwNiveisAssoc.Size = new System.Drawing.Size(556, 105);
            this.lstVwNiveisAssoc.TabIndex = 65;
            this.lstVwNiveisAssoc.UseCompatibleStateImageBehavior = false;
            this.lstVwNiveisAssoc.View = System.Windows.Forms.View.Details;
            this.lstVwNiveisAssoc.SelectedIndexChanged += new System.EventHandler(this.lstVwNiveisAssoc_SelectedIndexChanged);
            this.lstVwNiveisAssoc.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lstVwNiveisAssoc_KeyUp);
            // 
            // chID
            // 
            this.chID.Text = "Identificador";
            this.chID.Width = 81;
            // 
            // chDesignacao
            // 
            this.chDesignacao.Text = "Título";
            this.chDesignacao.Width = 301;
            // 
            // chCodigo
            // 
            this.chCodigo.Text = "Código referência";
            this.chCodigo.Width = 170;
            // 
            // chNivelDesc
            // 
            this.chNivelDesc.Text = "Nivel Descrição";
            this.chNivelDesc.Width = 140;
            // 
            // chDatasProd
            // 
            this.chDatasProd.Text = "Datas Produção";
            this.chDatasProd.Width = 160;
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRemove.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnRemove.Location = new System.Drawing.Point(569, 34);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(24, 24);
            this.btnRemove.TabIndex = 64;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // grpFiltro
            // 
            this.grpFiltro.Controls.Add(this.btnAplicar);
            this.grpFiltro.Controls.Add(this.txtFiltroDesignacao);
            this.grpFiltro.Controls.Add(this.lblFiltroDesignacao);
            this.grpFiltro.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpFiltro.Location = new System.Drawing.Point(0, 52);
            this.grpFiltro.Name = "grpFiltro";
            this.grpFiltro.Size = new System.Drawing.Size(600, 64);
            this.grpFiltro.TabIndex = 68;
            this.grpFiltro.TabStop = false;
            this.grpFiltro.Text = "Filtro";
            this.grpFiltro.Visible = false;
            // 
            // btnAplicar
            // 
            this.btnAplicar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAplicar.Location = new System.Drawing.Point(528, 32);
            this.btnAplicar.Name = "btnAplicar";
            this.btnAplicar.Size = new System.Drawing.Size(64, 24);
            this.btnAplicar.TabIndex = 4;
            this.btnAplicar.Text = "&Aplicar";
            this.btnAplicar.Click += new System.EventHandler(this.btnAplicar_Click);
            // 
            // txtFiltroDesignacao
            // 
            this.txtFiltroDesignacao.AcceptsReturn = true;
            this.txtFiltroDesignacao.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFiltroDesignacao.Location = new System.Drawing.Point(8, 32);
            this.txtFiltroDesignacao.Name = "txtFiltroDesignacao";
            this.txtFiltroDesignacao.Size = new System.Drawing.Size(514, 20);
            this.txtFiltroDesignacao.TabIndex = 1;
            this.txtFiltroDesignacao.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBox_KeyDown);
            // 
            // lblFiltroDesignacao
            // 
            this.lblFiltroDesignacao.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFiltroDesignacao.Location = new System.Drawing.Point(8, 16);
            this.lblFiltroDesignacao.Name = "lblFiltroDesignacao";
            this.lblFiltroDesignacao.Size = new System.Drawing.Size(308, 16);
            this.lblFiltroDesignacao.TabIndex = 0;
            this.lblFiltroDesignacao.Text = "Título";
            // 
            // PanelMensagem1
            // 
            this.PanelMensagem1.BackColor = System.Drawing.SystemColors.Control;
            this.PanelMensagem1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelMensagem1.IsLoaded = false;
            this.PanelMensagem1.IsPopulated = false;
            this.PanelMensagem1.Location = new System.Drawing.Point(0, 0);
            this.PanelMensagem1.Name = "PanelMensagem1";
            this.PanelMensagem1.Size = new System.Drawing.Size(600, 423);
            this.PanelMensagem1.TabIndex = 24;
            this.PanelMensagem1.TbBAuxListEventAssigned = false;
            this.PanelMensagem1.TheGenericDelegate = null;
            this.PanelMensagem1.Visible = false;
            // 
            // pnlDesc
            // 
            this.pnlDesc.Controls.Add(this.grpDesc);
            this.pnlDesc.Controls.Add(this.grpNotas);
            this.pnlDesc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDesc.Location = new System.Drawing.Point(0, 116);
            this.pnlDesc.Name = "pnlDesc";
            this.pnlDesc.Size = new System.Drawing.Size(600, 307);
            this.pnlDesc.TabIndex = 69;
            // 
            // grpNotas
            // 
            this.grpNotas.Controls.Add(this.txtNotas);
            this.grpNotas.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grpNotas.Location = new System.Drawing.Point(0, 129);
            this.grpNotas.Name = "grpNotas";
            this.grpNotas.Size = new System.Drawing.Size(600, 178);
            this.grpNotas.TabIndex = 68;
            this.grpNotas.TabStop = false;
            this.grpNotas.Text = "Notas";
            // 
            // txtNotas
            // 
            this.txtNotas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtNotas.Location = new System.Drawing.Point(3, 16);
            this.txtNotas.Multiline = true;
            this.txtNotas.Name = "txtNotas";
            this.txtNotas.Size = new System.Drawing.Size(594, 159);
            this.txtNotas.TabIndex = 0;
            // 
            // SlavePanelMovimentos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlDesc);
            this.Controls.Add(this.grpFiltro);
            this.Controls.Add(this.PanelMensagem1);
            this.Name = "SlavePanelMovimentos";
            this.Size = new System.Drawing.Size(600, 423);
            this.Controls.SetChildIndex(this.PanelMensagem1, 0);
            this.Controls.SetChildIndex(this.lblFuncao, 0);
            this.Controls.SetChildIndex(this.pnlToolbarPadding, 0);
            this.Controls.SetChildIndex(this.grpFiltro, 0);
            this.Controls.SetChildIndex(this.pnlDesc, 0);
            this.pnlToolbarPadding.ResumeLayout(false);
            this.grpDesc.ResumeLayout(false);
            this.grpFiltro.ResumeLayout(false);
            this.grpFiltro.PerformLayout();
            this.pnlDesc.ResumeLayout(false);
            this.pnlDesc.PerformLayout();
            this.grpNotas.ResumeLayout(false);
            this.grpNotas.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        protected internal System.Windows.Forms.ToolBarButton ToolBarButtonAuxList;
        private System.Windows.Forms.ToolBarButton ToolBarButtonFiltro;
        private System.Windows.Forms.ImageList ImageList1;
        private System.Windows.Forms.GroupBox grpDesc;
        private System.Windows.Forms.ListView lstVwNiveisAssoc;
        private System.Windows.Forms.ColumnHeader chDesignacao;
        private System.Windows.Forms.ColumnHeader chCodigo;
        private System.Windows.Forms.ColumnHeader chNivelDesc;
        private System.Windows.Forms.ColumnHeader chDatasProd;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.GroupBox grpFiltro;
        private System.Windows.Forms.Button btnAplicar;
        private System.Windows.Forms.TextBox txtFiltroDesignacao;
        private System.Windows.Forms.Label lblFiltroDesignacao;
        private System.Windows.Forms.ColumnHeader chID;
        private System.Windows.Forms.Panel pnlDesc;
        private System.Windows.Forms.GroupBox grpNotas;
        private System.Windows.Forms.TextBox txtNotas;

    }
}
