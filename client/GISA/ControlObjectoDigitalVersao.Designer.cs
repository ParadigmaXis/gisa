namespace GISA
{
    partial class ControlObjectoDigitalVersao
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
            this.components = new System.ComponentModel.Container();
            this.versionBox = new System.Windows.Forms.GroupBox();
            this.versionTimestampLabel = new System.Windows.Forms.Label();
            this.versionBar = new System.Windows.Forms.TrackBar();
            this.CurrentToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.versionBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.versionBar)).BeginInit();
            this.SuspendLayout();
            // 
            // versionBox
            // 
            this.versionBox.Controls.Add(this.versionTimestampLabel);
            this.versionBox.Controls.Add(this.versionBar);
            this.versionBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.versionBox.Location = new System.Drawing.Point(0, 0);
            this.versionBox.Name = "versionBox";
            this.versionBox.Size = new System.Drawing.Size(330, 62);
            this.versionBox.TabIndex = 15;
            this.versionBox.TabStop = false;
            this.versionBox.Text = "Versões";
            // 
            // versionTimestampLabel
            // 
            this.versionTimestampLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.versionTimestampLabel.BackColor = System.Drawing.SystemColors.Control;
            this.versionTimestampLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.versionTimestampLabel.Location = new System.Drawing.Point(75, 0);
            this.versionTimestampLabel.Name = "versionTimestampLabel";
            this.versionTimestampLabel.Size = new System.Drawing.Size(249, 16);
            this.versionTimestampLabel.TabIndex = 15;
            this.versionTimestampLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // versionBar
            // 
            this.versionBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.versionBar.AutoSize = false;
            this.versionBar.LargeChange = 1;
            this.versionBar.Location = new System.Drawing.Point(6, 19);
            this.versionBar.Maximum = 0;
            this.versionBar.Name = "versionBar";
            this.versionBar.Size = new System.Drawing.Size(315, 31);
            this.versionBar.TabIndex = 13;
            this.versionBar.ValueChanged += new System.EventHandler(this.versionBar_ValueChanged);
            this.versionBar.MouseCaptureChanged += new System.EventHandler(this.versionBar_MouseCaptureChanged);
            // 
            // ControlObjectoDigitalVersao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.versionBox);
            this.MaximumSize = new System.Drawing.Size(9999, 62);
            this.MinimumSize = new System.Drawing.Size(330, 62);
            this.Name = "ControlObjectoDigitalVersao";
            this.Size = new System.Drawing.Size(330, 62);
            this.versionBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.versionBar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox versionBox;
        private System.Windows.Forms.Label versionTimestampLabel;
        private System.Windows.Forms.TrackBar versionBar;
        private System.Windows.Forms.ToolTip CurrentToolTip;
    }
}
