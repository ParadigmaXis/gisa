namespace GISA
{
    partial class ControlObjetoDigital
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
            this.components = new System.ComponentModel.Container();
            this.grpNiveisOrObjFed = new System.Windows.Forms.GroupBox();
            this.grpODTitleAndPub = new System.Windows.Forms.GroupBox();
            this.chkKeepODComposto = new System.Windows.Forms.CheckBox();
            this.txtTitulo = new System.Windows.Forms.TextBox();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.chkPublicar = new System.Windows.Forms.CheckBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.versionControl = new GISA.ControlObjectoDigitalVersao();
            this.FicheirosOrderManager1 = new GISA.FicheirosOrderManager();
            this.DocumentoSimplesOrderManager1 = new GISA.DocumentoSimplesOrderManager();
            this.grpObjectosDigitaisComponentes = new System.Windows.Forms.GroupBox();
            this.previewControl = new GISA.ControlFedoraPdfViewer();
            this.CurrentToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.grpNiveisOrObjFed.SuspendLayout();
            this.grpODTitleAndPub.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.grpObjectosDigitaisComponentes.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpNiveisOrObjFed
            // 
            this.grpNiveisOrObjFed.Controls.Add(this.grpODTitleAndPub);
            this.grpNiveisOrObjFed.Controls.Add(this.splitContainer1);
            this.grpNiveisOrObjFed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpNiveisOrObjFed.Location = new System.Drawing.Point(0, 0);
            this.grpNiveisOrObjFed.Name = "grpNiveisOrObjFed";
            this.grpNiveisOrObjFed.Size = new System.Drawing.Size(675, 490);
            this.grpNiveisOrObjFed.TabIndex = 26;
            this.grpNiveisOrObjFed.TabStop = false;
            // 
            // grpODTitleAndPub
            // 
            this.grpODTitleAndPub.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grpODTitleAndPub.Controls.Add(this.chkKeepODComposto);
            this.grpODTitleAndPub.Controls.Add(this.txtTitulo);
            this.grpODTitleAndPub.Controls.Add(this.lblTitulo);
            this.grpODTitleAndPub.Controls.Add(this.chkPublicar);
            this.grpODTitleAndPub.Location = new System.Drawing.Point(6, 19);
            this.grpODTitleAndPub.Name = "grpODTitleAndPub";
            this.grpODTitleAndPub.Size = new System.Drawing.Size(663, 52);
            this.grpODTitleAndPub.TabIndex = 13;
            this.grpODTitleAndPub.TabStop = false;
            // 
            // chkKeepODComposto
            // 
            this.chkKeepODComposto.AutoSize = true;
            this.chkKeepODComposto.Location = new System.Drawing.Point(9, 0);
            this.chkKeepODComposto.Name = "chkKeepODComposto";
            this.chkKeepODComposto.Size = new System.Drawing.Size(139, 17);
            this.chkKeepODComposto.TabIndex = 20;
            this.chkKeepODComposto.Text = "Objeto Digital Composto";
            this.chkKeepODComposto.UseVisualStyleBackColor = true;
            this.chkKeepODComposto.CheckedChanged += new System.EventHandler(this.chkKeepODComposto_CheckedChanged);
            // 
            // txtTitulo
            // 
            this.txtTitulo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTitulo.Location = new System.Drawing.Point(47, 22);
            this.txtTitulo.MaxLength = 255;
            this.txtTitulo.Name = "txtTitulo";
            this.txtTitulo.Size = new System.Drawing.Size(540, 20);
            this.txtTitulo.TabIndex = 19;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Location = new System.Drawing.Point(6, 25);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(35, 13);
            this.lblTitulo.TabIndex = 18;
            this.lblTitulo.Text = "Título";
            // 
            // chkPublicar
            // 
            this.chkPublicar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkPublicar.Location = new System.Drawing.Point(593, 22);
            this.chkPublicar.Name = "chkPublicar";
            this.chkPublicar.Size = new System.Drawing.Size(64, 21);
            this.chkPublicar.TabIndex = 17;
            this.chkPublicar.Text = "Publicar";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(6, 77);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.DocumentoSimplesOrderManager1);
            this.splitContainer1.Panel1.Controls.Add(this.versionControl);
            this.splitContainer1.Panel1.Controls.Add(this.FicheirosOrderManager1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.grpObjectosDigitaisComponentes);
            this.splitContainer1.Size = new System.Drawing.Size(663, 407);
            this.splitContainer1.SplitterDistance = 464;
            this.splitContainer1.TabIndex = 12;
            // 
            // versionControl
            // 
            this.versionControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.versionControl.Location = new System.Drawing.Point(0, 345);
            this.versionControl.MaximumSize = new System.Drawing.Size(9999, 62);
            this.versionControl.MinimumSize = new System.Drawing.Size(330, 62);
            this.versionControl.Name = "versionControl";
            this.versionControl.Size = new System.Drawing.Size(464, 62);
            this.versionControl.TabIndex = 12;
            this.versionControl.VersionChanged += new GISA.ControlObjectoDigitalVersao.OnVersionChange(this.versionControl_VersionChanged);
            // 
            // FicheirosOrderManager1
            // 
            this.FicheirosOrderManager1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.FicheirosOrderManager1.Location = new System.Drawing.Point(0, 0);
            this.FicheirosOrderManager1.Name = "FicheirosOrderManager1";
            this.FicheirosOrderManager1.Size = new System.Drawing.Size(464, 346);
            this.FicheirosOrderManager1.TabIndex = 11;
            this.FicheirosOrderManager1.Visible = false;
            // 
            // DocumentoSimplesOrderManager1
            // 
            this.DocumentoSimplesOrderManager1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DocumentoSimplesOrderManager1.Location = new System.Drawing.Point(0, 0);
            this.DocumentoSimplesOrderManager1.Name = "DocumentoSimplesOrderManager1";
            this.DocumentoSimplesOrderManager1.Size = new System.Drawing.Size(464, 407);
            this.DocumentoSimplesOrderManager1.TabIndex = 11;
            this.DocumentoSimplesOrderManager1.Visible = false;
            // 
            // grpObjectosDigitaisComponentes
            // 
            this.grpObjectosDigitaisComponentes.Controls.Add(this.previewControl);
            this.grpObjectosDigitaisComponentes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpObjectosDigitaisComponentes.Location = new System.Drawing.Point(0, 0);
            this.grpObjectosDigitaisComponentes.Name = "grpObjectosDigitaisComponentes";
            this.grpObjectosDigitaisComponentes.Size = new System.Drawing.Size(195, 407);
            this.grpObjectosDigitaisComponentes.TabIndex = 10;
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
            this.previewControl.Qualidade = "Média";
            this.previewControl.Size = new System.Drawing.Size(186, 384);
            this.previewControl.TabIndex = 0;
            // 
            // ControlObjetoDigital
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpNiveisOrObjFed);
            this.Name = "ControlObjetoDigital";
            this.Size = new System.Drawing.Size(675, 490);
            this.grpNiveisOrObjFed.ResumeLayout(false);
            this.grpODTitleAndPub.ResumeLayout(false);
            this.grpODTitleAndPub.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.grpObjectosDigitaisComponentes.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpNiveisOrObjFed;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private FicheirosOrderManager FicheirosOrderManager1;
        private DocumentoSimplesOrderManager DocumentoSimplesOrderManager1;
        private System.Windows.Forms.GroupBox grpObjectosDigitaisComponentes;
        private ControlFedoraPdfViewer previewControl;
        private System.Windows.Forms.ToolTip CurrentToolTip;
        internal System.Windows.Forms.GroupBox grpODTitleAndPub;
        private System.Windows.Forms.CheckBox chkKeepODComposto;
        private System.Windows.Forms.TextBox txtTitulo;
        private System.Windows.Forms.Label lblTitulo;
        internal System.Windows.Forms.CheckBox chkPublicar;
        private ControlObjectoDigitalVersao versionControl;
    }
}
