namespace GISA
{
    partial class MasterPanelNivelImagensIlustracao
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
            this.nivelGrupoArquivosList1 = new GISA.Controls.Nivel.NivelGrupoArquivosList();
            this.pnlToolbarPadding.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblFuncao
            // 
            this.lblFuncao.Location = new System.Drawing.Point(0, 0);
            // 
            // pnlToolbarPadding
            // 
            this.pnlToolbarPadding.Location = new System.Drawing.Point(0, 24);
            // 
            // nivelGrupoArquivosList1
            // 
            this.nivelGrupoArquivosList1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.nivelGrupoArquivosList1.CustomizedSorting = false;
            this.nivelGrupoArquivosList1.FilterVisible = false;
            this.nivelGrupoArquivosList1.Location = new System.Drawing.Point(0, 55);
            this.nivelGrupoArquivosList1.MultiSelectListView = false;
            this.nivelGrupoArquivosList1.Name = "nivelGrupoArquivosList1";
            this.nivelGrupoArquivosList1.Padding = new System.Windows.Forms.Padding(6);
            this.nivelGrupoArquivosList1.Size = new System.Drawing.Size(600, 225);
            this.nivelGrupoArquivosList1.TabIndex = 2;
            // 
            // MasterPanelNivelImagemIlustracao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.nivelGrupoArquivosList1);
            this.Name = "MasterPanelNivelImagemIlustracao";
            this.Controls.SetChildIndex(this.lblFuncao, 0);
            this.Controls.SetChildIndex(this.pnlToolbarPadding, 0);
            this.Controls.SetChildIndex(this.nivelGrupoArquivosList1, 0);
            this.pnlToolbarPadding.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.Nivel.NivelGrupoArquivosList nivelGrupoArquivosList1;
    }
}
