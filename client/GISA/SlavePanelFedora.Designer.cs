namespace GISA
{
    partial class SlavePanelFedora : GISA.SinglePanel
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
            this.controlObjetoDigital1 = new GISA.ControlObjetoDigital();
            this.pnlToolbarPadding.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblFuncao
            // 
            this.lblFuncao.Location = new System.Drawing.Point(0, 0);
            this.lblFuncao.Size = new System.Drawing.Size(675, 24);
            // 
            // ToolBar
            // 
            this.ToolBar.Size = new System.Drawing.Size(2530, 26);
            this.ToolBar.Visible = false;
            // 
            // pnlToolbarPadding
            // 
            this.pnlToolbarPadding.Location = new System.Drawing.Point(0, 24);
            this.pnlToolbarPadding.Size = new System.Drawing.Size(675, 28);
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
            this.PanelMensagem1.Size = new System.Drawing.Size(675, 493);
            this.PanelMensagem1.TabIndex = 24;
            this.PanelMensagem1.TbBAuxListEventAssigned = false;
            this.PanelMensagem1.TheGenericDelegate = null;
            this.PanelMensagem1.Visible = false;
            // 
            // controlObjetoDigital1
            // 
            this.controlObjetoDigital1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.controlObjetoDigital1.CurrentAnexo = null;
            this.controlObjetoDigital1.CurrentODComp = null;
            this.controlObjetoDigital1.CurrentODSimples = null;
            this.controlObjetoDigital1.Location = new System.Drawing.Point(0, 24);
            this.controlObjetoDigital1.Name = "controlObjetoDigital1";
            this.controlObjetoDigital1.Size = new System.Drawing.Size(675, 490);
            this.controlObjetoDigital1.TabIndex = 1;
            this.controlObjetoDigital1.Titulo = "";
            // 
            // SlavePanelFedora
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.controlObjetoDigital1);
            this.Controls.Add(this.PanelMensagem1);
            this.Name = "SlavePanelFedora";
            this.Size = new System.Drawing.Size(675, 517);
            this.Controls.SetChildIndex(this.PanelMensagem1, 0);
            this.Controls.SetChildIndex(this.lblFuncao, 0);
            this.Controls.SetChildIndex(this.pnlToolbarPadding, 0);
            this.Controls.SetChildIndex(this.controlObjetoDigital1, 0);
            this.pnlToolbarPadding.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal GISA.PanelMensagem PanelMensagem1;
        private ControlObjetoDigital controlObjetoDigital1;
    }
}
