namespace GISA {
    partial class FormLeituraLocalizacaoNumPolicia {
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblDesignacao = new System.Windows.Forms.Label();
            this.txtDesignacao = new System.Windows.Forms.TextBox();
            this.button_FormPickControloAut = new System.Windows.Forms.Button();
            this.lblNumPolicia = new System.Windows.Forms.Label();
            this.txtNumPolicia = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(350, 88);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "Ok";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(431, 88);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancelar";
            // 
            // lblDesignacao
            // 
            this.lblDesignacao.AutoSize = true;
            this.lblDesignacao.Location = new System.Drawing.Point(19, 24);
            this.lblDesignacao.Name = "lblDesignacao";
            this.lblDesignacao.Size = new System.Drawing.Size(64, 13);
            this.lblDesignacao.TabIndex = 5;
            this.lblDesignacao.Text = "Designação";
            // 
            // txtDesignacao
            // 
            this.txtDesignacao.Location = new System.Drawing.Point(121, 21);
            this.txtDesignacao.Name = "txtDesignacao";
            this.txtDesignacao.Size = new System.Drawing.Size(304, 20);
            this.txtDesignacao.TabIndex = 6;
            // 
            // button_FormPickControloAut
            // 
            this.button_FormPickControloAut.Location = new System.Drawing.Point(431, 21);
            this.button_FormPickControloAut.Name = "button_FormPickControloAut";
            this.button_FormPickControloAut.Size = new System.Drawing.Size(75, 20);
            this.button_FormPickControloAut.TabIndex = 7;
            this.button_FormPickControloAut.Text = "...";
            this.button_FormPickControloAut.UseVisualStyleBackColor = true;
            this.button_FormPickControloAut.Click += new System.EventHandler(this.button_FormPickControloAut_Click);
            // 
            // lblNumPolicia
            // 
            this.lblNumPolicia.AutoSize = true;
            this.lblNumPolicia.Location = new System.Drawing.Point(19, 59);
            this.lblNumPolicia.Name = "lblNumPolicia";
            this.lblNumPolicia.Size = new System.Drawing.Size(94, 13);
            this.lblNumPolicia.TabIndex = 8;
            this.lblNumPolicia.Text = "Número de polícia";
            // 
            // txtNumPolicia
            // 
            this.txtNumPolicia.Location = new System.Drawing.Point(121, 59);
            this.txtNumPolicia.Name = "txtNumPolicia";
            this.txtNumPolicia.Size = new System.Drawing.Size(118, 20);
            this.txtNumPolicia.TabIndex = 9;
            // 
            // FormLeituraLocalizacaoNumPolicia
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(518, 123);
            this.Controls.Add(this.txtNumPolicia);
            this.Controls.Add(this.lblNumPolicia);
            this.Controls.Add(this.button_FormPickControloAut);
            this.Controls.Add(this.txtDesignacao);
            this.Controls.Add(this.lblDesignacao);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormLeituraLocalizacaoNumPolicia";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button btnOk;
        internal System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblDesignacao;
        private System.Windows.Forms.TextBox txtDesignacao;
        private System.Windows.Forms.Button button_FormPickControloAut;
        private System.Windows.Forms.Label lblNumPolicia;
        private System.Windows.Forms.TextBox txtNumPolicia;
    }
}