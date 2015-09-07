namespace GISA.Controls.Nivel
{
    partial class NivelEstruturalList
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
            this.txtFiltroDesignacao = new System.Windows.Forms.TextBox();
            this.lblFiltroDesignacao = new System.Windows.Forms.Label();
            this.chDesignacao = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDataProducao = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.grpResultados.SuspendLayout();
            this.grpFiltro.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstVwPaginated
            // 
            this.lstVwPaginated.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chDesignacao,
            this.colDataProducao});
            // 
            // grpFiltro
            // 
            this.grpFiltro.Controls.Add(this.txtFiltroDesignacao);
            this.grpFiltro.Controls.Add(this.lblFiltroDesignacao);
            this.grpFiltro.Controls.SetChildIndex(this.lblFiltroDesignacao, 0);
            this.grpFiltro.Controls.SetChildIndex(this.btnAplicar, 0);
            this.grpFiltro.Controls.SetChildIndex(this.txtFiltroDesignacao, 0);
            // 
            // btnAplicar
            // 
            this.btnAplicar.Location = new System.Drawing.Point(767, 30);
            // 
            // txtFiltroDesignacao
            // 
            this.txtFiltroDesignacao.AcceptsReturn = true;
            this.txtFiltroDesignacao.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFiltroDesignacao.Location = new System.Drawing.Point(8, 33);
            this.txtFiltroDesignacao.Name = "txtFiltroDesignacao";
            this.txtFiltroDesignacao.Size = new System.Drawing.Size(753, 20);
            this.txtFiltroDesignacao.TabIndex = 8;
            // 
            // lblFiltroDesignacao
            // 
            this.lblFiltroDesignacao.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFiltroDesignacao.Location = new System.Drawing.Point(5, 17);
            this.lblFiltroDesignacao.Name = "lblFiltroDesignacao";
            this.lblFiltroDesignacao.Size = new System.Drawing.Size(118, 16);
            this.lblFiltroDesignacao.TabIndex = 7;
            this.lblFiltroDesignacao.Text = "Designação";
            // 
            // chDesignacao
            // 
            this.chDesignacao.Text = "Título";
            this.chDesignacao.Width = 437;
            // 
            // colDataProducao
            // 
            this.colDataProducao.Text = "Data de Produção";
            this.colDataProducao.Width = 138;
            // 
            // NivelEstruturalList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.FilterVisible = true;
            this.Name = "NivelEstruturalList";
            this.grpResultados.ResumeLayout(false);
            this.grpFiltro.ResumeLayout(false);
            this.grpFiltro.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtFiltroDesignacao;
        private System.Windows.Forms.Label lblFiltroDesignacao;
        private System.Windows.Forms.ColumnHeader chDesignacao;
        private System.Windows.Forms.ColumnHeader colDataProducao;
    }
}
