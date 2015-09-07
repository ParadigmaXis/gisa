namespace GISA
{
    partial class AutoEliminacaoList
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
            this.colDesignacao = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.txtFiltroDesignacao = new System.Windows.Forms.TextBox();
            this.lblFiltroDesignacao = new System.Windows.Forms.Label();
            this.grpResultados.SuspendLayout();
            this.grpFiltro.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpResultados
            // 
            this.grpResultados.Size = new System.Drawing.Size(620, 273);
            // 
            // txtNroPagina
            // 
            this.txtNroPagina.Location = new System.Drawing.Point(588, 64);
            // 
            // lstVwPaginated
            // 
            this.lstVwPaginated.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colDesignacao});
            this.lstVwPaginated.Size = new System.Drawing.Size(572, 249);
            // 
            // btnProximo
            // 
            this.btnProximo.Location = new System.Drawing.Point(588, 92);
            // 
            // btnAnterior
            // 
            this.btnAnterior.Location = new System.Drawing.Point(588, 32);
            // 
            // grpFiltro
            // 
            this.grpFiltro.Controls.Add(this.txtFiltroDesignacao);
            this.grpFiltro.Controls.Add(this.lblFiltroDesignacao);
            this.grpFiltro.Size = new System.Drawing.Size(620, 59);
            this.grpFiltro.Controls.SetChildIndex(this.btnAplicar, 0);
            this.grpFiltro.Controls.SetChildIndex(this.lblFiltroDesignacao, 0);
            this.grpFiltro.Controls.SetChildIndex(this.txtFiltroDesignacao, 0);
            // 
            // btnAplicar
            // 
            this.btnAplicar.Location = new System.Drawing.Point(548, 21);
            // 
            // colDesignacao
            // 
            this.colDesignacao.Text = "Designação";
            this.colDesignacao.Width = 403;
            // 
            // txtFiltroDesignacao
            // 
            this.txtFiltroDesignacao.AcceptsReturn = true;
            this.txtFiltroDesignacao.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFiltroDesignacao.Location = new System.Drawing.Point(8, 32);
            this.txtFiltroDesignacao.Name = "txtFiltroDesignacao";
            this.txtFiltroDesignacao.Size = new System.Drawing.Size(534, 20);
            this.txtFiltroDesignacao.TabIndex = 1;
            // 
            // lblFiltroDesignacao
            // 
            this.lblFiltroDesignacao.Location = new System.Drawing.Point(8, 16);
            this.lblFiltroDesignacao.Name = "lblFiltroDesignacao";
            this.lblFiltroDesignacao.Size = new System.Drawing.Size(71, 16);
            this.lblFiltroDesignacao.TabIndex = 0;
            this.lblFiltroDesignacao.Text = "Designação";
            // 
            // AutoEliminacaoList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.FilterVisible = true;
            this.Name = "AutoEliminacaoList";
            this.Size = new System.Drawing.Size(632, 344);
            this.grpResultados.ResumeLayout(false);
            this.grpFiltro.ResumeLayout(false);
            this.grpFiltro.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.ColumnHeader colDesignacao;
        internal System.Windows.Forms.TextBox txtFiltroDesignacao;
        internal System.Windows.Forms.Label lblFiltroDesignacao;
    }
}
