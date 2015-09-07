namespace GISA
{
    partial class PanelConteudoInformacional
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
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.txtConteudoInformacional = new System.Windows.Forms.TextBox();
            this.contInfLicencaObras1 = new GISA.ContInfLicencaObras();
            this.contInfLicencaObrasSD1 = new GISA.ContInfLicencaObrasSD();
            this.GroupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // GroupBox1
            // 
            this.GroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GroupBox1.Controls.Add(this.txtConteudoInformacional);
            this.GroupBox1.Controls.Add(this.contInfLicencaObras1);
            this.GroupBox1.Controls.Add(this.contInfLicencaObrasSD1);
            this.GroupBox1.Location = new System.Drawing.Point(0, 0);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(800, 600);
            this.GroupBox1.TabIndex = 6;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "Conteúdo informacional";
            // 
            // txtConteudoInformacional
            // 
            this.txtConteudoInformacional.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtConteudoInformacional.Location = new System.Drawing.Point(6, 19);
            this.txtConteudoInformacional.MaxLength = 40000;
            this.txtConteudoInformacional.Multiline = true;
            this.txtConteudoInformacional.Name = "txtConteudoInformacional";
            this.txtConteudoInformacional.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtConteudoInformacional.Size = new System.Drawing.Size(788, 575);
            this.txtConteudoInformacional.TabIndex = 1;
            this.txtConteudoInformacional.Visible = false;
            // 
            // contInfLicencaObras1
            // 
            this.contInfLicencaObras1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.contInfLicencaObras1.AutoScroll = true;
            this.contInfLicencaObras1.IsLoaded = false;
            this.contInfLicencaObras1.IsPopulated = false;
            this.contInfLicencaObras1.Location = new System.Drawing.Point(6, 19);
            this.contInfLicencaObras1.MinSize = new System.Drawing.Size(0, 0);
            this.contInfLicencaObras1.Name = "contInfLicencaObras1";
            this.contInfLicencaObras1.Size = new System.Drawing.Size(788, 575);
            this.contInfLicencaObras1.TabIndex = 0;
            this.contInfLicencaObras1.TbBAuxListEventAssigned = false;
            this.contInfLicencaObras1.TheGenericDelegate = null;
            this.contInfLicencaObras1.Visible = false;
            // 
            // contInfLicencaObrasSD1
            // 
            this.contInfLicencaObrasSD1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.contInfLicencaObrasSD1.AutoScroll = true;
            this.contInfLicencaObrasSD1.IsLoaded = false;
            this.contInfLicencaObrasSD1.IsPopulated = false;
            this.contInfLicencaObrasSD1.Location = new System.Drawing.Point(6, 19);
            this.contInfLicencaObrasSD1.MinSize = new System.Drawing.Size(0, 0);
            this.contInfLicencaObrasSD1.Name = "contInfLicencaObrasSD1";
            this.contInfLicencaObrasSD1.Size = new System.Drawing.Size(788, 575);
            this.contInfLicencaObrasSD1.TabIndex = 0;
            this.contInfLicencaObrasSD1.TbBAuxListEventAssigned = false;
            this.contInfLicencaObrasSD1.TheGenericDelegate = null;
            this.contInfLicencaObrasSD1.Visible = false;
            // 
            // PanelConteudoInformacional
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.GroupBox1);
            this.MinSize = new System.Drawing.Size(900, 600);
            this.Name = "PanelConteudoInformacional";
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.GroupBox GroupBox1;
        private ContInfLicencaObras contInfLicencaObras1;
        private ContInfLicencaObrasSD contInfLicencaObrasSD1;
        private System.Windows.Forms.TextBox txtConteudoInformacional;
    }
}
