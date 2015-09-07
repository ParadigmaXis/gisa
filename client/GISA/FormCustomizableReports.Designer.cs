//INSTANT C# NOTE: Formerly VB.NET project-level imports:
using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using System.Collections.Generic;
using System.Text;
using GISA.Reports;
using DBAbstractDataLayer.DataAccessRules;

namespace GISA
{
	[Microsoft.VisualBasic.CompilerServices.DesignerGenerated()]
	public partial class FormCustomizableReports : System.Windows.Forms.Form
	{

		//Form overrides dispose to clean up the component list.
		internal FormCustomizableReports()
		{
			InitializeComponent();

            Button1.Click += button1_Click;
            Button3.Click += Button3_Click;
            Button4.Click += Button4_Click;
		}
		[System.Diagnostics.DebuggerNonUserCode()]
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && components != null)
				{
					components.Dispose();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		//Required by the Windows Form Designer
		private System.ComponentModel.IContainer components = null;

		//NOTE: The following procedure is required by the Windows Form Designer
		//It can be modified using the Windows Form Designer.  
		//Do not modify it using the code editor.
		[System.Diagnostics.DebuggerStepThrough()]
		private void InitializeComponent()
		{
			this.ListView1 = new System.Windows.Forms.ListView();
			this.Button1 = new System.Windows.Forms.Button();
			this.Button2 = new System.Windows.Forms.Button();
			this.Button3 = new System.Windows.Forms.Button();
			this.Button4 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			//
			//ListView1
			//
			this.ListView1.Anchor = (System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right));
			this.ListView1.AutoArrange = false;
			this.ListView1.CheckBoxes = true;
			this.ListView1.Location = new System.Drawing.Point(12, 12);
			this.ListView1.MultiSelect = false;
			this.ListView1.Name = "ListView1";
			this.ListView1.Size = new System.Drawing.Size(522, 335);
			this.ListView1.TabIndex = 0;
			this.ListView1.UseCompatibleStateImageBehavior = false;
			this.ListView1.View = System.Windows.Forms.View.List;
			//
			//Button1
			//
			this.Button1.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
			this.Button1.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.Button1.Location = new System.Drawing.Point(466, 353);
			this.Button1.Name = "Button1";
			this.Button1.Size = new System.Drawing.Size(68, 23);
			this.Button1.TabIndex = 1;
			this.Button1.Text = "Continuar";
			this.Button1.UseVisualStyleBackColor = true;
			//
			//Button2
			//
			this.Button2.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right));
			this.Button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.Button2.Location = new System.Drawing.Point(546, 353);
			this.Button2.Name = "Button2";
			this.Button2.Size = new System.Drawing.Size(69, 23);
			this.Button2.TabIndex = 2;
			this.Button2.Text = "Cancelar";
			this.Button2.UseVisualStyleBackColor = true;
			//
			//Button3
			//
			this.Button3.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
			this.Button3.Location = new System.Drawing.Point(540, 12);
			this.Button3.Name = "Button3";
			this.Button3.Size = new System.Drawing.Size(75, 35);
			this.Button3.TabIndex = 3;
			this.Button3.Text = "Seleccionar todos";
			this.Button3.UseVisualStyleBackColor = true;
			//
			//Button4
			//
			this.Button4.Anchor = (System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
			this.Button4.Location = new System.Drawing.Point(540, 53);
			this.Button4.Name = "Button4";
			this.Button4.Size = new System.Drawing.Size(75, 35);
			this.Button4.TabIndex = 4;
			this.Button4.Text = "Limpar seleção";
			this.Button4.UseVisualStyleBackColor = true;
			//
			//FormCustomizableReports
			//
			this.AcceptButton = this.Button1;
			this.AutoScaleDimensions = new System.Drawing.SizeF((float)(6.0), (float)(13.0));
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.Button2;
			this.ClientSize = new System.Drawing.Size(627, 388);
			this.ControlBox = false;
			this.Controls.Add(this.Button4);
			this.Controls.Add(this.Button3);
			this.Controls.Add(this.Button2);
			this.Controls.Add(this.Button1);
			this.Controls.Add(this.ListView1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "FormCustomizableReports";
			this.ShowInTaskbar = false;
			this.Text = "Seleccionar campos";
			this.ResumeLayout(false);

			//INSTANT C# NOTE: Converted event handlers:
			

		}
		internal System.Windows.Forms.ListView ListView1;
		internal System.Windows.Forms.Button Button1;
		internal System.Windows.Forms.Button Button2;
		internal System.Windows.Forms.Button Button3;
		internal System.Windows.Forms.Button Button4;
	}

} //end of root namespace