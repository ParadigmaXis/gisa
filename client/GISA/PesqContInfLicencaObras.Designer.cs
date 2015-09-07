namespace GISA {
    partial class PesqContInfLicencaObras {
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
            this.txtTipoDeObra = new System.Windows.Forms.TextBox();
            this.lblTipoObra = new System.Windows.Forms.Label();
            this.btnLocalizacaoDeObra = new System.Windows.Forms.Button();
            this.txtRequerente = new System.Windows.Forms.TextBox();
            this.lblRequerente = new System.Windows.Forms.Label();
            this.txtLocalizacao = new System.Windows.Forms.TextBox();
            this.lblLocalizacao = new System.Windows.Forms.Label();
            this.btnTecnicoDeObra = new System.Windows.Forms.Button();
            this.txtTecnicoDeObra = new System.Windows.Forms.TextBox();
            this.lblTecnicoObra = new System.Windows.Forms.Label();
            this.txtAtestadoHab = new System.Windows.Forms.TextBox();
            this.lblAtestadoHab = new System.Windows.Forms.Label();
            this.chkPH = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cdbDataInicio = new GISA.Controls.PxCompleteDateBox();
            this.lblDataProducaoInicio = new System.Windows.Forms.Label();
            this.lblDataProducaoFim = new System.Windows.Forms.Label();
            this.cdbDataFim = new GISA.Controls.PxCompleteDateBox();
            this.lbl_NumPolicia = new System.Windows.Forms.Label();
            this.txtNumPolicia = new System.Windows.Forms.TextBox();
            this.lblLocalizacaoAntiga = new System.Windows.Forms.Label();
            this.txtBoxLocalizacaoAntiga = new System.Windows.Forms.TextBox();
            this.lblNumPoliciaAntigo = new System.Windows.Forms.Label();
            this.txtBoxNumPoliciaAntigo = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtTipoDeObra
            // 
            this.txtTipoDeObra.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTipoDeObra.Location = new System.Drawing.Point(156, 160);
            this.txtTipoDeObra.Name = "txtTipoDeObra";
            this.txtTipoDeObra.Size = new System.Drawing.Size(572, 20);
            this.txtTipoDeObra.TabIndex = 26;
            this.txtTipoDeObra.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PesqContInfLicencaObras_KeyDown);
            // 
            // lblTipoObra
            // 
            this.lblTipoObra.Location = new System.Drawing.Point(4, 157);
            this.lblTipoObra.Name = "lblTipoObra";
            this.lblTipoObra.Size = new System.Drawing.Size(128, 24);
            this.lblTipoObra.TabIndex = 28;
            this.lblTipoObra.Text = "Tipo de obra:";
            this.lblTipoObra.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnLocalizacaoDeObra
            // 
            this.btnLocalizacaoDeObra.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLocalizacaoDeObra.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.btnLocalizacaoDeObra.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnLocalizacaoDeObra.ImageIndex = 1;
            this.btnLocalizacaoDeObra.Location = new System.Drawing.Point(735, 34);
            this.btnLocalizacaoDeObra.Name = "btnLocalizacaoDeObra";
            this.btnLocalizacaoDeObra.Size = new System.Drawing.Size(24, 20);
            this.btnLocalizacaoDeObra.TabIndex = 25;
            this.btnLocalizacaoDeObra.Click += new System.EventHandler(this.btnLocalizacaoDeObra_Click);
            // 
            // txtRequerente
            // 
            this.txtRequerente.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRequerente.Location = new System.Drawing.Point(156, 7);
            this.txtRequerente.Name = "txtRequerente";
            this.txtRequerente.Size = new System.Drawing.Size(572, 20);
            this.txtRequerente.TabIndex = 22;
            this.txtRequerente.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PesqContInfLicencaObras_KeyDown);
            // 
            // lblRequerente
            // 
            this.lblRequerente.Location = new System.Drawing.Point(4, 6);
            this.lblRequerente.Name = "lblRequerente";
            this.lblRequerente.Size = new System.Drawing.Size(128, 24);
            this.lblRequerente.TabIndex = 27;
            this.lblRequerente.Text = "Requerente:";
            this.lblRequerente.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtLocalizacao
            // 
            this.txtLocalizacao.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLocalizacao.Location = new System.Drawing.Point(156, 38);
            this.txtLocalizacao.Name = "txtLocalizacao";
            this.txtLocalizacao.Size = new System.Drawing.Size(573, 20);
            this.txtLocalizacao.TabIndex = 24;
            this.txtLocalizacao.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PesqContInfLicencaObras_KeyDown);
            // 
            // lblLocalizacao
            // 
            this.lblLocalizacao.Location = new System.Drawing.Point(4, 33);
            this.lblLocalizacao.Name = "lblLocalizacao";
            this.lblLocalizacao.Size = new System.Drawing.Size(146, 24);
            this.lblLocalizacao.TabIndex = 21;
            this.lblLocalizacao.Text = "Localização da obra (atual):";
            this.lblLocalizacao.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnTecnicoDeObra
            // 
            this.btnTecnicoDeObra.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTecnicoDeObra.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnTecnicoDeObra.ImageIndex = 1;
            this.btnTecnicoDeObra.Location = new System.Drawing.Point(735, 187);
            this.btnTecnicoDeObra.Name = "btnTecnicoDeObra";
            this.btnTecnicoDeObra.Size = new System.Drawing.Size(24, 20);
            this.btnTecnicoDeObra.TabIndex = 31;
            this.btnTecnicoDeObra.Click += new System.EventHandler(this.btnTecnicoDeObra_Click);
            // 
            // txtTecnicoDeObra
            // 
            this.txtTecnicoDeObra.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTecnicoDeObra.Location = new System.Drawing.Point(156, 186);
            this.txtTecnicoDeObra.Name = "txtTecnicoDeObra";
            this.txtTecnicoDeObra.Size = new System.Drawing.Size(573, 20);
            this.txtTecnicoDeObra.TabIndex = 30;
            this.txtTecnicoDeObra.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PesqContInfLicencaObras_KeyDown);
            // 
            // lblTecnicoObra
            // 
            this.lblTecnicoObra.Location = new System.Drawing.Point(4, 183);
            this.lblTecnicoObra.Name = "lblTecnicoObra";
            this.lblTecnicoObra.Size = new System.Drawing.Size(128, 24);
            this.lblTecnicoObra.TabIndex = 29;
            this.lblTecnicoObra.Text = "Técnico de obra:";
            this.lblTecnicoObra.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtAtestadoHab
            // 
            this.txtAtestadoHab.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAtestadoHab.Location = new System.Drawing.Point(156, 212);
            this.txtAtestadoHab.Name = "txtAtestadoHab";
            this.txtAtestadoHab.Size = new System.Drawing.Size(572, 20);
            this.txtAtestadoHab.TabIndex = 33;
            this.txtAtestadoHab.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PesqContInfLicencaObras_KeyDown);
            // 
            // lblAtestadoHab
            // 
            this.lblAtestadoHab.Location = new System.Drawing.Point(4, 209);
            this.lblAtestadoHab.Name = "lblAtestadoHab";
            this.lblAtestadoHab.Size = new System.Drawing.Size(147, 24);
            this.lblAtestadoHab.TabIndex = 32;
            this.lblAtestadoHab.Text = "Atestado de habitabilidade:";
            this.lblAtestadoHab.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkPH
            // 
            this.chkPH.Location = new System.Drawing.Point(544, 259);
            this.chkPH.Name = "chkPH";
            this.chkPH.Size = new System.Drawing.Size(184, 21);
            this.chkPH.TabIndex = 34;
            this.chkPH.Text = "Existe propriedade horizontal";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cdbDataInicio);
            this.groupBox1.Controls.Add(this.lblDataProducaoInicio);
            this.groupBox1.Controls.Add(this.lblDataProducaoFim);
            this.groupBox1.Controls.Add(this.cdbDataFim);
            this.groupBox1.Location = new System.Drawing.Point(7, 240);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(491, 54);
            this.groupBox1.TabIndex = 57;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Data da licença de construção";
            // 
            // cdbDataInicio
            // 
            this.cdbDataInicio.Checked = false;
            this.cdbDataInicio.Day = 1;
            this.cdbDataInicio.Location = new System.Drawing.Point(47, 19);
            this.cdbDataInicio.Month = 1;
            this.cdbDataInicio.Name = "cdbDataInicio";
            this.cdbDataInicio.Size = new System.Drawing.Size(166, 22);
            this.cdbDataInicio.TabIndex = 7;
            this.cdbDataInicio.Year = 1;
            // 
            // lblDataProducaoInicio
            // 
            this.lblDataProducaoInicio.Location = new System.Drawing.Point(11, 19);
            this.lblDataProducaoInicio.Name = "lblDataProducaoInicio";
            this.lblDataProducaoInicio.Size = new System.Drawing.Size(44, 24);
            this.lblDataProducaoInicio.TabIndex = 12;
            this.lblDataProducaoInicio.Text = "entre";
            this.lblDataProducaoInicio.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDataProducaoFim
            // 
            this.lblDataProducaoFim.Location = new System.Drawing.Point(243, 19);
            this.lblDataProducaoFim.Name = "lblDataProducaoFim";
            this.lblDataProducaoFim.Size = new System.Drawing.Size(16, 24);
            this.lblDataProducaoFim.TabIndex = 14;
            this.lblDataProducaoFim.Text = "e";
            this.lblDataProducaoFim.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cdbDataFim
            // 
            this.cdbDataFim.Checked = false;
            this.cdbDataFim.Day = 1;
            this.cdbDataFim.Location = new System.Drawing.Point(277, 18);
            this.cdbDataFim.Month = 1;
            this.cdbDataFim.Name = "cdbDataFim";
            this.cdbDataFim.Size = new System.Drawing.Size(166, 22);
            this.cdbDataFim.TabIndex = 8;
            this.cdbDataFim.Year = 1;
            // 
            // lbl_NumPolicia
            // 
            this.lbl_NumPolicia.Location = new System.Drawing.Point(4, 59);
            this.lbl_NumPolicia.Name = "lbl_NumPolicia";
            this.lbl_NumPolicia.Size = new System.Drawing.Size(146, 24);
            this.lbl_NumPolicia.TabIndex = 58;
            this.lbl_NumPolicia.Text = "Número de polícia (atual):";
            this.lbl_NumPolicia.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtNumPolicia
            // 
            this.txtNumPolicia.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNumPolicia.Location = new System.Drawing.Point(156, 62);
            this.txtNumPolicia.Name = "txtNumPolicia";
            this.txtNumPolicia.Size = new System.Drawing.Size(342, 20);
            this.txtNumPolicia.TabIndex = 59;
            this.txtNumPolicia.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PesqContInfLicencaObras_KeyDown);
            // 
            // lblLocalizacaoAntiga
            // 
            this.lblLocalizacaoAntiga.Location = new System.Drawing.Point(4, 95);
            this.lblLocalizacaoAntiga.Name = "lblLocalizacaoAntiga";
            this.lblLocalizacaoAntiga.Size = new System.Drawing.Size(146, 24);
            this.lblLocalizacaoAntiga.TabIndex = 60;
            this.lblLocalizacaoAntiga.Text = "Localização da obra (antiga):";
            this.lblLocalizacaoAntiga.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtBoxLocalizacaoAntiga
            // 
            this.txtBoxLocalizacaoAntiga.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBoxLocalizacaoAntiga.Location = new System.Drawing.Point(156, 98);
            this.txtBoxLocalizacaoAntiga.Name = "txtBoxLocalizacaoAntiga";
            this.txtBoxLocalizacaoAntiga.Size = new System.Drawing.Size(573, 20);
            this.txtBoxLocalizacaoAntiga.TabIndex = 61;
            this.txtBoxLocalizacaoAntiga.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PesqContInfLicencaObras_KeyDown);
            // 
            // lblNumPoliciaAntigo
            // 
            this.lblNumPoliciaAntigo.Location = new System.Drawing.Point(4, 118);
            this.lblNumPoliciaAntigo.Name = "lblNumPoliciaAntigo";
            this.lblNumPoliciaAntigo.Size = new System.Drawing.Size(146, 24);
            this.lblNumPoliciaAntigo.TabIndex = 62;
            this.lblNumPoliciaAntigo.Text = "Número de polícia (antigo):";
            this.lblNumPoliciaAntigo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtBoxNumPoliciaAntigo
            // 
            this.txtBoxNumPoliciaAntigo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBoxNumPoliciaAntigo.Location = new System.Drawing.Point(156, 123);
            this.txtBoxNumPoliciaAntigo.Name = "txtBoxNumPoliciaAntigo";
            this.txtBoxNumPoliciaAntigo.Size = new System.Drawing.Size(342, 20);
            this.txtBoxNumPoliciaAntigo.TabIndex = 63;
            this.txtBoxNumPoliciaAntigo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PesqContInfLicencaObras_KeyDown);
            // 
            // PesqContInfLicencaObras
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtBoxNumPoliciaAntigo);
            this.Controls.Add(this.lblNumPoliciaAntigo);
            this.Controls.Add(this.txtBoxLocalizacaoAntiga);
            this.Controls.Add(this.lblLocalizacaoAntiga);
            this.Controls.Add(this.txtNumPolicia);
            this.Controls.Add(this.lbl_NumPolicia);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.chkPH);
            this.Controls.Add(this.txtAtestadoHab);
            this.Controls.Add(this.lblAtestadoHab);
            this.Controls.Add(this.btnTecnicoDeObra);
            this.Controls.Add(this.txtTecnicoDeObra);
            this.Controls.Add(this.lblTecnicoObra);
            this.Controls.Add(this.txtTipoDeObra);
            this.Controls.Add(this.lblTipoObra);
            this.Controls.Add(this.btnLocalizacaoDeObra);
            this.Controls.Add(this.txtRequerente);
            this.Controls.Add(this.lblRequerente);
            this.Controls.Add(this.txtLocalizacao);
            this.Controls.Add(this.lblLocalizacao);
            this.Name = "PesqContInfLicencaObras";
            this.Size = new System.Drawing.Size(821, 305);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TextBox txtTipoDeObra;
        internal System.Windows.Forms.Label lblTipoObra;
        internal System.Windows.Forms.Button btnLocalizacaoDeObra;
        internal System.Windows.Forms.TextBox txtRequerente;
        internal System.Windows.Forms.Label lblRequerente;
        internal System.Windows.Forms.TextBox txtLocalizacao;
        internal System.Windows.Forms.Label lblLocalizacao;
        internal System.Windows.Forms.Button btnTecnicoDeObra;
        internal System.Windows.Forms.TextBox txtTecnicoDeObra;
        internal System.Windows.Forms.Label lblTecnicoObra;
        internal System.Windows.Forms.TextBox txtAtestadoHab;
        internal System.Windows.Forms.Label lblAtestadoHab;
        internal System.Windows.Forms.CheckBox chkPH;
        private System.Windows.Forms.GroupBox groupBox1;
        internal GISA.Controls.PxCompleteDateBox cdbDataInicio;
        internal System.Windows.Forms.Label lblDataProducaoInicio;
        internal System.Windows.Forms.Label lblDataProducaoFim;
        internal GISA.Controls.PxCompleteDateBox cdbDataFim;
        internal System.Windows.Forms.Label lbl_NumPolicia;
        internal System.Windows.Forms.TextBox txtNumPolicia;
        internal System.Windows.Forms.Label lblLocalizacaoAntiga;
        internal System.Windows.Forms.TextBox txtBoxLocalizacaoAntiga;
        internal System.Windows.Forms.Label lblNumPoliciaAntigo;
        internal System.Windows.Forms.TextBox txtBoxNumPoliciaAntigo;
    }
}
