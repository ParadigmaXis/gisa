using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace GISA.GUIHelper
{
	public class FormDeletionReport : FormReport
	{

	#region  Windows Form Designer generated code 

		public FormDeletionReport() : base()
		{
			InitializeComponent();
		}

		//Form overrides dispose to clean up the component list.
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		//Required by the Windows Form Designer
		private System.ComponentModel.IContainer components = null;

		//NOTE: The following procedure is required by the Windows Form Designer
		//It can be modified using the Windows Form Designer.  
		//Do not modify it using the code editor.
		internal System.Windows.Forms.Label lblQuestion;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
            this.lblQuestion = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Panel1
            // 
            this.Panel1.Controls.Add(this.lblQuestion);
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
            // FormDeletionReport
            // 
            this.Name = "FormDeletionReport";
            this.Text = "Remoção de item(s)";
            this.ResumeLayout(false);

		}

	#endregion

		public string Interrogacao
		{
			get { return lblQuestion.Text; }
			set { lblQuestion.Text = value; }
		}
	}
}