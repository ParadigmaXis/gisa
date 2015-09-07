using GISA.Controls.Nivel;

namespace GISA
{
    partial class MasterPanelDocumentosRequisitados
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MasterPanelDocumentosRequisitados));
            this.NivelDocumentalList1 = new NivelDocumentalList();
            this.tbFiltro = new System.Windows.Forms.ToolBarButton();
            this.ilIcons = new System.Windows.Forms.ImageList(this.components);
            this.pnlToolbarPadding.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblFuncao
            // 
            this.lblFuncao.Location = new System.Drawing.Point(0, 0);
            this.lblFuncao.Text = "Documentos requisitados";
            // 
            // ToolBar
            // 
            this.ToolBar.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.tbFiltro});
            this.ToolBar.ImageList = this.ilIcons;
            // 
            // pnlToolbarPadding
            // 
            this.pnlToolbarPadding.Location = new System.Drawing.Point(0, 24);
            // 
            // NivelDocumentalList1
            // 
            this.NivelDocumentalList1.BackColor = System.Drawing.SystemColors.Control;
            this.NivelDocumentalList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.NivelDocumentalList1.IDMovimento = ((long)(0));
            this.NivelDocumentalList1.Location = new System.Drawing.Point(0, 52);
            this.NivelDocumentalList1.Name = "NivelDocumentalList1";
            this.NivelDocumentalList1.Padding = new System.Windows.Forms.Padding(6, 0, 6, 6);
            this.NivelDocumentalList1.Size = new System.Drawing.Size(600, 228);
            this.NivelDocumentalList1.TabIndex = 5;
            this.NivelDocumentalList1.Visible = false;
            // 
            // tbFiltro
            // 
            this.tbFiltro.ImageIndex = 0;
            this.tbFiltro.Name = "tbFiltro";
            this.tbFiltro.Style = System.Windows.Forms.ToolBarButtonStyle.ToggleButton;
            // 
            // ilIcons
            // 
            this.ilIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilIcons.ImageStream")));
            this.ilIcons.TransparentColor = System.Drawing.Color.Magenta;
            this.ilIcons.Images.SetKeyName(0, "FiltroOn.bmp");
            // 
            // MasterPanelDocumentosRequisitados
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.NivelDocumentalList1);
            this.Name = "MasterPanelDocumentosRequisitados";
            this.Controls.SetChildIndex(this.lblFuncao, 0);
            this.Controls.SetChildIndex(this.pnlToolbarPadding, 0);
            this.Controls.SetChildIndex(this.NivelDocumentalList1, 0);
            this.pnlToolbarPadding.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        protected internal NivelDocumentalList NivelDocumentalList1;
        private System.Windows.Forms.ToolBarButton tbFiltro;
        private System.Windows.Forms.ImageList ilIcons;
    }
}