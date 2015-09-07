namespace GISA
{
    partial class PanelDepUFEliminadas
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
        private void InitializeComponent()
        {
            this.grp_NotasEliminacao = new System.Windows.Forms.GroupBox();
            this.txt_NotasEliminacao = new System.Windows.Forms.TextBox();
            this.grpUnidadesFisicasAvaliadas = new System.Windows.Forms.GroupBox();
            this.lstVwUnidadesFisicas = new GISA.Controls.PxListView();
            this.chEliminar = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chCodigo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chDesignacao = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chDimensoes = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.grpAutosEliminacao = new System.Windows.Forms.GroupBox();
            this.lstVwAutosEliminacao = new System.Windows.Forms.ListView();
            this.chAEDesignacao = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.grp_NotasEliminacao.SuspendLayout();
            this.grpUnidadesFisicasAvaliadas.SuspendLayout();
            this.grpAutosEliminacao.SuspendLayout();
            this.SuspendLayout();
            // 
            // grp_NotasEliminacao
            // 
            this.grp_NotasEliminacao.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grp_NotasEliminacao.Controls.Add(this.txt_NotasEliminacao);
            this.grp_NotasEliminacao.Location = new System.Drawing.Point(3, 202);
            this.grp_NotasEliminacao.Name = "grp_NotasEliminacao";
            this.grp_NotasEliminacao.Size = new System.Drawing.Size(793, 398);
            this.grp_NotasEliminacao.TabIndex = 5;
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
            this.txt_NotasEliminacao.Size = new System.Drawing.Size(776, 373);
            this.txt_NotasEliminacao.TabIndex = 0;
            // 
            // grpUnidadesFisicasAvaliadas
            // 
            this.grpUnidadesFisicasAvaliadas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpUnidadesFisicasAvaliadas.Controls.Add(this.lstVwUnidadesFisicas);
            this.grpUnidadesFisicasAvaliadas.Location = new System.Drawing.Point(367, 3);
            this.grpUnidadesFisicasAvaliadas.Name = "grpUnidadesFisicasAvaliadas";
            this.grpUnidadesFisicasAvaliadas.Size = new System.Drawing.Size(429, 193);
            this.grpUnidadesFisicasAvaliadas.TabIndex = 4;
            this.grpUnidadesFisicasAvaliadas.TabStop = false;
            this.grpUnidadesFisicasAvaliadas.Text = "Unidades fisicas";
            // 
            // lstVwUnidadesFisicas
            // 
            this.lstVwUnidadesFisicas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstVwUnidadesFisicas.CheckBoxes = true;
            this.lstVwUnidadesFisicas.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chEliminar,
            this.chCodigo,
            this.chDesignacao,
            this.chDimensoes});
            this.lstVwUnidadesFisicas.CustomizedSorting = true;
            this.lstVwUnidadesFisicas.FullRowSelect = true;
            this.lstVwUnidadesFisicas.HideSelection = false;
            this.lstVwUnidadesFisicas.Location = new System.Drawing.Point(6, 19);
            this.lstVwUnidadesFisicas.Name = "lstVwUnidadesFisicas";
            this.lstVwUnidadesFisicas.ReturnSubItemIndex = false;
            this.lstVwUnidadesFisicas.Size = new System.Drawing.Size(417, 168);
            this.lstVwUnidadesFisicas.TabIndex = 0;
            this.lstVwUnidadesFisicas.UseCompatibleStateImageBehavior = false;
            this.lstVwUnidadesFisicas.View = System.Windows.Forms.View.Details;
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
            // grpAutosEliminacao
            // 
            this.grpAutosEliminacao.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.grpAutosEliminacao.Controls.Add(this.lstVwAutosEliminacao);
            this.grpAutosEliminacao.Location = new System.Drawing.Point(3, 3);
            this.grpAutosEliminacao.Name = "grpAutosEliminacao";
            this.grpAutosEliminacao.Size = new System.Drawing.Size(364, 193);
            this.grpAutosEliminacao.TabIndex = 6;
            this.grpAutosEliminacao.TabStop = false;
            this.grpAutosEliminacao.Text = "Autos de eliminação";
            // 
            // lstVwAutosEliminacao
            // 
            this.lstVwAutosEliminacao.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstVwAutosEliminacao.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chAEDesignacao});
            this.lstVwAutosEliminacao.Location = new System.Drawing.Point(6, 19);
            this.lstVwAutosEliminacao.Name = "lstVwAutosEliminacao";
            this.lstVwAutosEliminacao.Size = new System.Drawing.Size(352, 168);
            this.lstVwAutosEliminacao.TabIndex = 0;
            this.lstVwAutosEliminacao.UseCompatibleStateImageBehavior = false;
            this.lstVwAutosEliminacao.View = System.Windows.Forms.View.Details;
            this.lstVwAutosEliminacao.SelectedIndexChanged += new System.EventHandler(this.lstVwAutosEliminacao_SelectedIndexChanged);
            // 
            // chAEDesignacao
            // 
            this.chAEDesignacao.Text = "Designação";
            this.chAEDesignacao.Width = 200;
            // 
            // PanelDepUFEliminadas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpAutosEliminacao);
            this.Controls.Add(this.grp_NotasEliminacao);
            this.Controls.Add(this.grpUnidadesFisicasAvaliadas);
            this.Name = "PanelDepUFEliminadas";
            this.grp_NotasEliminacao.ResumeLayout(false);
            this.grp_NotasEliminacao.PerformLayout();
            this.grpUnidadesFisicasAvaliadas.ResumeLayout(false);
            this.grpAutosEliminacao.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grp_NotasEliminacao;
        private System.Windows.Forms.TextBox txt_NotasEliminacao;
        private System.Windows.Forms.GroupBox grpUnidadesFisicasAvaliadas;
        private GISA.Controls.PxListView lstVwUnidadesFisicas;
        private System.Windows.Forms.ColumnHeader chEliminar;
        private System.Windows.Forms.ColumnHeader chCodigo;
        private System.Windows.Forms.ColumnHeader chDesignacao;
        private System.Windows.Forms.ColumnHeader chDimensoes;
        private System.Windows.Forms.GroupBox grpAutosEliminacao;
        private System.Windows.Forms.ListView lstVwAutosEliminacao;
        private System.Windows.Forms.ColumnHeader chAEDesignacao;
    }
}
