namespace GISA
{
    partial class MasterPanelDocInPorto
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
            this.controlDocInPorto1 = new GISA.IntGestDoc.UserInterface.DocInPorto.ControlDocInPorto();
            this.pnlToolbarPadding.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblFuncao
            // 
            this.lblFuncao.Location = new System.Drawing.Point(0, 0);
            this.lblFuncao.Size = new System.Drawing.Size(930, 24);
            // 
            // ToolBar
            // 
            this.ToolBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.ToolBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.ToolBar.Location = new System.Drawing.Point(5, 0);
            this.ToolBar.Size = new System.Drawing.Size(920, 0);
            // 
            // pnlToolbarPadding
            // 
            this.pnlToolbarPadding.Location = new System.Drawing.Point(0, 24);
            this.pnlToolbarPadding.Size = new System.Drawing.Size(930, 0);
            // 
            // controlDocInPorto1
            // 
            this.controlDocInPorto1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controlDocInPorto1.Location = new System.Drawing.Point(0, 24);
            this.controlDocInPorto1.Name = "controlDocInPorto1";
            this.controlDocInPorto1.Size = new System.Drawing.Size(930, 577);
            this.controlDocInPorto1.TabIndex = 2;
            // 
            // MasterPanelDocInPorto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.controlDocInPorto1);
            this.Name = "MasterPanelDocInPorto";
            this.Size = new System.Drawing.Size(930, 601);
            this.Controls.SetChildIndex(this.lblFuncao, 0);
            this.Controls.SetChildIndex(this.pnlToolbarPadding, 0);
            this.Controls.SetChildIndex(this.controlDocInPorto1, 0);
            this.pnlToolbarPadding.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private GISA.IntGestDoc.UserInterface.DocInPorto.ControlDocInPorto controlDocInPorto1;


    }
}
