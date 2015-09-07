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
		public class FormTestPxComboBox : System.Windows.Forms.Form
		{

	#region  Windows Form Designer generated code 

			public FormTestPxComboBox() : base()
			{

				//This call is required by the Windows Form Designer.
				InitializeComponent();

				//Add any initialization after the InitializeComponent() call
				PxComboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
				PxComboBox1.ImagePadding = 2;
				PxComboBox1.Items.Add("ola");
				PxComboBox1.Items.Add("ole");
				PxComboBox1.Items.Add("oli");
				PxComboBox1.Items.Add("olo");
				PxComboBox1.ImageList = ImageList1;
				PxComboBox1.ImageIndexes = new ArrayList(new int[] {1, 2, 3, 4});
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
			private System.ComponentModel.IContainer components;

			//NOTE: The following procedure is required by the Windows Form Designer
			//It can be modified using the Windows Form Designer.  
			//Do not modify it using the code editor.
			internal GISA.Controls.PxComboBox PxComboBox1;
			internal System.Windows.Forms.ImageList ImageList1;
			[System.Diagnostics.DebuggerStepThrough()]
			private void InitializeComponent()
			{
				this.components = new System.ComponentModel.Container();
				System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormTestPxComboBox));
				this.PxComboBox1 = new GISA.Controls.PxComboBox();
				this.ImageList1 = new System.Windows.Forms.ImageList(this.components);
				this.SuspendLayout();
				//
				//PxComboBox1
				//
				this.PxComboBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
				this.PxComboBox1.Location = new System.Drawing.Point(72, 64);
				this.PxComboBox1.Name = "PxComboBox1";
				this.PxComboBox1.Size = new System.Drawing.Size(121, 21);
				this.PxComboBox1.TabIndex = 0;
				this.PxComboBox1.Text = "ComboBox1";
				//
				//ImageList1
				//
				this.ImageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
				this.ImageList1.ImageSize = new System.Drawing.Size(16, 18);
				this.ImageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageList1.ImageStream"));
				this.ImageList1.TransparentColor = System.Drawing.Color.Fuchsia;
				//
				//FormTestPxComboBox
				//
				this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
				this.ClientSize = new System.Drawing.Size(292, 273);
				this.Controls.Add(this.PxComboBox1);
				this.Name = "FormTestPxComboBox";
				this.Text = "Form1";
				this.ResumeLayout(false);

			}

	#endregion


			[STAThread]
			static void Main()
			{
				Application.Run(new FormTestPxComboBox());
			}

		}
	}
} //end of root namespace