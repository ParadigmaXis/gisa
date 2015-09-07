namespace GISA.Controls.Nivel
{
    partial class NivelNavigator
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
            this.nivelEstruturalList1 = new NivelEstruturalList();
            this.nivelDocumentalListNavigator1 = new GISA.Controls.Nivel.NivelDocumentalListNavigator();
            this.controloNivelListEstrutural1 = new GISA.Controls.Localizacao.ControloNivelListEstrutural();
            this.SuspendLayout();
            // 
            // nivelEstruturalList1
            // 
            this.nivelEstruturalList1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.nivelEstruturalList1.BackColor = System.Drawing.SystemColors.Control;
            this.nivelEstruturalList1.Location = new System.Drawing.Point(0, 0);
            this.nivelEstruturalList1.Name = "nivelDocumentalListNavigator1";
            this.nivelEstruturalList1.Padding = new System.Windows.Forms.Padding(6, 0, 6, 6);
            this.nivelEstruturalList1.Size = new System.Drawing.Size(647, 391);
            this.nivelEstruturalList1.TabIndex = 0;
            this.nivelEstruturalList1.Visible = false;
            // 
            // nivelDocumentalListNavigator1
            // 
            this.nivelDocumentalListNavigator1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.nivelDocumentalListNavigator1.BackColor = System.Drawing.SystemColors.Control;
            this.nivelDocumentalListNavigator1.IsParentSupport = false;
            this.nivelDocumentalListNavigator1.MovimentoID = ((long)0);
            this.nivelDocumentalListNavigator1.Location = new System.Drawing.Point(0, 0);
            this.nivelDocumentalListNavigator1.Name = "nivelDocumentalListNavigator1";
            this.nivelDocumentalListNavigator1.Padding = new System.Windows.Forms.Padding(6, 0, 6, 6);
            this.nivelDocumentalListNavigator1.Size = new System.Drawing.Size(647, 391);
            this.nivelDocumentalListNavigator1.TabIndex = 0;
            this.nivelDocumentalListNavigator1.Visible = false;
            // 
            // controloNivelListEstrutural1
            // 
            this.controloNivelListEstrutural1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.controloNivelListEstrutural1.Location = new System.Drawing.Point(0, 0);
            this.controloNivelListEstrutural1.Name = "controloNivelListEstrutural1";
            this.controloNivelListEstrutural1.Size = new System.Drawing.Size(647, 391);
            this.controloNivelListEstrutural1.TabIndex = 1;
            // 
            // NivelNavigator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.controloNivelListEstrutural1);
            this.Controls.Add(this.nivelDocumentalListNavigator1);
            this.Controls.Add(this.nivelEstruturalList1);
            this.Name = "NivelNavigator";
            this.Size = new System.Drawing.Size(647, 391);
            this.ResumeLayout(false);

        }

        #endregion

        private NivelDocumentalListNavigator nivelDocumentalListNavigator1;
        private NivelEstruturalList nivelEstruturalList1;
        private GISA.Controls.Localizacao.ControloNivelListEstrutural controloNivelListEstrutural1;
    }
}
