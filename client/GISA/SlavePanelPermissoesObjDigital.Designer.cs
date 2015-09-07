namespace GISA
{
    partial class SlavePanelPermissoesObjDigital
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
            this.PanelMensagem1 = new GISA.PanelMensagem();
            this.grpObjDigitais = new System.Windows.Forms.GroupBox();
            this.permissoesObjetoDigitalList1 = new GISA.PermissoesObjetoDigitalList();
            this.pnlToolbarPadding.SuspendLayout();
            this.grpObjDigitais.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblFuncao
            // 
            this.lblFuncao.Location = new System.Drawing.Point(0, 0);
            this.lblFuncao.Text = "Definir Permissões";
            // 
            // pnlToolbarPadding
            // 
            this.pnlToolbarPadding.Location = new System.Drawing.Point(0, 24);
            // 
            // PanelMensagem1
            // 
            this.PanelMensagem1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PanelMensagem1.BackColor = System.Drawing.SystemColors.Control;
            this.PanelMensagem1.IsLoaded = false;
            this.PanelMensagem1.IsPopulated = false;
            this.PanelMensagem1.Location = new System.Drawing.Point(0, 24);
            this.PanelMensagem1.Name = "PanelMensagem1";
            this.PanelMensagem1.Size = new System.Drawing.Size(600, 312);
            this.PanelMensagem1.TabIndex = 24;
            this.PanelMensagem1.TbBAuxListEventAssigned = false;
            this.PanelMensagem1.TheGenericDelegate = null;
            this.PanelMensagem1.Visible = false;
            // 
            // grpObjDigitais
            // 
            this.grpObjDigitais.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpObjDigitais.Controls.Add(this.permissoesObjetoDigitalList1);
            this.grpObjDigitais.Location = new System.Drawing.Point(0, 27);
            this.grpObjDigitais.Name = "grpObjDigitais";
            this.grpObjDigitais.Size = new System.Drawing.Size(600, 309);
            this.grpObjDigitais.TabIndex = 26;
            this.grpObjDigitais.TabStop = false;
            this.grpObjDigitais.Text = "Objetos digitais";
            // 
            // permissoesObjetoDigitalList1
            // 
            this.permissoesObjetoDigitalList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.permissoesObjetoDigitalList1.Location = new System.Drawing.Point(3, 16);
            this.permissoesObjetoDigitalList1.Name = "permissoesObjetoDigitalList1";
            this.permissoesObjetoDigitalList1.Size = new System.Drawing.Size(594, 290);
            this.permissoesObjetoDigitalList1.TabIndex = 0;
            // 
            // SlavePanelPermissoesObjDigital
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpObjDigitais);
            this.Controls.Add(this.PanelMensagem1);
            this.Name = "SlavePanelPermissoesObjDigital";
            this.Size = new System.Drawing.Size(600, 336);
            this.Controls.SetChildIndex(this.lblFuncao, 0);
            this.Controls.SetChildIndex(this.pnlToolbarPadding, 0);
            this.Controls.SetChildIndex(this.PanelMensagem1, 0);
            this.Controls.SetChildIndex(this.grpObjDigitais, 0);
            this.pnlToolbarPadding.ResumeLayout(false);
            this.grpObjDigitais.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        internal GISA.PanelMensagem PanelMensagem1;
        private System.Windows.Forms.GroupBox grpObjDigitais;
        private PermissoesObjetoDigitalList permissoesObjetoDigitalList1;
    }
}
