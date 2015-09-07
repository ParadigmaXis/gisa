namespace GISA
{
    partial class FormPickTítulo
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ToolBar1 = new System.Windows.Forms.ToolBar();
            this.ToolBarButtonNew = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButtonEdit = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButtonDelete = new System.Windows.Forms.ToolBarButton();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.grpFiltro = new System.Windows.Forms.GroupBox();
            this.btnAplicar = new System.Windows.Forms.Button();
            this.txtFiltroDesignacao = new System.Windows.Forms.TextBox();
            this.lblFiltroDesignacao = new System.Windows.Forms.Label();
            this.lstTitulos = new System.Windows.Forms.ListBox();
            this.grpFiltro.SuspendLayout();
            this.SuspendLayout();
            // 
            // ToolBar1
            // 
            this.ToolBar1.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
            this.ToolBar1.AutoSize = false;
            this.ToolBar1.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.ToolBarButtonNew,
            this.ToolBarButtonEdit,
            this.ToolBarButtonDelete});
            this.ToolBar1.DropDownArrows = true;
            this.ToolBar1.Location = new System.Drawing.Point(0, 0);
            this.ToolBar1.Name = "ToolBar1";
            this.ToolBar1.ShowToolTips = true;
            this.ToolBar1.Size = new System.Drawing.Size(423, 26);
            this.ToolBar1.TabIndex = 10;
            // 
            // ToolBarButtonNew
            // 
            this.ToolBarButtonNew.ImageIndex = 0;
            this.ToolBarButtonNew.Name = "ToolBarButtonNew";
            // 
            // ToolBarButtonEdit
            // 
            this.ToolBarButtonEdit.ImageIndex = 1;
            this.ToolBarButtonEdit.Name = "ToolBarButtonEdit";
            // 
            // ToolBarButtonDelete
            // 
            this.ToolBarButtonDelete.ImageIndex = 2;
            this.ToolBarButtonDelete.Name = "ToolBarButtonDelete";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(336, 304);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancelar";
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(255, 304);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 8;
            this.btnOk.Text = "Aceitar";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // grpFiltro
            // 
            this.grpFiltro.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpFiltro.Controls.Add(this.btnAplicar);
            this.grpFiltro.Controls.Add(this.txtFiltroDesignacao);
            this.grpFiltro.Controls.Add(this.lblFiltroDesignacao);
            this.grpFiltro.Location = new System.Drawing.Point(12, 32);
            this.grpFiltro.Name = "grpFiltro";
            this.grpFiltro.Size = new System.Drawing.Size(399, 64);
            this.grpFiltro.TabIndex = 11;
            this.grpFiltro.TabStop = false;
            this.grpFiltro.Text = "Filtro";
            // 
            // btnAplicar
            // 
            this.btnAplicar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAplicar.Location = new System.Drawing.Point(327, 32);
            this.btnAplicar.Name = "btnAplicar";
            this.btnAplicar.Size = new System.Drawing.Size(64, 24);
            this.btnAplicar.TabIndex = 4;
            this.btnAplicar.Text = "Aplicar";
            this.btnAplicar.Click += new System.EventHandler(this.btnAplicar_Click);
            // 
            // txtFiltroDesignacao
            // 
            this.txtFiltroDesignacao.AcceptsReturn = true;
            this.txtFiltroDesignacao.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFiltroDesignacao.Location = new System.Drawing.Point(8, 32);
            this.txtFiltroDesignacao.Name = "txtFiltroDesignacao";
            this.txtFiltroDesignacao.Size = new System.Drawing.Size(313, 20);
            this.txtFiltroDesignacao.TabIndex = 1;
            // 
            // lblFiltroDesignacao
            // 
            this.lblFiltroDesignacao.Location = new System.Drawing.Point(8, 16);
            this.lblFiltroDesignacao.Name = "lblFiltroDesignacao";
            this.lblFiltroDesignacao.Size = new System.Drawing.Size(77, 16);
            this.lblFiltroDesignacao.TabIndex = 0;
            this.lblFiltroDesignacao.Text = "Designação";
            // 
            // lstTitulos
            // 
            this.lstTitulos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstTitulos.FormattingEnabled = true;
            this.lstTitulos.Location = new System.Drawing.Point(12, 102);
            this.lstTitulos.Name = "lstTitulos";
            this.lstTitulos.Size = new System.Drawing.Size(399, 186);
            this.lstTitulos.TabIndex = 12;
            this.lstTitulos.SelectedIndexChanged += new System.EventHandler(this.lstTitulos_SelectedIndexChanged);
            // 
            // FormPickTítulo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(423, 339);
            this.ControlBox = false;
            this.Controls.Add(this.lstTitulos);
            this.Controls.Add(this.grpFiltro);
            this.Controls.Add(this.ToolBar1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Name = "FormPickTítulo";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Escolher título";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormPickTítulo_FormClosing);
            this.grpFiltro.ResumeLayout(false);
            this.grpFiltro.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.ToolBar ToolBar1;
        internal System.Windows.Forms.ToolBarButton ToolBarButtonNew;
        internal System.Windows.Forms.ToolBarButton ToolBarButtonEdit;
        internal System.Windows.Forms.ToolBarButton ToolBarButtonDelete;
        internal System.Windows.Forms.Button btnCancel;
        internal System.Windows.Forms.Button btnOk;
        public System.Windows.Forms.GroupBox grpFiltro;
        public System.Windows.Forms.Button btnAplicar;
        public System.Windows.Forms.TextBox txtFiltroDesignacao;
        public System.Windows.Forms.Label lblFiltroDesignacao;
        private System.Windows.Forms.ListBox lstTitulos;
    }
}