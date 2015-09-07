namespace GISA
{
    partial class FicheirosOrderManager : ListViewOrderManager
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
            this.btnRemover = new System.Windows.Forms.Button();
            this.btnAdicionar = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnAdicionar);
            this.groupBox1.Controls.Add(this.btnRemover);
            this.groupBox1.Text = "Ficheiros";
            this.groupBox1.Controls.SetChildIndex(this.lstVw, 0);
            this.groupBox1.Controls.SetChildIndex(this.btnRemover, 0);
            this.groupBox1.Controls.SetChildIndex(this.btnAdicionar, 0);
            // 
            // lstVw
            // 
            this.lstVw.SelectedIndexChanged += new System.EventHandler(this.lstVw_SelectedIndexChanged);
            this.lstVw.DragDrop += new System.Windows.Forms.DragEventHandler(this.lstVw_DragDrop);
            this.lstVw.DragEnter += new System.Windows.Forms.DragEventHandler(this.lstVw_DragEnter);
            // 
            // btnRemover
            // 
            this.btnRemover.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemover.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRemover.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnRemover.Location = new System.Drawing.Point(407, 79);
            this.btnRemover.Name = "btnRemover";
            this.btnRemover.Size = new System.Drawing.Size(26, 26);
            this.btnRemover.TabIndex = 18;
            this.btnRemover.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnAdicionar
            // 
            this.btnAdicionar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdicionar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAdicionar.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnAdicionar.Location = new System.Drawing.Point(407, 47);
            this.btnAdicionar.Name = "btnAdicionar";
            this.btnAdicionar.Size = new System.Drawing.Size(26, 26);
            this.btnAdicionar.TabIndex = 19;
            this.btnAdicionar.Click += new System.EventHandler(this.button1_Click);
            // 
            // FicheirosOrderManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "FicheirosOrderManager";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.Button btnRemover;
        internal System.Windows.Forms.Button btnAdicionar;
    }
}
