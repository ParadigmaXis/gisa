namespace GISA
{
    partial class frmObjetoDigitalSimples
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.grpODTitleAndPub = new System.Windows.Forms.GroupBox();
            this.ButtonTI = new System.Windows.Forms.Button();
            this.txtTitulo = new System.Windows.Forms.TextBox();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.chkPublicar = new System.Windows.Forms.CheckBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.versionControl = new GISA.ControlObjectoDigitalVersao();
            this.ficheirosOrderManager1 = new GISA.FicheirosOrderManager();
            this.grpObjectosDigitaisComponentes = new System.Windows.Forms.GroupBox();
            this.previewControl = new GISA.ControlFedoraPdfViewer();
            this.CurrentToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.grpODTitleAndPub.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.grpObjectosDigitaisComponentes.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(840, 717);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "Aceitar";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(921, 717);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // grpODTitleAndPub
            // 
            this.grpODTitleAndPub.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpODTitleAndPub.Controls.Add(this.ButtonTI);
            this.grpODTitleAndPub.Controls.Add(this.txtTitulo);
            this.grpODTitleAndPub.Controls.Add(this.lblTitulo);
            this.grpODTitleAndPub.Controls.Add(this.chkPublicar);
            this.grpODTitleAndPub.Location = new System.Drawing.Point(0, 0);
            this.grpODTitleAndPub.Name = "grpODTitleAndPub";
            this.grpODTitleAndPub.Size = new System.Drawing.Size(638, 41);
            this.grpODTitleAndPub.TabIndex = 12;
            this.grpODTitleAndPub.TabStop = false;
            // 
            // ButtonTI
            // 
            this.ButtonTI.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonTI.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ButtonTI.ImageIndex = 1;
            this.ButtonTI.Location = new System.Drawing.Point(524, 16);
            this.ButtonTI.Name = "ButtonTI";
            this.ButtonTI.Size = new System.Drawing.Size(24, 20);
            this.ButtonTI.TabIndex = 20;
            this.ButtonTI.Click += new System.EventHandler(this.ButtonTI_Click);
            // 
            // txtTitulo
            // 
            this.txtTitulo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTitulo.Location = new System.Drawing.Point(47, 16);
            this.txtTitulo.MaxLength = 255;
            this.txtTitulo.Name = "txtTitulo";
            this.txtTitulo.Size = new System.Drawing.Size(468, 20);
            this.txtTitulo.TabIndex = 19;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Location = new System.Drawing.Point(6, 19);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(35, 13);
            this.lblTitulo.TabIndex = 18;
            this.lblTitulo.Text = "Título";
            // 
            // chkPublicar
            // 
            this.chkPublicar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkPublicar.Location = new System.Drawing.Point(568, 17);
            this.chkPublicar.Name = "chkPublicar";
            this.chkPublicar.Size = new System.Drawing.Size(64, 18);
            this.chkPublicar.TabIndex = 17;
            this.chkPublicar.Text = "Publicar";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 12);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.versionControl);
            this.splitContainer1.Panel1.Controls.Add(this.grpODTitleAndPub);
            this.splitContainer1.Panel1.Controls.Add(this.ficheirosOrderManager1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.grpObjectosDigitaisComponentes);
            this.splitContainer1.Size = new System.Drawing.Size(984, 690);
            this.splitContainer1.SplitterDistance = 641;
            this.splitContainer1.TabIndex = 13;
            // 
            // versionControl
            // 
            this.versionControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.versionControl.Location = new System.Drawing.Point(1, 623);
            this.versionControl.MaximumSize = new System.Drawing.Size(9999, 67);
            this.versionControl.MinimumSize = new System.Drawing.Size(330, 67);
            this.versionControl.Name = "versionControl";
            this.versionControl.Size = new System.Drawing.Size(638, 67);
            this.versionControl.TabIndex = 14;
            this.versionControl.VersionChanged += new GISA.ControlObjectoDigitalVersao.OnVersionChange(this.controlObjectoDigitalVersao1_VersionChanged);
            // 
            // ficheirosOrderManager1
            // 
            this.ficheirosOrderManager1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ficheirosOrderManager1.Location = new System.Drawing.Point(0, 42);
            this.ficheirosOrderManager1.Name = "ficheirosOrderManager1";
            this.ficheirosOrderManager1.Size = new System.Drawing.Size(638, 581);
            this.ficheirosOrderManager1.TabIndex = 2;
            // 
            // grpObjectosDigitaisComponentes
            // 
            this.grpObjectosDigitaisComponentes.Controls.Add(this.previewControl);
            this.grpObjectosDigitaisComponentes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpObjectosDigitaisComponentes.Location = new System.Drawing.Point(0, 0);
            this.grpObjectosDigitaisComponentes.Name = "grpObjectosDigitaisComponentes";
            this.grpObjectosDigitaisComponentes.Size = new System.Drawing.Size(339, 690);
            this.grpObjectosDigitaisComponentes.TabIndex = 11;
            this.grpObjectosDigitaisComponentes.TabStop = false;
            this.grpObjectosDigitaisComponentes.Text = "Visualizador";
            // 
            // previewControl
            // 
            this.previewControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.previewControl.Location = new System.Drawing.Point(3, 17);
            this.previewControl.Name = "previewControl";
            this.previewControl.Qualidade = "Baixa";
            this.previewControl.Size = new System.Drawing.Size(330, 667);
            this.previewControl.TabIndex = 0;
            // 
            // frmObjetoDigitalSimples
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(1008, 752);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.KeyPreview = true;
            this.MinimizeBox = false;
            this.Name = "frmObjetoDigitalSimples";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Objeto Digital Simples";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmObjetoDigitalSimples_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmObjetoDigital_FormClosed);
            this.Shown += new System.EventHandler(this.frmObjetoDigitalSimples_Shown);
            this.grpODTitleAndPub.ResumeLayout(false);
            this.grpODTitleAndPub.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.grpObjectosDigitaisComponentes.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private FicheirosOrderManager ficheirosOrderManager1;
        internal System.Windows.Forms.GroupBox grpODTitleAndPub;
        private System.Windows.Forms.TextBox txtTitulo;
        private System.Windows.Forms.Label lblTitulo;
        internal System.Windows.Forms.CheckBox chkPublicar;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox grpObjectosDigitaisComponentes;
        private ControlFedoraPdfViewer previewControl;
        internal System.Windows.Forms.Button ButtonTI;
        private System.Windows.Forms.ToolTip CurrentToolTip;
        private ControlObjectoDigitalVersao versionControl;
    }
}