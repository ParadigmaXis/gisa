namespace GISA.Controls.Nivel
{
    partial class FormNivelDocumentalFedora : FormAddNivel
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
            this.grpTipologia = new System.Windows.Forms.GroupBox();
            this.ButtonTI = new System.Windows.Forms.Button();
            this.txtTipologia = new System.Windows.Forms.TextBox();
            this.CurrentToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.grpTitulo.SuspendLayout();
            this.grpCodigo.SuspendLayout();
            this.grpTipologia.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtCodigo
            // 
            this.txtCodigo.Location = new System.Drawing.Point(6, 20);
            this.txtCodigo.TabIndex = 0;
            // 
            // grpTitulo
            // 
            this.grpTitulo.TabIndex = 1;
            // 
            // txtDesignacao
            // 
            this.txtDesignacao.MaxLength = 255;
            this.txtDesignacao.TabIndex = 0;
            // 
            // grpCodigo
            // 
            this.grpCodigo.Size = new System.Drawing.Size(360, 48);
            this.grpCodigo.TabIndex = 0;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(456, 158);
            this.btnCancel.TabIndex = 4;
            // 
            // btnAccept
            // 
            this.btnAccept.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAccept.Location = new System.Drawing.Point(376, 158);
            this.btnAccept.TabIndex = 3;
            // 
            // grpTipologia
            // 
            this.grpTipologia.Controls.Add(this.ButtonTI);
            this.grpTipologia.Controls.Add(this.txtTipologia);
            this.grpTipologia.Location = new System.Drawing.Point(5, 101);
            this.grpTipologia.Name = "grpTipologia";
            this.grpTipologia.Size = new System.Drawing.Size(559, 43);
            this.grpTipologia.TabIndex = 2;
            this.grpTipologia.TabStop = false;
            this.grpTipologia.Text = "Tipologia";
            // 
            // ButtonTI
            // 
            this.ButtonTI.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonTI.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ButtonTI.ImageIndex = 1;
            this.ButtonTI.Location = new System.Drawing.Point(529, 16);
            this.ButtonTI.Name = "ButtonTI";
            this.ButtonTI.Size = new System.Drawing.Size(24, 20);
            this.ButtonTI.TabIndex = 1;
            this.ButtonTI.Click += new System.EventHandler(this.ButtonTI_Click);
            // 
            // txtTipologia
            // 
            this.txtTipologia.Location = new System.Drawing.Point(6, 16);
            this.txtTipologia.Name = "txtTipologia";
            this.txtTipologia.ReadOnly = true;
            this.txtTipologia.Size = new System.Drawing.Size(515, 20);
            this.txtTipologia.TabIndex = 0;
            this.txtTipologia.TextChanged += new System.EventHandler(this.txtTipologia_TextChanged);
            // 
            // CurrentToolTip
            // 
            this.CurrentToolTip.ShowAlways = true;
            // 
            // FormNivelDocumentalFedora
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(568, 193);
            this.ControlBox = false;
            this.Controls.Add(this.grpTipologia);
            this.Name = "FormNivelDocumentalFedora";
            this.Text = "FormNivelDocumentalFedora";
            this.Controls.SetChildIndex(this.grpTipologia, 0);
            this.Controls.SetChildIndex(this.btnAccept, 0);
            this.Controls.SetChildIndex(this.btnCancel, 0);
            this.Controls.SetChildIndex(this.grpCodigo, 0);
            this.Controls.SetChildIndex(this.grpTitulo, 0);
            this.grpTitulo.ResumeLayout(false);
            this.grpTitulo.PerformLayout();
            this.grpCodigo.ResumeLayout(false);
            this.grpCodigo.PerformLayout();
            this.grpTipologia.ResumeLayout(false);
            this.grpTipologia.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpTipologia;
        private System.Windows.Forms.TextBox txtTipologia;
        internal System.Windows.Forms.Button ButtonTI;
        protected internal System.Windows.Forms.ToolTip CurrentToolTip;
    }
}