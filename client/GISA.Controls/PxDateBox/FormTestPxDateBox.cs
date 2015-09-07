using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace GISA
{
	namespace Controls.TestForms
	{
		public class FormTestPxDateBox : System.Windows.Forms.Form
		{

	#region  Windows Form Designer generated code 

			public FormTestPxDateBox() : base()
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
			internal PxDateBox Panel1;
			[System.Diagnostics.DebuggerStepThrough()]
			private void InitializeComponent()
			{
				this.Panel1 = new GISA.Controls.PxDateBox();
				this.SuspendLayout();
				//
				//Panel1
				//
				this.Panel1.Location = new System.Drawing.Point(132, 44);
				this.Panel1.Name = "Panel1";
				this.Panel1.Size = new System.Drawing.Size(88, 24);
				this.Panel1.TabIndex = 0;
				//
				//Form1
				//
				this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
				this.ClientSize = new System.Drawing.Size(292, 273);
				this.Controls.Add(this.Panel1);
				this.Name = "Form1";
				this.Text = "Form1";
				this.ResumeLayout(false);

			}

	#endregion

		}
	}
} //end of root namespace