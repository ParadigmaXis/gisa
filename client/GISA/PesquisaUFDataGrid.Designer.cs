namespace GISA
{
    partial class PesquisaUFDataGrid
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
            this.grpResultados.SuspendLayout();
            this.grpFiltro.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpResultados
            // 
            this.grpResultados.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpResultados.Location = new System.Drawing.Point(0, 0);
            this.grpResultados.Size = new System.Drawing.Size(851, 344);
            // 
            // btnAnterior
            // 
            this.btnAnterior.Location = new System.Drawing.Point(819, 32);
            // 
            // btnProximo
            // 
            this.btnProximo.Location = new System.Drawing.Point(819, 92);
            // 
            // txtNroPagina
            // 
            this.txtNroPagina.Location = new System.Drawing.Point(819, 64);
            // 
            // PesquisaDataGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.FilterVisible = true;
            this.Name = "PesquisaDataGrid";
            this.grpResultados.ResumeLayout(false);
            this.grpFiltro.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
