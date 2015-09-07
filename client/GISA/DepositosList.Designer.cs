using GISA.Controls;

namespace GISA
{
    partial class DepositosList
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
            this.chDesignacao = new System.Windows.Forms.ColumnHeader();
            this.grpResultados.SuspendLayout();
            this.grpFiltro.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstVwPaginated
            // 
            this.lstVwPaginated.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chDesignacao});
            // 
            // grpFiltro
            // 
            this.grpFiltro.Controls.Add(this.txtFiltroDesignacao);
            this.grpFiltro.Controls.Add(this.lblFiltroDesignacao);
            this.grpFiltro.Controls.SetChildIndex(this.btnAplicar, 0);
            this.grpFiltro.Controls.SetChildIndex(this.lblFiltroDesignacao, 0);
            this.grpFiltro.Controls.SetChildIndex(this.txtFiltroDesignacao, 0);
            // 
            // txtFiltroDesignacao
            // 
            this.txtFiltroDesignacao.AcceptsReturn = true;
            this.txtFiltroDesignacao.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFiltroDesignacao.Location = new System.Drawing.Point(8, 32);
            this.txtFiltroDesignacao.Name = "txtFiltroDesignacao";
            this.txtFiltroDesignacao.Size = new System.Drawing.Size(753, 20);
            this.txtFiltroDesignacao.TabIndex = 6;
            // 
            // lblFiltroDesignacao
            // 
            this.lblFiltroDesignacao.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFiltroDesignacao.Location = new System.Drawing.Point(5, 16);
            this.lblFiltroDesignacao.Name = "lblFiltroDesignacao";
            this.lblFiltroDesignacao.Size = new System.Drawing.Size(118, 16);
            this.lblFiltroDesignacao.TabIndex = 5;
            this.lblFiltroDesignacao.Text = "Designação";
            // 
            // chDesignacao
            // 
            this.chDesignacao.Text = "Designação";
            this.chDesignacao.Width = 600;
            // 
            // DepositosList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "DepositosList";
            this.grpResultados.ResumeLayout(false);
            this.grpFiltro.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.TextBox txtFiltroDesignacao;
        internal System.Windows.Forms.Label lblFiltroDesignacao;
        private System.Windows.Forms.ColumnHeader chDesignacao;

    }
}
