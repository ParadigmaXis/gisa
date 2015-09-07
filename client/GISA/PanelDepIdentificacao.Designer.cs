namespace GISA
{
    partial class PanelDepIdentificacao
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
            this.grpDetalhes = new System.Windows.Forms.GroupBox();
            this.grpMetragem = new System.Windows.Forms.GroupBox();
            this.txtMetragem = new System.Windows.Forms.TextBox();
            this.grpUFsAssociadas = new System.Windows.Forms.GroupBox();
            this.lblInfoSuporte = new System.Windows.Forms.Label();
            this.lstVwUnidadesFisicasAssoc = new System.Windows.Forms.ListView();
            this.chCodigo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chDesignacao = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chTipo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chDimensoes = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chCota = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chProducao = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chEliminado = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnRemove = new System.Windows.Forms.Button();
            this.grpDesignacao = new System.Windows.Forms.GroupBox();
            this.txtDesignacao = new System.Windows.Forms.TextBox();
            this.grpDetalhes.SuspendLayout();
            this.grpMetragem.SuspendLayout();
            this.grpUFsAssociadas.SuspendLayout();
            this.grpDesignacao.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpDetalhes
            // 
            this.grpDetalhes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpDetalhes.Controls.Add(this.grpMetragem);
            this.grpDetalhes.Controls.Add(this.grpUFsAssociadas);
            this.grpDetalhes.Controls.Add(this.grpDesignacao);
            this.grpDetalhes.Location = new System.Drawing.Point(0, 0);
            this.grpDetalhes.Name = "grpDetalhes";
            this.grpDetalhes.Size = new System.Drawing.Size(797, 597);
            this.grpDetalhes.TabIndex = 62;
            this.grpDetalhes.TabStop = false;
            this.grpDetalhes.Text = "Detalhes";
            // 
            // grpMetragem
            // 
            this.grpMetragem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.grpMetragem.Controls.Add(this.txtMetragem);
            this.grpMetragem.Location = new System.Drawing.Point(600, 19);
            this.grpMetragem.Name = "grpMetragem";
            this.grpMetragem.Size = new System.Drawing.Size(191, 47);
            this.grpMetragem.TabIndex = 67;
            this.grpMetragem.TabStop = false;
            this.grpMetragem.Text = "Metragem";
            // 
            // txtMetragem
            // 
            this.txtMetragem.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMetragem.Enabled = false;
            this.txtMetragem.Location = new System.Drawing.Point(6, 19);
            this.txtMetragem.Name = "txtMetragem";
            this.txtMetragem.Size = new System.Drawing.Size(179, 20);
            this.txtMetragem.TabIndex = 0;
            // 
            // grpUFsAssociadas
            // 
            this.grpUFsAssociadas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpUFsAssociadas.Controls.Add(this.lblInfoSuporte);
            this.grpUFsAssociadas.Controls.Add(this.lstVwUnidadesFisicasAssoc);
            this.grpUFsAssociadas.Controls.Add(this.btnRemove);
            this.grpUFsAssociadas.Location = new System.Drawing.Point(8, 72);
            this.grpUFsAssociadas.Name = "grpUFsAssociadas";
            this.grpUFsAssociadas.Size = new System.Drawing.Size(783, 519);
            this.grpUFsAssociadas.TabIndex = 67;
            this.grpUFsAssociadas.TabStop = false;
            this.grpUFsAssociadas.Text = "Suporte";
            // 
            // lblInfoSuporte
            // 
            this.lblInfoSuporte.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblInfoSuporte.AutoSize = true;
            this.lblInfoSuporte.Location = new System.Drawing.Point(459, 0);
            this.lblInfoSuporte.Name = "lblInfoSuporte";
            this.lblInfoSuporte.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblInfoSuporte.Size = new System.Drawing.Size(0, 13);
            this.lblInfoSuporte.TabIndex = 68;
            this.lblInfoSuporte.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lstVwUnidadesFisicasAssoc
            // 
            this.lstVwUnidadesFisicasAssoc.AllowDrop = true;
            this.lstVwUnidadesFisicasAssoc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstVwUnidadesFisicasAssoc.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chCodigo,
            this.chDesignacao,
            this.chTipo,
            this.chDimensoes,
            this.chCota,
            this.chProducao,
            this.chEliminado});
            this.lstVwUnidadesFisicasAssoc.FullRowSelect = true;
            this.lstVwUnidadesFisicasAssoc.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstVwUnidadesFisicasAssoc.HideSelection = false;
            this.lstVwUnidadesFisicasAssoc.Location = new System.Drawing.Point(6, 19);
            this.lstVwUnidadesFisicasAssoc.Name = "lstVwUnidadesFisicasAssoc";
            this.lstVwUnidadesFisicasAssoc.Size = new System.Drawing.Size(741, 494);
            this.lstVwUnidadesFisicasAssoc.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.lstVwUnidadesFisicasAssoc.TabIndex = 0;
            this.lstVwUnidadesFisicasAssoc.UseCompatibleStateImageBehavior = false;
            this.lstVwUnidadesFisicasAssoc.View = System.Windows.Forms.View.Details;
            // 
            // chCodigo
            // 
            this.chCodigo.Text = "Código parcial";
            this.chCodigo.Width = 80;
            // 
            // chDesignacao
            // 
            this.chDesignacao.Text = "Designação";
            this.chDesignacao.Width = 301;
            // 
            // chTipo
            // 
            this.chTipo.Text = "Tipo";
            // 
            // chDimensoes
            // 
            this.chDimensoes.Text = "Dimensões";
            this.chDimensoes.Width = 110;
            // 
            // chCota
            // 
            this.chCota.Text = "Cota";
            this.chCota.Width = 80;
            // 
            // chProducao
            // 
            this.chProducao.Text = "Produção";
            this.chProducao.Width = 140;
            // 
            // chEliminado
            // 
            this.chEliminado.Text = "Em depósito";
            this.chEliminado.Width = 80;
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRemove.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnRemove.Location = new System.Drawing.Point(753, 36);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(24, 24);
            this.btnRemove.TabIndex = 1;
            // 
            // grpDesignacao
            // 
            this.grpDesignacao.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpDesignacao.Controls.Add(this.txtDesignacao);
            this.grpDesignacao.Location = new System.Drawing.Point(8, 19);
            this.grpDesignacao.Name = "grpDesignacao";
            this.grpDesignacao.Size = new System.Drawing.Size(586, 47);
            this.grpDesignacao.TabIndex = 66;
            this.grpDesignacao.TabStop = false;
            this.grpDesignacao.Text = "Designação";
            // 
            // txtDesignacao
            // 
            this.txtDesignacao.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDesignacao.Enabled = false;
            this.txtDesignacao.Location = new System.Drawing.Point(6, 19);
            this.txtDesignacao.Name = "txtDesignacao";
            this.txtDesignacao.Size = new System.Drawing.Size(574, 20);
            this.txtDesignacao.TabIndex = 0;
            // 
            // PanelDepIdentificacao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpDetalhes);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "PanelDepIdentificacao";
            this.grpDetalhes.ResumeLayout(false);
            this.grpMetragem.ResumeLayout(false);
            this.grpMetragem.PerformLayout();
            this.grpUFsAssociadas.ResumeLayout(false);
            this.grpUFsAssociadas.PerformLayout();
            this.grpDesignacao.ResumeLayout(false);
            this.grpDesignacao.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.GroupBox grpDetalhes;
        private System.Windows.Forms.GroupBox grpUFsAssociadas;
        private System.Windows.Forms.Label lblInfoSuporte;
        internal System.Windows.Forms.ListView lstVwUnidadesFisicasAssoc;
        internal System.Windows.Forms.ColumnHeader chCodigo;
        internal System.Windows.Forms.ColumnHeader chDesignacao;
        internal System.Windows.Forms.ColumnHeader chTipo;
        internal System.Windows.Forms.ColumnHeader chDimensoes;
        internal System.Windows.Forms.ColumnHeader chCota;
        internal System.Windows.Forms.ColumnHeader chProducao;
        internal System.Windows.Forms.ColumnHeader chEliminado;
        internal System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.GroupBox grpDesignacao;
        private System.Windows.Forms.TextBox txtDesignacao;
        private System.Windows.Forms.GroupBox grpMetragem;
        private System.Windows.Forms.TextBox txtMetragem;
    }
}
