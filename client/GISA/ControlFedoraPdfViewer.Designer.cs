namespace GISA
{
    partial class ControlFedoraPdfViewer {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.grpQualidade = new System.Windows.Forms.GroupBox();
            this.cmbQuality = new System.Windows.Forms.ComboBox();
            this.errorLabel = new System.Windows.Forms.Label();
            this.groupFicheiro = new System.Windows.Forms.GroupBox();
            this.lblFicheiro = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.grpQualidade.SuspendLayout();
            this.groupFicheiro.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // webBrowser
            // 
            this.webBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowser.Location = new System.Drawing.Point(0, 47);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.Size = new System.Drawing.Size(347, 402);
            this.webBrowser.TabIndex = 19;
            this.webBrowser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser_DocumentCompleted);
            // 
            // grpQualidade
            // 
            this.grpQualidade.Controls.Add(this.cmbQuality);
            this.grpQualidade.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpQualidade.Location = new System.Drawing.Point(0, 0);
            this.grpQualidade.Name = "grpQualidade";
            this.grpQualidade.Size = new System.Drawing.Size(195, 41);
            this.grpQualidade.TabIndex = 20;
            this.grpQualidade.TabStop = false;
            this.grpQualidade.Text = "Qualidade";
            // 
            // cmbQuality
            // 
            this.cmbQuality.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbQuality.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbQuality.Enabled = false;
            this.cmbQuality.FormattingEnabled = true;
            this.cmbQuality.Location = new System.Drawing.Point(6, 15);
            this.cmbQuality.Name = "cmbQuality";
            this.cmbQuality.Size = new System.Drawing.Size(183, 21);
            this.cmbQuality.TabIndex = 0;
            this.cmbQuality.SelectedIndexChanged += new System.EventHandler(this.cmbQuality_SelectedIndexChanged);
            // 
            // errorLabel
            // 
            this.errorLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.errorLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(86)))), ((int)(((byte)(86)))));
            this.errorLabel.ForeColor = System.Drawing.Color.White;
            this.errorLabel.Location = new System.Drawing.Point(3, 47);
            this.errorLabel.Name = "errorLabel";
            this.errorLabel.Size = new System.Drawing.Size(344, 402);
            this.errorLabel.TabIndex = 21;
            this.errorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupFicheiro
            // 
            this.groupFicheiro.Controls.Add(this.lblFicheiro);
            this.groupFicheiro.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupFicheiro.Location = new System.Drawing.Point(0, 0);
            this.groupFicheiro.Name = "groupFicheiro";
            this.groupFicheiro.Size = new System.Drawing.Size(148, 41);
            this.groupFicheiro.TabIndex = 22;
            this.groupFicheiro.TabStop = false;
            this.groupFicheiro.Text = "Ficheiro";
            // 
            // lblFicheiro
            // 
            this.lblFicheiro.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFicheiro.Location = new System.Drawing.Point(6, 18);
            this.lblFicheiro.Name = "lblFicheiro";
            this.lblFicheiro.Size = new System.Drawing.Size(136, 18);
            this.lblFicheiro.TabIndex = 23;
            this.lblFicheiro.Text = "label1";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupFicheiro);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.grpQualidade);
            this.splitContainer1.Size = new System.Drawing.Size(347, 41);
            this.splitContainer1.SplitterDistance = 148;
            this.splitContainer1.TabIndex = 24;
            // 
            // ControlFedoraPdfViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.errorLabel);
            this.Controls.Add(this.webBrowser);
            this.Name = "ControlFedoraPdfViewer";
            this.Size = new System.Drawing.Size(347, 452);
            this.grpQualidade.ResumeLayout(false);
            this.groupFicheiro.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowser;
        internal System.Windows.Forms.GroupBox grpQualidade;
        private System.Windows.Forms.ComboBox cmbQuality;
        private System.Windows.Forms.Label errorLabel;
        private System.Windows.Forms.GroupBox groupFicheiro;
        private System.Windows.Forms.Label lblFicheiro;
        private System.Windows.Forms.SplitContainer splitContainer1;

    }
}
