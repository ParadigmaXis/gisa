namespace GISA
{
    partial class MasterPanelStatusObjDigital
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
            this.pxDataGridView1 = new GISA.Controls.PxDataGridView();
            this.ToolBarButtonRefresh = new System.Windows.Forms.ToolBarButton();
            this.ToolBarButtonClearProcessed = new System.Windows.Forms.ToolBarButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.grpPDF = new System.Windows.Forms.GroupBox();
            this.controloLocalizacao1 = new GISA.Controls.Localizacao.ControloLocalizacao();
            this.pnlToolbarPadding.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pxDataGridView1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.grpPDF.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblFuncao
            // 
            this.lblFuncao.Location = new System.Drawing.Point(0, 0);
            this.lblFuncao.Size = new System.Drawing.Size(771, 24);
            // 
            // ToolBar
            // 
            this.ToolBar.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
            this.ToolBarButtonRefresh,
            this.ToolBarButtonClearProcessed});
            this.ToolBar.ImageList = null;
            this.ToolBar.Size = new System.Drawing.Size(1435, 26);
            // 
            // pnlToolbarPadding
            // 
            this.pnlToolbarPadding.Location = new System.Drawing.Point(0, 24);
            this.pnlToolbarPadding.Size = new System.Drawing.Size(771, 28);
            // 
            // pxDataGridView1
            // 
            this.pxDataGridView1.AllowUserToAddRows = false;
            this.pxDataGridView1.AllowUserToDeleteRows = false;
            this.pxDataGridView1.BackgroundColor = System.Drawing.SystemColors.Window;
            this.pxDataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.pxDataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pxDataGridView1.Location = new System.Drawing.Point(3, 16);
            this.pxDataGridView1.MultiSelect = false;
            this.pxDataGridView1.Name = "pxDataGridView1";
            this.pxDataGridView1.ReadOnly = true;
            this.pxDataGridView1.Size = new System.Drawing.Size(758, 230);
            this.pxDataGridView1.SmallImageList = null;
            this.pxDataGridView1.TabIndex = 2;
            this.pxDataGridView1.SelectionChanged += new System.EventHandler(this.pxDataGridView1_SelectionChanged);
            // 
            // ToolBarButtonRefresh
            // 
            this.ToolBarButtonRefresh.Name = "ToolBarButtonRefresh";
            // 
            // ToolBarButtonClearProcessed
            // 
            this.ToolBarButtonClearProcessed.Name = "ToolBarButtonClearProcessed";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(4, 58);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.grpPDF);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.controloLocalizacao1);
            this.splitContainer1.Size = new System.Drawing.Size(764, 499);
            this.splitContainer1.SplitterDistance = 249;
            this.splitContainer1.TabIndex = 3;
            // 
            // grpPDF
            // 
            this.grpPDF.Controls.Add(this.pxDataGridView1);
            this.grpPDF.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpPDF.Location = new System.Drawing.Point(0, 0);
            this.grpPDF.Name = "grpPDF";
            this.grpPDF.Size = new System.Drawing.Size(764, 249);
            this.grpPDF.TabIndex = 3;
            this.grpPDF.TabStop = false;
            this.grpPDF.Text = "Últimos pedidos de geração de PDFs";
            // 
            // controloLocalizacao1
            // 
            this.controloLocalizacao1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.controloLocalizacao1.Location = new System.Drawing.Point(0, 0);
            this.controloLocalizacao1.Name = "controloLocalizacao1";
            this.controloLocalizacao1.Size = new System.Drawing.Size(764, 246);
            this.controloLocalizacao1.TabIndex = 0;
            // 
            // MasterPanelStatusObjDigital
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "MasterPanelStatusObjDigital";
            this.Size = new System.Drawing.Size(771, 560);
            this.Controls.SetChildIndex(this.lblFuncao, 0);
            this.Controls.SetChildIndex(this.pnlToolbarPadding, 0);
            this.Controls.SetChildIndex(this.splitContainer1, 0);
            this.pnlToolbarPadding.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pxDataGridView1)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.grpPDF.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.PxDataGridView pxDataGridView1;
        internal System.Windows.Forms.ToolBarButton ToolBarButtonRefresh;
        internal System.Windows.Forms.ToolBarButton ToolBarButtonClearProcessed;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox grpPDF;
        private Controls.Localizacao.ControloLocalizacao controloLocalizacao1;
    }
}
