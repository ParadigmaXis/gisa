namespace GISA.IntGestDoc.UserInterface
{
    partial class FormIntergationReport
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
            this.Panel1 = new System.Windows.Forms.Panel();
            this.btnDetails = new System.Windows.Forms.Button();
            this.lblQuestion = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.Panel2 = new System.Windows.Forms.Panel();
            this.txtReport = new System.Windows.Forms.TextBox();
            this.Panel1.SuspendLayout();
            this.Panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // Panel1
            // 
            this.Panel1.Controls.Add(this.btnDetails);
            this.Panel1.Controls.Add(this.lblQuestion);
            this.Panel1.Controls.Add(this.btnOk);
            this.Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Panel1.Location = new System.Drawing.Point(5, 5);
            this.Panel1.Name = "Panel1";
            this.Panel1.Size = new System.Drawing.Size(440, 0);
            this.Panel1.TabIndex = 5;
            // 
            // btnDetails
            // 
            this.btnDetails.Location = new System.Drawing.Point(8, 63);
            this.btnDetails.Name = "btnDetails";
            this.btnDetails.Size = new System.Drawing.Size(88, 23);
            this.btnDetails.TabIndex = 1;
            this.btnDetails.Text = "Detalhes";
            // 
            // lblQuestion
            // 
            this.lblQuestion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblQuestion.Location = new System.Drawing.Point(40, 10);
            this.lblQuestion.Name = "lblQuestion";
            this.lblQuestion.Size = new System.Drawing.Size(392, 54);
            this.lblQuestion.TabIndex = 6;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(344, 63);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(88, 23);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "Confirmar";
            // 
            // Panel2
            // 
            this.Panel2.Controls.Add(this.txtReport);
            this.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.Panel2.Location = new System.Drawing.Point(5, -66);
            this.Panel2.Name = "Panel2";
            this.Panel2.Size = new System.Drawing.Size(440, 160);
            this.Panel2.TabIndex = 6;
            this.Panel2.Visible = false;
            // 
            // txtReport
            // 
            this.txtReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtReport.Location = new System.Drawing.Point(0, 0);
            this.txtReport.MaxLength = 99999999;
            this.txtReport.Multiline = true;
            this.txtReport.Name = "txtReport";
            this.txtReport.ReadOnly = true;
            this.txtReport.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtReport.Size = new System.Drawing.Size(440, 160);
            this.txtReport.TabIndex = 3;
            // 
            // FormIntergationReport
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(450, 99);
            this.ControlBox = false;
            this.Controls.Add(this.Panel1);
            this.Controls.Add(this.Panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormIntergationReport";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Relatório de integração";
            this.Panel1.ResumeLayout(false);
            this.Panel2.ResumeLayout(false);
            this.Panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        internal System.Windows.Forms.Panel Panel1;
        internal System.Windows.Forms.Button btnDetails;
        internal System.Windows.Forms.Label lblQuestion;
        internal System.Windows.Forms.Button btnOk;
        internal System.Windows.Forms.Panel Panel2;
        internal System.Windows.Forms.TextBox txtReport;

        #endregion
    }
}