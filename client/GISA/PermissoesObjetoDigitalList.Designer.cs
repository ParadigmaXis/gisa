namespace GISA
{
    partial class PermissoesObjetoDigitalList
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
            this.colIdentificador = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDesignacao = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.grpResultados.SuspendLayout();
            this.grpFiltro.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpResultados
            // 
            this.grpResultados.Size = new System.Drawing.Size(572, 210);
            // 
            // txtNroPagina
            // 
            this.txtNroPagina.Location = new System.Drawing.Point(540, 62);
            this.txtNroPagina.TabIndex = 4;
            // 
            // lstVwPaginated
            // 
            this.lstVwPaginated.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colIdentificador,
            this.colDesignacao});
            this.lstVwPaginated.Size = new System.Drawing.Size(524, 186);
            // 
            // btnProximo
            // 
            this.btnProximo.Location = new System.Drawing.Point(540, 92);
            // 
            // btnAnterior
            // 
            this.btnAnterior.Location = new System.Drawing.Point(540, 32);
            // 
            // grpFiltro
            // 
            this.grpFiltro.Size = new System.Drawing.Size(572, 59);
            // 
            // btnAplicar
            // 
            this.btnAplicar.Location = new System.Drawing.Point(500, 21);
            // 
            // colIdentificador
            // 
            this.colIdentificador.Text = "Identificador";
            this.colIdentificador.Width = 106;
            // 
            // colDesignacao
            // 
            this.colDesignacao.Text = "Título";
            this.colDesignacao.Width = 273;
            // 
            // PermissoesObjetoDigitalList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.FilterVisible = true;
            this.Name = "PermissoesObjetoDigitalList";
            this.Size = new System.Drawing.Size(584, 281);
            this.grpResultados.ResumeLayout(false);
            this.grpFiltro.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.ColumnHeader colDesignacao;
        internal System.Windows.Forms.ColumnHeader colIdentificador;
    }
}
