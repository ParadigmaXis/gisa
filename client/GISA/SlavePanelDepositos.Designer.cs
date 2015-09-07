namespace GISA
{
    partial class SlavePanelDepositos
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
            this.PanelMensagem1 = new GISA.PanelMensagem();
            this.grpUnidadesFisicasAvaliadas = new System.Windows.Forms.GroupBox();
            this.pxListView_UnidadesFisicasDocs = new GISA.Controls.PxListView();
            this.chEliminar = new System.Windows.Forms.ColumnHeader();
            this.chCodigo = new System.Windows.Forms.ColumnHeader();
            this.chDesignacao = new System.Windows.Forms.ColumnHeader();
            this.chDimensoes = new System.Windows.Forms.ColumnHeader();
            this.grp_NotasEliminacao = new System.Windows.Forms.GroupBox();
            this.txt_NotasEliminacao = new System.Windows.Forms.TextBox();
            this.pnlToolbarPadding.SuspendLayout();
            this.grpUnidadesFisicasAvaliadas.SuspendLayout();
            this.grp_NotasEliminacao.SuspendLayout();
            this.SuspendLayout();
            // 
            // ToolBar
            // 
            this.ToolBar.Size = new System.Drawing.Size(580, 2);
            // 
            // pnlToolbarPadding
            // 
            this.pnlToolbarPadding.Size = new System.Drawing.Size(600, 5);
            // 
            // PanelMensagem1
            // 
            this.PanelMensagem1.BackColor = System.Drawing.SystemColors.Control;
            this.PanelMensagem1.IsLoaded = false;
            this.PanelMensagem1.IsPopulated = false;
            this.PanelMensagem1.Location = new System.Drawing.Point(0, 0);
            this.PanelMensagem1.Name = "PanelMensagem1";
            this.PanelMensagem1.Size = new System.Drawing.Size(680, 328);
            this.PanelMensagem1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PanelMensagem1.TabIndex = 0;
            this.PanelMensagem1.TbBAuxListEventAssigned = false;
            // 
            // grpUnidadesFisicasAvaliadas
            // 
            this.grpUnidadesFisicasAvaliadas.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpUnidadesFisicasAvaliadas.Controls.Add(this.pxListView_UnidadesFisicasDocs);
            this.grpUnidadesFisicasAvaliadas.Location = new System.Drawing.Point(6, 11);
            this.grpUnidadesFisicasAvaliadas.Name = "grpUnidadesFisicasAvaliadas";
            this.grpUnidadesFisicasAvaliadas.Size = new System.Drawing.Size(591, 249);
            this.grpUnidadesFisicasAvaliadas.TabIndex = 2;
            this.grpUnidadesFisicasAvaliadas.TabStop = false;
            this.grpUnidadesFisicasAvaliadas.Text = "Unidades fisicas";
            // 
            // pxListView_UnidadesFisicasDocs
            // 
            this.pxListView_UnidadesFisicasDocs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pxListView_UnidadesFisicasDocs.CheckBoxes = true;
            this.pxListView_UnidadesFisicasDocs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chEliminar,
            this.chCodigo,
            this.chDesignacao,
            this.chDimensoes});
            this.pxListView_UnidadesFisicasDocs.CustomizedSorting = true;
            this.pxListView_UnidadesFisicasDocs.FullRowSelect = true;
            this.pxListView_UnidadesFisicasDocs.HideSelection = false;
            this.pxListView_UnidadesFisicasDocs.Location = new System.Drawing.Point(6, 18);
            this.pxListView_UnidadesFisicasDocs.Name = "pxListView_UnidadesFisicasDocs";
            this.pxListView_UnidadesFisicasDocs.ReturnSubItemIndex = false;
            this.pxListView_UnidadesFisicasDocs.Size = new System.Drawing.Size(574, 225);
            this.pxListView_UnidadesFisicasDocs.TabIndex = 0;
            this.pxListView_UnidadesFisicasDocs.UseCompatibleStateImageBehavior = false;
            this.pxListView_UnidadesFisicasDocs.View = System.Windows.Forms.View.Details;
            // 
            // chEliminar
            // 
            this.chEliminar.Text = "Eliminar";
            this.chEliminar.Width = 50;
            // 
            // chCodigo
            // 
            this.chCodigo.Text = "Código";
            this.chCodigo.Width = 170;
            // 
            // chDesignacao
            // 
            this.chDesignacao.Text = "Título";
            this.chDesignacao.Width = 350;
            // 
            // chDimensoes
            // 
            this.chDimensoes.Text = "Dimensões";
            this.chDimensoes.Width = 130;
            // 
            // grp_NotasEliminacao
            // 
            this.grp_NotasEliminacao.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grp_NotasEliminacao.Controls.Add(this.txt_NotasEliminacao);
            this.grp_NotasEliminacao.Location = new System.Drawing.Point(6, 266);
            this.grp_NotasEliminacao.Name = "grp_NotasEliminacao";
            this.grp_NotasEliminacao.Size = new System.Drawing.Size(591, 208);
            this.grp_NotasEliminacao.TabIndex = 3;
            this.grp_NotasEliminacao.TabStop = false;
            this.grp_NotasEliminacao.Text = "Notas de eliminação";
            // 
            // txt_NotasEliminacao
            // 
            this.txt_NotasEliminacao.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_NotasEliminacao.Location = new System.Drawing.Point(6, 19);
            this.txt_NotasEliminacao.Multiline = true;
            this.txt_NotasEliminacao.Name = "txt_NotasEliminacao";
            this.txt_NotasEliminacao.Size = new System.Drawing.Size(574, 183);
            this.txt_NotasEliminacao.TabIndex = 0;
            // 
            // SlavePanelDepositos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grp_NotasEliminacao);
            this.Controls.Add(this.grpUnidadesFisicasAvaliadas);
            this.Controls.Add(this.PanelMensagem1);
            this.Name = "SlavePanelDepositos";
            this.Size = new System.Drawing.Size(600, 486);
            this.Controls.SetChildIndex(this.grpUnidadesFisicasAvaliadas, 0);
            this.Controls.SetChildIndex(this.grp_NotasEliminacao, 0);
            this.Controls.SetChildIndex(this.pnlToolbarPadding, 0);
            this.pnlToolbarPadding.ResumeLayout(false);
            this.grpUnidadesFisicasAvaliadas.ResumeLayout(false);
            this.grp_NotasEliminacao.ResumeLayout(false);
            this.grp_NotasEliminacao.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpUnidadesFisicasAvaliadas;
        private GISA.Controls.PxListView pxListView_UnidadesFisicasDocs;
        private System.Windows.Forms.ColumnHeader chCodigo;
        private System.Windows.Forms.ColumnHeader chDesignacao;
        private System.Windows.Forms.GroupBox grp_NotasEliminacao;
        private System.Windows.Forms.TextBox txt_NotasEliminacao;
        private System.Windows.Forms.ColumnHeader chEliminar;
        private System.Windows.Forms.ColumnHeader chDimensoes;

    }
}
