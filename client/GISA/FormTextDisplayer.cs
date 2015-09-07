using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace GISA
{
	public class FormTextDisplayer : System.Windows.Forms.Form
	{

	#region  Windows Form Designer generated code 

		public FormTextDisplayer() : base()
		{

			//This call is required by the Windows Form Designer.
			InitializeComponent();

			//Add any initialization after the InitializeComponent() call

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
		internal System.Windows.Forms.Button btnOk;
		internal System.Windows.Forms.RichTextBox rtbText;
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			this.btnOk = new System.Windows.Forms.Button();
			this.rtbText = new System.Windows.Forms.RichTextBox();
			this.SuspendLayout();
			//
			//btnOk
			//
			this.btnOk.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
			this.btnOk.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnOk.Location = new System.Drawing.Point(260, 392);
			this.btnOk.Name = "btnOk";
			this.btnOk.TabIndex = 2;
			this.btnOk.Text = "OK";
			//
			//rtbText
			//
			this.rtbText.Anchor = (System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
			this.rtbText.Location = new System.Drawing.Point(4, 4);
			this.rtbText.Name = "rtbText";
			this.rtbText.ReadOnly = true;
			this.rtbText.Size = new System.Drawing.Size(584, 380);
			this.rtbText.TabIndex = 1;
			//
			//FormTextDisplayer
			//
			this.AcceptButton = this.btnOk;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnOk;
			this.ClientSize = new System.Drawing.Size(592, 423);
			this.ControlBox = false;
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.rtbText);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormTextDisplayer";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Ajuda";
			this.ResumeLayout(false);

		}

	#endregion

	}

} //end of root namespace