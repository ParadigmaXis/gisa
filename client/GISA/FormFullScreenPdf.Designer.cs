namespace GISA
{
    partial class FormFullScreenPdf
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lstView = new GISA.Controls.PxListView();
            this.colDesignation = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.previewControl = new GISA.ControlFedoraPdfViewer();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lstView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.previewControl);
            this.splitContainer1.Size = new System.Drawing.Size(708, 477);
            this.splitContainer1.SplitterDistance = 166;
            this.splitContainer1.TabIndex = 3;
            // 
            // lstView
            // 
            this.lstView.AllowColumnReorder = true;
            this.lstView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colDesignation});
            this.lstView.CustomizedSorting = false;
            this.lstView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstView.FullRowSelect = true;
            this.lstView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstView.HideSelection = false;
            this.lstView.Location = new System.Drawing.Point(0, 0);
            this.lstView.MultiSelect = false;
            this.lstView.Name = "lstView";
            this.lstView.ReturnSubItemIndex = false;
            this.lstView.Size = new System.Drawing.Size(166, 477);
            this.lstView.TabIndex = 1;
            this.lstView.UseCompatibleStateImageBehavior = false;
            this.lstView.View = System.Windows.Forms.View.Details;
            this.lstView.SelectedIndexChanged += new System.EventHandler(this.lstView_SelectedIndexChanged);
            // 
            // colDesignation
            // 
            this.colDesignation.Text = "Designação";
            this.colDesignation.Width = 240;
            // 
            // previewControl
            // 
            this.previewControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.previewControl.Location = new System.Drawing.Point(0, 0);
            this.previewControl.Name = "previewControl";
            this.previewControl.Padding = new System.Windows.Forms.Padding(3);
            this.previewControl.Size = new System.Drawing.Size(538, 477);
            this.previewControl.TabIndex = 0;
            // 
            // FormFullScreenPdf
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(708, 477);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormFullScreenPdf";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Visualizador de Objetos Digitais";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormFullScreenPdf_FormClosed);
            this.Load += new System.EventHandler(this.FormFullScreenPdf_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormFullScreenPdf_KeyDown);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ColumnHeader colDesignation;
        protected GISA.Controls.PxListView lstView;
        private System.Windows.Forms.ToolTip toolTip;
        private ControlFedoraPdfViewer previewControl;
    }
}